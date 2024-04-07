using FineNances.Core;
using FineNances.Model;
using FineNances.Model.Data;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Documents;
using System.Windows.Input;

namespace FineNances.ViewModel
{
    internal class CategoryVM : ViewModelBase
    {
        private string _name;
        private string _note;

        private Category _selectedCategory;

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string Note
        {
            get { return _note; }
            set { _note = value; OnPropertyChanged(nameof(Note)); }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }

        public ObservableCollection<Category> Categories { get; set; }

        public CategoryVM()
        {
            Categories = new ObservableCollection<Category>(DataWorker.GetAllCategories());
        }

        public ICommand AddCategoryCommand => new RelayCommand(AddCategory);
        public ICommand SaveCategoryCommand => new RelayCommand(SaveCategory);
        public ICommand CancelCategoryCommand => new RelayCommand(CancelCategory);

        private bool _dialogIsOpen;
        public bool DialogIsOpen
        {
            get { return _dialogIsOpen; }
            set { _dialogIsOpen = value; OnPropertyChanged(nameof(DialogIsOpen)); }
        }

        private void AddCategory(object obj)
        {
            DialogIsOpen = true;
        }

        private void SaveCategory(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                Category newCategory = new Category(Name);

                Categories.Add(newCategory);
                DataWorker.AddCategory(newCategory);

                Name = string.Empty;
                DialogIsOpen = false;
            }
        }

        private void CancelCategory(object obj)
        {
            DialogIsOpen = false;
        }
    }
}
