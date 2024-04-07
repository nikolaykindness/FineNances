using FineNances.Core;
using FineNances.Model.Interfaces;
using System.Globalization;

namespace FineNances.Model
{
    internal class Transaction : ViewModelBase, ITransaction
    {
        public enum TransactionTypeEnum
        {
            Income,
            Expense,
            Transfer,
            Unknown
        }

        private int _transactionId;
        private int _walletId;
        private Wallet _wallet;
        private int _categoryId;
        private Category _category;

        private decimal _amount;
        private string _transactionDate;
        private TransactionTypeEnum _transactionType;
        private int _percantage;

        public int TransactionId
        {
            get { return _transactionId; }
            set {  _transactionId = value; }
        }

        public int WalletId
        {
            get { return _walletId; }
            set { _walletId = value; }
        }

        public virtual Wallet Wallet
        {
            get { return _wallet; }
            set { _wallet = value; OnPropertyChanged("ExpensingWallet"); }
        }

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }

        public Category Category
        {
            get { return _category; }
            set { _category = value; OnPropertyChanged(nameof(Category)); }
        }

        public string TransactionDate
        {
            get { return _transactionDate; }
            set { _transactionDate = value; OnPropertyChanged(nameof(TransactionDate)); }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged("AmountExpense"); }
        }

        public string DisplayAmount
        {
            get { return string.Format("{0:N} {1}", Amount, new CultureInfo(Wallet.CurrencyTypeCulture).NumberFormat.CurrencySymbol); }
        }

        public string DisplayAmountWithSymbol
        {
            get
            {
                return string.Format("{0}{1:N} {2}", TransactionType == TransactionTypeEnum.Income ? "+" : "-", Amount,
                                                       new CultureInfo(Wallet.CurrencyTypeCulture).NumberFormat.CurrencySymbol);
            }
        }

        public int Percantage
        {
            get { return _percantage; }
            set { _percantage = value; OnPropertyChanged(nameof(Percantage)); }
        }

        public string DisplayPercantage
        {
            get { return $"{Percantage}%"; }
        }

        public TransactionTypeEnum TransactionType
        {
            get { return _transactionType; }
            set { _transactionType = value; OnPropertyChanged(nameof(TransactionType)); }
        }

        public Transaction()
        {
            Wallet = new Wallet();
            Amount = 0;
            TransactionType = TransactionTypeEnum.Unknown;
            Category = new Category();
            TransactionDate = string.Empty;
        }

        public Transaction(Wallet wallet, decimal amount, TransactionTypeEnum transactionType, Category category, string transactionDate)
        {
            Wallet = wallet;
            Amount = amount;
            TransactionType = transactionType;
            Category = category;
            TransactionDate = transactionDate;
        }
    }
}
