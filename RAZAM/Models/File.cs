using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAZAM.Models
{
    public class File
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public DateTime Date { get; set; }
        public string ContentType { get; set; }
        public string GuidName { get; set; }
        public string UserId { get; set; }
        public RazamUser User { get; set; }
    }
}