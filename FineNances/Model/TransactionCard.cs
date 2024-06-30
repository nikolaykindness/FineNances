using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;
using System.Linq;

using FineNances.Core;

namespace FineNances.Model
{
    internal class TransactionCard : BindableBase
    {
        private decimal _totalAmount;

        public decimal TotalAmount
        {
            get { return _totalAmount; }
            set { _totalAmount = value; OnPropertyChanged(nameof(TotalAmount)); }
        }

        public ObservableCollection<Transaction> Transactions { get; set; }

        [NotMapped]
        public string DisplayTotalAmount
        {
            get => string.Format("{0:N} {1}", TotalAmount,
                                             new CultureInfo(Transactions.First().TransactionWallet.CurrencyTypeCulture).NumberFormat.CurrencySymbol);
        }

        public TransactionCard()
        {
            Transactions = new ObservableCollection<Transaction>();
            Transactions.CollectionChanged += OnTransactionsCollectionChanged;
        }

        public TransactionCard(ObservableCollection<Transaction> transactions)
        {
            Transactions = transactions;
            Transactions.CollectionChanged += OnTransactionsCollectionChanged;

            TotalAmount = Transactions.Sum(element => element.Amount);
        }

        private void OnTransactionsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:

                    if (e.NewItems?[0] is Transaction newTransaction)
                        TotalAmount += newTransaction.Amount;
                    
                    break;
                case NotifyCollectionChangedAction.Remove:
                    
                    if (e.NewItems?[0] is Transaction oldTransaction)
                        TotalAmount -= oldTransaction.Amount;
                    
                    break;
                default:
                    break;
            }
        }
    }
}
