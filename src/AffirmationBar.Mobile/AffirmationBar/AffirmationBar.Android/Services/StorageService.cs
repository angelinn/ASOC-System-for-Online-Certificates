﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AffirmationBar.Services;
using AffirmationBar.Droid.Services;
using System.Threading.Tasks;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(StorageService))]
namespace AffirmationBar.Droid.Services
{
    public class StorageService : IStorageService
    {
        public string FilePath { get; private set; }

        public Task<bool> SaveFile(string fileName, byte[] bytes)
        {
            try
            {
                string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
                string filePath = Path.Combine(documentsPath, fileName);
                File.WriteAllBytes(filePath, bytes);

                FilePath = filePath;
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                return Task.FromResult(false);
            }
        }
    }
}
