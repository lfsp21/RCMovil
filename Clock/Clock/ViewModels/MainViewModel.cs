using Clock.Views;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace Clock.ViewModels
{
    /// <summary>
    /// NAVIGATION PAGE DE TODAS LAS PANTALLAS DE LA APLICACIÓN
    /// </summary>
    public class MainViewModel
    {
        public RegistersViewModel Registers { get; set; }
        public AddRegisterViewModel AddRegister { get; set; }
        public LoginViewModel Login { get; set; }
        public SuccessRegisterViewModel Success { get; set; }
        public MainViewModel()
        {
            instance = this;
        
        }

        #region SINGLETON
        private static MainViewModel instance;
        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }

            return instance;
        }
        #endregion

        public ICommand AddRegisterCommand
        {
            get
            {
                return new RelayCommand(GoToAddRegister);
            }
        }

        private async void GoToAddRegister()
        {
            //this.AddRegister = new AddRegisterViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new AddRegisterPage());
        }

        public ICommand RegistersCommand
        {
            get
            {
                return new RelayCommand(GoToRegisters);
            }
        }

        private async void GoToRegisters()
        {
            this.Registers = new RegistersViewModel();
            await Application.Current.MainPage.Navigation.PushAsync(new RegistersPage());
        }
    }
}
