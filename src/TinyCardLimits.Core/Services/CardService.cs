using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using TinyCardLimits.Core.Consts;
using TinyCardLimits.Core.Data;
using TinyCardLimits.Core.Model;
using TinyCardLimits.Core.Services.Interfaces;
using TinyCardLimits.Core.Services.Options;
using TinyCardLimits.Core.Services.Results;

namespace TinyCardLimits.Core.Services
{
    public class CardService : ICardService
    {
        private readonly TinyCardLimitsDBContext _dbContext;

        public CardService(TinyCardLimitsDBContext dbContext)
        {
            _dbContext = dbContext;
        }


        #region CRUD methods
        public Result<Card> Register(RegisterCardOptions options)
        {
            if (string.IsNullOrWhiteSpace(options.CardNumber))
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number cannot be empty !"
                };
            }

            var card = new Card()
            {
                CardNumber = options.CardNumber,
                AvailableBalance = options.AvailableBalance,
            };

            try
            {
                _dbContext.Add<Card>(card);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.InternalServerError,
                    Message = $"Failed to save card. Details: {ex.Message}",
                };
            }

            return new Result<Card>()
            {
                Code = ResultCodes.Success,
                Message = "Card saved.",
                Data = card
            };
        }

        public async Task<Result<Card>> RegisterAsync(RegisterCardOptions options)
        {
            if(string.IsNullOrWhiteSpace(options.CardNumber))
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number cannot be empty !"
                };
            }

            if (options.CardNumber.Length != 16)
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number cannot be empty !"
                };
            }

            var card = new Card()
            {
                CardNumber = options.CardNumber,
                AvailableBalance = options.AvailableBalance,
            };

            try
            {
                await _dbContext.AddAsync<Card>(card);
                await _dbContext.SaveChangesAsync();
            }
            catch(Exception ex)
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.InternalServerError,
                    Message = $"Failed to save card. Details: {ex.Message}",
                };
            }

            return new Result<Card>()
            {
                Code = ResultCodes.Success,
                Message = "Card saved.",
                Data = card
            };
        }
        #endregion CRUD methods

        #region Info methods
        public async Task<Result<Card>> GetCardbyNumberAsync(string cardNumber)
        {
            if(string.IsNullOrWhiteSpace(cardNumber))
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number cannot be empty !"
                };
            }

            var card = await _dbContext.Cards
                .Where(n => n.CardNumber == cardNumber)
                .Include(l => l.Limits)
                .SingleOrDefaultAsync();

            if(card == null)
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.NotFound,
                    Message = $"Card number {cardNumber} not found"
                };
            }
            else
            {
                return new Result<Card>()
                {
                    Code = ResultCodes.Success,
                    Message = "Card information found",
                    Data = card
                };
            }
        }
        #endregion Info methods
    }
}
