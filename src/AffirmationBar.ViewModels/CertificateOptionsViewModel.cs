using SusiAPIClient;
using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
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
            this.Student.Reason = Reasons.First();
        }

        public StudentInfo Student { get; set; }

        public IList<string> Reasons { get; set; } = new List<string>()
        {
            "БДЖ",
            "Градски транспорт",
            "НОИ",
            "ПСБО"
        };
        
        public async Task<Certificate> GetCertificateAsync()
        {
            IsLoading = true;
            Certificate bytes = await SusiClient.GetCertificate(Student.Reason);
            IsLoading = false;

            return bytes;
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
                return Student.Names?.Split(' ')[0];
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
