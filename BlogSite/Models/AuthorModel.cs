using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BlogSite.Models
{
    public class AuthorModel
    {
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorAbout { get; set; }

        public string AuthorImage { get; set; }

        public string AuthorFacebook { get; set; }

        public string AuthorTwitter { get; set; }

        public string AuthorInstagram { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorPassword { get; set; }

    }
}