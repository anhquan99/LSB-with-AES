using Algorithm;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
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
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] PostFile postFile)
        {
            try
            {
                switch (postFile.fileType.ToLower())
                {
                    case "image":
                        switch (postFile.action.ToLower())
                        {
                            case "encrypt":
                                using (var memoryStream = new MemoryStream())
                                {
                                    await postFile.file.CopyToAsync(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = aes.encrypt(postFile.message, postFile.key, postFile.keySize);
                                    Bitmap encryptedImage = LSB.watermarkImage(memoryStream, byteMessage);
                                    ImageFormat streamFomat;
                                    string fileFomat = postFile.file.FileName.Split('.')[postFile.file.FileName.Split('.').Length - 1];
                                    switch (fileFomat)
                                    {
                                        case "bmp":
                                            streamFomat = ImageFormat.Bmp;
                                            break;
                                        case "emf":
                                            streamFomat = ImageFormat.Emf;
                                            break;
                                        case "exif":
                                            streamFomat = ImageFormat.Exif;
                                            break;
                                        case "gif":
                                            streamFomat = ImageFormat.Gif;
                                            break;
                                        case "ico":
                                            streamFomat = ImageFormat.Icon;
                                            break;
                                        case "jpeg":
                                            streamFomat = ImageFormat.Jpeg;
                                            break;
                                        case "dmp":
                                            streamFomat = ImageFormat.MemoryBmp;
                                            break;
                                        case "png":
                                            streamFomat = ImageFormat.Png;
                                            break;
                                        case "tiff":
                                            streamFomat = ImageFormat.Tiff;
                                            break;
                                        case "wmf":
                                            streamFomat = ImageFormat.Wmf;
                                            break;
                                        case "jpg":
                                            streamFomat = ImageFormat.Jpeg;
                                            break;
                                        default:
                                            streamFomat = ImageFormat.Jpeg;
                                            break;
                                    }
                                    encryptedImage.Save(memoryStream, streamFomat);
                                    var content = memoryStream.ToArray();
                                    return File(content, "image/" + fileFomat, "encrypted_" + postFile.file.FileName);
                                }
                            case "decrypt":
                                using (var memoryStream = new MemoryStream())
                                {
                                    await postFile.file.CopyToAsync(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = LSB.extractImage(memoryStream);
                                    byte[] message = Convert.FromBase64String(aes.decrypt(byteMessage, postFile.key, postFile.keySize));
                                    return File(message, "text/plain", "decrypted_" + postFile.file.FileName.Split('.')[0]);
                                }
                        }
                        break;
                    case "audio":
                        switch (postFile.action.ToLower())
                        {
                            case "encrypt":
                                using (var memoryStream = new MemoryStream())
                                {
                                    await postFile.file.CopyToAsync(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = aes.encrypt(postFile.message, postFile.key, postFile.keySize);
                                    byte[] encryptedAudio = LSB.watermarkAudio(memoryStream, byteMessage);
                                    //var encryptedStream = new MemoryStream(encryptedAudio);
                                    return File(encryptedAudio, "audio/wav", "encrypted_" + postFile.file.FileName);

                                }
                            case "decrypt":
                                using (var memoryStream = new MemoryStream())
                                {
                                    await postFile.file.CopyToAsync(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = LSB.extractAudio(memoryStream);
                                    byte[] message = Convert.FromBase64String(aes.decrypt(byteMessage, postFile.key, postFile.keySize));
                                    return File(message, "text/plain", "decrypted_" + postFile.file.FileName.Split('.')[0]);
                                }
                        }
                        break;
                    default:
                        return BadRequest("File fomat not supported");
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
            return BadRequest("Something went wrong");

        }
        //[HttpPost]
        //public IActionResult Post([FromForm] PostFile file)
        //{
        //    MemoryStream stream = new MemoryStream();
        //    file.file.CopyTo(stream);
        //    var content = stream.ToArray();
        //    return File(content, "image/jpg", "result.jpg");
        //}
        [HttpGet]
        public IActionResult Get()
        {
            Bitmap image = new Bitmap("E:\\Work\\PTIT\\ATPM\\FinalATPM\\test\\apple.jpg");
            MemoryStream stream = new MemoryStream();
            image.Save(stream, ImageFormat.Jpeg);
            var content = stream.ToArray();
            return File(content, "image/jpg", "result.jpg");
        }
    }
}
