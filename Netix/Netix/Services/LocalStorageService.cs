using System;
using Xamarin.Essentials;

namespace Netix.Services
{
    public class LocalStorageService
    {
        public static string UniqId
        {
            get => Preferences.Get(nameof(UniqId), string.Empty);
            set => Preferences.Set(nameof(UniqId), value);
        }
    }
}