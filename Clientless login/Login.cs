using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SilkroadSecurityApi;
namespace Clientless_login
{
    class Login
    {
        public static void HandleCharList(Packet packet)
        {
            if (packet.ReadUInt8() == 2) // character listening
            {
                if (packet.ReadUInt8() == 1) // result
                {
                    byte charCount = packet.ReadUInt8();
                    for (int i = 0; i < charCount; i++)
                    {
                        uint CharID = packet.ReadUInt32();
                        string CharName = packet.ReadAscii();
                        packet.ReadUInt8();
                        packet.ReadUInt8();
                        packet.ReadUInt64();
                        packet.ReadUInt16();
                        packet.ReadUInt16();
                        packet.ReadUInt16();
                        packet.ReadUInt32();
                        packet.ReadUInt32();

                        byte doDelete = packet.ReadUInt8();
                        if (doDelete == 1)
                            packet.ReadUInt32();

                        packet.ReadUInt16();
                        packet.ReadUInt8();
                        byte itemCount = packet.ReadUInt8();

                        for (int y = 0; y < itemCount; y++)
                        {
                            UInt32 item_id = packet.ReadUInt32();
                            byte item_plus = packet.ReadUInt8();
                        }

                        byte Avatars_count = packet.ReadUInt8();
                        for (int y = 0; y < Avatars_count; y++)
                        {
                            UInt32 item_id = packet.ReadUInt32();
                            byte item_plus = packet.ReadUInt8();
                        }
                        Globals.MainWindow.char_list.Items.Add(CharName);

                    }
                }
            }
        }

        public static void sendCredential()
        {
            //Packet p = new Packet(0x6102);
            //p.WriteUInt8(22);
            //p.WriteAscii(id.Text);
            //p.WriteAscii(pw.Text);
            //p.WriteUInt16(64);
            //Gateway.SendToServer(p);
        }


        public static void sendCredentialWith(string u,string pw,Security gw)
        {
            Packet p = new Packet(0x6102);
            p.WriteUInt8(22);
            p.WriteAscii(u);
            p.WriteAscii(pw);
            p.WriteUInt16(64);
            Gateway.SendToServer(p, gw);
        }

        public static void sendCaptcha(Security gw)
        {
            Captcha.SendCaptcha("a", gw);
        }

        public static void sendCharName()
        {
            //Packet p = new Packet(0x7001);
            //p.WriteAscii(char_list.SelectedItem);
            //Agent.Send(p);
        }

        public static void sendCharNameWith(string c, Security ag_security)
        {
            Packet p = new Packet(0x7001);
            p.WriteAscii(c);
            ag_security.Send(p);
        }
    }
}
