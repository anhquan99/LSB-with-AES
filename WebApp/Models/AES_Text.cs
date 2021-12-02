using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class AES_Text
    {
        public string message { get; set; }
        public string key { get; set; } 
        public int keySize { get; set; }
        public string isByte { get; set; }
    }
}
