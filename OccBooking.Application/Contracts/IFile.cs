using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace OccBooking.Application.Contracts
{
    public interface IFile
    {
        Task CopyToAsync(MemoryStream memoryStream);
    }
}
