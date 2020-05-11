using Bank.Web.Repositories;
using Bank.Web.Services;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Bank.Tests
{
    public class AccountServiceTests
    {
        private readonly AccountService _sut;
        private readonly Mock<IAccountRepository> _accountRepoMock = new Mock<IAccountRepository>();
        private readonly Mock<ITransactionRepository> _transactionRepoMock = new Mock<ITransactionRepository>();

        public AccountServiceTests()
        {
            _sut = new AccountService(_accountRepoMock.Object, _transactionRepoMock.Object);
        }

        [Fact(DisplayName = "Withdrawal_should_throw_exception_when_account_has_insufficient_funds")]
        public async Task Withdrawal_should_throw_exception_when_account_has_insufficient_funds()
        {

        }
    }
}
