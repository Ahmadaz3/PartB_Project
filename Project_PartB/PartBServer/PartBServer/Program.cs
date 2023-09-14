using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PartBServer
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IPAddress localIP = IPAddress.Parse("127.0.0.1");
            int port = 1234;
            //The TcpListener class is part of the .NET Framework and is used to listen for incoming
            //TCP connections on a specified
            //IP address and port.
            TcpListener server = new TcpListener(localIP, port);
            // start listening for incoming TCP connections on the specified IP address and port.
            server.Start();
            // give note that 
            Console.WriteLine("Server listening...");

            try
            {
                while (true)
                {
                    TcpClient client = await server.AcceptTcpClientAsync();
                    Console.WriteLine("Client connected.");

                    _ = HandleClientAsync(client); // Start asynchronous handling of the client
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        static async Task HandleClientAsync(TcpClient client)
        {
            try
            {
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];

                int bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                Console.WriteLine(message);
 
                bool isAuthenticated = VerifyUser(message);

                if (isAuthenticated)
                {
                    string responseMessage = "Authenticated";
                    byte[] responseBuffer = Encoding.ASCII.GetBytes(responseMessage);
                    await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);

                }
                else
                {
                    string responseMessage = "Authentication failed";
                    byte[] responseBuffer = Encoding.ASCII.GetBytes(responseMessage);
                    await stream.WriteAsync(responseBuffer, 0, responseBuffer.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }

        static bool VerifyUser(string message)
        {
            if (message == "A" + " " + "B")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
