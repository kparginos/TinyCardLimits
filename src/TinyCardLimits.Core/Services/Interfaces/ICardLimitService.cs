using System.Collections.Generic;
using System.Threading.Tasks;

using TinyCardLimits.Core.Model;
using TinyCardLimits.Core.Services.Options;
using TinyCardLimits.Core.Services.Results;

namespace TinyCardLimits.Core.Services.Interfaces
{
    public interface ICardLimitService
    {
        public Task<Result<List<CardLimit>>> RegisterAsync(RegisterCardLimitOptions options);
    }
}
