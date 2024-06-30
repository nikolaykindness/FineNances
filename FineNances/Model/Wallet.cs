using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

using FineNances.Core;
using FineNances.Model.Interfaces;

namespace FineNances.Model
{
    internal sealed class Wallet : BindableBase, IWallet
    {
        public enum CurrencyTypeEnum
        {
            RU,
            US,
            UK,
            Unknown
        }

        private Dictionary<CurrencyTypeEnum, string> _currencyTypes = new Dictionary<CurrencyTypeEnum, string>()
        {
            { CurrencyTypeEnum.RU, "ru-RU" },
            { CurrencyTypeEnum.US, "en-US" },
            { CurrencyTypeEnum.UK, "en-GB" }
        };

        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyTypeId { get; set; }
        public ICollection<Transaction> Transactions { get; set; }

        [NotMapped]
        public CurrencyTypeEnum CurrencyType => (CurrencyTypeEnum)CurrencyTypeId;
        [NotMapped]
        public string CurrencyTypeCulture => _currencyTypes[CurrencyType];
        [NotMapped]
        public string DisplayAmount => string.Format("{0:N} {1}", Amount, new CultureInfo(_currencyTypes[CurrencyType]).NumberFormat.CurrencySymbol);
    }
}
