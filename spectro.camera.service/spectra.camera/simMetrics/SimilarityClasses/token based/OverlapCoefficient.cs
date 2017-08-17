#region Copyright
/*
 * The original .NET implementation of the SimMetrics library is taken from the Java
 * source and converted to NET using the Microsoft Java converter.
 * It is not clear who made the initial convertion to .NET.
 * 
 * This updated version has started with the 1.0 .NET release of SimMetrics and used
 * FxCop (http://www.gotdotnet.com/team/fxcop/) to highlight areas where changes needed 
 * to be made to the converted code.
 * 
 * this version with updates Copyright (c) 2006 Chris Parkinson.
 * 
 * For any queries on the .NET version please contact me through the 
 * sourceforge web address.
 * 
 * SimMetrics - SimMetrics is a java library of Similarity or Distance
 * Metrics, e.g. Levenshtein Distance, that provide float based similarity
 * measures between string Data. All metrics return consistant measures
 * rather than unbounded similarity scores.
 *
 * Copyright (C) 2005 Sam Chapman - Open Source Release v1.1
 *
 * Please Feel free to contact me about this library, I would appreciate
 * knowing quickly what you wish to use it for and any criticisms/comments
 * upon the SimMetric library.
 *
 * email:       s.chapman@dcs.shef.ac.uk
 * www:         http://www.dcs.shef.ac.uk/~sam/
 * www:         http://www.dcs.shef.ac.uk/~sam/stringmetrics.html
 *
 * address:     Sam Chapman,
 *              Department of Computer Science,
 *              University of Sheffield,
 *              Sheffield,
 *              S. Yorks,
 *              S1 4DP
 *              United Kingdom,
 *
 * This program is free software; you can redistribute it and/or modify it
 * under the terms of the GNU General Public License as published by the
 * Free Software Foundation; either version 2 of the License, or (at your
 * option) any later version.
 *
 * This program is distributed in the hope that it will be useful, but
 * WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY
 * or FITNESS FOR A PARTICULAR PURPOSE. See the GNU General Public License
 * for more details.
 *
 * You should have received a copy of the GNU General Public License along
 * with this program; if not, write to the Free Software Foundation, Inc.,
 * 59 Temple Place, Suite 330, Boston, MA 02111-1307 USA
 */
#endregion

namespace npongo.wpf.simMetrics.SimMetricsMetricUtilities
{
    using System;
    using npongo.wpf.simMetrics.SimMetricsApi;
    using npongo.wpf.simMetrics.SimMetricsUtilities;

    /// <summary>
    /// 
    /// </summary>
   
    sealed public class OverlapCoefficient : AbstractStringMetric {
        const double defaultMismatchScore = 0.0;

        /// <summary>
        /// constructor
        /// </summary>
        public OverlapCoefficient() : this(new TokeniserWhitespace()) {}

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="tokeniserToUse">the tokeniser to use should a different tokeniser be required</param>
        public OverlapCoefficient(ITokeniser tokeniserToUse) {
            tokeniser = tokeniserToUse;
            tokenUtilities = new TokeniserUtilities<string>();
        }

        /// <summary>
        /// a constant for calculating the estimated timing cost.
        /// </summary>
        double estimatedTimingConstant = 0.00014F;
        TokeniserUtilities<string> tokenUtilities;
        /// <summary>
        /// private tokeniser for tokenisation of the query strings.
        /// </summary>
        ITokeniser tokeniser;

        /// <summary>
        /// gets the similarity of the two strings using OverlapCoefficient
        /// </summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <returns>a value between 0-1 of the similarity</returns>
        /// <remarks>overlap_coefficient(q,r) = ( | q and r | ) / min{ | q | , | r | }.</remarks>
        public override double GetSimilarity(string firstWord, string secondWord) {
            if ((firstWord != null) && (secondWord != null)) {
                //Collection<string> allTokens =
                tokenUtilities.CreateMergedSet(tokeniser.Tokenize(firstWord), tokeniser.Tokenize(secondWord));
                return
                    tokenUtilities.CommonSetTerms() /
                    (double)Math.Min(tokenUtilities.FirstSetTokenCount, tokenUtilities.SecondSetTokenCount);
            }
            return defaultMismatchScore;
        }

        /// <summary> gets a div class xhtml similarity explaining the operation of the metric.</summary>
        /// <param name="firstWord">string 1</param>
        /// <param name="secondWord">string 2</param>
        /// <returns> a div class html section detailing the metric operation.</returns>
        public override string GetSimilarityExplained(string firstWord, string secondWord) {
            throw new NotImplementedException();
        }

        /// <summary>
        /// gets the estimated time in milliseconds it takes to perform a similarity timing.
        /// </summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <returns>the estimated time in milliseconds taken to perform the similarity measure</returns>
        public override double GetSimilarityTimingEstimated(string firstWord, string secondWord) {
            if ((firstWord != null) && (secondWord != null)) {
                double firstTokens = tokeniser.Tokenize(firstWord).Count;
                double secondTokens = tokeniser.Tokenize(secondWord).Count;
                return firstTokens * secondTokens * estimatedTimingConstant;
            }
            return 0.0;
        }

        /// <summary> 
        /// gets the un-normalised similarity measure of the metric for the given strings.</summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <returns> returns the score of the similarity measure (un-normalised)</returns>
        public override double GetUnnormalisedSimilarity(string firstWord, string secondWord) {
            return GetSimilarity(firstWord, secondWord);
        }

        /// <summary>
        ///  returns the long string identifier for the metric.
        /// </summary>
        public override string LongDescriptionString {
            get {
                return
                    "Implements the Overlap Coefficient algorithm providing a similarity measure between two string where it is determined to what degree a string is a subset of another";
            }
        }

        /// <summary>
        /// returns the string identifier for the metric.
        /// </summary>
        public override string ShortDescriptionString { get { return "OverlapCoefficient"; } }
    }
}