using System.Threading.Tasks;

using TinyCardLimits.Core.Model;
using TinyCardLimits.Core.Services.Options;
using TinyCardLimits.Core.Services.Results;

namespace TinyCardLimits.Core.Services.Interfaces
{
    public interface ICardService
    {
        public Result<Card> Register(RegisterCardOptions options);
        public Task<Result<Card>> RegisterAsync(RegisterCardOptions options);

        public Task<Result<Card>> GetCardbyNumberAsync(string cardNumber);
    }
}
