using SuperSocket.SocketBase;
using SuperSocket.SocketBase.Protocol;
using SuperSocket.SocketEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelnetSuperSocket
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            {
            //    Console.WriteLine("Press any key to start the server!");

            //    Console.ReadKey();
            //    Console.WriteLine();

            //    var appServer = new TelnetServer();

            //    appServer.NewSessionConnected += new SessionHandler<TelnetSession>(appServer_NewSessionConnected);

            //    //方法一
            //    //appServer.NewRequestReceived += new RequestHandler<AppSession, StringRequestInfo>(appServer_NewRequestReceived);

            //    if (!appServer.Setup(2012))
            //    {
            //        Console.WriteLine("Failed to setup!");
            //        Console.ReadKey();
            //        return;
            //    }

            //    Console.WriteLine();

            //    if (!appServer.Start())
            //    {
            //        Console.WriteLine("Failed to start!");
            //        Console.ReadKey();
            //        return;
            //    }

            //    Console.WriteLine("The server started successfully, press key 'q' to stop it!");

            //    while (Console.ReadKey().KeyChar != 'q')
            //    {
            //        Console.WriteLine();
            //        continue;
            //    }

            //    appServer.Stop();

            //    Console.WriteLine("The server was stopped!");
            //    Console.ReadKey();
            }

            Console.WriteLine("Press any key to start the server!");

            Console.ReadKey();
            Console.WriteLine();

            var bootstrap = BootstrapFactory.CreateBootstrap();

            if (!bootstrap.Initialize())
            {
                Console.WriteLine("Failed to initialize!");
                Console.ReadKey();
                return;
            }

            var result = bootstrap.Start();

            Console.WriteLine("Start result: {0}!", result);

            if (result == StartResult.Failed)
            {
                Console.WriteLine("Failed to start!");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Press key 'q' to stop it!");

            while (Console.ReadKey().KeyChar != 'q')
            {
                Console.WriteLine();
                continue;
            }

            Console.WriteLine();

            //Stop the appServer
            bootstrap.Stop();

            Console.WriteLine("The server was stopped!");
            Console.ReadKey();
        }

        private static void appServer_NewSessionConnected(TelnetSession session)
        {
            session.Send("Welcome to SuperSocket Telnet Server");
        }

        static void appServer_NewRequestReceived(AppSession session, StringRequestInfo requestInfo)
        {
            switch (requestInfo.Key.ToUpper())
            {
                case ("ECHO"):
                    session.Send(requestInfo.Body);
                    break;

                case ("ADD"):
                    session.Send(requestInfo.Parameters.Select(p => Convert.ToInt32(p)).Sum().ToString());
                    break;

                case ("MULT"):

                    var result = 1;

                    foreach (var factor in requestInfo.Parameters.Select(p => Convert.ToInt32(p)))
                    {
                        result *= factor;
                    }

                    session.Send(result.ToString());
                    break;
            }
        }
    }
}