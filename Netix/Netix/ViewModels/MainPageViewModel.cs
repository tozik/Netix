using System;
using System.IO;
using System.Threading.Tasks;
using Netix.Services;
using Xamarin.Forms;

namespace Netix.ViewModels
{
    public class MainPageViewModel : BindableObject
    {
        private int i = 0;
        private string _text;
        private ImageSource _imageSource;
        private bool _waiting;

        private string _base64ImageSource = string.Empty;

        public ImageSource SourceImage
        {
            get => _imageSource;
            set
            {
                _imageSource = value;
                OnPropertyChanged(nameof(SourceImage));
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                if (!_text?.Equals(value) ?? true)
                {
                    _text = value;
                    OnPropertyChanged(nameof(Text));
                }
            }
        }

        public string CountOfRequest => $"Count of request {i}";

        public void OnAppearing()
        {
            if (!ScheduledService.Instance.IsInitialised)
            {
                ScheduledService.Instance.ScheduleTimer(GetData);
            }
        }

        private async Task GetData()
        {
            if (!_waiting)
            {
                try
                {
                    var result = await NetixService.Instance.GetModel(LocalStorageService.UniqId);
                    if (!result?.Picture?.Equals(_base64ImageSource) ?? true)
                    {
                        _base64ImageSource = result.Picture;
                        var byteArray = Convert.FromBase64String(result.Picture);
                        var stream = new MemoryStream(byteArray);
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            SourceImage = ImageSource.FromStream(() => stream);
                        });
                    }

                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Text = result.Text;
                    });

                }
                catch (Exception ex)
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        _waiting = true;
                        _ = await App.Current.MainPage.DisplayAlert("Warn", ex.Message, "OK", "Cancel");
                        _waiting = false;
                    });

                }
                finally
                {
                    i++;
                    OnPropertyChanged(nameof(CountOfRequest));
                }
            }
        }
    }
}

