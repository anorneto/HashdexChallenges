import React from "react";
import logo from "./logo.svg";
import "./App.css";
import RedisConnector from "./Services/redisConnector";

function App() {
  let redisConnector = new RedisConnector();
  redisConnector
    .getClient()
    .then(function(client) {
      console.log("Sucefully Conected");
      redisConnector.subscribe(client);
    })
    .catch(function(error){
      console.log(error);
    });

  return (
    <div className="App">
      <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>
    </div>
  );
}

export default App;
