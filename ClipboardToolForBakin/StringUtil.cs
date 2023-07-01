using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace ClipboardToolForBakin2
{
    public class StringUtil
    {
        public class StringData
        {
            public string NPL;
            public string NPC;
            public string NPR;
            public int Blspd;
            public int Blrate;
            public float Lipspd;
            public string text;
        }

        private static void CheckMatchAndUpdateData(string pattern, ref string input, StringData data)
        {
            var regex = new Regex(pattern, RegexOptions.Compiled);
            var match = regex.Match(input);
            if (match.Success && match.Index + match.Length <= input.Length)
            {
                string tag = match.Groups[1].Value.TrimStart('\\');
                switch (tag)
                {
                    case "NPL":
                        data.NPL = match.Groups[2].Value;
                        break;
                    case "NPC":
                        data.NPC = match.Groups[2].Value;
                        break;
                    case "NPR":
                        data.NPR = match.Groups[2].Value;
                        break;
                    case "blspd":
                        data.Blspd = int.Parse(match.Groups[2].Value);
                        break;
                    case "blrate":
                        data.Blrate = int.Parse(match.Groups[2].Value);
                        break;
                    case "lipspd":
                        data.Lipspd = float.Parse(match.Groups[2].Value);
                        break;
                }
                input = input.Remove(match.Index, match.Length);
            }
        }

        public static StringData ParseString(string input)
        {
            var patternNPL = @"(\\NPL)\[(.*?)\]";
            var patternNPC = @"(\\NPC)\[(.*?)\]";
            var patternNPR = @"(\\NPR)\[(.*?)\]";
            var patternBlSpd = @"(\\blspd)\[(\d+)\]";
            var patternBlRate = @"(\\blrate)\[(\d+)\]";
            var patternLipSpd = @"(\\lipspd)\[(\d+\.?\d*)\]";
            var data = new StringData();
            CheckMatchAndUpdateData(patternNPL, ref input, data);
            CheckMatchAndUpdateData(patternNPC, ref input, data);
            CheckMatchAndUpdateData(patternNPR, ref input, data);
            CheckMatchAndUpdateData(patternBlSpd, ref input, data);
            CheckMatchAndUpdateData(patternBlRate, ref input, data);
            CheckMatchAndUpdateData(patternLipSpd, ref input, data);
            data.text = input;
            return data;
        }

        public static string CombineString(StringData data)
        {
            string output = data.text;

            if (!string.IsNullOrEmpty(data.Lipspd.ToString()) && data.Lipspd != 0)
            {
                output = $"\\lipspd[{data.Lipspd}]" + output;
            }
            if (!string.IsNullOrEmpty(data.Blrate.ToString()) && data.Blrate != 0)
            {
                output = $"\\blrate[{data.Blrate}]" + output;
            }
            if (!string.IsNullOrEmpty(data.Blspd.ToString()) && data.Blspd != 0)
            {
                output = $"\\blspd[{data.Blspd}]" + output;
            }
            if (!string.IsNullOrEmpty(data.NPR))
            {
                output = $"\\NPR[{data.NPR}]" + output;
            }
            if (!string.IsNullOrEmpty(data.NPC))
            {
                output = $"\\NPC[{data.NPC}]" + output;
            }
            if (!string.IsNullOrEmpty(data.NPL))
            {
                output = $"\\NPL[{data.NPL}]" + output;
            }

            return output;
        }

    }
}