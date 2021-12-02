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
using System.Text;
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
        public IActionResult Post([FromForm] PostFile postFile)
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
                                    postFile.file.CopyTo(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = aes.encrypt(postFile.message, postFile.key, postFile.keySize);
                                    Bitmap encryptedImage = LSB.watermarkImage(memoryStream, byteMessage);
                                    MemoryStream saveFile = new MemoryStream();
                                    encryptedImage.Save(saveFile, ImageFormat.Png);
                                    var content = saveFile.ToArray();
                                    return File(content, "image/png", "encrypted_" + postFile.file.FileName.Split('.')[0] + ".png");
                                }
                            case "decrypt":
                                using (var memoryStream = new MemoryStream())
                                {
                                    postFile.file.CopyTo(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = LSB.extractImage(memoryStream);
                                    byte[] message = Encoding.UTF8.GetBytes(aes.decrypt(byteMessage, postFile.key, postFile.keySize));
                                    return File(message, "text/plain", "decrypted_" + postFile.file.FileName.Split('.')[0] + ".txt");
                                }
                        }
                        break;
                    case "audio":
                        switch (postFile.action.ToLower())
                        {
                            case "encrypt":
                                using (var memoryStream = new MemoryStream())
                                {
                                    postFile.file.CopyTo(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = aes.encrypt(postFile.message, postFile.key, postFile.keySize);
                                    byte[] encryptedAudio = LSB.watermarkAudio(memoryStream, byteMessage);
                                    //var encryptedStream = new MemoryStream(encryptedAudio);
                                    return File(encryptedAudio, "audio/wav", "encrypted_" + postFile.file.FileName);

                                }
                            case "decrypt":
                                using (var memoryStream = new MemoryStream())
                                {
                                    postFile.file.CopyTo(memoryStream);
                                    AES aes = new AES();
                                    byte[] byteMessage = LSB.extractAudio(memoryStream);
                                    byte[] message = Encoding.UTF8.GetBytes(aes.decrypt(byteMessage, postFile.key, postFile.keySize));
                                    return File(message, "text/plain", "decrypted_" + postFile.file.FileName.Split('.')[0] + ".txt");
                                }
                        }
                        break;
                    default:
                        return BadRequest("File format not supported");
                }
                return StatusCode(StatusCodes.Status500InternalServerError, "Something went wrong!!!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }

        }
        [HttpPost("encrypt_text")]
        public IActionResult encryptText([FromForm] AES_Text input)
        {
            try
            {
                AES aes = new AES();
                byte[] resultByte = aes.encrypt(input.message, input.key, input.keySize);
                string resultString = Encoding.UTF8.GetString(resultByte);
                var result = new EncryptTextResult(resultString, resultByte);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("decrypt_text")]
        public IActionResult decryptText([FromForm] AES_Text input)
        {
            try
            {
                AES aes = new AES();
                byte[] inputByteMessage;
                if (input.isByte == "on")
                {
                    input.message = input.message.Replace(",", "");
                    var messageByteArr = input.message.Split(' ');
                    List<byte> messageByteList = new List<byte>();
                    foreach(var i in messageByteArr)
                    {
                        messageByteList.Add((byte)int.Parse(i));
                    }
                    inputByteMessage = messageByteList.ToArray();
                }
                else
                {
                    inputByteMessage = Encoding.UTF8.GetBytes(input.message);
                }
                string result = aes.decrypt(inputByteMessage, input.key, input.keySize);
                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost("lsb")]
        public IActionResult watermark([FromForm] PostFile postFile)
        {
            try
            {
                if (postFile.action.ToLower() == "watermark")
                {
                    byte[] messageByte = Encoding.UTF8.GetBytes(postFile.message);
                    if (postFile.fileType.ToLower() == "image")
                    {
                        using (var stream = new MemoryStream())
                        {
                            postFile.file.CopyTo(stream);
                            Bitmap watermarkedImage = LSB.watermarkImage(stream, messageByte);
                            MemoryStream saveFile = new MemoryStream();
                            watermarkedImage.Save(saveFile, ImageFormat.Png);
                            var content = saveFile.ToArray();
                            return File(content, "image/png", "watermarked_" + postFile.file.FileName.Split('.')[0] + ".png");
                        }
                    }
                    else if (postFile.fileType.ToLower() == "audio")
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            postFile.file.CopyTo(memoryStream);
                            byte[] watermarkedAudio = LSB.watermarkAudio(memoryStream, messageByte);
                            return File(watermarkedAudio, "audio/wav", "watermarked_" + postFile.file.FileName);

                        }
                    }
                    else
                    {
                        return BadRequest("File format not supported");
                    }
                }
                else if (postFile.action.ToLower() == "extract")
                {
                    if (postFile.fileType.ToLower() == "image")
                    {
                        using (var stream = new MemoryStream())
                        {
                            postFile.file.CopyTo(stream);
                            byte[] extractedMessage = LSB.extractImage(stream);
                            string result = Encoding.UTF8.GetString(extractedMessage);
                            byte[] resultByte = Encoding.UTF8.GetBytes(result);
                            return File(resultByte, "text/plain", "extract_" + postFile.file.FileName.Split('.')[0] + ".txt");
                        }
                    }
                    else if (postFile.fileType.ToLower() == "audio")
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            postFile.file.CopyTo(memoryStream);
                            byte[] extractedMessage = LSB.extractAudio(memoryStream);
                            string result = Encoding.UTF8.GetString(extractedMessage);
                            byte[] resultByte = Encoding.UTF8.GetBytes(result);
                            return File(resultByte, "text/plain", "extract_" + postFile.file.FileName.Split('.')[0] + ".txt");
                        }
                    }
                    else
                    {
                        return BadRequest("File format not supported");
                    }
                }
                else return BadRequest("Unknow action!!!");
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
