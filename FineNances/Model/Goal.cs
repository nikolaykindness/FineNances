using FineNances.Core;
using FineNances.Model.Interfaces;
using System.Collections.Generic;
using System.Globalization;

namespace FineNances.Model
{
    internal class Goal : ViewModelBase, IWallet
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
        private decimal _currentAmount;
        private decimal _finalAmount;

        private CurrencyTypeEnum _currencyType;
        private Dictionary<CurrencyTypeEnum, string> _currencyTypes = new Dictionary<CurrencyTypeEnum, string>()
        {
            { CurrencyTypeEnum.RU, "ru-RU" },
            { CurrencyTypeEnum.US, "en-US" },
            { CurrencyTypeEnum.UK, "en-GB" }
        };


        public int ID 
        { 
            get { return _id; } 
            set { _id = value; OnPropertyChanged(nameof(ID)); }
        }
        public string Name 
        { 
            get { return _name; } 
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public decimal CurrentAmount
        {
            get { return _currentAmount; }
            set { _currentAmount = value; OnPropertyChanged(nameof(CurrentAmount)); }
        }

        public int PercentageProgress
        {
            get { return (int)(_currentAmount / _finalAmount * 100m); }
        }

        public string DisplayCurrentAmount
        {
            get { return string.Format("{0:N} из {1:N} {2}", _currentAmount, _finalAmount, new CultureInfo(_currencyTypes[_currencyType]).NumberFormat.CurrencySymbol); }
        }

        public CurrencyTypeEnum CurrencyType
        {
            get { return _currencyType; }
            set { _currencyType = value; OnPropertyChanged(nameof(CurrencyType)); }
        }

        public decimal FinalAmount
        {
            get { return _finalAmount; }
            set { _finalAmount = value; OnPropertyChanged(nameof(FinalAmount)); }
        }

        public Goal()
        {
            ID = -1;
            CurrentAmount = 0;
            FinalAmount = 0;
            Name = string.Empty;
            _currencyType = CurrencyTypeEnum.Unknown;
        }

        public Goal(string name, decimal finalAmount, decimal currentAmount, CurrencyTypeEnum currencyType)
        {
            Name = name;
            FinalAmount = finalAmount;
            CurrentAmount = currentAmount;
            _currencyType = currencyType;
        }
    }
}
