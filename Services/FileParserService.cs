using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bank2Pdf.Services
{
    class FileParser
    {
        public async Task ParseAsync(string path)
        {
            var lines = await File.ReadAllLinesAsync(path);

            // TODO: implement parsing logic later
        }
    }
}
