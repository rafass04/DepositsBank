using AutoFixture;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VerifyDeposits.Models;
using VerifyDeposits.Repositories.DepositStatusRepository;
using Xunit;

namespace VerifyDepositsUnitTest.Repositories.DepositStatusRepositoryTests
{
    public class DepositStatusRepositoryTest
    {
        public DepositStatusDbContext? DepositStatusDbContext { get; set; }
        public IDepositStatusRepository? DepositStatusRepository { get; set; }
        public Fixture Fixture { get; set; } = new Fixture();

        [Fact]
        public async Task ShouldReturnAllDepositsStatusAsync()
        {
            //Arrange
            DepositStatusDbContext = await GetDatabaseContext();

            DepositStatusRepository = new DepositStatusRepository(DepositStatusDbContext);

            //Act
            var output = DepositStatusRepository.GetAllDepositsStatusAsync();

            //Assert
            Assert.NotNull(output);
            Assert.IsAssignableFrom<List<DepositStatus>>(output);
        }

        [Fact]
        public async Task ShouldReturnDepositStatusByAccountAsync()
        {
            //Arrange
            var accountDeposit = 10.ToString();
            DepositStatusDbContext = await GetDatabaseContext();

            DepositStatusRepository = new DepositStatusRepository(DepositStatusDbContext);

            //Act
            var output = await DepositStatusRepository.GetDepositStatusByAccountAsync(accountDeposit);

            //Assert
            Assert.NotNull(output);
            Assert.IsAssignableFrom<DepositStatus>(output);
        }

        private async Task<DepositStatusDbContext> GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<DepositStatusDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new DepositStatusDbContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.DepositStatus.CountAsync() <= 0)
            {
                for (int i = 1; i <= 10; i++)
                {
                    databaseContext.DepositStatus.Add(Fixture.Build<DepositStatus>()
                        .With(x => x.ContaDestino, i.ToString()).Create());
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;
        }
    }
}