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
        public ICollection<Note> ReceivedNotes { get; set; }
        public ICollection<Note> SendedNotes { get; set; }

        public RazamUser()
        {
            Files = new List<File>();
            ReceivedNotes = new List<Note>();
            SendedNotes = new List<Note>();
        }
    }
 
}