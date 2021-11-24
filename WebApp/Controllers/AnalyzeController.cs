using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Utility;
using WebApp.Models;

namespace WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnalyzeController : ControllerBase
    {
        [HttpPost]
        public IActionResult post([FromForm] AnalyzeModel model)
        {
            try
            {
                if (model.fileType.ToLower() == "image")
                {
                    List<AnalyzeByte> result = new List<AnalyzeByte>();
                    var originaMemoryStream = new MemoryStream();
                    model.original.CopyTo(originaMemoryStream);
                    ImageHandler original = new ImageHandler(originaMemoryStream);

                    var watermarkedMemoryStream = new MemoryStream();
                    model.watermarked.CopyTo(watermarkedMemoryStream);
                    ImageHandler watermarked = new ImageHandler(watermarkedMemoryStream);

                    result.Add(new AnalyzeByte(original.redBytes, watermarked.redBytes));
                    result.Add(new AnalyzeByte(original.greenBytes, watermarked.greenBytes));
                    result.Add(new AnalyzeByte(original.blueBytes, watermarked.greenBytes));
                    return Ok(result);
                }
                else if (model.fileType.ToLower() == "audio")
                {
                    var originaMemoryStream = new MemoryStream();
                    model.original.CopyTo(originaMemoryStream);
                    AudioWavHandler original = new AudioWavHandler(originaMemoryStream);

                    var watermarkedMemoryStream = new MemoryStream();
                    model.watermarked.CopyTo(watermarkedMemoryStream);
                    AudioWavHandler watermarked = new AudioWavHandler(watermarkedMemoryStream);

                    List<int> resultOriginal = new List<int>();
                    List<int> resultWatermarked = new List<int>();
                    foreach (var i in original.data)
                    {
                        resultOriginal.Add(i);
                    }
                    foreach (var i in watermarked.data)
                    {
                        resultWatermarked.Add(i);
                    }
                    AnalyzeByte result = new AnalyzeByte(resultOriginal, resultWatermarked);
                    return Ok(result);
                }
                else
                {
                    return BadRequest("File format not supported");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
    }
}
