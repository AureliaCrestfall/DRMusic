using DRMusic.Model;

namespace DRMusic.Repo
{
    public interface IMusicRepo
    {

        List<Music> GetAllMusics();
        Music Add(string title, string artist, int duration, DateTime publicationYear);
        Music Delete(int id);
        Music Get(int id);
        Music Update(int id, string title, string artist, int duration, DateTime publicationYear);
    }
}
