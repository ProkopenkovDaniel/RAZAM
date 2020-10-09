using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RAZAM.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string Nickname { get; set; }
        public ICollection<File> Files { get; set; }

        public User()
        {
            Files = new List<File>();
        }
    }
}