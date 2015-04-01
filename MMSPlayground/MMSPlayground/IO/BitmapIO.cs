using MMSPlayground.Views.Forms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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
                        IImageReader reader = new ShannonFanoReader(new BpfReader());
                        return reader.ReadFromFile(fileName);
                    }
                default:
                    {
                        return new Bitmap(fileName);
                    }
            }
        }

        public static void Save(Bitmap bitmap, string fileName, DownsamplingMode mode = DownsamplingMode.Y)
        {
            string extension = Path.GetExtension(fileName);

            switch (extension)
            {
                case ".bpf":
                    {
                        Downsampler sampler = new Downsampler();
                        Bitmap[] bitmaps = new Bitmap[4];
                        bitmaps[0] = (Bitmap)bitmap.Clone();
                        bitmaps[1] = sampler.DownsamplePreview(bitmaps[0], DownsamplingMode.Y);
                        bitmaps[2] = sampler.DownsamplePreview(bitmaps[0], DownsamplingMode.Cb);
                        bitmaps[3] = sampler.DownsamplePreview(bitmaps[0], DownsamplingMode.Cr);

                        DownsamplingForm dsForm = new DownsamplingForm();
                        dsForm.DisplayDownsampled(bitmaps);
                        dsForm.ShowDialog();

                        if (dsForm.DialogResult == DialogResult.OK)
                            mode = dsForm.GetSelectedMode();

                        dsForm.Dispose();

                        IImageWriter writer = new ShannonFanoWriter(new BpfWriter(mode));
                        writer.SaveToFile(bitmap, fileName);
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
