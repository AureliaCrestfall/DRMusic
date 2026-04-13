using DRMusic.Model;
using Microsoft.Data.SqlClient;

namespace DRMusic.Repo
{
    public class RecordRepo : IRecordRepo
    {

        private string _connectionString;

        public RecordRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Record> GetAllRecords()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Title, Artist, Duration, PublicationYear FROM Records", connection);
                var reader = command.ExecuteReader();
                var records = new List<Record>();
                while (reader.Read())
                {
                    var record = new Record
                    {
                        Title = reader.GetString(0),
                        Artist = reader.GetString(1),
                        Duration = reader.GetInt32(2),
                        PublicationYear = reader.GetDateTime(3)
                    };
                    records.Add(record);
                }
                return records;
            }
        }
    }
}
