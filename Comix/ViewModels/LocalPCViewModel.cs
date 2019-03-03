using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Windows.Storage;
using Humanizer;

namespace Comix.ViewModels
{
    public class LocalPCModel
    {
        public StorageFolder StorageFolder { get; set; }
        public long Capacity { get; set; }
        public long FreeSpace { get; set; }
        public string CapacityHuman => Capacity.Bytes().ToString("0.0");
        public string FreeSpaceHuman => FreeSpace.Bytes().ToString("0.0");
        public long Current => Capacity - FreeSpace;
        public string CurrentHuman => Current.Bytes().ToString("0.0");
    }

    public class LocalPCViewModel : ViewModelBase
    {
        private List<LocalPCModel> _disks;

        public List<LocalPCModel> Disks
        {
            get => _disks;
            set
            {
                _disks = value;
                OnPropertyChanged();
            }
        }

        public LocalPCViewModel()
        {
            Init();
        }
        private async void Init()
        {
            var list = new[] {"System.FreeSpace", "System.Capacity"};
            var folders = await Task.WhenAll(DriveInfo.GetDrives().Select(it => it.Name)
                .Select(async it => await StorageFolder.GetFolderFromPathAsync(it)));
            var properties = await Task.WhenAll(folders.Select(async it => new
            {
                size = await it.Properties.RetrievePropertiesAsync(list),
                folder = it
            }));
            Disks = properties.Select(it => new LocalPCModel
            {
                StorageFolder = it.folder,
                FreeSpace = Convert.ToInt64(it.size["System.FreeSpace"]),
                Capacity = Convert.ToInt64(it.size["System.Capacity"])
            }).ToList();
        }
    }
}