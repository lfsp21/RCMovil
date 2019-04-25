namespace Clock.ViewModels
{

    using System.Windows.Input;
    using Clock.Views;
    using GalaSoft.MvvmLight.Command;
using Xamarin.Forms;

public class SuccessRegisterViewModel : BaseViewModel
    {
     
        #region ATTRIBUTES
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region PROPERTIES

        public bool IsRunning
        {
            get { return this.isRunning; }
            set { this.SetValue(ref this.isRunning, value); }
        }

        public bool IsEnabled
        {
            get { return this.isEnabled; }
            set { this.SetValue(ref this.isEnabled, value); }
        }

        #endregion

        #region CONSTRUCTORS
        public SuccessRegisterViewModel()
        {
            this.IsEnabled = true;
        }
        #endregion

        #region COMMANDS
        public ICommand NewRegisterCommand
        {
            get
            {
                return new RelayCommand(NewRegister);
            }
        }
        #endregion

        private void NewRegister()
        {
            MainViewModel.GetInstance().AddRegister = new AddRegisterViewModel();
            Application.Current.MainPage = new AddRegisterPage();

            this.IsRunning = false;
            this.IsEnabled = true;
        }
    }
}