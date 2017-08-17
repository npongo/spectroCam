#region Copyright
/*
 * The original .NET implementation of the SimMetrics library is taken from the Java
 * source and converted to NET using the Microsoft Java converter.
 * It is notclear who made the initial convertion to .NET.
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
    /// needlemanwunch implements an edit distance function
    /// </summary>
   
    sealed public class NeedlemanWunch : AbstractStringMetric {
        const double defaultGapCost = 2.0;
        const double defaultMismatchScore = 0.0;
        const double defaultPerfectMatchScore = 1.0;

        /// <summary>
        /// constructor
        /// </summary>
        public NeedlemanWunch() : this(defaultGapCost, new SubCostRange0To1()) {}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="costG">the cost of a gap</param>
        public NeedlemanWunch(double costG) : this(costG, new SubCostRange0To1()) {}

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="costG">the cost of a gap</param>
        /// <param name="costFunction">the cost function to use</param>
        public NeedlemanWunch(double costG, AbstractSubstitutionCost costFunction) {
            gapCost = costG;
            dCostFunction = costFunction;
        }

        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="costFunction">the cost function to use</param>
        public NeedlemanWunch(AbstractSubstitutionCost costFunction) : this(defaultGapCost, costFunction) {}

        /// <summary>
        /// the private cost function used in the levenstein distance.
        /// </summary>
        AbstractSubstitutionCost dCostFunction;
        /// <summary>
        /// a constant for calculating the estimated timing cost.
        /// </summary>
        double estimatedTimingConstant = 0.0001842F;
        /// <summary>
        /// the cost of a gap.
        /// </summary>
        double gapCost;

        /// <summary>
        /// gets the similarity of the two strings using Needleman Wunch distance.
        /// </summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <returns>a value between 0-1 of the similarity</returns>
        public override double GetSimilarity(string firstWord, string secondWord) {
            if ((firstWord != null) && (secondWord != null)) {
                double needlemanWunch = GetUnnormalisedSimilarity(firstWord, secondWord);
                double maxValue = Math.Max(firstWord.Length, secondWord.Length);
                double minValue = maxValue;
                if (dCostFunction.MaxCost > gapCost) {
                    maxValue *= dCostFunction.MaxCost;
                }
                else {
                    maxValue *= gapCost;
                }
                if (dCostFunction.MinCost < gapCost) {
                    minValue *= dCostFunction.MinCost;
                }
                else {
                    minValue *= gapCost;
                }
                if (minValue < defaultMismatchScore) {
                    maxValue -= minValue;
                    needlemanWunch -= minValue;
                }
                if (maxValue == defaultMismatchScore) {
                    return defaultPerfectMatchScore;
                }
                else {
                    return defaultPerfectMatchScore - needlemanWunch / maxValue;
                }
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
                double firstLength = firstWord.Length;
                double secondLength = secondWord.Length;
                return firstLength * secondLength * estimatedTimingConstant;
            }
            return defaultMismatchScore;
        }

        /// <summary> 
        /// gets the un-normalised similarity measure of the metric for the given strings.</summary>
        /// <param name="firstWord"></param>
        /// <param name="secondWord"></param>
        /// <returns> returns the score of the similarity measure (un-normalised)</returns>
        public override double GetUnnormalisedSimilarity(string firstWord, string secondWord) {
            if ((firstWord != null) && (secondWord != null)) {
                int n = firstWord.Length;
                int m = secondWord.Length;
                if (n == 0) {
                    return m;
                }
                if (m == 0) {
                    return n;
                }
                double[][] d = new double[n + 1][];
                for (int i = 0; i < n + 1; i++) {
                    d[i] = new double[m + 1];
                }
                for (int i = 0; i <= n; i++) {
                    d[i][0] = i;
                }

                for (int j = 0; j <= m; j++) {
                    d[0][j] = j;
                }

                for (int i = 1; i <= n; i++) {
                    for (int j = 1; j <= m; j++) {
                        double cost = dCostFunction.GetCost(firstWord, i - 1, secondWord, j - 1);
                        d[i][j] = MathFunctions.MinOf3(d[i - 1][j] + gapCost, d[i][j - 1] + gapCost, d[i - 1][j - 1] + cost);
                    }
                }

                return d[n][m];
            }
            return 0.0;
        }

        /// <summary>
        /// set/get the d(i,j) cost function.
        /// </summary>
        public AbstractSubstitutionCost DCostFunction { get { return dCostFunction; } set { dCostFunction = value; } }

        /// <summary>
        /// sets/gets the gap cost for the distance function.
        /// </summary>
        public double GapCost { get { return gapCost; } set { gapCost = value; } }

        /// <summary>
        /// returns the long string identifier for the metric.
        /// </summary>
        public override string LongDescriptionString {
            get {
                return
                    "Implements the Needleman-Wunch algorithm providing an edit distance based similarity measure between two strings";
            }
        }

        /// <summary>
        /// returns the string identifier for the metric.
        /// </summary>
        public override string ShortDescriptionString { get { return "NeedlemanWunch"; } }
    }
}