using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace UdpClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var udpClient = new UdpClient();

            IPEndPoint epUDP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 100);
            IPEndPoint epTCP = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);

            //connect to udpServer
            udpClient.Connect(epUDP);

            var receivedData = new byte[1024 * 4];
            //Console.WriteLine("Select a username:");
            //var name = Console.ReadLine();

            //var binFormatter = new BinaryFormatter();
            //var mStream = new MemoryStream();

            //binFormatter.Serialize(mStream, name);
            //var sending = mStream.ToArray();

            //udpClient.Send(sending, sending.Length);

            //receivedData = udpClient.Receive(ref epUDP);

            //var stream1 = new MemoryStream();
            //var binaryFormatter1 = new BinaryFormatter();

            //stream1.Write(receivedData, 0, receivedData.Length);
            //stream1.Position = 0;

            //var result1 = binaryFormatter1.Deserialize(stream1) as dynamic;

            //if(result1 is bool)
            //{
            //    if (result1)
            //    {
            //        Console.WriteLine("Successfully added.");
            //    }
            //    else
            //    {
            //        Console.WriteLine("Error! Name already exists.");
            //    }
            //}

            while (true)
            {
                Console.WriteLine("Press any 1 or 2 ...");
                var input = Console.ReadLine();
                switch (input)
                {
                    case "1":
                        Console.WriteLine("Set new name:");
                        var name = Console.ReadLine();

                        var binFormatter = new BinaryFormatter();
                        var mStream = new MemoryStream();

                        binFormatter.Serialize(mStream, name);
                        var sending = mStream.ToArray();

                        udpClient.Send(sending, sending.Length);

                        receivedData = udpClient.Receive(ref epUDP);

                        var stream1 = new MemoryStream();
                        var binaryFormatter1 = new BinaryFormatter();

                        stream1.Write(receivedData, 0, receivedData.Length);
                        stream1.Position = 0;

                        var result1 = binaryFormatter1.Deserialize(stream1) as dynamic;

                        if (result1 is string)
                        {
                            Console.WriteLine(result1);
                        }
                        if (result1 is Dictionary<int, string>)
                        {
                            Console.WriteLine("Users are:");
                            Console.WriteLine();
                            foreach (var item in result1)
                            {
                                Console.WriteLine($"{item.Value} with ID: {item.Key}");
                            }
                        }
                        break;

                    case "2":
                        var binFormatter1 = new BinaryFormatter();
                        var mStream1 = new MemoryStream();

                        binFormatter1.Serialize(mStream1, false);

                        var emptyArr = mStream1.ToArray(); udpClient.Send(emptyArr, emptyArr.Length);

                        receivedData = udpClient.Receive(ref epUDP);

                        var stream = new MemoryStream();
                        var binaryFormatter = new BinaryFormatter();

                        stream.Write(receivedData, 0, receivedData.Length);
                        stream.Position = 0;

                        var result = binaryFormatter.Deserialize(stream) as dynamic;

                        if (result is string)
                        {
                            Console.WriteLine(result);
                        }
                        if (result is Dictionary<int, string>)
                        {
                            Console.WriteLine("Users are:");
                            Console.WriteLine();
                            foreach (var item in result)
                            {
                                Console.WriteLine($"{item.Value} with ID: {item.Key}");
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Nothing happend!...");
                        break;
                }


            }
        }
    }
}
