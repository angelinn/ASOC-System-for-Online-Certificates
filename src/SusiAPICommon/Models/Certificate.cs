using System;
using System.Collections.Generic;
using System.Text;

namespace SusiAPICommon.Models
{
    public class Certificate
    {
        public string Content { get; set; }
        public byte[] ToByteArray()
        {
            return Encoding.UTF8.GetBytes(Content);
        }
    }
}
