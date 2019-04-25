namespace Clock.ViewModels
{
    using System.Collections.ObjectModel;
    using System.Windows.Input;
    using Clock.Common.Models;
    using Clock.Services;
    using Clock.Views;
    using GalaSoft.MvvmLight.Command;
    using Xamarin.Forms;

    public class RegistersViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        private ApiService apiService;
        private bool isRefreshing;
        public AddRegisterViewModel AddRegister { get; set; }
        #endregion

        #region PROPERTIES
        private ObservableCollection<Register> registers;

        public ObservableCollection<Register> Registers
        {
            get { return this.registers; }
            set { this.SetValue(ref this.registers, value); }

        }
        #endregion


        public bool IsRefreshing
        {
            get { return this.isRefreshing; }
            set { this.SetValue(ref this.isRefreshing, value); }
        }

        #region CONSTRUCTORS 
        public RegistersViewModel()
        {
            instance = this;
            this.apiService = new ApiService();
        }
        #endregion

        #region SINGLETON
        private static RegistersViewModel instance;
        public static RegistersViewModel GetInstance()
        {
            if(instance == null)
            {
                return new RegistersViewModel();
            }

            return instance;
        }
        #endregion

        #region METHODS

        #endregion

        #region COMMANDS
        public ICommand AddRegisterCommand
        {
            get
            {
                return new RelayCommand(GoToAddRegister);
            }
        }

        private async void GoToAddRegister()
        {
            this.AddRegister = new AddRegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddRegisterPage());
        }
        #endregion

    }
}
