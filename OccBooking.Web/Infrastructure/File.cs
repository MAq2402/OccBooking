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
        private readonly IFormFile file;

        public File(IFormFile formFile)
        {
            file = formFile;
        }

        public async Task CopyToAsync(MemoryStream memoryStream)
        {
            await file.CopyToAsync(memoryStream);
        }
    }
}