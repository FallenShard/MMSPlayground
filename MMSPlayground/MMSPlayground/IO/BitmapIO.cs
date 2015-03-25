using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MMSPlayground.IO
{
    public class BitmapIO
    {
        public static Bitmap Open(string fileName)
        {
            string extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case ".bpf":
                    {
                        BpfReader bpfReader = new BpfReader();
                        return bpfReader.ReadFromFile(fileName);
                    }
                default:
                    {
                        return new Bitmap(fileName);
                    }
            }
        }

        public static void Save(Bitmap bitmap, string fileName)
        {
            string extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case ".bpf":
                    {
                        BpfWriter bpfWriter = new BpfWriter();
                        bpfWriter.SaveToFile(bitmap, fileName);
                        break;
                    }
                default:
                    {
                        bitmap.Save(fileName);
                        break;
                    }
            }
        }
    }
}
