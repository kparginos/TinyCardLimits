using System.Collections.Generic;

namespace TinyCardLimits.Core.Model
{
    public class Card
    {
        public int CardId { get; set; }
        public string CardNumber { get; set; }
        public decimal? AvailableBalance { get; set; }
        public List<CardLimit> Limits { get; set; }
    }
}
