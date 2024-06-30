using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

using FineNances.Core;

namespace FineNances.Model
{
    internal class Transaction : BindableBase
    {
        private DateTime _transactionDate;

        public enum TransactionTypeEnum
        {
            Income,
            Expense,
            Unknown
        }

        public int Id { get; set; }

        public int WalletId { get; set; }
        public virtual Wallet Wallet { get; set; }
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        public decimal Amount { get; set; }
        public DateTime TransactionDate { get; set; }
        public int TransactionTypeId { get; set; }

        [NotMapped]
        public int Percantage { get; set; }
        [NotMapped]
        public string DisplayPercantage => $"{Percantage}%";
        [NotMapped]
        public TransactionTypeEnum TransactionType => (TransactionTypeEnum)TransactionTypeId;
        [NotMapped]
        public Category TransactionCategory => DataWorker.GetCategoryById(CategoryId);
        [NotMapped]
        public Wallet TransactionWallet => DataWorker.GetWalletById(WalletId);
        [NotMapped]
        public string DisplayAmount => string.Format("{0:N} {1}", Amount, 
                                             new CultureInfo(TransactionWallet.CurrencyTypeCulture).NumberFormat.CurrencySymbol);
        [NotMapped]
        public string DisplayAmountWithSymbol => string.Format("{0}{1:N} {2}", TransactionType == TransactionTypeEnum.Income ? "+" : "-", Amount,
                                                       new CultureInfo(TransactionWallet.CurrencyTypeCulture).NumberFormat.CurrencySymbol);
    }
}
