using AffirmationBar.Services;
using AffirmationBar.UWP.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Provider;

[assembly: Xamarin.Forms.Dependency(typeof(StorageService))]
namespace AffirmationBar.UWP.Services
{
    public class StorageService : IStorageService
    {
        public string FilePath { get; private set; }

        public async Task<bool> SaveFile(string fileName, byte[] bytes)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("HTML file", new List<string>() { ".html" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = fileName;
            StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                FilePath = file.Path;
                CachedFileManager.DeferUpdates(file);
                await FileIO.WriteBytesAsync(file, bytes);

                if (await CachedFileManager.CompleteUpdatesAsync(file) == FileUpdateStatus.Complete)
                    return true;
            }

            return false;
        }
    }
}
