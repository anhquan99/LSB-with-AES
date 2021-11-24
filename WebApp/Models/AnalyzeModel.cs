using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class AnalyzeModel
    {
        public IFormFile original { get; set; }
        public IFormFile watermarked { get; set; }
        public string fileType { get; set; }
    }
}
