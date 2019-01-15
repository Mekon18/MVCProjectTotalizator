using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;

namespace MVCProjectTotalizator.Models
{
    public class SearchViewModel
    {
        public DateTime Date { get; set; }
        public List<KindOfSport> KindsOfSport { get; set; }
        public int KindId { get; set; }
        public string Status { get; set; }

        public List<SportEvent> SportEvents { get; set; }
    }
}