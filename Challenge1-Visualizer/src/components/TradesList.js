import React from "react";
import socketIO from "socket.io-client";
import { TransitionGroup } from 'react-transition-group';
import CardComponent from './CardComponent';
import {PubSubChannel} from '../utils/constants';

class TradesList extends React.Component {
  constructor(props) {
    super(props);
    this.state = { trades: [] };
  }

  componentDidMount() {
    const socket = socketIO("http://localhost:8000");
    socket.on(PubSubChannel, data => {
      let newList = [data].concat(this.state.trades);
      console.info(this.state.trades);
      this.setState({ trades: newList });
    });
  }

  render() {
    let trades = this.state.trades;

    let tradesCards = (
      <TransitionGroup
        transitionName="example"
        transitionEnterTimeout={500}
        transitionLeaveTimeout={300}
      >
        {trades.map((x, i) => (
          <CardComponent key={i} data={x} />
        ))}
      </TransitionGroup>
    );

    /*     let itemsCards = <CSSTransitionGroup
      transitionName="example"
      transitionEnterTimeout={500}
      transitionLeaveTimeout={300}>
      {items.map((x, i) =>
        <CardComponent key={i} data={x} />
      )}
    </CSSTransitionGroup>; */

    let loading = (
      <div>
        <p className="flow-text">Listening to Streams</p>
        <div className="progress lime lighten-3">
          <div className="indeterminate pink accent-1"></div>
        </div>
      </div>
    );

    return (
      <div className="row">
        <div className="col s12 m4 l4">
          <p>Teste 123</p>
        </div>
        <div className="col s12 m4 l4">
          <div>{trades.length > 0 ? tradesCards : loading}</div>
        </div>
        <div className="col s12 m4 l4"></div>
      </div>
    );
  }
}

export default TradesList;
