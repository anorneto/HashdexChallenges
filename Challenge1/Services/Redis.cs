using StackExchange.Redis;


namespace HashdexChallenge1.Services
{
    public class Redis
    {
        private ConnectionMultiplexer _redisInstance;
        private ISubscriber _sub;

        ~Redis() => _redisInstance.Close();

        public Redis(string host = Utils.Constants.DefaultRedisAddres, int port = Utils.Constants.DefaultRedisPort)
        {
            _redisInstance = ConnectionMultiplexer.Connect($"{host}:{port}");
            _sub = _redisInstance.GetSubscriber();
        }

        public void Publish(string message, string channel = Utils.Constants.DefaultPubSubChannel)
        {
            _sub.Publish(channel, message);
        }

    }
}
