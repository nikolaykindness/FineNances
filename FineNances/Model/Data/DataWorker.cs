using System;
using System.Linq;
using System.ComponentModel;
using System.Collections.Generic;

using FineNances.Model.Data;

namespace FineNances.Model
{
    delegate void DbSetChangedHandler(ListChangedType changedType);

    internal static class DataWorker
    { 
        public static event DbSetChangedHandler OnWalletsChanged;
        public static event DbSetChangedHandler OnCategoriesChanged;
        public static event DbSetChangedHandler OnTransactionsChanged;

        #region Wallets
        // Remove All Wallets
        public static void ClearAllWallets()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.ExecuteSqlCommand($"delete from {nameof(db.Wallets)}");
                db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('{nameof(db.Wallets)}', RESEED, 0)");
            }
        }

        // Get all wallets
        public static List<Wallet> GetAllWallets()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var wallets = db.Wallets.ToList();
                return wallets;
            }
        }

        // Get wallet by id
        public static Wallet GetWalletById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var wallet = db.Wallets.FirstOrDefault(element => element.Id == id);
                return wallet;
            }
        }

        // Add wallet
        public static bool AddWallet(Wallet wallet)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Wallets.Any(element => element.Name == wallet.Name);
                var some = db.Categories.Where(element => element.Name == "1241").ToArray();

                if (!checkIsExist)
                {
                    db.Wallets.Add(wallet);
                    db.SaveChanges();
                    OnWalletsChanged?.Invoke(ListChangedType.ItemAdded);
                    result = true;
                }

                return result;
            }
        }

        // Remove wallet
        public static bool RemoveWallet(Wallet wallet)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Wallets.Any(element => element.Id == wallet.Id);

                if (checkIsExist)
                {
                    db.Wallets.Attach(wallet);
                    db.Wallets.Remove(wallet);
                    db.SaveChanges();
                    OnWalletsChanged?.Invoke(ListChangedType.ItemDeleted);
                    result = true;
                }

                return result;
            }
        }

        // Edit category
        public static bool EditWallet(Wallet newWallet)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                var oldWallet = db.Wallets.FirstOrDefault(el => el.Id == newWallet.Id);

                if (oldWallet != null)
                {
                    oldWallet.Name = newWallet.Name;
                    oldWallet.Amount = newWallet.Amount;
                    db.SaveChanges();
                    OnWalletsChanged?.Invoke(ListChangedType.ItemChanged);
                    result = true;
                }
            }

            return result;
        }
        #endregion
        #region Categories
        // Remove all Categories
        public static void ClearAllCategories()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Database.ExecuteSqlCommand($"delete from {nameof(db.Categories)}");
                db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('{nameof(db.Categories)}', RESEED, 0)");
            }
        }

        // Get all categories
        public static List<Category> GetAllCategories()
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var categories = db.Categories.ToList();
                return categories;
            }
        }

        // Get category by id
        public static Category GetCategoryById(int id)
        {
            using (ApplicationContext db = new ApplicationContext())
            {
                var category = db.Categories.FirstOrDefault(element => element.Id == id);
                return category;
            }
        }

        // Add category
        public static bool AddCategory(Category category)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Categories.Any(element => element.Name == category.Name);

                if (!checkIsExist)
                {
                    db.Categories.Add(category);
                    db.SaveChanges();
                    OnCategoriesChanged?.Invoke(ListChangedType.ItemAdded);
                    result = true;
                }

                return result;
            }
        }

        // Remove category
        public static bool RemoveCategory(Category category)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Categories.Any(element => element.Id == category.Id);

                if (checkIsExist)
                {
                    db.Categories.Attach(category);
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    OnCategoriesChanged?.Invoke(ListChangedType.ItemDeleted);
                    result = true;
                }

                return result;
            }
        }

        // Edit category
        public static bool EditCategory(Category newCategory)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                var oldCategory = db.Categories.FirstOrDefault(element => element.Id == newCategory.Id);
                
                if(oldCategory != null)
                {
                    oldCategory.Name = newCategory.Name;
                    db.SaveChanges();
                    OnCategoriesChanged?.Invoke(ListChangedType.ItemChanged);
                    result = true;
                }
            }

            return result;
        }
        #endregion
        #region Transactions
        // Remove all Transactions
        public static void ClearAllTransactions()
        {
            using (ApplicationContext db = new())
            {
                db.Database.ExecuteSqlCommand($"delete from {nameof(db.Transactions)}");
                db.Database.ExecuteSqlCommand($"DBCC CHECKIDENT('{nameof(db.Transactions)}', RESEED, 0)");
            }
        }

        // Get all transactions
        public static List<Transaction> GetAllTransactions()
        {
            using (ApplicationContext db = new())
            {
                var transactions = db.Transactions.ToList();
                return transactions;
            }
        }

        // Add transaction
        public static bool AddTransaction(Transaction.TransactionTypeEnum type, DateTime date, decimal amount, Wallet wallet, Category category)
        {
            bool result = false;

            using (ApplicationContext db = new())
            {
                Transaction newTransaction = new()
                {
                    TransactionTypeId = (int)type,
                    TransactionDate = date,
                    Amount = amount,
                    WalletId = wallet.Id,
                    CategoryId = category.Id
                };

                db.Transactions.Add(newTransaction);
                db.SaveChanges();
                OnTransactionsChanged?.Invoke(ListChangedType.ItemAdded);
                result = true;

                return result;
            }
        }

        // Remove transaction
        public static bool RemoveTransaction(Transaction transaction)
        {
            bool result = false;

            using (ApplicationContext db = new())
            {
                db.Transactions.Attach(transaction);
                db.Transactions.Remove(transaction);
                db.SaveChanges();
                OnTransactionsChanged?.Invoke(ListChangedType.ItemDeleted);
                result = true;

                return result;
            }
        }
        #endregion
    }
}
