using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xSimulate.Util
{
    public class WildCard
    {
        public static bool Test(string wildCard, string content)
        {
            if (wildCard == null || content == null)
            {
                return false;
            }

            int w = 0;
            int c = 0;
            int cIndex = 0;

            while (c < content.Length &&
                w < wildCard.Length &&
                wildCard[w] != '*')
            {
                if (wildCard[w] != content[c]) // forget "?"
                {
                    return false;
                }

                w++;
                c++;
            }

            while (c < content.Length && w < wildCard.Length)
            {
                if (w < wildCard.Length &&
                    wildCard[w] == '*')
                {
                    if (++w == wildCard.Length)
                    {
                        return true;
                    }

                    cIndex = c + 1;
                }
                else if (w < wildCard.Length &&
                    wildCard[w] == content[c]) // forget "?"
                {
                    w++;
                    c++;
                }
                else
                {
                    c = cIndex++;
                }
            }

            //while (w < wildCard.Length && wildCard[w] == '*')
            //{
            //    w++;
            //}

            return w == wildCard.Length && c == content.Length;
        }
    }
}
