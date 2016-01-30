using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PicsWheel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        IReadOnlyList<StorageFile> files;
        int currentImage;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void LoadImage_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker picker = new FileOpenPicker();
            picker.ViewMode = PickerViewMode.List;
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(".jpg");

            files = await picker.PickMultipleFilesAsync();
            currentImage = 0;

            if (files != null)
            {
                    using (IRandomAccessStream fileStream = await files[currentImage].OpenAsync(Windows.Storage.FileAccessMode.Read))
                    {
                        BitmapImage bitmap = new BitmapImage();
                        bitmap.SetSource(fileStream);
                        LoadImageDynam.Source = bitmap;
                    }
            }
        }

        private async void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage < files.Count && files != null)
            {
                currentImage++;
                using (IRandomAccessStream fileStream = await files[currentImage].OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(fileStream);
                    LoadImageDynam.Source = bitmap;
                }
            }
        }

        private async void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (currentImage > 0 && files != null)
            {
                currentImage--;
                using (IRandomAccessStream fileStream = await files[currentImage].OpenAsync(Windows.Storage.FileAccessMode.Read))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.SetSource(fileStream);
                    LoadImageDynam.Source = bitmap;
                }
            }
        }
    }
}
