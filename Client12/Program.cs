using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace Client12
{
    class Program
    {
        static void Main(string[] args)
        {
            Client();
        }



        private static void Client()
        {
            byte[] bytes = new byte[1024];

            try
            {
                
                IPHostEntry host = Dns.GetHostEntry("localhost");
                IPAddress ipAddress = host.AddressList[0];
                IPEndPoint remoteEP = new IPEndPoint(ipAddress, 5000);

                Socket sender = new Socket(ipAddress.AddressFamily,
                    SocketType.Stream, ProtocolType.Tcp);

 
                try
                {
                      
                    sender.Connect(remoteEP);

                    Console.WriteLine("Socket connected to {0}",
                        sender.RemoteEndPoint.ToString());

                    int packetNo = 1;
                    byte[] msg = Encoding.ASCII.GetBytes($"This is packet {packetNo}");

                    

                       
                    int bytesSent = sender.Send(msg);
                     
                    int bytesRec = sender.Receive(bytes);
                    while (sender.Connected)
                    {
                        Timer thread = new Timer(callback, "", TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
                        Thread.Sleep(5000);
                        msg = Encoding.ASCII.GetBytes($"This is packet {packetNo}");
                        
                            
                        bytesSent = sender.Send(msg);

                        packetNo++;
                    }
                    sender.Shutdown(SocketShutdown.Both);
                    sender.Close();

                }
                catch (ArgumentNullException ane)
                {
                    Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
                }
                catch (SocketException se)
                {
                    Console.WriteLine("SocketException : {0}", se.ToString());
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unexpected exception : {0}", e.ToString());
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        private static void callback(object state)
        {
            
        }

    }

    
}
