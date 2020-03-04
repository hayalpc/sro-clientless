using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SilkroadSecurityApi;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;
using System.IO;

namespace Clientless_login
{
    public class Gateway
    {
        private static System.Timers.Timer aTimer;

        static uint version = UInt32.Parse("188");
        static uint locale = UInt32.Parse("22");

        public Packet response = null;

        public Security gw_security;
        static List<User> users = new List<User>();
        static int i = 0;
        static int yeter = 0;

        static private Mutex mut = new Mutex();

        public static void StartThread()
        {
            //while (yeter < 5)
            //{
                using (var reader = new StreamReader(@"list.csv"))
                {
                    int i = 0;
                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();
                        var values = line.Split(',');
                        users.Add(new User(values[0].Replace("'", string.Empty), values[1].Replace("'", string.Empty), values[2].Replace("'", string.Empty)));
                    }
                }
            //yeter++;
            //}
            Log("length" + users.Count());
            aTimer = new System.Timers.Timer(500);
            aTimer.Elapsed += OnTimedEvent;
            aTimer.AutoReset = true;
            aTimer.Enabled = true;
        }

        static void OnTimedEvent(Object source, ElapsedEventArgs e)
        {
            try
            {
                Start("88.99.150.117", "15779", "443", users[i++]);
                if (i >= users.Count)
                {
                    Log("Bitti " + i);
                    aTimer.Enabled = false;
                    //aTimer.Stop();
                    //aTimer.Start();
                    //i = 0;
                }
            }
            catch
            {

            }
            
        }

        static public void Start(string _IP, string _Port,String versionF, User u)
        {
            version = UInt32.Parse(versionF);

            Thread loop = new Thread(new ThreadStart(() => Gateway_thread(u, _IP, _Port)));
            loop.IsBackground = true;
            loop.Priority = ThreadPriority.Lowest;
            loop.Start();
        }

        static public void Gateway_thread(User _user, string _IP, string _Port)
        {
            string IP = _IP;
            string Port = _Port;

            Socket gw_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            gw_socket.Connect(IP, Int32.Parse(Port));

            gw_socket.Blocking = false;
            gw_socket.NoDelay = true;

            Security gw_security = new Security();

            Packet response = new Packet(0x2002);
            gw_security.Send(response);

            TransferBuffer gw_recv_buffer = new TransferBuffer(4096, 0, 0);

            while (true)
            {
                SocketError err;
                gw_recv_buffer.Size = gw_socket.Receive(gw_recv_buffer.Buffer, 0, gw_recv_buffer.Buffer.Length, SocketFlags.None, out err);
                if (err != SocketError.Success)
                {
                    if (err != SocketError.WouldBlock)
                    {
                        break;
                    }
                }
                else
                {
                    if (gw_recv_buffer.Size > 0)
                    {
                        gw_security.Recv(gw_recv_buffer);
                    }
                    else
                    {
                        break;
                    }
                }

                List<Packet> tmp_packets = gw_security.TransferIncoming();
                List<Packet> gw_packets = new List<Packet>();

                if (tmp_packets != null)
                {
                    gw_packets.AddRange(tmp_packets);
                }

                if (gw_packets.Count > 0)
                {
                    foreach (Packet packet in gw_packets)
                    {

                        byte[] packet_bytes = packet.GetBytes();

                        Log("[S->C][{0:X4}][{1} bytes]{2}{3}{4}{5}{6}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes), Environment.NewLine);

                        if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000)
                        {
                            continue;
                        }
                        if (packet.Opcode == 0x2001)
                        {
                            if (packet.ReadAscii() == "GatewayServer")
                            {
                                response = new Packet(0x6100, true, false);
                                response.WriteUInt8(locale);
                                response.WriteAscii("SR_Client");
                                response.WriteUInt32(version);
                                gw_security.Send(response);
                            }
                        }
                        else if (packet.Opcode == 0xA100)
                        {
                            byte result = packet.ReadUInt8();
                            if (result == 1)
                            {
                                response = new Packet(0x6101, true);
                                gw_security.Send(response);
                            }
                            else
                            {
                                return;
                            }
                        }
                        else if (packet.Opcode == 0xA102)
                        {
                            uint login = packet.ReadUInt8();
                            if (login == 1)
                            {
                                uint LoginID = packet.ReadUInt32();
                                string ip = packet.ReadAscii();
                                ushort port = packet.ReadUInt16();
                                Agent ag = new Agent();
                                ag.Start(ip, port.ToString(), LoginID, _user.Username, _user.Password, _user.CharName16,version.ToString());
                                break;
                            }
                            else
                            {
                                Log("Oyunda " + _user.Username);
                                break;
                            }
                        }
                        else if (packet.Opcode == 0x2322)
                        {
                            UInt32[] pixels = Captcha.GeneratePacketCaptcha(packet);
                            Random rnd = new Random();
                            Captcha.SaveCaptchaToBMP(pixels, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + rnd.Next(000000, 999999) + ".bmp");

                            //Login.sendCaptcha(gw_security);
                        }

                        else if (packet.Opcode == 0xA101)
                        {
                            Thread.Sleep(500);
                            Login.sendCredentialWith(_user.Username, _user.Password, gw_security);
                        }
                        Thread.Sleep(1000);
                    }
                    gw_packets.Clear();
                }

                List<KeyValuePair<TransferBuffer, Packet>> tmp_buffers = gw_security.TransferOutgoing();
                if (tmp_buffers != null)
                {
                    foreach (var kvp in tmp_buffers)
                    {
                        TransferBuffer buffer = kvp.Key;
                        Packet packet = kvp.Value;
                        err = SocketError.Success;
                        while (buffer.Offset != buffer.Size)
                        {

                            int sent = gw_socket.Send(buffer.Buffer, buffer.Offset, buffer.Size - buffer.Offset, SocketFlags.None, out err);
                            if (err != SocketError.Success)
                            {
                                if (err != SocketError.WouldBlock)
                                {
                                    break;
                                }
                            }
                            buffer.Offset += sent;
                        }
                        if (err != SocketError.Success)
                        {
                            break;
                        }

                        byte[] packet_bytes = packet.GetBytes();
                        //Log("[C->S][{0:X4}][{1} bytes]{2}{3}{4}{5}{6}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes), Environment.NewLine);
                    }

                    if (err != SocketError.Success)
                    {
                        break;
                    }
                }
                Thread.Sleep(1);
            }
        }

        static public void Log(string msg, params object[] values)
        {
            mut.WaitOne();
            msg = string.Format(msg, values);
            Globals.MainWindow.logs.AppendText(msg + "\n");
            Globals.MainWindow.logs.Update();
            mut.ReleaseMutex();
        }
        public static void SendToServer(Packet packet,Security gw_security)
        {
            gw_security.Send(packet);
        }

    }
}
