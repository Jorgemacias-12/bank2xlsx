using System;
using System.Collections.Generic;
using System.Text;

namespace Bank2Pdf.Models
{
    public class FooterItem
    {
        public string Key { get; set; } = "";

        public string Value { get; set; } = "";

        public bool IsCurrency { get; set; }
    }
}
