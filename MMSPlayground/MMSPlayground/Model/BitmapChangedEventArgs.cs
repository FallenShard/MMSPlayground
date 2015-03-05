using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace MMSPlayground.Model
{
    public class BitmapChangedEventArgs : EventArgs
    {
        private Bitmap bitmapPriv;
        private Bitmap[] channelsPriv;

        public Bitmap Bitmap
        {
            get
            {
                return this.bitmapPriv;
            }
        }

        public Bitmap[] Channels
        {
            get
            {
                return this.channelsPriv;
            }
        }


        public BitmapChangedEventArgs(Bitmap bitmap, Bitmap[] channels)
        {
            bitmapPriv = bitmap;
            channelsPriv = channels;
        }
    }
}
