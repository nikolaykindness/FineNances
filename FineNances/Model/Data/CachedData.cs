using System.Collections.Generic;

namespace FineNances.Model.Data
{
    internal static class CachedData
    {
        private static List<Category> _allCategories;
        public static List<Category> AllCategories => _allCategories ??= DataWorker.GetAllCategories();
    }
}
