using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.AccessCache;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Comix.ViewModels;
using Humanizer;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Comix.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StorageActivity
    {
        public StorageActivity()
        {
            this.InitializeComponent();
        }

        protected override async void OnCreate(object parameter)
        {
            base.OnCreate(parameter);
            ItemList_Frame.Navigate(typeof(LocalPCPage));
        }

        private void GobackClick(object sender, RoutedEventArgs e)
        {
            ItemList_Frame.GoBack();
        }

        private async void GoupClick(object sender, RoutedEventArgs e)
        {
            if (ItemList_Frame.Content is FrameworkElement element && element.DataContext is StorageFolderViewModel viewModel && viewModel.CurrentFolder is StorageFolder folder)
            {
                var parent = await folder.GetParentAsync();
                if (parent != null)
                {
                    ItemList_Frame.Navigate(typeof(StorageFolderPage), new StorageFolderViewModel(parent));
                }
            }
        }
    }
}
