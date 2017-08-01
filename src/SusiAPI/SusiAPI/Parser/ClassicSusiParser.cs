using System;
using System.Collections.Generic;
using SusiAPI.Models;
using System.Net.Http;
using System.Net;
using HtmlAgilityPack;
using System.Threading.Tasks;

namespace SusiAPI.Parser
{
	public class ClassicSusiParser : ISusiParser
	{
		private const string SUSI_URL = "https://susi.uni-sofia.bg/";
		private static readonly string LOGIN_URL = $"{SUSI_URL}ISSU/forms/Login.aspx";

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

		public ClassicSusiParser()
		{
			handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
			client = new HttpClient(handler);
		}

		public Task<StudentInfo> GetStudentInfoAsync()
		{
			var rootDocument = new HtmlDocument();
			rootDocument.Load("file1.txt");

			HtmlNode node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"HeaderMenu1_HeaderLeft1_lblFacultyName\"]");
			student.Faculty = node.InnerText;

			node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblFullName\"]");
			student.Names = node.InnerText;

			node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblPersonalNumber\"]");
			student.EGN = node.InnerText;

			node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_pnlAddresses\"]/table[2]/tbody/tr[3]/td[5]");
			student.City = node.InnerText;

			node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_pnlAddresses\"]/table[2]/tbody/tr[3]/td[3]");
			student.Region = node.InnerText;

			node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblSex\"]");
			student.Gender = node.InnerText;


			if ((DateTime.Now.Month >= 2 && DateTime.Now.Day >= 20) && (DateTime.Now.Month <= 7 && DateTime.Now.Day <= 7))
			{
				student.Semester = "летен";
				student.StartYear = DateTime.Now.Year - YEAR - 1;
			}

			else
			{
				student.Semester = "зимен";
				student.StartYear = DateTime.Now.Year - YEAR;
			}

			student.EndYear = student.StartYear + 1;

			node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblEducationPlan\"]");
			string educationPlan = node.InnerText;

			student.Program = educationPlan.Substring(5, educationPlan.IndexOf('-') - 5);

			node = rootDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentPersonalData1_lblStudentEntranceTypeName\"]");
			student.Degree = node.InnerText;

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

			if (stringResult.Contains("header start"))
			{
				isAuthenticated = true;

				//get student year
				HtmlDocument htmlDocument = new HtmlAgilityPack.HtmlDocument();
				htmlDocument.LoadHtml(stringResult);

				HtmlNode nodeTemp = htmlDocument.DocumentNode.SelectSingleNode("//*[@id=\"StudentInfo1_lblCourse\"]");
				string educationYear = nodeTemp.InnerText;

				switch (educationYear)
				{
					case "Курс 4":
						student.Year = "четвърти";
						break;
					case "Курс 3":
						student.Year = "трети";
						break;
					case "Курс 2":
						student.Year = "втори";
						break;
					case "Курс 1":
						student.Year = "първи";
						break;
					default:
						student.Year = "Няма студентски права";
						break;
				}

				//check is currently a student
				if (student.Year != "Няма студентски права")
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
