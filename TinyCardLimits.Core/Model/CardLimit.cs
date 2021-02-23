using System;

using TinyCardLimits.Core.Model.Types;

namespace TinyCardLimits.Core.Model
{
    public class CardLimit
    {
        public int CardLimitId { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public decimal? AggrAmount { get; set; }
        public DateTime? ApplyDate { get; set; }
    }
}
