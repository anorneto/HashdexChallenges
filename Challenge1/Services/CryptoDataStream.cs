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
        private Redis _redis;

        public BinanceDataStream(Redis redisInstance)
        {
            this._socketClient = new BinanceSocketClient();
            this._redis = redisInstance;
        }

        ~BinanceDataStream() => _socketClient.Dispose();

        public async Task PubTrades(string symbol)
        {
            String channel = "trade:binance:" + symbol.ToLower();
            // Streams
            CryptoExchange.Net.Objects.CallResult<CryptoExchange.Net.Sockets.UpdateSubscription> result;

            for (int i = 0; i < Utils.Constants.MaxTries; i++)
            {
                result = await _socketClient.SubscribeToTradeUpdatesAsync(symbol, (data) => publishToRedis(data, channel));
                if (result.Success)
                {
                    Console.WriteLine("     Sucefully connected to Trade WebSocket\n");
                    break;
                }
                else
                {
                    Console.WriteLine($"    ERROR : {result.Error}");
                    await Task.Delay(Utils.Constants.TryDelay);
                    Console.WriteLine($"     Attempting to Connect {i + 1} of {Utils.Constants.MaxTries}");
                };
            }

            

        }

        public async Task StopStreams()
        {
            Console.WriteLine("Stopping all Streams");
            await _socketClient.UnsubscribeAll();
        }

        private void publishToRedis(Binance.Net.Objects.BinanceStreamTrade data, string channel)
        {
            Models.Trade tradeObj;
            tradeObj = new Models.Trade(TradeId: data.TradeId.ToString(), TradeTime: data.TradeTime, Symbol: data.Symbol,
                                        Price: data.Price, Qty: data.Quantity);
            string json = JsonSerializer.Serialize(tradeObj);

            Console.WriteLine(json);

            _redis.Publish(json, channel);
        }
    }
}