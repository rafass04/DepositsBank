using BankDeposits.src.Services;
using Xunit;

namespace BankDepositsUnitTest.Services
{
    public class GetBankDepositsServiceTests
    {
        public IGetBankDepositsService GetBankDepositsService { get; set; }

        [Fact]
        public async void ShouldReturnBankDepositsAsync()
        {
            //Arrange
            GetBankDepositsService = new GetBankDepositsService();

            //Act
            var output = await GetBankDepositsService.GetBankDepositsAsync();

            //Assert
            Assert.NotNull(output);
        }
    }
}