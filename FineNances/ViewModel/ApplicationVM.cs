using FineNances.Core;
using FineNances.Model.Data;

namespace FineNances.ViewModel
{
    internal class ApplicationVM : ViewModelBase
    {
        private NavigationVM _navigationVM;
        public NavigationVM NavigationVM 
        {
            get { return _navigationVM; }
            set
            {
                _navigationVM = value;
                OnPropertyChanged(nameof(NavigationVM));
            }
        }

        ApplicationContext _context;

        public ApplicationVM()
        {
            NavigationVM = new NavigationVM();
            _context = new ApplicationContext();
        }
    }
}
