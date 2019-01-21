using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using Common;

namespace MVCProjectTotalizator.Models
{
    public class HomeViewModel
    {
        public List<SportEvent> SportEvents { get; set; }
        public List<KindOfSport> KindsOfSport { get; set; }
        public List<string> Advertisements { get; set; }
    }
}