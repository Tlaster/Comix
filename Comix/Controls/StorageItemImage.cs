﻿using System;
using Windows.Storage;
using Windows.Storage.FileProperties;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

// The Templated Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234235

namespace Comix.Controls
{
    public sealed class StorageItemImage : Control
    {
        public static readonly DependencyProperty StorageItemProperty = DependencyProperty.Register(
            nameof(StorageItem), typeof(IStorageItem), typeof(StorageItemImage),
            new PropertyMetadata(default(IStorageItem), OnStorageItemChanged));

        public static readonly DependencyProperty ThumbnailModeProperty = DependencyProperty.Register(
            nameof(ThumbnailMode), typeof(ThumbnailMode), typeof(StorageItemImage),
            new PropertyMetadata(ThumbnailMode.DocumentsView));

        public StorageItemImage()
        {
            DefaultStyleKey = typeof(StorageItemImage);
        }

        public ThumbnailMode ThumbnailMode
        {
            get => (ThumbnailMode) GetValue(ThumbnailModeProperty);
            set => SetValue(ThumbnailModeProperty, value);
        }

        public IStorageItem StorageItem
        {
            get => (IStorageItem) GetValue(StorageItemProperty);
            set => SetValue(StorageItemProperty, value);
        }

        private Image ImageView { get; set; }

        private static void OnStorageItemChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as StorageItemImage).OnStorageItemChanged(e.NewValue as IStorageItem);
        }

        private async void OnStorageItemChanged(IStorageItem storageItem)
        {
            if (storageItem == null)
            {
                ImageView.Source = null;
                return;
            }

            if (storageItem is IStorageItemProperties properties)
            {
                var thumb = await properties.GetThumbnailAsync(ThumbnailMode);
                var bitmap = new BitmapImage();
                await bitmap.SetSourceAsync(thumb);
                ImageView.Source = bitmap;
            }
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ImageView = GetTemplateChild("StorageItemImage_Image") as Image;
        }
    }
}