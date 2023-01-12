using System;
using Netix.Services;
using Netix.ViewModels;
using Plugin.DeviceInfo;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Netix
{
    public partial class App : Application
    {
        public bool IsAppForeground { get; private set; }

        public App()
        {
            InitializeComponent();

            if (string.IsNullOrEmpty(LocalStorageService.UniqId))
            {
                LocalStorageService.UniqId = CrossDeviceInfo.IsSupported ? CrossDeviceInfo.Current.Id : Guid.NewGuid().ToString();
            }

            MainPage = new NavigationPage(new MainPage
            {
                BindingContext = new MainPageViewModel()
            });
        }

        protected override void OnStart()
        {
            IsAppForeground = true;
        }

        protected override void OnSleep()
        {
            IsAppForeground = false;
            ScheduledService.Instance.Pause();
        }

        protected override void OnResume()
        {
            IsAppForeground = true;
            ScheduledService.Instance.Continue();
        }
    }
}

