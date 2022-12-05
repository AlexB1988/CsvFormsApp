

namespace CsvFormsApp.Services
{
    public interface IObjectService
    {
        Task GetObjectList(string path, int period, string connectionString,
                            bool currentFlow,int sublistID,int unitID,decimal rate);
    }
}
