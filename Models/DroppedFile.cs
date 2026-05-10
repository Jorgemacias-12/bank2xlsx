using Bank2Pdf.Helpers;
using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bank2Pdf.Models
{
    partial class DroppedFile : ObservableObject
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
