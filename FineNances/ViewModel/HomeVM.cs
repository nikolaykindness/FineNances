using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

using FineNances.Core;
using FineNances.Model;

namespace FineNances.ViewModel
{
    internal class HomeVM : ViewModelBase
    {
        private string _totalExpenses;
        private Wallet _selectedWallet;
        private Goal _selectedGoal;
        private Transaction _selectedExpenseTransaction;
        private Transaction _selectedRecentlyTransaction;

        public string DisplayTotalExpenses
        {
            get { return _totalExpenses; }
            set { _totalExpenses = value; OnPropertyChanged(nameof(DisplayTotalExpenses)); }
        }

        public ObservableCollection<Goal> Goals { get; set; }
        public ObservableCollection<Transaction> RecentlyTransactions { get; set; }
        public ObservableCollection<Wallet> Wallets { get; set; }
        public ObservableCollection<Transaction> Transactions { get; set; }

        public Wallet SelectedWallet
        {
            get { return _selectedWallet; }
            set { _selectedWallet = value; OnPropertyChanged(nameof(SelectedWallet)); }
        }
        public Transaction SelectedExpenseTransaction
        {
            get { return _selectedExpenseTransaction; }
            set { _selectedExpenseTransaction = value; OnPropertyChanged(nameof(SelectedExpenseTransaction)); }
        }

        public Transaction SelectedRecentlyTransaction
        {
            get { return _selectedRecentlyTransaction; }
            set { _selectedRecentlyTransaction = value; OnPropertyChanged(nameof(SelectedRecentlyTransaction)); }
        }

        public Goal SelectedGoal
        {
            get { return _selectedGoal; }
            set { _selectedGoal = value; OnPropertyChanged(nameof(SelectedGoal)); }
        }

        public ICommand AddWallet => new RelayCommand(AddWalletToCollection);

        public HomeVM()
        {
            Wallets = new ObservableCollection<Wallet>()
            {
                new Wallet() { Name = "Банковский счет", Amount = 13590m, CurrencyType = Wallet.CurrencyTypeEnum.RU },
                new Wallet() { Name = "Наличные", Amount = 13590m, CurrencyType = Wallet.CurrencyTypeEnum.RU },
                new Wallet() { Name = "Кредитная карта", Amount = 13590m, CurrencyType = Wallet.CurrencyTypeEnum.RU }
            };

            Goals = new ObservableCollection<Goal>()
            {
                new Goal() { Name = "Компьютерные девайсы", FinalAmount = 20000m, CurrentAmount = 5000m, CurrencyType = Goal.CurrencyTypeEnum.RU },
                new Goal() { Name = "На монитор", FinalAmount = 25000, CurrentAmount = 12650m, CurrencyType = Goal.CurrencyTypeEnum.RU },
                new Goal() { Name = "На видеокарту", FinalAmount = 80000, CurrentAmount = 64300m, CurrencyType = Goal.CurrencyTypeEnum.RU }
            };

            Transactions = new ObservableCollection<Transaction>()
            {
                new Transaction() { Wallet = Wallets[0], Amount = 12000m, TransactionType = Transaction.TransactionTypeEnum.Expense, 
                    Category = new Category("Покупки"), TransactionDate = DateTime.Now.ToString("M") },
                new Transaction() { Wallet = Wallets[0], Amount = 10500m, TransactionType = Transaction.TransactionTypeEnum.Expense,
                    Category = new Category("Образование"), TransactionDate = DateTime.Now.ToString("M") },
                new Transaction() { Wallet = Wallets[0], Amount = 800m, TransactionType = Transaction.TransactionTypeEnum.Expense,
                    Category = new Category("Питание"), TransactionDate = DateTime.Now.ToString("M") },
            };

            RecentlyTransactions = new ObservableCollection<Transaction>()
            {
                new Transaction() { Wallet = Wallets[0], TransactionDate = DateTime.Now.ToString("M"), Amount = 5000m,
                                    Category = new Category("Зарплата"), TransactionType = Transaction.TransactionTypeEnum.Income },
                new Transaction() { Wallet = Wallets[0], TransactionDate = DateTime.Now.ToString("M"), Amount = 300m,
                                    Category = new Category("Питание"), TransactionType = Transaction.TransactionTypeEnum.Expense },
                new Transaction() { Wallet = Wallets[0], TransactionDate = DateTime.Now.ToString("M"), Amount = 10990m,
                                    Category = new Category("Техника"), TransactionType = Transaction.TransactionTypeEnum.Expense}
            };

            decimal sum = 0m;

            foreach (Transaction item in Transactions)
            {
                sum += item.Amount;
            }

            DisplayTotalExpenses = string.Format(new CultureInfo(Wallets[0].CurrencyTypeCulture), "{0:N0} {1}", sum, new CultureInfo(Wallets[0].CurrencyTypeCulture).NumberFormat.CurrencySymbol);

            foreach (Transaction item in Transactions)
            {
                item.Percantage = (int)(item.Amount / sum * 100.0m);
            }
        }

        private void AddWalletToCollection(object obj)
        {
            Wallet wallet = new() { Name = "Банковский счет", Amount = 13590m, CurrencyType = Wallet.CurrencyTypeEnum.RU };
            Wallets.Add(wallet);
        }
    }
}
