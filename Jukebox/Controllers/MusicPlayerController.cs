using EmbedIO;
using EmbedIO.Routing;
using EmbedIO.WebApi;
using Jukebox.Database;
using Jukebox.Library;
using MediaManager;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Jukebox.Controllers
{
    public class MusicPlayerController : WebApiController
    {
        protected JukeboxDb _db;

      

        public MusicPlayerController()
        {
            _db = JukeboxDb.GetInstance();
        }

        [Route(HttpVerbs.Get, "/")]
        public async Task<string> NowPlaying()
        {
            return "";
        }

        [Route(HttpVerbs.Get, "/files")]
        public async Task<string> GetMusic(int limit = 100, int offset = 0)
        {
            var data = await _db.MusicFiles.All(limit, offset);
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            return json;
        }

        [Route(HttpVerbs.Get, "/play")]
        public Dictionary<string, string> PlayMusic()
        {
            JukeboxMediaManager.GetInstance().Play();
            var currentSong = CrossMediaManager.Current.Queue.Current.DisplayTitle;
            var currentArtist = CrossMediaManager.Current.Queue.Current.Artist;
            var currentAlbum = CrossMediaManager.Current.Queue.Current.Album;
            var currentDuration = CrossMediaManager.Current.Queue.Current.Duration.ToString();

            var metaData = new Dictionary<string, string>()
            {
                { "Title", currentSong }, {"Artist", currentArtist }, {"Album",currentAlbum },
                { "Duration", currentDuration }
            };

            return metaData;
        }

        [Route(HttpVerbs.Get, "/play/{id}")]
        public string PlayMusic(int id)
        {
            JukeboxMediaManager.GetInstance().Play();

            return "";
        }

        [Route(HttpVerbs.Get, "/pause")]
        public Dictionary<string, string> PauseMusic()
        {
            JukeboxMediaManager.GetInstance().Pause();
            var currentSong = CrossMediaManager.Current.Queue.Current.DisplayTitle;
            var currentArtist = CrossMediaManager.Current.Queue.Current.Artist;
            var currentAlbum = CrossMediaManager.Current.Queue.Current.Album;
            var currentDuration = CrossMediaManager.Current.Queue.Current.Duration.ToString();

            var metaData = new Dictionary<string, string>()
            {
                { "Title", currentSong }, {"Artist", currentArtist }, {"Album",currentAlbum },
                { "Duration", currentDuration }
            };
            return metaData;
        }

        [Route(HttpVerbs.Get, "/next")]
        public async Task<Dictionary<string, string>>  NextTrack()
        {
            await Task.Run(() => JukeboxMediaManager.GetInstance().PlayNext());
            if(CrossMediaManager.Current.Queue.HasNext)
            {
                var currentSong = CrossMediaManager.Current.Queue.Next.DisplayTitle;
                var currentArtist = CrossMediaManager.Current.Queue.Next.Artist;
                var currentAlbum = CrossMediaManager.Current.Queue.Next.Album;
                var currentDuration = CrossMediaManager.Current.Queue.Next.Duration.ToString();

                var metaData = new Dictionary<string, string>()
            {
                { "Title", currentSong }, {"Artist", currentArtist }, {"Album",currentAlbum },
                { "Duration", currentDuration }
            };
                return metaData;
            }
            else
            {
                var currentSong = CrossMediaManager.Current.Queue.Current.DisplayTitle;
                var currentArtist = CrossMediaManager.Current.Queue.Current.Artist;
                var currentAlbum = CrossMediaManager.Current.Queue.Current.Album;
                var currentDuration = CrossMediaManager.Current.Queue.Current.Duration.ToString();

                var metaData = new Dictionary<string, string>()
                 {
                    { "Title", currentSong }, {"Artist", currentArtist }, {"Album",currentAlbum },
                    { "Duration", currentDuration }
                 };
                return metaData;
            };

        }

       

        [Route(HttpVerbs.Get, "/prev")]
        public async Task<Dictionary<string, string>> PreviousTrack()
        {
            await Task.Run(()=> JukeboxMediaManager.GetInstance().PlayPrev());
            if (CrossMediaManager.Current.Queue.HasPrevious)
            {
                var currentSong = CrossMediaManager.Current.Queue.Previous.DisplayTitle;
                var currentArtist = CrossMediaManager.Current.Queue.Previous.Artist;
                var currentAlbum = CrossMediaManager.Current.Queue.Previous.Album;
                var currentDuration = CrossMediaManager.Current.Queue.Previous.Duration.ToString();

                var metaData = new Dictionary<string, string>()
                 {
                    { "Title", currentSong }, {"Artist", currentArtist }, {"Album",currentAlbum },
                    { "Duration", currentDuration }
                 };
                return metaData;
            }
            else {
                var currentSong = CrossMediaManager.Current.Queue.Current.DisplayTitle;
                var currentArtist = CrossMediaManager.Current.Queue.Current.Artist;
                var currentAlbum = CrossMediaManager.Current.Queue.Current.Album;
                var currentDuration = CrossMediaManager.Current.Queue.Current.Duration.ToString();

                var metaData = new Dictionary<string, string>()
                 {
                    { "Title", currentSong }, {"Artist", currentArtist }, {"Album",currentAlbum },
                    { "Duration", currentDuration }
                 };
                return metaData;
            };


               
        }

        [Route(HttpVerbs.Get, "/volDown")]
        public async Task<string> VolumeDown()
        {
            JukeboxMediaManager.GetInstance().VolumeDown();
            return "";
        }

        [Route(HttpVerbs.Get, "/volUp")]
        public async Task<string> VolumeUp()
        {
            JukeboxMediaManager.GetInstance().VolumeUp();
            return "";
        }

        [Route(HttpVerbs.Get, "/mute")]
        public async Task<string> Mute()
        {
            JukeboxMediaManager.GetInstance().Mute();
            return "";
        }
    }
}
