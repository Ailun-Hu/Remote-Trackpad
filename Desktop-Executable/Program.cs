// See https://aka.ms/new-console-template for more information
using System.Net.WebSockets;
using System.Buffers;
using System.Text.Encodings;


namespace MoveMouse
{
    class Program{

        static void Main(string[] args){
            
            ListenAndMove();  
            Console.WriteLine("Hello");
        }

        static async void ListenAndMove()
        {
            // int x = 0;
            // int y = 0;
            Console.WriteLine("Listen");
            var result = new byte[1024];
            Uri link = new Uri("ws://localhost:8080");
            ClientWebSocket ws = new ClientWebSocket();
            Console.WriteLine(link.ToString());

            ws.ConnectAsync(link, CancellationToken.None).Wait();
            Console.WriteLine(ws.Options.KeepAliveInterval);
            Console.WriteLine("Connected");
            while (ws.State == WebSocketState.Open)
            {
                Console.WriteLine("IN Loop");
                ws.ReceiveAsync(new ArraySegment<byte>(result), CancellationToken.None).Wait();
                Console.WriteLine(System.Text.Encoding.Default.GetString(result));
                Console.WriteLine("Test");
            }
            Console.WriteLine("Ended");
        }
    }
}