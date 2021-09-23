using FTPFilesDashboard.Models;
using System.Collections.Generic;

namespace FTPFilesDashboard.DataAccess
{
    public interface IRecordingsData
    {
        void GenerateLink(RecordingsModel recordingsModel);
        List<RecordingsModel> LoadZoomRecordingsByKeyword(string keyword);
    }
}