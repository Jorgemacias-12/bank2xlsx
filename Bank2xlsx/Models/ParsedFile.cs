namespace Bank2xlsx.Models
{
    public class ParsedFile
    {
        public DroppedFile? SourceFile { get; set; }

        public List<string> Headers { get; set; } = [];

        public List<List<string>> Rows { get; set; } = [];

        public List<FooterItem> Footer { get; set; } = [];
    }
}
