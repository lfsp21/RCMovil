namespace Clock.ViewModels
{
    using System.Windows.Input;
    using Clock.Helpers;
    using Clock.Services;
    using Clock.Views;
    using GalaSoft.MvvmLight.Command;

    using Xamarin.Forms;

    public class LoginViewModel : BaseViewModel

    {
        #region ATTRIBUTES
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region PROPERTIES
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRemembered { get; set; }

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
        public LoginViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.IsRemembered = true;

        }
        #endregion

        #region COMMANDS
        public ICommand LoginCommand
        {
            get
            {
                return new RelayCommand(Login);
            }
        }
        /// <summary>
        /// METODO QUE VALIDARÁ LA CONEXION A INTERNET Y QUE LAS CLAVES SEAN CORRECTAS
        /// DESPUES, SE CONECTARA CON LA API PARA GENERAR EL TOKEN DE ACCESO DEL TIPO DE USUARIO
        /// </summary>
        private async void Login()
        {
            if (string.IsNullOrEmpty(this.Email))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Ingresa un correo válido", "Cancelar");
                return;
            }

            if (string.IsNullOrEmpty(this.Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Clave incorrecta", "Cancelar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;
            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Verifique su conexión", "Aceptar");
                return;
            }

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var token = await this.apiService.GetToken(url, this.Email, this.Password);

            if(token == null || string.IsNullOrEmpty(token.AccessToken))
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert("Error", "Acceso denegado", "Aceptar");
                return;
            }

            Settings.TokenType = token.TokenType;
            Settings.AccessToken = token.AccessToken;
            Settings.IsRemembered = this.IsRemembered;


            MainViewModel.GetInstance().AddRegister = new AddRegisterViewModel();
            Application.Current.MainPage = new AddRegisterPage();

            this.IsRunning = false;
            this.IsEnabled = true;
        }
        #endregion

        //Futuro: Método que registre el acceso con huella digital
        


    }
}
