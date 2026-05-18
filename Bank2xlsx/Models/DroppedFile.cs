using Bank2xlsx.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bank2xlsx.Models
{
    public partial class DroppedFile : ObservableObject
    {
        [ObservableProperty]
        private string name = "";

        [ObservableProperty]
        private string fullPath = "";

        [ObservableProperty]
        private long size;

        [ObservableProperty]
        private string status = "Pendiente";

        public string FormattedSize =>
            FileSizeFormatter.Format(Size);
    }
}
