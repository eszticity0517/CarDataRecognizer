using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CarDataRecognizer.Services.Dir
{
    public interface IDirectoryService
    {
        FileInfo[] ListFiles();
    }
}
