using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows.Input;

using FineNances.Core;
using FineNances.Controls;
using FineNances.Model;
using FineNances.View;
using System.Windows.Documents;
using System.Collections.Generic;

namespace FineNances.ViewModel
{
    internal class HomeVM : BindableBase
    {
        private ObservableCollection<Transaction> _topExpenses;
        private ObservableCollection<Transaction> _recentlyTransactions;

        public string DisplayTotalExpenses { get; set; }

        public Wallet SelectedWallet { get; set; }
        public Transaction SelectedExpenseTransaction { get; set; }
        public Transaction SelectedRecentlyTransaction { get; set; }
        public Goal SelectedGoal { get; set; }

        public ObservableCollection<Goal> Goals { get; set; }
        public ObservableCollection<Wallet> Wallets { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }

        public ObservableCollection<Transaction> TopExpenses
        {
            get { return _topExpenses; }
            set {  _topExpenses = value; OnPropertyChanged(nameof(TopExpenses)); }        
        }
        public ObservableCollection<Transaction> RecentlyTransactions
        {
            get { return _recentlyTransactions; }
            set { _recentlyTransactions = value; OnPropertyChanged(nameof(RecentlyTransactions)); }
        }

        public ICommand AddWalletCommand => new RelayCommand(AddWalletModal);
        public ICommand SaveWalletCommand => new RelayCommand(SaveWallet);
        public ICommand CancelModalCommand => new RelayCommand(CancelModal);

        public ICommand ToHistory => new RelayCommand(ToHistoryNavigate);

        public HomeVM()
        {
            Wallets = new ObservableCollection<Wallet>(DataWorker.GetAllWallets());

            Goals = new ObservableCollection<Goal>()
            {
                new Goal() { Name = "Компьютерные девайсы", FinalAmount = 20000m, CurrentAmount = 5000m, CurrencyType = Goal.CurrencyTypeEnum.RU },
                new Goal() { Name = "На монитор", FinalAmount = 25000, CurrentAmount = 12650m, CurrencyType = Goal.CurrencyTypeEnum.RU },
                new Goal() { Name = "На видеокарту", FinalAmount = 80000, CurrentAmount = 64300m, CurrencyType = Goal.CurrencyTypeEnum.RU }
            };

            var temp = DataWorker.GetAllTransactions();

            Transactions = new ObservableCollection<Transaction>(temp);

            UpdateTopExpenses(temp);

            RecentlyTransactions = new ObservableCollection<Transaction>(temp.Skip(Math.Max(0, temp.Count() - 3)).Reverse());

            decimal sum = 0m;

            foreach (Transaction item in Transactions)
            {
                if(item.TransactionDate.Month == DateTime.Now.Month
                   && item.TransactionType == Transaction.TransactionTypeEnum.Expense)
                {
                    sum += item.Amount;
                }
            }

            DisplayTotalExpenses = string.Format(new CultureInfo("ru-RU"), "{0:N0} {1}", sum, new CultureInfo("ru-RU").NumberFormat.CurrencySymbol);
            foreach (var item in TopExpenses)
            {
                item.Percantage = (int)(item.Amount / sum * 100.0m);
            }
        }

        private void UpdateTopExpenses(List<Transaction> transactions)
        {
            var topExpenses = transactions.Where(el => el.TransactionType == Transaction.TransactionTypeEnum.Expense
                                         && el.TransactionDate.Month == DateTime.Now.Month)
                                  .GroupBy(el => new { el.CategoryId }, el => el, (key, g) => new { key.CategoryId, expenses = g.ToList() })
                                  .Select(x => new { x.CategoryId, WalletsId = x.expenses.Select(y => y.WalletId).ToArray(), CategorySum = x.expenses.Select(y => y.Amount).Sum() })
                                  .ToArray();

            TopExpenses = new ObservableCollection<Transaction>();
            foreach (var group in topExpenses)
            {
                Transaction newTransaction = new Transaction();
                foreach (var item in group.WalletsId)
                {
                    newTransaction = new() { CategoryId = group.CategoryId, WalletId = item, Amount = group.CategorySum };
                }

                TopExpenses.Add(newTransaction);
            }
        }

        private void ToHistoryNavigate(object obj)
        {
            MainWindow.Instance.History.Command.Execute(MainWindow.Instance.History.CommandParameter);
            MainWindow.Instance.History.IsChecked = true;
        }

        #region Wallet
        public string WalletName { get; set; }
        public decimal InitialBalance { get; set; }

        private bool AddWalletToCollection()
        {
            if (!string.IsNullOrWhiteSpace(WalletName))
            {
                Wallet wallet = new() { Name = WalletName, Amount = InitialBalance, CurrencyTypeId = (int)Wallet.CurrencyTypeEnum.RU };

                DataWorker.AddWallet(wallet);
                Wallets.Add(wallet);

                WalletName = string.Empty;

                return true;
            }

            return false;
        }

        private KindnessModalDialog modalWnd;

        private void AddWalletModal(object obj)
        {
            modalWnd = (KindnessModalDialog)obj;
            modalWnd.IsOpen = true;
        }

        private void SaveWallet(object obj)
        {
            if (AddWalletToCollection())
            {
                modalWnd.IsOpen = false;
                modalWnd = null;
            }
        }
        private void CancelModal(object obj)
        {
            modalWnd.IsOpen = false;
            modalWnd = null;
        }
        #endregion


    }
}
