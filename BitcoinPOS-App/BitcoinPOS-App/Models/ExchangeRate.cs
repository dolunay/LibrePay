using System;

namespace BitcoinPOS_App.Models
{
    public class ExchangeRate
    {
        public decimal Rate { get; set; }

        public string DisplayRate => $"฿/BRL {Rate:N2}";

        public DateTime Date { get; set; }

        public decimal GetExchangedValue(decimal valueFiat)
            => Math.Round(valueFiat / Rate, Constants.BitcoinDecimals, MidpointRounding.AwayFromZero);

        public ExchangeRate()
        {
            Date = DateTime.Now;
        }

        public ExchangeRate(decimal rate, DateTime date)
        {
            Rate = rate;
            Date = date;
        }
    }
}