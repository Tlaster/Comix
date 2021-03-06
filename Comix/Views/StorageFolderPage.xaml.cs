﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Comix.ViewModels;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Comix.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StorageFolderPage : Page
    {
        public StorageFolderPage()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            this.DataContext = e.Parameter;
        }

        private void GridViewDoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            if (e.OriginalSource is FrameworkElement element && element.DataContext is IStorageItem item)
            {
                switch (item)
                {
                    case StorageFolder folder:
                        Frame.Navigate(typeof(StorageFolderPage), new StorageFolderViewModel(folder));
                        break;
                    case StorageFile file:
                        break;
                }
            }
        }
    }
}
