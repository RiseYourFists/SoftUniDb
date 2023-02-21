using MusicHub.Data;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace MusicHub
{
    public class StartUp
    {
        static void Main(string[] args)
        {
           
            var context = new MusicHubDbContext();
            var result = ExportSongsAboveDuration(context, 4);
            Console.WriteLine(result);
        }

        public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
        {
            var producers = context.Producers
                .Where(p => p.Id == producerId)
                .Include(p => p.Albums)
                .ThenInclude(a => a.Songs)
                .ThenInclude(a => a.Writer)
                .ToArray();

            var sb = new StringBuilder();

            foreach (var producedAlbums in producers)
            {
                var albums = producedAlbums.Albums.OrderByDescending(a => a.Price);
                foreach (var album in albums)
                {
                    sb.AppendLine($"-AlbumName: {album.Name}");
                    sb.AppendLine($"-ReleaseDate: {album.ReleaseDate.ToString("MM/dd/yyyy")}");
                    sb.AppendLine($"-ProducerName: {album.Producer.Name}");
                    sb.AppendLine("-Songs:");
                    var counter = 1;

                    var songs = album.Songs
                        .OrderByDescending(s => s.Name)
                        .ThenBy(s => s.Writer.Name);

                    foreach (var song in songs)
                    {
                        sb.AppendLine($"---#{counter}");
                        counter++;
                        sb.AppendLine($"---SongName: {song.Name}");
                        sb.AppendLine($"---Price: {song.Price:f2}");
                        sb.AppendLine($"---Writer: {song.Writer.Name}");
                    }

                    sb.AppendLine($"-AlbumPrice: {album.Price:f2}");
                }
            }
            return  sb.ToString().TrimEnd();
        }

        public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
        {
            var sb = new StringBuilder();

            var songs = context.Songs
                .Include(s => s.SongPerformers)
                .ThenInclude(s => s.Performer)
                .Include(s => s.Writer)
                .Include(s => s.Album)
                .ThenInclude(s => s.Producer)
                .ToArray()
                .Where(s => s.Duration.TotalSeconds > duration)
                .Select(s => new
                {
                    s.Duration,
                    SongName = s.Name,
                    WriterName = s.Writer.Name,
                    ProducerName = s.Album.Producer.Name,
                    PerformerName = s.SongPerformers
                        .Select(sp 
                            => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
                        .FirstOrDefault()

                })
                .OrderBy(s => s.SongName)
                .ThenBy(s => s.WriterName)
                .ThenBy(s => s.PerformerName)
                .ToArray();


            var counter = 1;
            foreach (var song in songs)
            {
                sb.AppendLine($"-Song #{counter}");
                sb.AppendLine($"---SongName: {song.SongName}");
                sb.AppendLine($"---Writer: {song.WriterName}");
                sb.AppendLine($"---Performer: {song.PerformerName}");
                sb.AppendLine($"---AlbumProducer: {song.ProducerName}");
                sb.AppendLine($"---Duration: {song.Duration.ToString("c")}");
                counter++;
            }


            return sb.ToString().TrimEnd();
        }
    }
}
