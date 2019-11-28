
using System;
using Binance.Net;

namespace HashdexChallenge1
{
    class Program
    {
        static void Main(string[] args)
        {

            var socketClient = new BinanceSocketClient();
            // Streams
            var successTrades = socketClient.SubscribeToTradeUpdates("ethbtc", (data) =>
            {
                // handle data
                Console.WriteLine("Receive Time: {5} ,Trade Time: {0} , Trade ID: {1} , Symbol: {2} , Price: {3}, Qty: {4} ",
                    data.TradeTime, data.TradeId, data.Symbol, data.Price, data.Quantity, DateTime.Now);
            });
            Console.ReadLine();
        }
    }
}
