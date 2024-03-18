using FineNances.Core;
using System.Windows;
using System.Windows.Input;

namespace FineNances.ViewModel
{
    internal class NavigationVM : ViewModelBase
    {
        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(nameof(CurrentView)); }
        }

        public ICommand HomeCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand CategoryCommand { get; set; }

        private void Home(object view) => CurrentView = new HomeVM();
        private void History(object view) => CurrentView = new HistoryVM();
        private void Category(object view) => CurrentView = new CategoryVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            HistoryCommand = new RelayCommand(History);
            CategoryCommand = new RelayCommand(Category);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}
