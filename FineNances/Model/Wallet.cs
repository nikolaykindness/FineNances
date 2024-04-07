using FineNances.Core;
using FineNances.Model.Interfaces;
using System.Collections.Generic;
using System.Globalization;

namespace FineNances.Model
{
    internal class Wallet : ViewModelBase, IWallet
    {
        public enum CurrencyTypeEnum
        {
            RU,
            US,
            UK,
            Unknown
        }

        private int _id;
        private string _name;
        private decimal _amount;
        private CurrencyTypeEnum _currencyType;
        private Dictionary<CurrencyTypeEnum, string> _currencyTypes = new Dictionary<CurrencyTypeEnum, string>()
        {
            { CurrencyTypeEnum.RU, "ru-RU" },
            { CurrencyTypeEnum.US, "en-US" },
            { CurrencyTypeEnum.UK, "en-GB" }
        };

        public int Id
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged("WalletID"); }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged("WalletName"); }
        }

        public decimal Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged("WalletAmount"); }
        }

        public CurrencyTypeEnum CurrencyType
        {
            get { return _currencyType; }
            set { _currencyType = value; OnPropertyChanged(nameof(CurrencyType)); }
        }

        public string CurrencyTypeCulture
        {
            get { return _currencyTypes[CurrencyType]; }
        }

        public string DisplayAmount
        {
            get { return string.Format("{0:N} {1}", Amount, new CultureInfo(_currencyTypes[CurrencyType]).NumberFormat.CurrencySymbol); }
        }

        public Wallet()
        {
            Name = string.Empty;
            Amount = 0;
            CurrencyType = CurrencyTypeEnum.Unknown;
        }

        public Wallet(string name, CurrencyTypeEnum currencyType, decimal amount = 0)
        {
            Name = name;
            Amount = amount;
            CurrencyType = currencyType;
        }
    }
}
