using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;

namespace MMSPlayground.IO
{
    public class ShannonFanoWriter : IImageWriter
    {
        private BpfWriter m_bpfWriter;

        public ShannonFanoWriter(BpfWriter bpfWriter)
        {
            m_bpfWriter = bpfWriter;
        }

        public void SaveToFile(Bitmap bitmap, string fileName)
        {
            byte[] stream = m_bpfWriter.GetRawBytes(bitmap);

            // Shannon-Fano simple implementation

            // 1. Form a list of probabilities/occurences
            IDictionary<byte, int> symbolProbs = new Dictionary<byte, int>();

            for (int i = 0; i < stream.Length; i++)
            {
                byte symbol = stream[i];
                if (symbolProbs.ContainsKey(symbol))
                    symbolProbs[symbol]++;
                else
                    symbolProbs.Add(symbol, 1);
            }

            // 2. Sort the frequency list in descending order while keeping symbol order
            int[] probs = symbolProbs.Values.ToArray();
            byte[] symbols = symbolProbs.Keys.ToArray();

            Array.Sort(probs, symbols);
            Array.Reverse(probs);
            Array.Reverse(symbols);

            // 3. Perform a recursive division and code assignment
            IDictionary<byte, string> symbolCodes = new Dictionary<byte, string>();
            for (int i = 0; i < symbols.Length; i++)
                symbolCodes.Add(symbols[i], "");

            RecursiveDivide(probs, symbols, 0, probs.Length - 1, symbolCodes);

            // 4. Write to file, we'll need:
            /* - dictionary length 
             * - nodes in form of [symbol, len, code]
             * - padding
             * - actual coded data bit stream
             **/
            using (FileStream fstream = File.Create(fileName))
            using (MemoryStream mstream = new MemoryStream())
            {
                // 4a - length
                mstream.Write(BitConverter.GetBytes(symbolCodes.Count), 0, sizeof(int));

                symbols = symbolCodes.Keys.ToArray();
                string[] codes = symbolCodes.Values.ToArray();

                // 4b - nodes
                for (int i = 0; i < symbols.Length; i++)
                {
                    mstream.WriteByte(symbols[i]);

                    byte len = (byte)codes[i].Length;
                    mstream.WriteByte(len);

                    int code = Convert.ToInt32(codes[i], 2);
                    mstream.Write(BitConverter.GetBytes(code), 0, sizeof(int));
                    byte[] array = mstream.ToArray();
                }

                // 4c and 4d - padding + bitstream
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < stream.Length; i++)
                    builder.Append(symbolCodes[stream[i]]);

                String finalString = "";
                int padding = 8 - builder.Length % 8;
                for (int i = 0; i < padding; i++)
                    finalString += "0";

                finalString += builder.ToString();
                int x = finalString.Length;

                byte[] bytes = Enumerable.Range(0, finalString.Length / 8). Select(pos => Convert.ToByte(finalString.Substring(pos * 8, 8), 2)).ToArray();

                mstream.WriteByte((byte)padding);
                mstream.Write(bytes, 0, bytes.Length);

                mstream.WriteTo(fstream);
            }
        }

        private void RecursiveDivide(int[] probs, byte[] symbols, int start, int end, IDictionary<byte, string> symbolCodes)
        {
            if (start >= end)
                return;

            int leftIndex = start;
            int rightIndex = end;

            int leftSum = 0;
            int rightSum = 0;
                
            while (leftIndex <= rightIndex)
            {
                if (leftSum <= rightSum)
                {
                    leftSum += probs[leftIndex];
                    leftIndex++;
                }
                else
                {
                    rightSum += probs[rightIndex];
                    rightIndex--;
                }
            }

            for (int i = start; i < leftIndex; i++)
            {
                symbolCodes[symbols[i]] = symbolCodes[symbols[i]] + "0";
            }

            for (int i = leftIndex; i <= end; i++)
                symbolCodes[symbols[i]] = symbolCodes[symbols[i]] + "1";

            RecursiveDivide(probs, symbols, start, leftIndex - 1, symbolCodes);
            RecursiveDivide(probs, symbols, leftIndex, end, symbolCodes);
        }
    }
}
