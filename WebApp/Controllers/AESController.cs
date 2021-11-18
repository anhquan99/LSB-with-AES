using Algorithm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using WebApp.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AESController : ControllerBase
    {

         //POST api/<AESController>
        //[Microsoft.AspNetCore.Mvc.HttpPost]
        //public async IHttpActionResult Post([FromForm] PostFile postFile)
        //{
        //    switch (postFile.fileType.ToLower())
        //    {
        //        case "image":
        //            switch (postFile.action.ToLower())
        //            {
        //                case "encrypt":
        //                    using(var memoryStream = new MemoryStream())
        //                    {
        //                        await postFile.file.CopyToAsync(memoryStream);
        //                        AES aes = new AES();
        //                        byte[] byteMessage = aes.encrypt(postFile.message, postFile.key, postFile.keySize);
        //                        LSB.watermarkImage(memoryStream, byteMessage);
        //                    }
        //                    break;
        //                case "decrypt":
        //                    using (var memoryStream = new MemoryStream())
        //                    {
        //                        await postFile.file.CopyToAsync(memoryStream);
        //                        AES aes = new AES();
        //                        byte[] byteMessage = LSB.extractImage(memoryStream);
        //                        string message = aes.decrypt(byteMessage, postFile.key, postFile.keySize);
        //                    }
        //                    break;
        //            }
        //            break;
        //        case "audio":
        //            switch (postFile.action.ToLower())
        //            {
        //                case "encrypt":
        //                    break;
        //                case "decrypt":
        //                    break;
        //            }
        //            break;
        //        default:
        //            return BadRequest("File fomat not supported");
        //    }
        //}
        [HttpPost]
        public HttpResponseMessage Post([FromForm] PostFile file)
        {
            MemoryStream stream = new MemoryStream();
            file.file.CopyTo(stream);
            var result = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(stream.ToArray())
            };
            result.Content.Headers.ContentDisposition =
                new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment")
                {
                    FileName = "CertificationCard.pdf"
                };
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/octet-stream");

            return result;
            //return File(stream, "image/jpg", "result.jpg");
        }
    }
}
