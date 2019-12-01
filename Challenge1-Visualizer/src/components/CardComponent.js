import React from 'react';

class CardComponent extends React.Component {
    render() {
        let data = this.props.data;

        return (
            <div>
                Receive Time : {data.ReceiveTime},
                Trade Time : {data.TradeTime},
                Trade Id : {data.TradeId},
                Symbol : {data.Symbol},
                Price: {data.Price},
                Qty: {data.Qty},
                
            </div>
        );
    }
}

export default CardComponent;