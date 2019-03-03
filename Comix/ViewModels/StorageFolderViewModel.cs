using System;
using System.Collections.Generic;
using System.Linq;
using Windows.Storage;

namespace Comix.ViewModels
{
    public class StorageFolderViewModel : ViewModelBase
    {
        private IStorageItem _selectedItem;
        private List<IStorageItem> _storageItems;

        public StorageFolderViewModel(IStorageFolder folder)
        {
            CurrentFolder = folder;
            Init();
        }

        public IStorageItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public List<IStorageItem> StorageItems
        {
            get => _storageItems;
            set
            {
                _storageItems = value;
                OnPropertyChanged();
            }
        }

        public IStorageFolder CurrentFolder { get; }

        private async void Init()
        {
            IsLoading = true;
            StorageItems = (await CurrentFolder.GetItemsAsync()).ToList();
            IsLoading = false;
        }
    }
}