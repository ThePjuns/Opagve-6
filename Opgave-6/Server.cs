using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.Json.Serialization;
using FansOutPut;
using Newtonsoft.Json;

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

            TcpListener serverSocket = new TcpListener(IPAddress.Any, 4646); // til lokal/egen forbindelse.
            //TcpListener serverSocket = new TcpListener(4646); // ingen IP angivet, modtager fra alle.

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

                switch (message.ToLower())
                {
                    case "hentalle":
                        string dataen = JsonConvert.SerializeObject(fanOuts);
                        sw.WriteLine(dataen);
                        break;

                    case "hent":
                        string message2 = sr.ReadLine();
                        int id = Convert.ToInt32(message2);
                        FanOutPut fan = fanOuts.Find(i => i.Id == id);
                        string dataen2 = JsonConvert.SerializeObject(fan);
                        sw.WriteLine(dataen2);
                        break;

                    case "gem":
                        string fans = sr.ReadLine();
                        FanOutPut fan2 = JsonConvert.DeserializeObject<FanOutPut>(fans);
                        fanOuts.Add(fan2);
                        break;
                }
            }

            ns.Close();
            Console.WriteLine("net stream closed");
            connectionSocket.Close();
            Console.WriteLine("connection socket closed");
        }


        //COULD NOT GET 2 WORK
        //public static void HentAlle(TcpClient connectionSocket)
        //{
        //    Stream ns = connectionSocket.GetStream();
        //    StreamReader sr = new StreamReader(ns);
        //    StreamWriter sw = new StreamWriter(ns);
        //    sw.AutoFlush = true; //enables automatic flushing

        //    string message = sr.ReadLine();

        //    if (message == "hentalle")
        //    {
        //        string data = JsonConvert.SerializeObject(fanOuts);
        //        sw.WriteLine(data);
        //    }
        //}

        //public static void Hent(TcpClient connectionSocket)
        //{
        //    Stream ns = connectionSocket.GetStream();
        //    StreamReader sr = new StreamReader(ns);
        //    StreamWriter sw = new StreamWriter(ns);
        //    sw.AutoFlush = true; //enables automatic flushing
        //}

        //public static void Gem(TcpClient connectionSocket)
        //{

        //    Stream ns = connectionSocket.GetStream();
        //    StreamReader sr = new StreamReader(ns);
        //    StreamWriter sw = new StreamWriter(ns);
        //    sw.AutoFlush = true; //enables automatic flushing

        //}
    }
}

