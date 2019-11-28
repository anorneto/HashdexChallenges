using System;

namespace HashdexChallenge1.Models
{
    public class Trade
    {
        public DateTime ReceiveTime { get; set; } //Time in which the trade was received 
        public DateTime TradeTime { get; set; } // Time in which the trade was executed
        public string TradeId { get; set; } // Trade ID as sent by the exchange
        public string Symbol { get; set; } // Represents the asset being traded
        public decimal Price { get; set; } // Trade price
        public decimal Qty { get; set; } // Quantity of assets traded

        public Trade(DateTime TradeTime, string TradeId, string Symbol, decimal Price, decimal Qty)
        {
            this.ReceiveTime = DateTime.Now;
            this.TradeTime = TradeTime;
            this.TradeId = TradeId;
            this.Symbol = Symbol;
            this.Price = Price;
            this.Qty = Qty;
        }
    }
}
