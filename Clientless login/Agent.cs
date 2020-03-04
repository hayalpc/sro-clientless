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

namespace Clientless_login
{
    public class Agent
    {
        Security ag_security = null;
        Thread loop = null;

        static uint version = UInt32.Parse("188");
        static uint locale = UInt32.Parse("22");
        static string IP;
        static string Port;

        private System.Timers.Timer aTimer;

        public void Start(string _IP, string _Port, uint _loginID, string _username, string _password, string _charName, string _version)
        {
            Port = _Port;
            IP = _IP;
            loop = new Thread(new ThreadStart(() => Agent_thread(_loginID, _username, _password, _charName, _version.ToString())));
            loop.IsBackground = true;
            loop.Priority = ThreadPriority.Lowest;
            loop.Start();

            aTimer = new System.Timers.Timer(10000);
            aTimer.Elapsed += (Object source, ElapsedEventArgs e) => send(new Packet(0x2002));
            aTimer.AutoReset = false;
            aTimer.Enabled = true;
        }

        public void Agent_thread(uint _loginID, string _username, string _password, string _charName,string _version)
        {
            ag_security = new Security();

            List <Packet> ag_packets = new List<Packet>();
            Socket ag_socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ag_socket.Connect(IP, Int32.Parse(Port));
            ag_socket.Blocking = false;
            ag_socket.NoDelay = true;

            TransferBuffer ag_recv_buffer = new TransferBuffer(4096, 0, 0);


            while (true)
            {
                SocketError err;

                ag_recv_buffer.Size = ag_socket.Receive(ag_recv_buffer.Buffer, 0, ag_recv_buffer.Buffer.Length, SocketFlags.None, out err);
                if (err != SocketError.Success)
                {
                    if (err != SocketError.WouldBlock)
                    {
                        break;
                    }
                }
                else
                {
                    if (ag_recv_buffer.Size > 0)
                    {
                        ag_security.Recv(ag_recv_buffer);
                    }
                    else
                    {
                        //  Console.WriteLine("Status: The connection has been closed.");
                        break;
                    }
                }

                // Obtain all queued packets and add them to our own queue to process later.
                List<Packet> tmp_packets = ag_security.TransferIncoming();
                if (tmp_packets != null)
                {
                    ag_packets.AddRange(tmp_packets);
                }

                if (ag_packets.Count > 0)
                {
                    foreach (Packet packet in ag_packets)
                    {
                        byte[] packet_bytes = packet.GetBytes();

                        // Debug
                        Log("[A->C][{0:X4}][{1} bytes]{2}{3}{4}{5}{6}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes), Environment.NewLine);
                        if (packet.Opcode == 0x5000 || packet.Opcode == 0x9000)
                        {
                            continue;
                        }
                        // Identify
                        if (packet.Opcode == 0x2001)
                        {
                            if (packet.ReadAscii() == "GatewayServer")
                            {
                                Packet response = new Packet(0x6100, true, false);
                                response.WriteUInt8(locale);
                                response.WriteAscii("SR_Client");
                                response.WriteUInt32(_version);
                                send(response);
                            }
                            else
                            {
                                Packet p = new Packet(0x6103);
                                p.WriteUInt32(_loginID);
                                p.WriteAscii(_username);
                                p.WriteAscii(_password);
                                p.WriteUInt8(22);
                                p.WriteUInt32(0);
                                p.WriteUInt16(0);
                                send(p);
                            }
                        }
                        else if (packet.Opcode == 0xA103)
                        {
                            if (packet.ReadUInt8() == 1)
                            {
                                Packet response = new Packet(0x7007);
                                response.WriteUInt8(2);
                                send(response);
                            }
                        }
                        else if (packet.Opcode == 0xB007)
                        {
                            //Login.HandleCharList(packet);
                            Thread.Sleep(500);
                            Log("Girdi:" + _charName);
                            Login.sendCharNameWith(_charName, ag_security);
                        }
                        else if (packet.Opcode == 0x3020)
                        {
                            Packet p = new Packet(0x3012);
                            send(p);
                        }
                    }
                    ag_packets.Clear();
                }


                // Check to see if we have any packets to send
                List<KeyValuePair<TransferBuffer, Packet>> tmp_buffers = ag_security.TransferOutgoing();
                if (tmp_buffers != null)
                {
                   foreach (var kvp in tmp_buffers)
                   {
                       TransferBuffer buffer = kvp.Key;
                       Packet packet = kvp.Value;

                       err = SocketError.Success;

                       // Since TCP is a stream protocol, we have to support partial sends. To do this, we
                       // will just loop until we send all the data or an exception is generated.
                       while (buffer.Offset != buffer.Size)
                       {

                           int sent = ag_socket.Send(buffer.Buffer, buffer.Offset, buffer.Size - buffer.Offset, SocketFlags.None, out err);
                           if (err != SocketError.Success)
                           {
                               if (err != SocketError.WouldBlock)
                               {
                                   // Console.WriteLine("Error: Send returned error code {0}.", err);
                                   break;
                               }
                           }


                           buffer.Offset += sent;
                       }

                    // We need to check for an error to break out of the foreach loop
                    if (err != SocketError.Success)
                        {
                            break;
                        }

                        //byte[] packet_bytes = packet.GetBytes();


                        // Debug (logical packet)
                        //Console.WriteLine("*** Logical ***");
                        //Log("[C->S][{0:X4}][{1} bytes]{2}{3}{4}{5}{6}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes), Environment.NewLine);



                    // We need to check for an error to break out of the main loop
                    if (err != SocketError.Success)
                    {
                        break;
                    }
                }
                }
            }
        }

        public void Log(string msg, params object[] values)
        {
            msg = string.Format(msg, values);
            Globals.MainWindow.logs.AppendText(msg + "----\n");
        }

        public void send(Packet packet)
        {
            try
            {
                byte[] packet_bytes = packet.GetBytes();
                Log("[C->S][{0:X4}][{1} bytes]{2}{3}{4}{5}{6}", packet.Opcode, packet_bytes.Length, packet.Encrypted ? "[Encrypted]" : "", packet.Massive ? "[Massive]" : "", Environment.NewLine, Utility.HexDump(packet_bytes), Environment.NewLine);
                ag_security.Send(packet);
                aTimer.Stop();
                aTimer.Start();
        }catch
            {
            }
        }
    }
}
