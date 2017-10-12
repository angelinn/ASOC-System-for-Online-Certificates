using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.Services
{
    public interface IStorageService
    {   
        string FilePath { get; }
        Task<bool> SaveFile(string fileName, byte[] bytes);
    }
}
