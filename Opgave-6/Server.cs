using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using FansOutPut;

namespace Opgave_6
{
    class Server
    {
        private static readonly List<FanOutPut> fanOuts = new List<FanOutPut>()
        {
            new FanOutPut(1, "stue", 24.3, 65.02),
            new FanOutPut(2, "værelseet", 24.2, 52.53),
            new FanOutPut(3, "bryggers", 24.3, 79.02),
            new FanOutPut(4, "toilet", 15.6, 50.23),
            new FanOutPut(5, "værelseto", 24.3, 79.02)
        };
        public static void Start()
        {
            Console.WriteLine("starting echo yo");

            //TcpListener serverSocket = new TcpListener(IPAddress.Loopback, 7777); // til lokal/egen forbindelse.
            TcpListener serverSocket = new TcpListener(4646); // ingen IP angivet, modtager fra alle.

            // Start server
            serverSocket.Start();

            // accept tcp client
            TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("server activated");

            DoClient(connectionSocket);

            serverSocket.Stop();
            Console.WriteLine("server stopped");


        }



        public static void DoClient(TcpClient connectionSocket)
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; //enables automatic flushing

            while (true)
            {
                string message = sr.ReadLine();

                Console.WriteLine("received message: " + message);
                if (message != null)
                    sw.WriteLine(message.ToUpper());

                if (message.ToLower() == "luk")
                {
                    break;
                }
            }

            ns.Close();
            Console.WriteLine("net stream closed");
            connectionSocket.Close();
            Console.WriteLine("connection socket closed");
        }

        public static void HentAlle(TcpClient connectionSocket)
        {
            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; //enables automatic flushing
        }

        public static void Hent(TcpClient connectionSocket)
        {

            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; //enables automatic flushing

        }

        public static void Gem(TcpClient connectionSocket)
        {

            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; //enables automatic flushing

        }
    }
}
