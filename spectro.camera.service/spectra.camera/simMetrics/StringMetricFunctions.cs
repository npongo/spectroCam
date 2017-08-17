using npongo.wpf.simMetrics.SimMetricsMetricUtilities;
using System;
using System.Text;

namespace npongo.wpf.simMetrics
{
    public static class StringMetricFunctions
    { 
        #region SimMetrics

        public static double Levenstein(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new Levenstein()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double NeedlemanWunch(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new NeedlemanWunch()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double SmithWaterman(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new SmithWaterman()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double SmithWatermanGotoh(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new SmithWatermanGotoh()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double SmithWatermanGotohWindowedAffine(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new SmithWatermanGotohWindowedAffine()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double Jaro(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new Jaro()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double SwahiliJaroWinkler(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;
            firstWord = ((string)firstWord).Replace("R", "L").Replace("NG", "MW");
            secondWord = ((string)secondWord).Replace("R", "L").Replace("NG", "MW");

            return ((new JaroWinkler()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double JaroWinkler(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new JaroWinkler()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double ChapmanLengthDeviation(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new ChapmanLengthDeviation()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double ChapmanMeanLength(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new ChapmanMeanLength()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double QGramsDistance(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new QGramsDistance()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double BlockDistance(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new BlockDistance()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double CosineSimilarity(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new CosineSimilarity()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double DiceSimilarity(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new DiceSimilarity()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double EuclideanDistance(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new EuclideanDistance()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double JaccardSimilarity(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new JaccardSimilarity()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double MatchingCoefficient(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new MatchingCoefficient()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double MongeElkan(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new MongeElkan()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static double OverlapCoefficient(string firstWord, string secondWord)
        {
            if (string.IsNullOrWhiteSpace(firstWord) || string.IsNullOrWhiteSpace(secondWord))
                return 0;

            return ((new OverlapCoefficient()).GetSimilarity(firstWord.ToUpper(), secondWord.ToUpper()));
        }

        public static string SwahiliSoundex(string data, int length)
        {
            if (string.IsNullOrWhiteSpace(data) || data.Length == 0)
                return string.Empty;


            // Convert the word to all uppercase and .net string
            string word = ((string)data).ToUpper().Replace("'", "").Replace("NG", "MW");

            // Append the first character
            string value = word[0].ToString();
            value = value.Replace('L', 'R').Replace('N', 'M');



            // Make sure the word is at least two characters in length
            if (word.Length > 1)
            {

                // The current and previous character codes
                int prevCode = 0;
                int currCode = 0;

                // Loop through all the characters and convert them to the proper character code

                for (int i = 1; i < word.Length; i++)
                {
                    switch (word[i])
                    {
                        case 'A':
                        case 'E':
                        case 'I':
                        case 'O':
                        case 'U':
                        case 'H':
                        case 'W':
                        case 'Y':
                            currCode = 0;
                            break;
                        case 'B':
                        case 'F':
                        case 'P':
                        case 'V':
                            currCode = 1;
                            break;
                        case 'C':
                        case 'G':
                        case 'J':
                        case 'K':
                        case 'Q':
                        case 'S':
                        case 'X':
                        case 'Z':
                            currCode = 2;
                            break;
                        case 'D':
                        case 'T':
                            currCode = 3;
                            break;
                        case 'L':
                        case 'R':
                            currCode = 4;
                            break;
                        case 'M':
                        case 'N':
                            currCode = 5;
                            break;
                        //case 'R':
                        //      currCode = 6;
                        //      break;
                    }
                    // Add only if the current code is not the same as the last one and the current code is not 0 (a vowel)
                    if (currCode != prevCode && currCode != 0)
                        value += currCode;
                    // Set the new previous character code
                    prevCode = currCode;
                    // If the buffer size meets the length limit, then exit the loop
                    if (value.Length == length)
                        break;
                }
            }


            // Pad the buffer, if required
            value = value.PadRight(length, '0');
            // Return the value
            return (string)value;
        }

        public static string Soundex(string data)
        {
            StringBuilder result = new StringBuilder();
            string word = data.ToString().ToUpper();

            if (word != null && word.Length > 0)
            {
                string previousCode = "", currentCode = "",
                        currentLetter = "";

                result.Append(word.Substring(0, 1));

                for (int i = 1; i < word.Length; i++)
                {
                    currentLetter = word.Substring(i, 1).ToLower();
                    currentCode = "";

                    if ("bfpv".IndexOf(currentLetter) > -1)
                        currentCode = "1";
                    else if ("cgjkqsxz".IndexOf(currentLetter) > -1)
                        currentCode = "2";
                    else if ("dt".IndexOf(currentLetter) > -1)
                        currentCode = "3";
                    else if (currentLetter == "l")
                        currentCode = "4";
                    else if ("mn".IndexOf(currentLetter) > -1)
                        currentCode = "5";
                    else if (currentLetter == "r")
                        currentCode = "6";

                    if (currentCode != previousCode)
                        result.Append(currentCode);

                    if (result.Length == 4) break;

                    if (currentCode != "")
                        previousCode = currentCode;
                }
            }

            if (result.Length < 4)
                result.Append(new String('0', 4 - result.Length));

            return (string)(result.ToString().ToUpper());
        }

        public static string SwahiliSoundex2(string data)
        {
            StringBuilder result = new StringBuilder();
            string word = data.ToString().ToUpper().Replace("'", "").Replace("NG", "MW");


            if (word != null && word.Length > 0)
            {
                string previousCode = "", currentCode = "",
                        currentLetter = "";

                result.Append(word.Substring(0, 1));
                result = result.Replace('L', 'R').Replace('N', 'M');

                for (int i = 1; i < word.Length; i++)
                {
                    currentLetter = word.Substring(i, 1).ToLower();
                    currentCode = "";

                    if ("bfpv".IndexOf(currentLetter) > -1)
                        currentCode = "1";
                    else if ("cgjkqsxz".IndexOf(currentLetter) > -1)
                        currentCode = "2";
                    else if ("dt".IndexOf(currentLetter) > -1)
                        currentCode = "3";
                    else if ("rl".IndexOf(currentLetter) > -1)
                        currentCode = "4";
                    //else if (currentLetter == "l")
                    //    currentCode = "4";
                    else if ("mn".IndexOf(currentLetter) > -1)
                        currentCode = "5";
                    //else if (currentLetter == "r")
                    //    currentCode = "6";

                    if (currentCode != previousCode)
                        result.Append(currentCode);

                    if (result.Length == 4) break;

                    if (currentCode != "")
                        previousCode = currentCode;
                }
            }

            if (result.Length < 4)
                result.Append(new String('0', 4 - result.Length));

            return (string)(result.ToString().ToUpper());
        }

        public static int Difference(string data1, string data2)
        {

            if (string.IsNullOrWhiteSpace(data1) || string.IsNullOrWhiteSpace(data2))
                return 0;

            int result = 0;
            string soundex1 = (string)(Soundex(data1));
            string soundex2 = (string)(Soundex(data2));

            if (soundex1 == soundex2)
                result = 4;
            else
            {
                string sub1 = soundex1.Substring(1, 3);
                string sub2 = soundex1.Substring(2, 2);
                string sub3 = soundex1.Substring(1, 2);
                string sub4 = soundex1.Substring(1, 1);
                string sub5 = soundex1.Substring(2, 1);
                string sub6 = soundex1.Substring(3, 1);

                if (soundex2.IndexOf(sub1) > -1)
                    result = 3;
                else if (soundex2.IndexOf(sub2) > -1)
                    result = 2;
                else if (soundex2.IndexOf(sub3) > -1)
                    result = 2;
                else
                {
                    if (soundex2.IndexOf(sub4) > -1)
                        result++;

                    if (soundex2.IndexOf(sub5) > -1)
                        result++;

                    if (soundex2.IndexOf(sub6) > -1)
                        result++;
                }

                if (soundex1.Substring(0, 1) ==
                    soundex2.Substring(0, 1))
                    result++;
            }

            return (result == 0) ? 1 : result;
        }

        public static int SwahiliDifference(string data1, string data2, int length)
        {

            if (string.IsNullOrWhiteSpace(data1) || string.IsNullOrWhiteSpace(data2) || data1.ToString().Length == 0 || data2.ToString().Length == 0)
                return 0;

            if (data1 == data2)
                return 5;

            int result = 0;
            string soundex1 = (string)(SwahiliSoundex(data1, 4));
            string soundex2 = (string)(SwahiliSoundex(data2, 4));

            if (soundex1 == soundex2)
                result = 4;
            else
            {
                string sub1 = soundex1.Substring(1, 3);
                string sub2 = soundex1.Substring(2, 2);
                string sub3 = soundex1.Substring(1, 2);
                string sub4 = soundex1.Substring(1, 1);
                string sub5 = soundex1.Substring(2, 1);
                string sub6 = soundex1.Substring(3, 1);

                if (soundex2.IndexOf(sub1) > -1)
                    result = 3;
                else if (soundex2.IndexOf(sub2) > -1)
                    result = 2;
                else if (soundex2.IndexOf(sub3) > -1)
                    result = 2;
                else
                {
                    if (soundex2.IndexOf(sub4) > -1)
                        result++;

                    if (soundex2.IndexOf(sub5) > -1)
                        result++;

                    if (soundex2.IndexOf(sub6) > -1)
                        result++;
                }

                if (soundex1.Substring(0, 1) ==
                    soundex2.Substring(0, 1))
                    result++;
            }

            return (result == 0) ? 1 : result;
        }

        public static int SwahiliDifference2(string data1, string data2)
        {

            if (string.IsNullOrWhiteSpace(data1) || string.IsNullOrWhiteSpace(data2))
                return 0;

            int result = 0;
            string soundex1 = (string)(SwahiliSoundex2(data1));
            string soundex2 = (string)(SwahiliSoundex2(data2));

            if (soundex1 == soundex2)
                result = 4;
            else
            {
                string sub1 = soundex1.Substring(1, 3);
                string sub2 = soundex1.Substring(2, 2);
                string sub3 = soundex1.Substring(1, 2);
                string sub4 = soundex1.Substring(1, 1);
                string sub5 = soundex1.Substring(2, 1);
                string sub6 = soundex1.Substring(3, 1);

                if (soundex2.IndexOf(sub1) > -1)
                    result = 3;
                else if (soundex2.IndexOf(sub2) > -1)
                    result = 2;
                else if (soundex2.IndexOf(sub3) > -1)
                    result = 2;
                else
                {
                    if (soundex2.IndexOf(sub4) > -1)
                        result++;

                    if (soundex2.IndexOf(sub5) > -1)
                        result++;

                    if (soundex2.IndexOf(sub6) > -1)
                        result++;
                }

                if (soundex1.Substring(0, 1) ==
                    soundex2.Substring(0, 1))
                    result++;
            }

            return (result == 0) ? 1 : result;
        }
        #endregion

        #region Standardize strings

        public static string SwahiliStandardize(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                return name;

            name = name.ToUpper().Replace("'", "").Replace("L", "R");
            if (name.Length < 2)
                return name;

            if (name.Substring(0, 2) == "NG")
                name = "MW" + name.Substring(2, name.Length - 2);
            return name;
        }
        #endregion

    }
}
