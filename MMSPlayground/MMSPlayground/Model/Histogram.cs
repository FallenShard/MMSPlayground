using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMSPlayground.Model
{
    public class Histogram
    {
        private IList<int> m_data = null;
        private int m_maxValue = -1;
        private int m_minValue = -1;
        private int m_minBucket = 0;
        private int m_maxBucket = 255;

        public IList<int> Data
        {
            get
            {
                return m_data;
            }
            set
            {
                m_data = value;

                if (m_data.Count > 0)
                {
                    m_maxValue = m_data.Max();
                    m_minValue = m_data.Min();
                }
            }
        }

        public int MaxValue
        {
            get
            {
                return m_maxValue;
            }
        }

        public int MinValue
        {
            get
            {
                return m_minValue;
            }
        }

        public int MinBucket
        {
            get
            {
                return m_minBucket;
            }
        }

        public int MaxBucket
        {
            get
            {
                return m_maxBucket;
            }
        }

        public Histogram()
        {
            m_data = new List<int>();
        }

        public Histogram(int size)
        {
            m_data = new List<int>(size);

            for (int i = 0; i < size; i++)
                m_data.Add(i);
        }
    }
}
