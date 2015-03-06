using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.Model
{
    public class BitmapChangedEventArgs : EventArgs
    {
        private Bitmap bitmap;
        private Bitmap[] channels;

        public Bitmap Bitmap
        {
            get
            {
                return this.bitmap;
            }
        }

        public Bitmap[] Channels
        {
            get
            {
                return this.channels;
            }
        }


        public BitmapChangedEventArgs(Bitmap bitmap, Bitmap[] channels)
        {
            this.bitmap = bitmap;
            this.channels = channels;
        }
    }
}
