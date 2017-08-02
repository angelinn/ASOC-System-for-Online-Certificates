using System;
using System.Collections.Generic;
using System.Text;

namespace SusiAPI.Models
{
    public class Semester
    {
        public SemesterType Type { get; set; }
        public int Begins { get; set; }
        public int Ends => Begins + 1;
    }
}
