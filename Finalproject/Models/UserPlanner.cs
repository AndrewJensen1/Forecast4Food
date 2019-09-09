using System;
using System.Collections.Generic;

namespace Finalproject.Models
{
    public partial class UserPlanner
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Notes { get; set; }
        public string Weather { get; set; }
        public string Restaurants { get; set; }
        public string Dates { get; set; }
        public string Events { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
