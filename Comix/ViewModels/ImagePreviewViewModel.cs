using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Search;

namespace Comix.ViewModels
{
    public class ImagePreviewViewModel : ViewModelBase
    {
        private int _index;

        public int Index
        {
            get => _index;
            set
            {
                _index = value;
                OnPropertyChanged();
            }
        }

        public List<StorageFile> Files { get; set; }

        public ImagePreviewViewModel(StorageFolder folder, int index = 0)
        {
            this.Index = index;
            this.InitFiles(folder);
        }

        private void InitFiles(StorageFolder folder)
        {
            
        }
    }
}
