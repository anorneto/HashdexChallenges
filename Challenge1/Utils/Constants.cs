using System;
using System.Collections.Generic;
using System.Text;

namespace HashdexChallenge1.Utils
{
    static class Constants
    {
        public const string DefaultSymbol = "ethbtc";
        public const string DefaultPubSubChannel = "trade:binance:ethbtc";
        //redis-cli -c -h ec2-13-59-98-235.us-east-2.compute.amazonaws.com -p 6379 
        public const string DefaultRedisAddres = "localhost";
        public const int DefaultRedisPort = 6379;
    }
}
