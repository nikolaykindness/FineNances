using System.Collections.Generic;

using FineNances.Core;

namespace FineNances.Model
{
    internal class Category : BindableBase
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public Category()
        {
            Id = -1;
            Name = string.Empty;
        }

        public Category(string name)
        {
            Name = name;
        }
    }
}
