using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

using FineNances.Core;
using FineNances.Model;

namespace FineNances.ViewModel
{
    internal sealed class CategoryVM : BindableBase
    {
        private string _name;
        private string _note;
        private string _searchText;

        private Category _selectedCategory;

        private delegate void SearchTextHandler();
        private event SearchTextHandler SearchTextChanged;

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

        public string SearchText
        {
            get { return _searchText; }
            set 
            {
                _searchText = value;
                SearchTextChanged?.Invoke();
                OnPropertyChanged(nameof(SearchText));
            }
        }

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value; OnPropertyChanged(nameof(SelectedCategory)); }
        }

        public ObservableCollection<Category> Categories { get; set; }

        private ICollection<Category> _tempCategories;

        public CategoryVM()
        {
            UpdateCategories();

            SearchTextChanged += OnSearchTextChanged;
        }

        private void UpdateCategories()
        {
            _tempCategories = DataWorker.GetAllCategories();
            Categories = new ObservableCollection<Category>(_tempCategories);
        }

        private void OnSearchTextChanged()
        {
            Categories.Clear();

            if (!string.IsNullOrEmpty(SearchText))
            {
                var filteredItem = _tempCategories.Where(element => element.Name.ToUpper().StartsWith(SearchText.ToUpper()));

                foreach (var item in filteredItem)
                {
                    Categories.Add(item);
                }
            }
            else
            {
                foreach (var item in _tempCategories)
                {
                    Categories.Add(item);
                }
            }
        }

        public ICommand AddCategoryCommand => new RelayCommand(AddCategory);
        public ICommand RemoveCategoryCommand => new RelayCommand(RemoveCategory);
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

        private void RemoveCategory(object obj)
        {
            if(DataWorker.RemoveCategory(SelectedCategory))
            { 
                _tempCategories.Remove(SelectedCategory);
                Categories.Remove(SelectedCategory);
            }
        }

        private void SaveCategory(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                Category newCategory = new(Name);

                if (DataWorker.AddCategory(newCategory))
                {
                    _tempCategories = DataWorker.GetAllCategories();
                    Categories.Add(_tempCategories.Last());
                }

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
