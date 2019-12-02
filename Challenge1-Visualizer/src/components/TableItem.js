import React from "react";
import moment from "moment";


class TableItem extends React.Component {
  render() {
    let data = this.props.data;
    return ( 
      <tr key={data.TradeId}>
        <td>{moment(data.ReceivedTime).format('MM/DD/YYYY h:mm a')}</td>
        <td>{moment.utc(data.TradeTime).format('MM/DD/YYYY h:mm a')}</td>
        <td>{data.TradeId}</td>
        <td>{data.Symbol}</td>
        <td>{data.Price}</td>
        <td>{data.Qty}</td>
      </tr>
    );
  }
}

export default TableItem;
