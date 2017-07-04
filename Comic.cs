using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using System.Drawing.Imaging;

namespace ComicBookReader
{
    class Comic
    {
        List<Bitmap> comicRead = new List<Bitmap>();

        /*public string returnType
        {
            get
            {
                return returnType;
            }

            set
            {

            }
        }*/

        public Comic(string fileName)
        {
            string name = @fileName;
            readFile(name);
        }

        private void readFile(string name)
        {
            //string fileName = name;

            using (ZipArchive archive = ZipFile.OpenRead(name))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string extension = Path.GetExtension(entry.FullName.ToString());

                    switch (extension)
                    {
                        case ".jpg":
                            buildPage(entry);
                            break;
                        case ".jpeg":
                            buildPage(entry);
                            break;
                        case ".png":
                            buildPage(entry);
                            break;
                        case ".bmp":
                            buildPage(entry);
                            break;
                        case ".tiff":
                            buildPage(entry);
                            break;
                        default:
                            break;
                    }

                }

            }
        }

        private void buildPage(ZipArchiveEntry page)
        {
            var stream = page.Open();
            using (Image image = Image.FromStream(stream))
            {
                Bitmap a = new Bitmap(image);
                buildComic(a);
            }
            GC.Collect();
        }

        private void buildComic(Bitmap page)
        {
            comicRead.Add(page);

        }

        public List<Bitmap> returnPage()
        {
            return comicRead;

        }
    }
}
