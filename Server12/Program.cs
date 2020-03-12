using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text;

namespace Server12
{
    class Program
    {
        static void Main(string[] args)
        {
            Server();
        }

      
        public static void Server()
        {
            IPHostEntry host = Dns.GetHostEntry("localhost");
            IPAddress ipAddress = host.AddressList[0];
            IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 5000);

            try
            {

                Socket listener = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                listener.Bind(localEndPoint);
                
                listener.Listen(5);

                Console.WriteLine("Waiting for a connection...");
                Socket handler = listener.Accept();

                 
                string data = null;
                byte[] bytes = null;

                while (true)
                {
                    bytes = new byte[1024];
                    int bytesRec = handler.Receive(bytes);
                    data = Encoding.ASCII.GetString(bytes, 0, bytesRec);

                    Console.WriteLine("Text received : {0}", data);

                    byte[] msg = Encoding.ASCII.GetBytes(data);
                    handler.Send(msg);
                    if (data.IndexOf("STOP") > -1)
                    {
                        break;
                    }
                }

               
                
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }

            Console.WriteLine("\n Press any key to continue...");
            Console.ReadKey();
        }
    }
    }
