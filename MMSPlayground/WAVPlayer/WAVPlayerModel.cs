using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

namespace WAVPlayer
{
    public class WAVPlayerModel
    {
        byte[] m_audioData = null;

        int m_numChannels;
        int m_bitsPerSample;

        public byte[] AudioData
        {
            get
            {
                return m_audioData;
            }
            set
            {
                m_audioData = value;
            }
        }

        public int NumChannels
        {
            get
            {
                return m_numChannels;
            }
            set
            {
                m_numChannels = value;
            }
        }

        public int BitsPerSample
        {
            get
            {
                return m_bitsPerSample;
            }
            set
            {
                m_bitsPerSample = value;
            }
        }

        public WAVPlayerModel()
        {

        }

        public void ApplyOffset(byte[] offsets)
        {
            int bytesPerSample = m_bitsPerSample / 8;
            int channelsStep = m_numChannels * bytesPerSample;

            for (int i = 44; i < m_audioData.Length; i += channelsStep)
            {
                for (int k = 0; k < m_numChannels; k++)
                {
                    for (int b = 0; b < bytesPerSample; b++)
                    {
                        int index = i + k * bytesPerSample + b;
                        m_audioData[index] = (byte)((m_audioData[index] + offsets[k]) % 255);
                    }
                }
            }
        }
    }
}
