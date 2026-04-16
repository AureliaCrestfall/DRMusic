using DRMusic.Model;
using Microsoft.Data.SqlClient;
using System.Diagnostics;

namespace DRMusic.Repo
{
    public class MusicRepo : IMusicRepo
    {

        private string _connectionString;
        int nextID;
        public MusicRepo(string connectionString)
        {
            _connectionString = connectionString;
            nextID = GetAllMusics().Count();
            
        }

        public List<Music> GetAllMusics()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Title, Artist, Duration, PublicationYear, ID FROM MusicRecords", connection);
                var reader = command.ExecuteReader();
                var musics = new List<Music>();
                while (reader.Read())
                {
                    var music = new Music
                    {
                        Id = reader.GetInt32(4),
                        Title = reader.GetString(0),
                        Artist = reader.GetString(1),
                        Duration = reader.GetInt32(2),
                        PublicationYear = reader.GetDateTime(3)
                    };
                    musics.Add(music);
                }
                return musics;
            }
        }
        public Music Add(string title,string artist,int duration,DateTime publicationYear)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand(
                "insert into MusicRecords(ID,Title,Artist,Duration,publicationYear)" +
                " values(@id,@Title,@Artist,@Duration,@publicationYear); ",
                sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", nextID);
            sqlCommand.Parameters.AddWithValue("@title", title);
            sqlCommand.Parameters.AddWithValue("@artist", artist);
            sqlCommand.Parameters.AddWithValue("@duration", duration);
            sqlCommand.Parameters.AddWithValue("@publicationYear", publicationYear);

            Music music = new Music()
            {
                Id = nextID,
                Title = title,
                Artist = artist,
                Duration = duration,
                PublicationYear = publicationYear

            };
           

            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException sqlError)
            {
                throw new Exception("Databasefejl i MusicRecords: " + sqlError.Message);
            }
            finally
            {
                sqlConnection.Close();
                
            }
            return music;
        }

        public Music Delete(int id)
        {
            Music music = Get(id);
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand(
                "DELETE FROM MusicRecords where ID = @id",
                sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);

            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
            }
            catch (SqlException sqlError)
            {
                throw new Exception("Databasefejl i MusicRecords: " + sqlError.Message);
            }
            finally
            {
                sqlConnection.Close();

            }
            return music;

        }
        public Music Get(int id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Title, Artist, Duration, PublicationYear, ID FROM MusicRecords where ID = @id", connection);
                command.Parameters.AddWithValue("@id", id);
                var reader = command.ExecuteReader();
                var music = new Music();
                while (reader.Read())
                {
                     music = new Music
                    {
                        Id = reader.GetInt32(4),
                        Title = reader.GetString(0),
                        Artist = reader.GetString(1),
                        Duration = reader.GetInt32(2),
                        PublicationYear = reader.GetDateTime(3)
                    };
                    
                }
                return music;
            }
        }
        public Music Update(int id, string title, string artist, int duration, DateTime publicationYear)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);
            SqlCommand sqlCommand = new SqlCommand(
                "UPDATE  MusicRecords set  Title = @Title, Artist = @Artist, Duration = @Duration, publicationYear = @publicationYear where ID = @id" ,
                sqlConnection);

            sqlCommand.Parameters.AddWithValue("@id", id);
            sqlCommand.Parameters.AddWithValue("@title", title);
            sqlCommand.Parameters.AddWithValue("@artist", artist);
            sqlCommand.Parameters.AddWithValue("@duration", duration);
            sqlCommand.Parameters.AddWithValue("@publicationYear", publicationYear);

            Music music = new Music()
            {
                Id = nextID,
                Title = title,
                Artist = artist,
                Duration = duration,
                PublicationYear = publicationYear

            };


            try
            {
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();

            }
            catch (SqlException sqlError)
            {
                throw new Exception("Databasefejl i MusicRecords: " + sqlError.Message);
            }
            finally
            {
                sqlConnection.Close();

            }
            return music;
        }
    }
}
