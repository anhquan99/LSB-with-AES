using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class PostFile
    {
        public string message { set; get; }
        public string key { set; get; }
        public int keySize { set; get; }
        public string action { set; get; }
        public IFormFile file { set; get; }
        public string fileType { set; get; }
    }
}
