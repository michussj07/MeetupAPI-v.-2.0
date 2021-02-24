﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MeetupAPI.Controllers
{
    [Route("file")]
    //[Authorize]
    public class FileController : ControllerBase
    {
        [ResponseCache(Duration = 1200, VaryByQueryKeys = new[] { "name" })]
        [HttpGet]
        public IActionResult GetFile(string name)
        {
            var rootFolder = Directory.GetCurrentDirectory();
            var fileFullPath = rootFolder + "/PrivateAssets/" + name;

            if(!System.IO.File.Exists(fileFullPath))
            {
                return NotFound();
            }

            var file = System.IO.File.ReadAllBytes(fileFullPath);
            var fileProvider = new FileExtensionContentTypeProvider();

            fileProvider.TryGetContentType(fileFullPath, out var contentType);

            return File(file, contentType, name);

        }
    }
}
