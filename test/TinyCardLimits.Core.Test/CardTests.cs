using Microsoft.Extensions.DependencyInjection;

using TinyCardLimits.Core.Consts;
using TinyCardLimits.Core.Services.Interfaces;
using TinyCardLimits.Core.Services.Options;

using Xunit;

namespace TinyCardLimits.Core.Test
{
    public class CardTests : IClassFixture<TinyCardLimitsFixture>
    {
        private ICardService _card;

        public CardTests(TinyCardLimitsFixture fixture)
        {
            _card = fixture.Scope.ServiceProvider
                .GetRequiredService<ICardService>();
        }

        #region Test Async methods
        [Fact]
        public async void Add_Card_Success_Asyns()
        {
            var options = new RegisterCardOptions()
            {
                CardNumber = "9876411114127531",
                AvailableBalance = 1000.0m
            };

            var result = await _card.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }

        [Fact]
        public async void Add_Card_Fail_Asyns()
        {
            var options = new RegisterCardOptions()
            {
                CardNumber = "",
                AvailableBalance = 2500.0m
            };

            var result = await _card.RegisterAsync(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        #endregion Test Async methods

        #region Test normal methods
        [Fact]
        public void Add_Card_Success()
        {
            var options = new RegisterCardOptions()
            {
                CardNumber = "9876489674127531",
                AvailableBalance = 2500.0m
            };

            var result = _card.Register(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }

        [Fact]
        public void Add_Card_Fail()
        {
            var options = new RegisterCardOptions()
            {
                CardNumber = "",
                AvailableBalance = 2500.0m
            };

            var result = _card.Register(options);

            Assert.Equal(ResultCodes.Success, result.Code);
        }
        #endregion Test normal methods
    }
}
