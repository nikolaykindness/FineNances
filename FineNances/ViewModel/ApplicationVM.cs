using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Input;

using FineNances.Core;
using FineNances.Model;
using FineNances.Model.Data;

namespace FineNances.ViewModel
{
    internal class ApplicationVM : BindableBase
    {
        private bool _dialogIsOpen;
        private bool _isTransfer;

        private ObservableCollection<Category> _categories;
        private ObservableCollection<Wallet> _wallets;

        public bool DialogIsOpen 
        {
            get { return _dialogIsOpen; }
            set { _dialogIsOpen = value; OnPropertyChanged(nameof(DialogIsOpen)); }
        }

        public bool IsTransfer
        {
            get { return _isTransfer; }
            set { _isTransfer = value; OnPropertyChanged(nameof(IsTransfer)); }
        }

        public NavigationVM NavigationVM { get; }

        public Category SelectedCategory { get; set; }
        public Wallet SelectedWallet { get; set; }
        public ObservableCollection<Category> Categories 
        {
            get { return _categories; }
            set { _categories = value; OnPropertyChanged(nameof(Categories)); }
        }
        public ObservableCollection<Wallet> Wallets 
        {
            get { return _wallets; }
            set { _wallets = value; OnPropertyChanged(nameof(Wallets)); }
        }

        public string SelectedEnumType { get; set; }
        public Dictionary<string, Transaction.TransactionTypeEnum> ConverterEnumType { get; set; }
        public Array TransactionTypeEnum => ConverterEnumType.Keys.ToArray();

        ApplicationContext _context;

        public ApplicationVM()
        {
            NavigationVM = new NavigationVM();
            _context = new ApplicationContext();

            ConverterEnumType = new Dictionary<string, Transaction.TransactionTypeEnum>()
            {
                { "Доход", Transaction.TransactionTypeEnum.Income },
                { "Расход" , Transaction.TransactionTypeEnum.Expense }
            };

            Categories = new ObservableCollection<Category>(DataWorker.GetAllCategories());
            Wallets = new ObservableCollection<Wallet>(DataWorker.GetAllWallets());

            DataWorker.OnWalletsChanged += (e) =>
            {
                Wallets = new ObservableCollection<Wallet>(DataWorker.GetAllWallets());
            };

            DataWorker.OnCategoriesChanged += (e) =>
            {
                Categories = new ObservableCollection<Category>(DataWorker.GetAllCategories());
            };
        }

        public ICommand AddTranscationCommand => new RelayCommand(AddTransaction);
        public ICommand SaveTransactionCommand => new RelayCommand(SaveTransaction);
        public ICommand CancelTransactionCommand => new RelayCommand(CancelTransaction);

        private void AddTransaction(object parameter) => DialogIsOpen = true;

        public decimal TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.Now;

        private void SaveTransaction(object parameter)
        {
            ConverterEnumType.TryGetValue(SelectedEnumType, out Transaction.TransactionTypeEnum selected);

            SelectedWallet.Amount += selected == Transaction.TransactionTypeEnum.Expense ? -TransactionAmount : TransactionAmount;

            if (DataWorker.AddTransaction(selected, TransactionDate, TransactionAmount, SelectedWallet, SelectedCategory))
            {
                if(DataWorker.EditWallet(SelectedWallet))
                {
                    DialogIsOpen = false;
                }
            }
        }

        private void CancelTransaction(object parameter)
        {
            DialogIsOpen = false;
        }
    }
}
