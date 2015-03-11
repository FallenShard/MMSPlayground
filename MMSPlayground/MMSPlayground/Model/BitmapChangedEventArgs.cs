﻿using System;
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
        private IList<int>[] channelHistograms;

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

        public IList<int>[] ChannelHistograms
        {
            get
            {
                return this.channelHistograms;
            }

        }


        public BitmapChangedEventArgs(Bitmap bitmap, Bitmap[] channels, IList<int>[] channelHistograms)
        {
            this.bitmap = bitmap;
            this.channels = channels;
            this.channelHistograms = channelHistograms;
        }
    }
}
