using Bank2Pdf.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;

namespace Bank2Pdf.Models
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
