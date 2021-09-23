using FTPFilesDashboard.DataAccess.Internal;
using FTPFilesDashboard.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FTPFilesDashboard.DataAccess
{
    public class RecordingsData : IRecordingsData
    {
        private readonly ISQLDataAccess _sQLDataAccess;
        private readonly ILogger<RecordingsData> _logger;

        public RecordingsData(ISQLDataAccess sQLDataAccess, ILogger<RecordingsData> logger)
        {
            _sQLDataAccess = sQLDataAccess;
            _logger = logger;
        }
        //spLoadZoomRecordingsByKeyword

        public List<RecordingsModel> LoadZoomRecordingsByKeyword(string keyword)
        {
            try
            {
                return _sQLDataAccess.LoadData<RecordingsModel, dynamic>("dbo.spLoadZoomRecordingsByKeyword", new { keyword }, "ZoomDownloadsData");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
        public void GenerateLink(RecordingsModel recordingsModel)
        {
            var generatedLink = new GeneratedLinksModel
            {
                LinkID = recordingsModel.LinkID,
                RecordingID = recordingsModel.RecordingID,
                GeneratedLink = recordingsModel.GeneratedLink,
                CreatedOn = recordingsModel.LinkCreatedOn,
                ExpirationDate = recordingsModel.LinkExpirationDate,
                CreatedBy = recordingsModel.CreatedBy
            };

            if (generatedLink.LinkID == null) //Insert if link ID is returned NULL
            {
                try
                {
                    _sQLDataAccess.SaveData("dbo.spInsertGeneratedLink", generatedLink, "ZoomDownloadsData");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }
            else //update link
            {
                try
                {
                    _sQLDataAccess.SaveData("dbo.spUpdateGeneratedLink", generatedLink, "ZoomDownloadsData");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    throw;
                }
            }


        }
    }
}
