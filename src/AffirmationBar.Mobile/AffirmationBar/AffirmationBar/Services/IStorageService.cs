using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.Services
{
    public interface IStorageService
    {
        Task<bool> SaveFile(string fileName, byte[] bytes);
    }
}
