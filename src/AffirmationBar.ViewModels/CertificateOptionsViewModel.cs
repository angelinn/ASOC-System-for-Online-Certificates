using SusiAPICommon.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace AffirmationBar.ViewModels
{
    public class CertificateOptionsViewModel : BaseViewModel
    {
        public StudentInfo Student { get; set; }
        public IList<string> Reasons { get; set; } = new List<string>()
        {
            "БДЖ",
            "Градски транспорт",
            "НОИ",
            "ПСБО"
        };

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
