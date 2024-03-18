using FineNances.Core;
using FineNances.Model.Interfaces;
using static FineNances.Model.Interfaces.IWallet;
using System.Globalization;
using System.Collections.Generic;
using System;

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

        private Wallet _wallet;
        private decimal _amount;
        private Category _category;
        private string _transactionDate;
        private TransactionTypeEnum _transactionType;
        private int _percantage;

        public Wallet Wallet
        {
            get { return _wallet; }
            set { _wallet = value; OnPropertyChanged("ExpensingWallet"); }
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
