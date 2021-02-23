using TinyCardLimits.Core.Model.Types;

namespace TinyCardLimits.Core.Services.Options
{
    public class RegisterCardLimitOptions
    {
        public string CardNumber { get; set; }
        public TransactionTypes TransactionType { get; set; }
        public decimal TransAmount { get; set; }
    }
}
