using System.Linq;
using System.Collections.Generic;

using FineNances.Model.Data;

namespace FineNances.Model
{
    internal static class DataWorker
    {
        public static void ClearAllCategories()
        {
            using (ApplicationContext db = new ApplicationContext())
                db.Database.ExecuteSqlCommand($"delete from {nameof(db.Categories)}");
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

        // Create category
        public static bool CreateCategory(string name)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                bool checkIsExist = db.Categories.Any(element => element.Name == name);
                
                if (!checkIsExist)
                {
                    Category newCategory = new Category();
                    db.Categories.Add(newCategory);
                    db.SaveChanges();
                    result = true;
                }

                return result;
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
                    db.Categories.Remove(category);
                    db.SaveChanges();
                    result = true;
                }

                return result;
            }
        }

        // Edit category
        public static bool EditCategory(Category category, string newName)
        {
            bool result = false;

            using (ApplicationContext db = new ApplicationContext())
            {
                var oldCategory = db.Categories.FirstOrDefault(element => element.Id == category.Id);
                
                if(oldCategory != null)
                {
                    oldCategory.Name = newName;
                    db.SaveChanges();
                    result = true;
                }
            }

            return result;
        }
    }
}
