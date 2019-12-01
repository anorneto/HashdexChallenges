import redis from 'redis';
import * as Constants from '../Utils/constants';

export default RedisConnector;

class RedisConnector {
  constructor(host = Constants.RedisUrl, port = Constants.RedisPort) {
    this.host = host;
    this.port = port;
  }

  connect() {
    return new Promise((resolve, reject) => {
      const client = redis.createClient(
        this.host,
        this.port,
        {
          retry_strategy: function(options) {
            if (options.error && options.error.code === "ECONNREFUSED") {
              // End reconnecting on a specific error and flush all commands with
              // a individual error
              return new Error("The server refused the connection");
            }
            if (options.total_retry_time > 1000 * 60 * 60) {
              // End reconnecting after a specific timeout and flush all commands
              // with a individual error
              return new Error("Retry time exhausted");
            }
            if (options.attempt > 10) {
              // End reconnecting with built in error
              return undefined;
            }
            // reconnect after
            return Math.min(options.attempt * 100, 3000);
          }
        }
      );

      client.on("connect", () => {
        resolve(client);
      });

      client.on("error", () => {
        reject("Error: Failed to Connect");
      });
    });
  }

  subscribe(client, channel = Constants.PubSubChannel) {
    let msg_count = 0;
    client.subscribe(channel);
    client.on("message", function(channel, message) {
      console.log("sub channel " + channel + ": " + message);
      msg_count += 1;
      if (msg_count === 3) {
        client.unsubscribe();
        client.quit();
      }
    });
  }
}
