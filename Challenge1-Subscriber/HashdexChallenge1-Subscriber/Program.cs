
using System;
using System.Threading;

namespace HashdexChallenge1
{
    class Program
    {
        static void Main(string[] args)
        {
            var exitEvent = new ManualResetEvent(false);
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                eventArgs.Cancel = true; // cancel default Ctrl + C behavior
                exitEvent.Set(); // Unlocks main trhead
            };

            //Starts trade websocket stream
            Console.WriteLine("    Redis SUBSCRIBE of Binance Trades Started");
            Console.WriteLine("    To stop press Ctrl + C\n");


            Services.Redis redisInstance = new Services.Redis();
            redisInstance.Subscribe();

            //Locks the main thread until we press Ctrl + C
            exitEvent.WaitOne();

            Console.WriteLine("    Ctrl+C Received. \n    Exiting Program ...");
            exitEvent.Dispose();
        }
    }
}
