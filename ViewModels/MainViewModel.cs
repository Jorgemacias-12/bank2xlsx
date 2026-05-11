using Bank2Pdf.Models;
using Bank2Pdf.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;

namespace Bank2Pdf.ViewModels
{
    partial class MainViewModel : ObservableObject
    {
        private readonly FileParserService _parserService;

        public ObservableCollection<DroppedFile> Files { get; } = new();

        public MainViewModel()
        {
            _parserService = new FileParserService();
        }

        [RelayCommand]
        private void DragOver(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.None;
                e.Handled = true;

                return;
            }

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            bool hasTxt = files.Any(file =>
                Path.GetExtension(file)
                    .Equals(".txt", StringComparison.OrdinalIgnoreCase));

            e.Effects = hasTxt
                ? DragDropEffects.Copy
                : DragDropEffects.None;

            e.Handled = true;
        }

        [RelayCommand]
        private void Drop(DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
                return;

            var files = (string[])e.Data.GetData(DataFormats.FileDrop);

            HandleDrop(files);
        }

        public void HandleDrop(string[] files)
        {
            var txtFiles = files
                .Where(static f => Path.GetExtension(f)
                .Equals(".txt", StringComparison.OrdinalIgnoreCase));

            foreach (var file in txtFiles)
            {
                var info = new FileInfo(file);

                Files.Add(new DroppedFile
                {
                    Name = info.Name,
                    FullPath = info.FullName,
                    Size = info.Length
                });
            }
        }

        [RelayCommand]
        private async Task ParseFileAsync(DroppedFile file)
        {
            if (file is null)
                return;

            file.Status = "Convirtiendo...";

            try
            {
                await _parserService.ParseStreamingAsync(file);

                file.Status = "Completado";
            }
            catch
            {
                file.Status = "Error al convertir contacte con el creador";
            }
        }
    }
}
