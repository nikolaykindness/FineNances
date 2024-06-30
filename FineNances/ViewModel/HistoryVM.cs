using System.Collections.ObjectModel;
using System.Linq;

using FineNances.Core;
using FineNances.Model;

namespace FineNances.ViewModel
{
    internal class HistoryVM : BindableBase
    {
        private ObservableCollection<Wallet> _wallets;
        private ObservableCollection<Transaction> _transactions;
        private ObservableCollection<TransactionCard> _transactionCards;

        public Transaction SelectedTransaction { get; set; }
        public TransactionCard SelectedTransactionCard { get; set; }
        public Wallet SelectedWallet { get; set; }

        public ObservableCollection<Transaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; OnPropertyChanged(nameof(Transactions)); }
        }
        public ObservableCollection<TransactionCard> TransactionCards
        {
            get { return _transactionCards; }
            set { _transactionCards = value; OnPropertyChanged(nameof(TransactionCards)); }
        }
        public ObservableCollection<Wallet> Wallets
        {
            get { return _wallets; }
            set { _wallets = value; OnPropertyChanged(nameof(Wallets)); }
        }

        public HistoryVM()
        {
            UpdateTransactions();

            DataWorker.OnTransactionsChanged += (e) =>
            {
                UpdateTransactions();
            };

            Wallets = new ObservableCollection<Wallet>(DataWorker.GetAllWallets());

            DataWorker.OnWalletsChanged += (e) =>
            {
                Wallets = new ObservableCollection<Wallet>(DataWorker.GetAllWallets());
            };
        }

        private void UpdateTransactions()
        {
            TransactionCards = new ObservableCollection<TransactionCard>();
            Transactions = new ObservableCollection<Transaction>(DataWorker.GetAllTransactions());
            TransactionCards?.Clear();

            var some = Transactions.GroupBy(el => new { el.TransactionDate.Day, el.TransactionDate.Month });

            foreach (var group in some)
            {
                ObservableCollection<Transaction> newTransactions = [.. group];

                TransactionCards.Add(new TransactionCard(newTransactions));
            }
        }
    }
}
