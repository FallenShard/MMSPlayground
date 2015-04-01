using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MMSPlayground.IO
{
    public class ShannonFanoReader : IImageReader
    {
        private static readonly int LengthOffset = 0;

        private static readonly int NodeSize = 6;

        BpfReader m_bpfReader = null;

        private class ShannonNode
        {
            public int code;
            public int length;
            public byte symbol;
        }

        public ShannonFanoReader(BpfReader bpfReader)
        {
            m_bpfReader = bpfReader;
        }

        public Bitmap ReadFromFile(string fileName)
        {
            byte[] buff;

            using (FileStream file = File.Open(fileName, FileMode.Open))
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                buff = memoryStream.ToArray();
            }

            int dictLength = BitConverter.ToInt32(buff, LengthOffset);

            int nodeIter = 4;
            int nodeEnd = 4 + NodeSize * dictLength;

            IDictionary<int, ShannonNode> symbolTable = new Dictionary<int, ShannonNode>();

            while (nodeIter < nodeEnd)
            {
                byte symbol = buff[nodeIter];
                byte codeLen = buff[nodeIter + 1];
                int code = BitConverter.ToInt32(buff, nodeIter + 2);

                ShannonNode node = new ShannonNode();
                node.length = codeLen;
                node.symbol = symbol;
                node.code = code;
                symbolTable.Add(code, node);

                nodeIter += NodeSize;
            }

            int padding = buff[nodeIter];

            byte[] data = buff.Skip(nodeIter + 1).ToArray();

            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < data.Length; i++)
                builder.Append(Convert.ToString(data[i], 2).PadLeft(8, '0'));

            String bitString = builder.Remove(0, padding).ToString();

            IList<byte> bpfArray = new List<byte>();
            builder = new StringBuilder();
            int currentValue = 0;
            int currentLength = 0;
            for (int i = 0; i < bitString.Length; i++)
            {
                int currentBit = bitString[i] - '0';
                currentValue <<= 1;
                currentValue |= currentBit;
                currentLength++;
                
                if (symbolTable.ContainsKey(currentValue))
                {
                    ShannonNode node = symbolTable[currentValue];
                    if (node.code == currentValue && node.length == currentLength)
                    {
                        bpfArray.Add(node.symbol);
                        currentLength = 0;
                        currentValue = 0;
                    }
                }
            }

            return m_bpfReader.ReadFromBytes(bpfArray.ToArray());
        }
    }
}
