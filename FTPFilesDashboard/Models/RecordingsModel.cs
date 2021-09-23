using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTPFilesDashboard.Models
{
    public class RecordingsModel
    {
        public string RecordingID { get; set; }
        public string MeetingRecordID { get; set; }

        public string MeetingID { get; set; }
        public string HostID { get; set; }
        public string Host { get; set; }
        public string Topic { get; set; }
        public DateTime MeetingDate { get; set; }
        public string BackupLocation { get; set; }

        public DateTime LastModified { get; set; }
        public Int64 FileSize { get; set; }
        public double FileSizeMB { get; set; }
        public string FileType { get; set; }
        public int? LinkID { get; set; }
        public string GeneratedLink { get; set; }

        public DateTime? LinkCreatedOn { get; set; }

        public string CreatedBy { get; set; }

        public DateTime? LinkExpirationDate { get; set; }

    }
}
