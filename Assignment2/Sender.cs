using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment2
{
    class Sender
    {
        public static readonly IPEndPoint iep = new IPEndPoint(IPAddress.Parse("239.50.50.51"), Program.PORT);
        public static UdpClient sock;
        public static int counter = 0;

        static Sender() {
            sock = new UdpClient();
            //sock.Connect(IPAddress.Parse("239.50.50.51"), Program.PORT);
            counter++;
        }
        private static void Send(int msgType, string msg) {
            if (msgType == -1) // game msg
            {
                
            }
            else
            {
                msg = msgType + "\n" + msg;
            }
            byte[] data = Encoding.ASCII.GetBytes(msg);
            sock.Send(data, data.Length, iep);
            Console.WriteLine("Sent " + msg);
        }
        public static void SendGameMsg(int gameMsgType, string msg) {
            Send(-1, gameMsgType + "," + msg);
        }
    }

}
