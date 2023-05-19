using System.ComponentModel.DataAnnotations;
using System.IO.Compression;

namespace MyRepo.Models
{
    public class UploadedFile
    {
        [Key]
        public int FileId { get; set; }
        public string FileName { get; set; }
        public string ContentType { get; set; }
        public byte[] Content { get; set; }

        public void Compress()
        {
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(output, CompressionLevel.Optimal))
            {
                dstream.Write(Content, 0, Content.Length);
            }
            Content = output.ToArray();
        }

        public  void Decompress()
        {
            MemoryStream input = new MemoryStream(Content);
            MemoryStream output = new MemoryStream();
            using (DeflateStream dstream = new DeflateStream(input, CompressionMode.Decompress))
            {
                dstream.CopyTo(output);
            }
            Content = output.ToArray();
        }
    }
}
