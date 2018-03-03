using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net;
using HtmlAgilityPack;
using System.Threading.Tasks;
using SusiAPICommon.Models;

namespace SusiAPI.Parser
{
    public class ClassicSusiParser : ISusiParser
    {
        private const string SUSI_URL = "https://susi.uni-sofia.bg/";
        private static readonly string LOGIN_URL = $"{SUSI_URL}ISSU/forms/Login.aspx";
        private static readonly string PERSONALDATA_URL = $"{SUSI_URL}ISSU/forms/Students/PersonalData.aspx";
        private static readonly string ROLES_URL = $"{SUSI_URL}ISSU/forms/Roles.aspx";

        private const int YEAR = 2000;
        private const string USERNAME_KEY = "txtUserName";
        private const string PASSWORD_KEY = "txtPassword";

        private HttpClient client;
        private HttpClientHandler handler;

        //(poolparty)..may be :D
        private StudentInfo student = new StudentInfo();

        private bool isAuthenticated;
        public bool IsAuthenticated => isAuthenticated;

        private bool isCurrentlyAStudent;
        public bool IsCurrentlyAStudent => isCurrentlyAStudent;

        private async Task<string> ProcessMasterAsync(string loginHtml)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(loginHtml);

            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//input");

            Dictionary<string, string> formData = new Dictionary<string, string>();
            foreach (HtmlNode node in nodes)
            {
                string id = node.Id.TrimStart('\r', '\n');
                if (id.Contains("rptRoles"))
                {
                    id = id.Replace('_', '$');
                }

                formData.Add(id, (node.Attributes["value"] == null) ? String.Empty : node.Attributes["value"].Value);
                Console.WriteLine(value: formData);
            }

            HtmlNode nodeEventTarget = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"rptRoles_ctl02_lblRoleName\"]");

            //give the chosen role
            formData.Add("__EVENTTARGET", nodeEventTarget.Attributes["id"].Value.Replace('_', '$'));
            formData.Add("__EVENTARGUMENT", String.Empty);

            HttpResponseMessage response = await client.PostAsync(ROLES_URL, new FormUrlEncodedContent(formData));

            return await response.Content.ReadAsStringAsync();
        }

        public ClassicSusiParser()
        {
            handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
            client = new HttpClient(handler);
        }

        public Task<StudentInfo> GetStudentInfoAsync()
        {

            HttpResponseMessage response = client.GetAsync(PERSONALDATA_URL).Result;
            string stringResult = response.Content.ReadAsStringAsync().Result;

            HtmlDocument rootDocument = new HtmlDocument();
            rootDocument.LoadHtml(stringResult);

            HtmlNode node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"HeaderMenu1_HeaderLeft1_lblFacultyName\"]");
            student.FacultyName = node.InnerText;

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblFullName\"]");
            student.Names = node.InnerText;

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblPersonalNumber\"]");
            student.EGN = node.InnerText;

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_pnlAddresses\"]/table[2]/tr[3]/td[5]");
            student.City = node.InnerText;

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_pnlAddresses\"]/table[2]/tr[3]/td[3]");
            student.Region = node.InnerText;

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblSex\"]");
            student.Gender = node.InnerText;


            student.Semester = new Semester();
            if ((DateTime.Now.Month >= 2 && DateTime.Now.Day >= 20) && (DateTime.Now.Month <= 7 && DateTime.Now.Day <= 7))
            {
                student.Semester.Type = SemesterType.Summer;
                student.Semester.Begins = DateTime.Now.Year - YEAR - 1;
            }
            else
            {
                student.Semester.Type = SemesterType.Winter;
                student.Semester.Begins = DateTime.Now.Year - YEAR;
            }

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblEducationPlan\"]");
            string educationPlan = node.InnerText;

            string form = educationPlan.Substring(educationPlan.LastIndexOf("(") + 1, 2);
            student.FormOfEducation = (form.Contains("р")) ? FormOfEducation.Regular : FormOfEducation.Distance;

            int programStartIndex= educationPlan.LastIndexOf("-") + 1;
            student.Program = educationPlan.Substring(programStartIndex, educationPlan.LastIndexOf("(") - programStartIndex);

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblStudentEntranceTypeName\"]");
            student.Degree = node.InnerText.Contains("магистър") ? "магистър" : "бакалавър";

            node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblFacultyNumber\"]");
            student.FacultyNumber = Convert.ToInt32(node.InnerText);


            return Task.FromResult<StudentInfo>(student);

        }

        public async Task<bool> LoginAsync(string username, string password)
        {
            HtmlDocument rootDocument = new HtmlWeb().Load(SUSI_URL);
            HtmlNodeCollection nodes = rootDocument.DocumentNode.SelectNodes("//input");

            Dictionary<string, string> formData = new Dictionary<string, string>();
            foreach (HtmlNode node in nodes)
                formData.Add(node.Id.TrimStart('\r', '\n'), (node.Attributes["value"] == null) ? String.Empty : node.Attributes["value"].Value);

            formData[USERNAME_KEY] = username;
            formData[PASSWORD_KEY] = password;

            HttpResponseMessage response = await client.PostAsync(LOGIN_URL, new FormUrlEncodedContent(formData));
            string stringResult = await response.Content.ReadAsStringAsync();

            if (stringResult.Contains("Избор на роля"))
            {
                stringResult = await ProcessMasterAsync(stringResult);
            }

            if (stringResult.Contains("header start"))
            {
                isAuthenticated = true;

                //get student year
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(stringResult);

                HtmlNode nodeTemp = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentInfo1_lblCourse\"]");
                string educationYear = nodeTemp.InnerText;

                if (educationYear.Contains("Курс"))
                    student.Year = (StudyYear)educationYear[educationYear.Length - 1] - '0';
                else
                    student.Year = StudyYear.NoStudentAccess;

                //check is currently a student
                if (student.Year != StudyYear.NoStudentAccess)
                    isCurrentlyAStudent = true;
                else
                    isCurrentlyAStudent = false;
            }
            else
                isAuthenticated = false;

            return isAuthenticated;
        }
    }
}
