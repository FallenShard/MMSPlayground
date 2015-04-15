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
            // This byte stream is our custom file in its entirety (header + downsampled pixels)
            byte[] stream = m_bpfWriter.GetRawBytes(bitmap);

            /** Shannon-Fano simple implementation. Algorithm overview: 
             * 1. Form a list of probabilities/occurences for each symbol (0-255 are symbol (byte) values)
             * 2. Sort that least according to occurences in a descending order (highest first)
             * 3. Divide that list in two so that both parts have approximately same sum
             * 4. Left part gets binary digit 0, right part gets binary digit 1
             * 5. Recursively apply steps 3 and 4 to form symbol codes
             */

            /// 1. Form a list of probabilities/occurences (it's essentially like a histogram)
            IDictionary<byte, int> symbolProbs = new Dictionary<byte, int>();

            for (int i = 0; i < stream.Length; i++)
            {
                byte symbol = stream[i];
                if (symbolProbs.ContainsKey(symbol))
                    symbolProbs[symbol]++;
                else
                    symbolProbs.Add(symbol, 1);
            }

            /// 2. Sort the frequency list in descending order while keeping symbol order
            int[] probs = symbolProbs.Values.ToArray();
            byte[] symbols = symbolProbs.Keys.ToArray();

            // Sort does it in ascending order, we need a descending order, so reverse both arrays
            Array.Sort(probs, symbols);
            Array.Reverse(probs);
            Array.Reverse(symbols);

            // This dictionary will keep string codes for each of our symbols
            // It's in the form of symbolCodes[symbol] = "MyAwesomeCode";
            // symbol is a value 0-255 (byte range)
            // MyAwesomeCode, after the algorithm, will be "010" or "0111" etc.
            IDictionary<byte, string> symbolCodes = new Dictionary<byte, string>();
            for (int i = 0; i < symbols.Length; i++)
                symbolCodes.Add(symbols[i], "");

            /// 3. and 4. This is the scary recursive function! Notes below
            RecursiveDivide(probs, symbols, 0, probs.Length - 1, symbolCodes);

            /// 5. Write to file, we'll need:
            /** 5a - dictionary length 
             *  5b - nodes in form of [symbol, len, code]
             *  5c - padding
             *  5d - actual coded data bit stream
             */
            using (FileStream fstream = File.Create(fileName))
            using (MemoryStream mstream = new MemoryStream())
            {
                // 5a - length
                mstream.Write(BitConverter.GetBytes(symbolCodes.Count), 0, sizeof(int));

                symbols = symbolCodes.Keys.ToArray();
                string[] codes = symbolCodes.Values.ToArray();

                // 5b - nodes
                for (int i = 0; i < symbols.Length; i++)
                {
                    mstream.WriteByte(symbols[i]);

                    byte len = (byte)codes[i].Length;
                    mstream.WriteByte(len);

                    int code = Convert.ToInt32(codes[i], 2);
                    mstream.Write(BitConverter.GetBytes(code), 0, sizeof(int));
                    byte[] array = mstream.ToArray();
                }

                // 5d - bitstream
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < stream.Length; i++)
                    builder.Append(symbolCodes[stream[i]]);

                // 5c - add padding to the front
                String finalString = "";
                int padding = 8 - builder.Length % 8;
                for (int i = 0; i < padding; i++)
                    finalString += "0";

                // Add bitstream from StringBuilder to our padded part
                finalString += builder.ToString();
                int x = finalString.Length;

                // This is magic, it converts binary string (like "0010110111011011") aligned to 8 bits, into an actual byte stream WHILE KEEPING zeros in the front
                byte[] bytes = Enumerable.Range(0, finalString.Length / 8). Select(pos => Convert.ToByte(finalString.Substring(pos * 8, 8), 2)).ToArray();

                // Write this padding, we'll need to know how many zeros to throw away on the reading side
                mstream.WriteByte((byte)padding);
                mstream.Write(bytes, 0, bytes.Length);

                // We're done, we have our Shannon-Fano compressed file!
                mstream.WriteTo(fstream);
            }
        }

        /// <summary>
        /// This is our awesome recursive Shannon-Fano!
        /// </summary>
        /// <param name="probs">The array of probabilities for each symbol</param>
        /// <param name="symbols">The symbol array (256 distint values)</param>
        /// <param name="start">Starting index of the list (initially 0)</param>
        /// <param name="end">Ending index of the list (initially probs.length, so 255)</param>
        /// <param name="symbolCodes">This is dictionary which will remember our symbol codes</param>
        private void RecursiveDivide(int[] probs, byte[] symbols, int start, int end, IDictionary<byte, string> symbolCodes)
        {
            // Exit recursion if start is >= end, it means no symbols to process
            if (start >= end)
                return;

            // Algorithm overview follows in comments

            // Initialize our indices to sum stuff up 
            int leftIndex = start;
            int rightIndex = end;

            // These are initial sums, 0
            int leftSum = 0;
            int rightSum = 0;
                
            // If indices meet, we have reached a condition to exit
            while (leftIndex <= rightIndex)
            {
                // If left sum is smaller than right sum, increase left sum
                // and increase left index of course
                // Similarly if right side is smaller, in else, do the same
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

            // Okay what we do here is give all the items in the 'left' group
            // an additional '0' to their symbol codes
            // We know that they must be in range [start, leftIndex)
            // because that's how far left index advanced
            for (int i = start; i < leftIndex; i++)
            {
                symbolCodes[symbols[i]] = symbolCodes[symbols[i]] + "0";
            }

            // Do the same thing for the other group, give '1' to all the
            // symbols on the right side
            for (int i = leftIndex; i <= end; i++)
                symbolCodes[symbols[i]] = symbolCodes[symbols[i]] + "1";

            // Perform recursion on the left part
            RecursiveDivide(probs, symbols, start, leftIndex - 1, symbolCodes);

            // Perform recursion on the right part
            RecursiveDivide(probs, symbols, leftIndex, end, symbolCodes);
        }
    }
}
