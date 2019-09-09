using System;
using System.Collections.Generic;

namespace Finalproject.Models
{
    public partial class Favorites
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Restaurant { get; set; }
        public string Dates { get; set; }
        public string Weather { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
