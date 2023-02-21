using MusicHub.Data;
using System;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using MusicHub.Initializer;

namespace MusicHub
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            var context = new MusicHubDbContext();
            var result = ExportAlbumsInfo(context, 9);
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
    }
}
