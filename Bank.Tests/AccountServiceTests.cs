using Bank.Infrastructure.Entities;
using Bank.Infrastructure.Enums;
using Bank.Web.Exceptions;
using Bank.Web.Repositories;
using Bank.Web.Services;
using Moq;
using System;
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

        [Fact]
        public async Task Transfer_should_create_multiple_transactions_correctly()
        {
            // Arrange
            decimal amount = 368.55m;
            decimal fromAccountBalance = 847.39m;
            int fromAccountId = 1;
            int toAccountId = 2;
            decimal toAccountBalance = 1134.39m;

            Transactions fromTransaction = new Transactions
            {
                AccountId = fromAccountId,
                Amount = amount,
                Balance = 478.84m,
                Date = DateTime.Now,
                Type = TransactionType.Debit.ToString(),
                Operation = OperationType.RemittanceToAnotherBank.Value
            };

            Transactions toTransaction = new Transactions
            {
                AccountId = toAccountId,
                Amount = amount,
                Balance = 1502.94m,
                Date = DateTime.Now,
                Type = TransactionType.Credit.ToString(),
                Operation = OperationType.CollectionFromAnotherBank.Value
            };

            _accountRepoMock.Setup(x => x.Get(fromAccountId)).Returns(new Accounts
            {
                AccountId = fromAccountId,
                Balance = fromAccountBalance,
                Created = DateTime.Now
            });

            _accountRepoMock.Setup(x => x.Get(toAccountId)).Returns(new Accounts
            {
                AccountId = toAccountId,
                Balance = toAccountBalance,
                Created = DateTime.Now
            });

            _transactionRepoMock.Setup(x => x.CreateMultiple(fromTransaction, toTransaction)).ReturnsAsync(true);

            // Act
            await _sut.Transfer(fromAccountId, toAccountId, amount);

            // Assert
            _transactionRepoMock.Verify(x => x.CreateMultiple(It.IsAny<Transactions>(), It.IsAny<Transactions>()), Times.Once());
        }

        [Fact]
        public async Task Transfer_should_throw_NoNegativeAmountException_if_amount_is_negative()
        {
            // Arrange
            decimal amount = -424.88m;
            decimal fromAccountBalance = 847.39m;
            int fromAccountId = 1;
            int toAccountId = 2;
            decimal toAccountBalance = 451.55m;

            _accountRepoMock.Setup(x => x.Get(fromAccountId)).Returns(new Accounts
            {
                AccountId = fromAccountId,
                Balance = fromAccountBalance,
                Created = DateTime.Now
            });

            _accountRepoMock.Setup(x => x.Get(toAccountId)).Returns(new Accounts
            {
                AccountId = toAccountId,
                Balance = toAccountBalance,
                Created = DateTime.Now
            });

            // Act
            var exception = await Assert.ThrowsAsync<NoNegativeAmountException>(() => _sut.Transfer(fromAccountId, toAccountId, amount));

            // Assert
            Assert.IsType<NoNegativeAmountException>(exception);
        }

        [Fact]
        public async Task Transfer_should_throw_InsufficientFundsException_if_from_account_balance_is_insufficient()
        {
            // Arrange
            decimal amount = 612.31m;
            decimal fromAccountBalance = 233.98m;
            int fromAccountId = 1;
            int toAccountId = 2;
            decimal toAccountBalance = 451.55m;

            _accountRepoMock.Setup(x => x.Get(fromAccountId)).Returns(new Accounts
            {
                AccountId = fromAccountId,
                Balance = fromAccountBalance,
                Created = DateTime.Now
            });

            _accountRepoMock.Setup(x => x.Get(toAccountId)).Returns(new Accounts
            {
                AccountId = toAccountId,
                Balance = toAccountBalance,
                Created = DateTime.Now
            });

            // Act
            var exception = await Assert.ThrowsAsync<InsufficientFundsException>(() => _sut.Transfer(fromAccountId, toAccountId, amount));

            // Assert
            Assert.IsType<InsufficientFundsException>(exception);
        }

        [Fact]
        public async Task Transfer_should_return_true_if_from_account_balance_is_sufficient()
        {
            // Arrange
            decimal amount = 436.17m;
            decimal fromAccountBalance = 2697.68m;
            int fromAccountId = 1;
            int toAccountId = 2;
            decimal toAccountBalance = 358.61m;

            _accountRepoMock.Setup(x => x.Get(fromAccountId)).Returns(new Accounts
            {
                AccountId = fromAccountId,
                Balance = fromAccountBalance,
                Created = DateTime.Now
            });

            _accountRepoMock.Setup(x => x.Get(toAccountId)).Returns(new Accounts
            {
                AccountId = toAccountId,
                Balance = toAccountBalance,
                Created = DateTime.Now
            });

            // Act
            var result = await _sut.Transfer(fromAccountId, toAccountId, amount);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Withdrawal_should_create_transaction_correctly()
        {
            // Arrange
            int accountId = 1;
            decimal amount = 776.34m;
            decimal balance = 1834.59m;

            Transactions transaction = new Transactions
            {
                AccountId = accountId,
                Amount = amount,
                Balance = 1058.25m,
                Date = DateTime.Now,
                Type = TransactionType.Debit.ToString(),
                Operation = OperationType.WithdrawalInCash.Value
            };

            _accountRepoMock.Setup(x => x.Get(accountId)).Returns(new Accounts
            {
                AccountId = accountId,
                Balance = balance,
                Created = DateTime.Now
            });

            _transactionRepoMock.Setup(x => x.Create(transaction)).ReturnsAsync(true);

            // Act
            await _sut.Deposit(accountId, amount);

            // Assert
            _transactionRepoMock.Verify(x => x.Create(It.IsAny<Transactions>()), Times.Once());
        }

        [Fact]
        public async Task Withdrawal_should_throw_NoNegativeAmountException_if_amount_is_negative()
        {
            // Arrange
            int accountId = 1;
            decimal amount = -3467.13m;
            decimal balance = 497.96m;

            _accountRepoMock.Setup(x => x.Get(accountId)).Returns(new Accounts
            {
                AccountId = accountId,
                Balance = balance,
                Created = DateTime.Now
            });

            // Act
            var exception = await Assert.ThrowsAsync<NoNegativeAmountException>(() => _sut.Withdrawal(accountId, amount));

            // Assert
            Assert.IsType<NoNegativeAmountException>(exception);
        }

        [Fact]
        public async Task Withdrawal_should_throw_InsufficientFundsException_if_account_balance_is_insufficient()
        {
            // Arrange
            int accountId = 1;
            decimal amount = 1519.77m;
            decimal balance = 497.96m;

            _accountRepoMock.Setup(x => x.Get(accountId)).Returns(new Accounts
            {
                AccountId = accountId,
                Balance = balance,
                Created = DateTime.Now
            });

            // Act
            var exception = await Assert.ThrowsAsync<InsufficientFundsException>(() => _sut.Withdrawal(accountId, amount));

            // Assert
            Assert.IsType<InsufficientFundsException>(exception);
        }

        [Fact]
        public async Task Withdrawal_should_return_true_if_account_balance_is_sufficient()
        {
            // Arrange
            int accountId = 1;
            decimal amount = 774.56m;
            decimal balance = 1194.35m;

            _accountRepoMock.Setup(x => x.Get(accountId)).Returns(new Accounts
            {
                AccountId = accountId,
                Balance = balance,
                Created = DateTime.Now
            });

            // Act
            var result = await _sut.Withdrawal(accountId, amount);

            // Assert
            Assert.True(result);
        }

        [Fact]
        public async Task Deposit_should_create_transaction_correctly()
        {
            // Arrange
            int accountId = 1;
            decimal amount = 249.66m;
            decimal balance = 678.45m;

            Transactions transaction = new Transactions
            {
                AccountId = accountId,
                Amount = amount,
                Balance = 928.11m,
                Date = DateTime.Now,
                Type = TransactionType.Credit.ToString(),
                Operation = OperationType.CollectionFromAnotherBank.Value
            };

            _accountRepoMock.Setup(x => x.Get(accountId)).Returns(new Accounts
            {
                AccountId = accountId,
                Balance = balance,
                Created = DateTime.Now
            });

            _transactionRepoMock.Setup(x => x.Create(transaction)).ReturnsAsync(true);

            // Act
            await _sut.Deposit(accountId, amount);

            // Assert
            _transactionRepoMock.Verify(x => x.Create(It.IsAny<Transactions>()), Times.Once());
        }

        [Fact]
        public async Task Deposit_should_throw_NoNegativeAmountException_if_amount_is_negative()
        {
            // Arrange
            int accountId = 1;
            decimal amount = -1297.89m;
            decimal balance = 556.45m;

            _accountRepoMock.Setup(x => x.Get(accountId)).Returns(new Accounts
            {
                AccountId = accountId,
                Balance = balance,
                Created = DateTime.Now
            });

            // Act
            var exception = await Assert.ThrowsAsync<NoNegativeAmountException>(() => _sut.Deposit(accountId, amount));

            // Assert
            Assert.IsType<NoNegativeAmountException>(exception);
        }

        [Fact]
        public void GetAccount_should_return_account_when_account_exists()
        {
            // Arrange 
            var accountId = 1;
            var balance = 5000;
            _accountRepoMock
                .Setup(x => x.Get(accountId))
                .Returns(new Accounts { AccountId = accountId, Balance = balance, Created = DateTime.Now });

            // Act 
            var account = _sut.GetAccount(accountId);

            // Assert
            Assert.Equal(accountId, account.AccountId);
            Assert.Equal(balance, account.Balance);
        }

        [Fact]
        public void GetAccount_should_return_nothing_when_account_does_not_exist()
        {
            // Arrange 
            var accountId = 1;
            _accountRepoMock
                .Setup(x => x.Get(It.IsAny<int>()))
                .Returns(() => null);

            // Act 
            var account = _sut.GetAccount(accountId);

            // Assert
            Assert.Null(account);
        }
    }
}
