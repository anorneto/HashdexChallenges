import React from "react";
import socketIO from "socket.io-client";
import { Table, Spinner } from "react-bootstrap";
import TableItem from "./TableItem";
import { PubSubChannel } from "../utils/constants";
import "./Table.css";

class TradesTable extends React.Component {
  constructor(props) {
    super(props);
    this.state = { trades: [] };
  }

  componentDidMount() {
    const socket = socketIO("http://localhost:8000");
    socket.on(PubSubChannel, data => {
      let newList = [data].concat(this.state.trades.slice(0, 14));
      this.setState({ trades: newList });
    });
  }

  render() {
    let trades = this.state.trades;

    let tableItems = trades.map(item => <TableItem data={item} />);

    let loading = (
      <Spinner animation="border" role="status" id="loader">
        <span className="sr-only">Loading...</span>
      </Spinner>
    );

    if (trades.length > 0)
      return (
        <div>
          <h3 id="title">Binance Ethereum Trades</h3>
          <Table striped bordered hover size="sm">
            <thead>
              <tr>
                <th>Received Time</th>
                <th>Trade Time</th>
                <th>Trade Id</th>
                <th>Symbol</th>
                <th>Price</th>
                <th>Quantity</th>
              </tr>
            </thead>
            <tbody>{tableItems}</tbody>
          </Table>
        </div>
      );
    else return loading;
  }
}

export default TradesTable;
