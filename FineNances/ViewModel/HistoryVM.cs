using FineNances.Core;
using FineNances.Model;
using System;
using System.Collections.ObjectModel;
using System.Net.Http.Headers;

namespace FineNances.ViewModel
{
    internal class HistoryVM : ViewModelBase
    {
        private Transaction _selectedTransaction;
        private ObservableCollection<Transaction> _selectedTransactionsDay;

        public Transaction SelectedTransaction
        { 
            get { return _selectedTransaction; } 
            set { _selectedTransaction = value; OnPropertyChanged(nameof(SelectedTransaction)); }
        }
        public ObservableCollection<Transaction> SelectedTransactionsDay
        {
            get { return _selectedTransactionsDay; }
            set { _selectedTransactionsDay = value; OnPropertyChanged(nameof(SelectedTransactionsDay)); }
        }
        public ObservableCollection<Transaction> Transactions { get; set; }
        public ObservableCollection<ObservableCollection<Transaction>> TransactionsDay { get; set; }

        public HistoryVM() 
        {
            Transactions = new ObservableCollection<Transaction>()
            {
                new Transaction() { Wallet = null, Amount = 12000m, TransactionType = Transaction.TransactionTypeEnum.Expense,
                    Category = new Category("Покупки"), TransactionDate = DateTime.Now.ToString("M") },
                new Transaction() { Wallet = null, Amount = 10500m, TransactionType = Transaction.TransactionTypeEnum.Expense,
                    Category = new Category("Образование"), TransactionDate = DateTime.Now.ToString("M") },
                new Transaction() { Wallet = null, Amount = 800m, TransactionType = Transaction.TransactionTypeEnum.Expense,
                    Category = new Category("Питание"), TransactionDate = DateTime.Now.ToString("M") },
            };

            TransactionsDay = 
                [
                    Transactions, Transactions, Transactions,
                    Transactions, Transactions, Transactions,
                    Transactions, Transactions, Transactions,
                    Transactions, Transactions, Transactions
                ];
        }
    }
}
