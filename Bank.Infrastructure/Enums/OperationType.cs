namespace Bank.Infrastructure.Enums
{
    public class OperationType
    {
        public string Value { get; set; }

        private OperationType(string value)
        {
            Value = value;
        }

        public static OperationType CreditInCash
        {
            get { return new OperationType("Credit in Cash"); }
        }

        public static OperationType WithdrawalInCash
        {
            get { return new OperationType("Withdrawal in Cash"); }
        }

        public static OperationType CollectionFromAnotherBank
        {
            get { return new OperationType("Collection from Another Bank"); }
        }

        public static OperationType RemittanceToAnotherBank
        {
            get { return new OperationType("Remittance to Another Bank"); }
        }
    }
}
