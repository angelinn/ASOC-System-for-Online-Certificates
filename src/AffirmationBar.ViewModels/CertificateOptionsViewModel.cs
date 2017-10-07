using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AffirmationBar.ViewModels
{
    public class CertificateOptionsViewModel : BaseViewModel
    {
        public CertificateOptionsViewModel(StudentInfo studentInfo)
        {
            this.Student = studentInfo;
        }

        public StudentInfo Student { get; set; }

        public IList<string> Reasons { get; set; } = new List<string>()
        {
            "БДЖ",
            "Градски транспорт",
            "НОИ",
            "ПСБО"
        };

        public async Task GetCertificateAsync()
        {
            IsLoading = true;
            HttpClient wb = new HttpClient();
            HttpResponseMessage response = await wb.GetAsync("https://cdn-ssl.img.disneystore.com/content/ds/skyway/2014/category/full/fwb_frozen_20140110.jpg");
            File.WriteAllBytes("D:\\test.jpg", await response.Content.ReadAsByteArrayAsync());
            IsLoading = false;
        }

        private bool isLoading;
        public bool IsLoading
        {
            get
            {
                return isLoading;
            }
            set
            {
                isLoading = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get
            {
                return Student.Names.Split(' ')[0];
            }
        }

        private string selected;
        public string Selected
        {
            get
            {
                return selected;
            }
            set
            {
                selected = value;
                OnPropertyChanged();
            }
        }
    }
}
