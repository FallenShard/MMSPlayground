using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;

namespace WAVPlayer
{
    public class WAVPlayerPresenter
    {
        private int ChannelsOffset = 22;
        private int BitsPerSampleOffset = 34;

        WAVPlayerModel m_model = null;
        WAVPlayerForm m_view = null;

        private MemoryStream m_playerStream = null;
        private SoundPlayer m_soundPlayer = new SoundPlayer();
        
        public WAVPlayerPresenter()
        {
            m_model = new WAVPlayerModel();
            m_view = new WAVPlayerForm(this);
        }

        public void DisplayView()
        {
            m_view.Show();
        }

        public void SetWavFile(string fileName)
        {
            byte[] buffer = File.ReadAllBytes(fileName);

            char[] riff = new char[4]; Array.Copy(buffer, 0, riff, 0, 4);
            int chunkSize = BitConverter.ToInt32(buffer, 4);

            char[] wave = new char[4]; Array.Copy(buffer, 8, wave, 0, 4);

            char[] fmt = new char[4]; Array.Copy(buffer, 12, fmt, 0, 4);

            int subchunk1Size = BitConverter.ToInt32(buffer, 16);

            short audioFormat = BitConverter.ToInt16(buffer, 20);

            short numChannels = BitConverter.ToInt16(buffer, ChannelsOffset);

            int sampleRate = BitConverter.ToInt32(buffer, 24);

            int byteRate = BitConverter.ToInt32(buffer, 28);

            int blockAlign = BitConverter.ToInt16(buffer, 32);

            int bitsPerSample = BitConverter.ToInt16(buffer, BitsPerSampleOffset);

            char[] data = new char[4]; Array.Copy(buffer, 36, data, 0, 4);

            int subchunk2Size = BitConverter.ToInt32(buffer, 40);

            m_model.AudioData = buffer;
            m_model.NumChannels = numChannels;
            m_model.BitsPerSample = bitsPerSample;

            m_view.BuildChannelControls(numChannels);
        }

        public void ApplyOffset(byte[] offsets)
        {
            m_model.ApplyOffset(offsets);
        }

        public void Play()
        {
            if (m_playerStream != null)
                m_playerStream.Dispose();

            m_playerStream = new MemoryStream(m_model.AudioData);

            m_soundPlayer.Stream = m_playerStream;
            m_soundPlayer.Play();
        }

        public void Stop()
        {
            m_soundPlayer.Stop();
        }

        public void CleanUp()
        {
            m_soundPlayer.Stop();

            if (m_playerStream != null)
                m_playerStream.Dispose();
        }
    }
}
