const redis = require("ioredis");
const io = require("socket.io").listen(8000);
const constants = require("../utils/constants");


io.sockets.on("connection", function(socket) {
  console.log(`Socket ${socket.id} connected.`);
});

const client = new redis(constants.RedisPort, constants.RedisUrl);

client.on("connect", function() {
  console.log("REDIS CONNECTED");
});

client.on("message", (channel, message) => {
  io.sockets.emit(constants.PubSubChannel,JSON.parse(message));
  console.log(channel +" - " + Date());
});

client.subscribe(constants.PubSubChannel);

