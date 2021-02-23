using Microsoft.Extensions.DependencyInjection;

using TinyCardLimits.Core.Consts;
using TinyCardLimits.Core.Model.Types;
using TinyCardLimits.Core.Services.Interfaces;
using TinyCardLimits.Core.Services.Options;

using Xunit;


namespace TinyCardLimits.Core.Test
{
    public class CardLimitTests : IClassFixture<TinyCardLimitsFixture>
    {
        private ICardLimitService _cardLimit;

        public CardLimitTests(TinyCardLimitsFixture fixture)
        {
            _cardLimit = fixture.Scope.ServiceProvider
                .GetRequiredService<ICardLimitService>();
        }

        #region Test Async methods
        [Fact]
        public async void Add_CardLimit_Success_Async()
        {
            var options = new RegisterCardLimitOptions()
            {
                CardNumber = "9876489674127531",
                TransactionType = TransactionTypes.CardPresent,
                TransAmount = 400.0m
            };

            var result = await _cardLimit.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        [Fact]
        public async void Add_CardLimit_Success_UpdateExistingLimits_Async()
        {
            var options = new RegisterCardLimitOptions()
            {
                CardNumber = "9876411114127531",
                TransactionType = TransactionTypes.eCommerce,
                TransAmount = 400.0m
            };

            var result = await _cardLimit.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        [Fact]
        public async void Add_CardLimit_Fail_UpdateExistingLimits_Async()
        {
            var options = new RegisterCardLimitOptions()
            {
                CardNumber = "9876411114127531",
                TransactionType = TransactionTypes.eCommerce,
                TransAmount = 400.0m
            };

            var result = await _cardLimit.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        [Fact]
        public async void Add_CardLimit_Fail_UpdateExistingPRESENTLimits_Async()
        {
            var options = new RegisterCardLimitOptions()
            {
                CardNumber = "9876411114127531",
                TransactionType = TransactionTypes.CardPresent,
                TransAmount = 1000.0m
            };

            var result = await _cardLimit.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        [Fact]
        public async void Add_CardLimit_Fail_Empty_CardNum_Async()
        {
            var options = new RegisterCardLimitOptions()
            {
                CardNumber = "",
                TransactionType = TransactionTypes.CardPresent,
                TransAmount = 400.0m
            };

            var result = await _cardLimit.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        [Fact]
        public async void Add_CardLimit_Fail_Amount_Exceed_CardPresent_Async()
        {
            var options = new RegisterCardLimitOptions()
            {
                CardNumber = "9876489674127531",
                TransactionType = TransactionTypes.CardPresent,
                TransAmount = 400.0m
            };

            var result = await _cardLimit.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        [Fact]
        public async void Add_CardLimit_Fail_Amount_Exceed_eCommerce_Async()
        {
            var options = new RegisterCardLimitOptions()
            {
                CardNumber = "9876489674127531",
                TransactionType = TransactionTypes.CardPresent,
                TransAmount = 400.0m
            };

            var result = await _cardLimit.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        #endregion Test Async methods
    }
}
