using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;

namespace WebSocketsTest
{
    class Program
    {
        public static Dictionary<int, string> Users = new Dictionary<int, string>();
        public Unit Unit = new Unit();

        static void Main(string[] args)
        {
            while (true) { 
            int recv;
            byte[] data = new byte[1024 * 4];

            IPEndPoint endPoint = new IPEndPoint(IPAddress.Any, 100);
            Socket newSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            newSocket.Bind(endPoint);
            Console.WriteLine("Waiting for client...");
            IPEndPoint sender = new IPEndPoint(IPAddress.Any, 100);
            EndPoint tmpRemote = (EndPoint)sender;

            recv = newSocket.ReceiveFrom(data, ref tmpRemote);
            Console.WriteLine($"Message received from {tmpRemote.ToString()}");


            var mStream = new MemoryStream();
            var binFormatter = new BinaryFormatter();

            mStream.Write(data, 0, data.Length);
            mStream.Position = 0;

            var result = binFormatter.Deserialize(mStream) as dynamic;

            byte[] byteArr = Unit.Execute(result);


            newSocket.Connect(tmpRemote);
            if (newSocket.Connected)
            {
                newSocket.Send(byteArr);

            }
                newSocket.Close();
                continue;


                //while (true)
                //{
                //    if (!newSocket.Connected)
                //    {
                //        Console.WriteLine("Client disconnected");
                //        break;
                //    }
                //    data = new byte[1024 * 4];
                //    recv = newSocket.ReceiveFrom(data, ref tmpRemote);
                //    Console.WriteLine($"Message received from {tmpRemote.ToString()}");


                //    var mStream1 = new MemoryStream();
                //    var binFormatter1 = new BinaryFormatter();

                //    mStream1.Write(data, 0, data.Length);
                //    mStream1.Position = 0;

                //    var result1 = binFormatter1.Deserialize(mStream1) as dynamic;

                //    byte[] byteArr1 = Unit.Execute(result1);

                //    if (newSocket.Connected)
                //    {
                //        newSocket.Send(byteArr1);

                //    }
                //    if (recv == 0)
                //        break;

                //}
                //newSocket.Close();
            }
        }

        
    }
}

