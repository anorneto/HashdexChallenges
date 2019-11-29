
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
            Console.WriteLine("    Redis PUBLISH of Binance Trades Started");
            Console.WriteLine("    To stop press Ctrl + C\n");

            //subscribe
            /*  var redis = new Services.Redis();
              redis.Subscribe();*/

            Services.ICryptoDataStream dataStream = new Services.BinanceDataStream();
            dataStream.PubTrades(Utils.Constants.DefaultSymbol);

            //Locks the main thread until we press Ctrl + C
            exitEvent.WaitOne();

            Console.WriteLine("    Ctrl+C Received. \n    Exiting Program ...");
            dataStream.StopStreams();
            exitEvent.Dispose();
        }
    }
}
