using BankDeposits.src.Domain;
using BankDeposits.src.Services;
using BankDeposits.src.Services.Query;
using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BankDepositsUnitTest.Services
{
    public class ValidateDepositsServiceTests
    {
        private readonly Mock<IGetBankDepositsQuery> _getBankDepositsQuery;
        private readonly Mock<ISendDepositEmailService> _sendDepositEmailService;
        private readonly IValidateDepositsService _validateDepositsService;

        public ValidateDepositsServiceTests()
        {
            _getBankDepositsQuery = new Mock<IGetBankDepositsQuery>();
            _sendDepositEmailService = new Mock<ISendDepositEmailService>();
            _validateDepositsService = new ValidateDepositsService(_getBankDepositsQuery.Object, _sendDepositEmailService.Object);
        }

        [Fact]
        public async Task ShouldValidateDepositsAsync()
        {
            //Arrange
            var depositList = GetDepositList();

            //Act
            await _validateDepositsService.ValidateDepositsAsync(depositList);

            //Assert
            _sendDepositEmailService.Verify(mock => mock.SendDepositEmail(It.IsAny<Deposit>()), Times.Once);
            _getBankDepositsQuery.Verify(mock => mock.SaveDepositsAsync(It.IsAny<DepositStatus>()), Times.Exactly(depositList.Count));
        }

        private List<Deposit> GetDepositList()
        {
            return new List<Deposit>()
            {
                new Deposit { Id="1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",Nome="Luana Souza",AgenciaDestino="0001",ContaDestino="123456",AgenciaOrigem="0867",ContaOrigem="654321",Valor=130.45,DataTransacao=DateTime.Now},
                new Deposit { Id="1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",Nome="Luana Silva",AgenciaDestino="0001",ContaDestino="123456",AgenciaOrigem="0867",ContaOrigem="654321",Valor=130.45,DataTransacao=DateTime.Now},
                new Deposit { Id="1d9b4f37-7fac-467f-a1e1-b33ec2e8025d",Nome="Luana Souza",AgenciaDestino="0001",ContaDestino="123456",AgenciaOrigem="0867",ContaOrigem="654321",Valor=130.45,DataTransacao=DateTime.Now},
                new Deposit { Id="1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",Nome="Luana Souza",AgenciaDestino="0002",ContaDestino="123456",AgenciaOrigem="0867",ContaOrigem="654321",Valor=130.45,DataTransacao=DateTime.Now},
                new Deposit { Id="1d9b4f37-7fac-467f-a1e1-b33ec2e8025f",Nome="Luana Souza",AgenciaDestino="0001",ContaDestino="000000",AgenciaOrigem="0867",ContaOrigem="654321",Valor=130.45,DataTransacao=DateTime.Now}
            };
        }
    }
}