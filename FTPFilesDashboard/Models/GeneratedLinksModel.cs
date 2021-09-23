using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTPFilesDashboard.Models
{
    public class GeneratedLinksModel
    {
        public int? LinkID { get; set; }

        public string RecordingID { get; set; }
        public string GeneratedLink { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string CreatedBy { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
