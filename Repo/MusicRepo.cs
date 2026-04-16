using DRMusic.Model;
using Microsoft.Data.SqlClient;
using System.Reflection;

namespace DRMusic.Repo
{
    public class MusicRepo : IMusicRepo
    {

        private string _connectionString;

        public MusicRepo(string connectionString)
        {
            _connectionString = connectionString;
        }

        public List<Music> GetAllMusics(string search,string filter)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand($"SELECT Title, Artist, Duration, PublicationYear, ID FROM MusicRecords where {search} like {filter}%", connection);
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
                if(search != null)
                {
                    musics = musics.OrderBy(music => music.Id).ToList();
                }


                return musics;
            }
        }

    }
}
