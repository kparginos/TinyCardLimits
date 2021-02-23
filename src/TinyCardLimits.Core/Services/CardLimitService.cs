using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using TinyCardLimits.Core.Consts;
using TinyCardLimits.Core.Data;
using TinyCardLimits.Core.Model;
using TinyCardLimits.Core.Model.Types;
using TinyCardLimits.Core.Services.Interfaces;
using TinyCardLimits.Core.Services.Options;
using TinyCardLimits.Core.Services.Results;

namespace TinyCardLimits.Core.Services
{
    public class CardLimitService : ICardLimitService
    {
        const decimal MAX_CARDPRESENT = 1500.0m;
        const decimal MAX_ECOMMERCE = 500.0m;

        private readonly ICardService _card;
        private readonly TinyCardLimitsDBContext _dbContext;

        public CardLimitService(TinyCardLimitsDBContext dbContext,
            ICardService card)
        {
            _dbContext = dbContext;
            _card = card;
        }

        public DateTime? Datetime { get; private set; }

        public async Task<Result<List<CardLimit>>> RegisterAsync(RegisterCardLimitOptions options)
        {
            if(string.IsNullOrWhiteSpace(options.CardNumber))
            {
                return new Result<List<CardLimit>>
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number cannot be empty !"
                };
            }

            if(options.CardNumber.Length != 16)
            {
                return new Result<List<CardLimit>>
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number must be 16 digits long !"
                };
            }

            // Find the card
            var result = await _card.GetCardbyNumberAsync(options.CardNumber);
            if(result.Code != ResultCodes.Success)
            {
                return new Result<List<CardLimit>>()
                {
                    Code = result.Code,
                    Message = result.Message
                };
            }

            // Transaction amount must not exceed MAX_CARDPRESENT or MAX_ECOMMERCE
            if(options.TransAmount > MAX_CARDPRESENT && options.TransactionType == TransactionTypes.CardPresent)
            {
                return new Result<List<CardLimit>>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Transaction amount {options.TransAmount} exceeds maximun amount {MAX_CARDPRESENT}"
                };
            }
            if (options.TransAmount > MAX_ECOMMERCE && options.TransactionType == TransactionTypes.eCommerce)
            {
                return new Result<List<CardLimit>>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Transaction amount {options.TransAmount} exceeds maximun amount {MAX_ECOMMERCE}"
                };
            }


            // Check whether the card has an availiable amount
            var card = result.Data;
            if(card.AvailableBalance < options.TransAmount)
            {
                return new Result<List<CardLimit>>()
                {
                    Code = ResultCodes.BadRequest,
                    Message = $"Not enough balance. Card availiable balance is {card.AvailableBalance}"
                };
            }

            // Check if limits are there for card number and current date
            if (card.Limits.Count == 0)
            {
                // If not create the two limits for the card
                // One for the requested trans type
                var cardLimit = new CardLimit()
                {
                    TransactionType = options.TransactionType,
                    AggrAmount = options.TransAmount,
                    ApplyDate = DateTime.Today.Date
                };
                card.Limits.Add(cardLimit);

                cardLimit = new CardLimit()
                {
                    TransactionType = (options.TransactionType == TransactionTypes.CardPresent) ? TransactionTypes.eCommerce : TransactionTypes.CardPresent,
                    AggrAmount = 0.0m,
                    ApplyDate = DateTime.Today.Date
                };

                card.Limits.Add(cardLimit);

                try
                {
                    await _dbContext.SaveChangesAsync();

                    return new Result<List<CardLimit>>()
                    {
                        Code = ResultCodes.Success,
                        Message = $"New limits added for Card Number {card.CardNumber}",
                        Data = card.Limits
                    };
                }
                catch (Exception ex)
                {
                    return new Result<List<CardLimit>>()
                    {
                        Code = ResultCodes.InternalServerError,
                        Message = $"Unable to update Card Limits information for Card Number {card.CardNumber}. Details: {ex.Message}"
                    };
                }
            }
            else
            {
                // otherwise update the limits
                var carLimit = card.Limits
                    .Where(l => l.TransactionType == options.TransactionType)
                    .SingleOrDefault();

                if (carLimit.AggrAmount + options.TransAmount > MAX_CARDPRESENT && options.TransactionType == TransactionTypes.CardPresent ||
                   carLimit.AggrAmount + options.TransAmount > MAX_ECOMMERCE && options.TransactionType == TransactionTypes.eCommerce)
                {
                    return new Result<List<CardLimit>>()
                    {
                        Code = ResultCodes.BadRequest,
                        Message = $"Maximun card allowance exceeded for Card Number {card.CardNumber} and Transaction Type {options.TransactionType}"
                    };
                }
                var sumOfLimits = card.Limits
                    .Sum(l => l.AggrAmount);

                if(sumOfLimits + options.TransAmount > card.AvailableBalance)
                {
                    return new Result<List<CardLimit>>()
                    {
                        Code = ResultCodes.BadRequest,
                        Message = $"Card availiable balance <<{card.AvailableBalance}>> exceeded"
                    };
                }

                carLimit.AggrAmount += options.TransAmount;

                await _dbContext.SaveChangesAsync();

                return new Result<List<CardLimit>>()
                {
                    Code = ResultCodes.Success,
                    Message = $"Transaction completed. Availiable card balance is {card.AvailableBalance - (sumOfLimits + options.TransAmount)}",
                    Data = card.Limits
                };
            }
        }

        public async Task<Result<decimal>> GetCardLimitsAsync(string cardNumber)
        {
            if(string.IsNullOrWhiteSpace(cardNumber))
            {
                return new Result<decimal>
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number cannot be empty !",
                    Data = 0.0m
                };
            }

            if (options.CardNumber.Length != 16)
            {
                return new Result<List<CardLimit>>
                {
                    Code = ResultCodes.BadRequest,
                    Message = "Card Number must be 16 digits long !"
                };
            }

            var result = await _card.GetCardbyNumberAsync(cardNumber);

            if(result.Code == ResultCodes.Success)
            {
                var sum = result.Data.Limits
                    .Sum(l => l.AggrAmount);

                return new Result<decimal>()
                {
                    Code = ResultCodes.Success,
                    Message = $"Daily aggregate amounts for Card Number {cardNumber}",
                    Data = (decimal)sum
                };
            }
            else
            {
                return new Result<decimal>()
                {
                    Code = result.Code,
                    Message = result.Message,
                    Data = 0.0m
                };
            }
        }
    }
}
