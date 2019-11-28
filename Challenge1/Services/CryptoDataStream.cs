using System;
using System.Text.Json;
using System.Threading.Tasks;
using Binance.Net;
using Binance.Net.Interfaces;

namespace HashdexChallenge1.Services
{
    public interface ICryptoDataStream
    {
        public Task PubTrades(string symbol);
        public Task StopStreams();
    }

    public class BinanceDataStream : ICryptoDataStream
    {
        private IBinanceSocketClient _socketClient;

        public BinanceDataStream()
        {
            this._socketClient = new BinanceSocketClient();
        }

        ~BinanceDataStream() => _socketClient.Dispose();

        public async Task PubTrades(string symbol)
        {
            Redis redis = new Redis();
            Models.Trade tradeObj;
            // Streams
            var successTrades = await _socketClient.SubscribeToTradeUpdatesAsync(symbol, (data) =>
           {
               tradeObj = new Models.Trade(TradeId: data.TradeId.ToString(), TradeTime: data.TradeTime, Symbol: data.Symbol,
                                        Price: data.Price, Qty: data.Quantity);
               string json = JsonSerializer.Serialize(tradeObj);

               redis.Publish(json);
           });
        }

        public async Task StopStreams()
        {
            await _socketClient.UnsubscribeAll();
        }
    }
}