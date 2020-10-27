using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;       
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace RAZAM.Models
{
    public class RazamUser : IdentityUser
    {
        public ICollection<File> Files { get; set; }
        public ICollection<Note> Notes { get; set; }

        public RazamUser()
        {
            Files = new List<File>();
            Notes = new List<Note>();
        }
    }
 
}