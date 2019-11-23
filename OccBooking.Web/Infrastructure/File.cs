using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Http;
using OccBooking.Application.Contracts;

namespace OccBooking.Web.Infrastructure
{
    public class File : IFile
    {
        public File(IFormFile formFile)
        {
            FormFile = formFile;
        }

        public IFormFile FormFile { get; }
        public async Task CopyToAsync(MemoryStream memoryStream)
        {
            await FormFile.CopyToAsync(memoryStream);
        }
    }
}
