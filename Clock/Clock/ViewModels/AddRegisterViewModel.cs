namespace Clock.ViewModels
{
    using System.Windows.Input;
    using Common;
    using Helpers;
    using GalaSoft.MvvmLight.Command;
    using Plugin.Media;
    using Plugin.Media.Abstractions;
    using Services;
    using Xamarin.Forms;
    using System.Linq;
    using Clock.Common.Models;
    using Clock.Views;

    public class AddRegisterViewModel : BaseViewModel
    {
        #region ATTRIBUTES
        private MediaFile file;
        private ImageSource imageSource;
        private ApiService apiService;
        private bool isRunning;
        private bool isEnabled;
        #endregion

        #region PROPERTIES

        public string UserCode { get; set; }

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

        public ImageSource ImageSource
        {
            get { return this.imageSource; }
            set { this.SetValue(ref this.imageSource, value); }
        }
        #endregion

        #region CONSTRUCTORS
        public AddRegisterViewModel()
        {
            this.apiService = new ApiService();
            this.IsEnabled = true;
            this.ImageSource = "user";
        }
        #endregion

        #region COMMANDS 

        public ICommand ExitCommand {

            get
            {
                return new RelayCommand(SaveExit);
            }
        }

        public ICommand ChangeImageCommand
        {
            get
            {
                return new RelayCommand(ChangeImage);
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                return new RelayCommand(Save);
            }
        }
        #endregion

        #region METHODS

        /// <summary>
        /// METODO PARA ALMACENAR EL REGISTRO, SE VALIDARÁ QUE EL CAMPO CLAVE SEA CORRECTO
        /// Y OBTENDRÁ LA FOTOGRAFÍA DEL USUARIO MEDIANTE LA CLASE FILESHELPER
        /// </summary>
        private async void Save()
        {

            if (string.IsNullOrEmpty(this.UserCode))
            {
                await Application.Current.MainPage.DisplayAlert(
                    "Error", "Ingrese su clave", "Aceptar");
                return;
            }

            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error, verifique su conexión a internet",
                    connection.Message,
                    "Aceptar");
                return;
            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

        
            var register = new Register
            {
                ImageArray = imageArray,
                UserCode = UserCode
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlRegistersController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, register);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }

          
            this.IsRunning = false;
            this.IsEnabled = true;



            //MainViewModel.GetInstance().Registers = new RegistersViewModel();
            //Application.Current.MainPage = new RegistersPage();
            MainViewModel.GetInstance().Success = new SuccessRegisterViewModel();
            Application.Current.MainPage = new SuccessRegisterPage();
        }


        private async void ChangeImage()
        {
            await CrossMedia.Current.Initialize();

           
            this.file = await CrossMedia.Current.TakePhotoAsync(
                new StoreCameraMediaOptions
                {
                DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front,
                Directory = "Sample",
                Name = "prueba.jpg",
                PhotoSize = PhotoSize.Small,
                });

            if (this.file != null)
            {
                this.ImageSource = ImageSource.FromStream(() =>
                {
                    var stream = this.file.GetStream();
                    return stream;
                });
            }

        }

      

        private async void SaveExit()
        {
            this.IsRunning = true;
            this.IsEnabled = false;

            var connection = await this.apiService.CheckConnection();
            if (!connection.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    connection.Message,
                    "Aceptar");
                return;
            }

            byte[] imageArray = null;
            if (this.file != null)
            {
                imageArray = FilesHelper.ReadFully(this.file.GetStream());
            }

            var register = new Register
            {
                ImageArray = imageArray
            };

            var url = Application.Current.Resources["UrlAPI"].ToString();
            var prefix = Application.Current.Resources["UrlPrefix"].ToString();
            var controller = Application.Current.Resources["UrlRegistersController"].ToString();
            var response = await this.apiService.Post(url, prefix, controller, register);

            if (!response.IsSuccess)
            {
                this.IsRunning = false;
                this.IsEnabled = true;
                await Application.Current.MainPage.DisplayAlert(
                    "Error",
                    response.Message,
                    "Aceptar");
                return;
            }


            this.IsRunning = false;
            this.IsEnabled = true;
           // await Application.Current.MainPage.Navigation.PopAsync();
        }

        #endregion
    }
}
