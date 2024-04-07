using FineNances.Core;
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
        public ICommand PiggyBankCommand { get; set; }
        public ICommand HistoryCommand { get; set; }
        public ICommand CategoryCommand { get; set; }
        public ICommand AboutProgrammCommand { get; set; }

        private void Home(object view) => CurrentView = new HomeVM();
        private void PiggyBank(object view) => CurrentView = new PiggyBankVM();
        private void History(object view) => CurrentView = new HistoryVM();
        private void Category(object view) => CurrentView = new CategoryVM();
        private void AboutProgram(object view) => CurrentView = new AboutProgramVM();

        public NavigationVM()
        {
            HomeCommand = new RelayCommand(Home);
            PiggyBankCommand = new RelayCommand(PiggyBank);
            HistoryCommand = new RelayCommand(History);
            CategoryCommand = new RelayCommand(Category);
            AboutProgrammCommand = new RelayCommand(AboutProgram);

            // Startup Page
            CurrentView = new HomeVM();
        }
    }
}
