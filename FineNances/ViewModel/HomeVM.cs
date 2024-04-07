using FineNances.Core;
using FineNances.Model;
using FineNances.View;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace FineNances.ViewModel
{
    internal class HomeVM : ViewModelBase
    {
        private string _totalExpenses;
        private bool _walletDialogIsOpen;
        private Wallet _selectedWallet;
        private Goal _selectedGoal;
        private Transaction _selectedExpenseTransaction;
        private Transaction _selectedRecentlyTransaction;

        public bool WalletDialogIsOpen
        {
            get { return _walletDialogIsOpen; }
            set { _walletDialogIsOpen = value; OnPropertyChanged(nameof(WalletDialogIsOpen)); }
        }
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

        public ICommand AddWalletCommand => new RelayCommand(AddWalletModal);
        public ICommand SaveWalletCommand => new RelayCommand(SaveWallet);
        public ICommand CancelModalCommand => new RelayCommand(CancelModal);

        public ICommand ToHistory => new RelayCommand(ToHistoryNavigate);

        public HomeVM()
        {
            Wallets = new ObservableCollection<Wallet>()
            {
                /*new Wallet() { Name = "Банковский счет", Amount = 13590m, CurrencyType = Wallet.CurrencyTypeEnum.RU },
                new Wallet() { Name = "Наличные", Amount = 13590m, CurrencyType = Wallet.CurrencyTypeEnum.RU },
                new Wallet() { Name = "Кредитная карта", Amount = 13590m, CurrencyType = Wallet.CurrencyTypeEnum.RU }*/
            };

            Goals = new ObservableCollection<Goal>()
            {
                new Goal() { Name = "Компьютерные девайсы", FinalAmount = 20000m, CurrentAmount = 5000m, CurrencyType = Goal.CurrencyTypeEnum.RU },
                new Goal() { Name = "На монитор", FinalAmount = 25000, CurrentAmount = 12650m, CurrencyType = Goal.CurrencyTypeEnum.RU },
                new Goal() { Name = "На видеокарту", FinalAmount = 80000, CurrentAmount = 64300m, CurrencyType = Goal.CurrencyTypeEnum.RU }
            };

            Transactions = new ObservableCollection<Transaction>()
            {
                /*new Transaction() { Wallet = null, Amount = 12000m, TransactionType = Transaction.TransactionTypeEnum.Expense, 
                    Category = new Model.Category("Покупки"), TransactionDate = DateTime.Now.ToString("M") },
                new Transaction() { Wallet = null, Amount = 10500m, TransactionType = Transaction.TransactionTypeEnum.Expense,
                    Category = new Model.Category("Образование"), TransactionDate = DateTime.Now.ToString("M") },
                new Transaction() { Wallet = null, Amount = 800m, TransactionType = Transaction.TransactionTypeEnum.Expense,
                    Category = new Model.Category("Питание"), TransactionDate = DateTime.Now.ToString("M") },*/
            };

            RecentlyTransactions = new ObservableCollection<Transaction>()
            {
                /*new Transaction() { Wallet = null, TransactionDate = DateTime.Now.ToString("M"), Amount = 5000m,
                                    Category = new Model.Category("Зарплата"), TransactionType = Transaction.TransactionTypeEnum.Income },
                new Transaction() { Wallet = null, TransactionDate = DateTime.Now.ToString("M"), Amount = 300m,
                                    Category = new Model.Category("Питание"), TransactionType = Transaction.TransactionTypeEnum.Expense },
                new Transaction() { Wallet = null, TransactionDate = DateTime.Now.ToString("M"), Amount = 10990m,
                                    Category = new Model.Category("Техника"), TransactionType = Transaction.TransactionTypeEnum.Expense}*/
            };

            decimal sum = 0m;

            foreach (Transaction item in Transactions)
            {
                sum += item.Amount;
            }

            //DisplayTotalExpenses = string.Format(new CultureInfo(Wallets[0].CurrencyTypeCulture), "{0:N0} {1}", sum, new CultureInfo(Wallets[0].CurrencyTypeCulture).NumberFormat.CurrencySymbol);
            DisplayTotalExpenses = string.Format(new CultureInfo("ru-RU"), "{0:N0} {1}", sum, new CultureInfo("ru-RU").NumberFormat.CurrencySymbol);
            foreach (Transaction item in Transactions)
            {
                item.Percantage = (int)(item.Amount / sum * 100.0m);
            }
        }

        private void ToHistoryNavigate(object obj)
        {
            MainWindow.Instance.History.Command.Execute(MainWindow.Instance.History.CommandParameter);
            MainWindow.Instance.History.IsChecked = true;
        }

        #region Wallet
        private string _walletName;

        public string WalletName
        {
            get { return _walletName; }
            set
            {
                _walletName = value;
                OnPropertyChanged(nameof(WalletName));
            }
        }
        private bool AddWalletToCollection()
        {
            if (!string.IsNullOrWhiteSpace(_walletName))
            {
                Wallet wallet = new() { Name = WalletName, Amount = 0m, CurrencyType = Wallet.CurrencyTypeEnum.RU };
                Wallets.Add(wallet);
                WalletName = string.Empty;

                return true;
            }

            return false;
        }

        private void AddWalletModal(object obj)
        {
            WalletDialogIsOpen = true;
        }

        private void SaveWallet(object obj)
        {
            if (AddWalletToCollection())
            {
                WalletDialogIsOpen = false;
            }
        }
        #endregion

        private void CancelModal(object obj)
        {
            WalletDialogIsOpen = false;
        }
    }
}
