using System.Collections.Generic;

namespace FTPFilesDashboard.DataAccess.Internal
{
    public interface ISQLDataAccess
    {
        void CommitTransaction();
        void Dispose();
        string GetConnectionString(string name);
        List<T> LoadData<T, U>(string storedProcedure, U parameters, string connectionStringName);
        List<T> LoadDataBySQLTEXT<T, U>(string sqlText, string connectionStringName);
        List<T> LoadDataInTransaction<T, U>(string storedProcedure, U parameters);
        void RollbackTransaction();
        void SaveData<T>(string storedProcedure, T parameters, string connectionStringName);
        void SaveDataInTransaction<T>(string storedProcedure, T parameters);
        int SaveDataInTransactionWithOutPut<T>(string storedProcedure, T parameters, string OutputParameterName);
        int SaveDataWithOutPut<T>(string storedProcedure, T parameters, string OutputParameterName, string connectionStringName);
        void StartTransaction(string connectionStringName);
    }
}