namespace OtelYonetimSistemi.DbConnection
{
    public class GetConnectionString
    {
        public string GetConnection  
        {
            get { return DbConnectionStrings.localString; }
        }
    }
}
