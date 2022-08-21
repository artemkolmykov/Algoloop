/*
 * QUANTCONNECT.COM - Democratizing Finance, Empowering Individuals.
 * Lean Algorithmic Trading Engine v2.0. Copyright 2014 QuantConnect Corporation.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
*/

using System.Collections.Generic;
using QuantConnect.Orders;

namespace QuantConnect.Algorithm.CSharp
{
    /// <summary>
    /// Regression algorithm reproducing issue #5160 where delisting order would be cancelled because it was placed at the market close on the delisting day,
    /// in the case of daily resolution.
    /// </summary>
    public class DelistingFutureOptionDailyRegressionAlgorithm : DelistingFutureOptionRegressionAlgorithm
    {
        protected override Resolution Resolution => Resolution.Daily;

        protected override void PlaceOrder(Symbol symbol)
        {
            // We place a limit order because on daily resolution, data may come at a time when market is closed, so market orders are not allowed.
            // Also, we use a very high limit price to ensure the order is filled right away with the next bar.
            LimitOrder(symbol, 1, Securities[symbol].AskPrice * 2m);
        }

        /// <summary>
        /// Data Points count of all timeslices of algorithm
        /// </summary>
        public override long DataPoints => 13223;

        /// <summary>
        /// This is used by the regression test system to indicate what the expected statistics are from running the algorithm
        /// </summary>
        public override Dictionary<string, string> ExpectedStatistics => new Dictionary<string, string>
        {
            {"Total Trades", "16"},
            {"Average Win", "0.01%"},
            {"Average Loss", "-0.02%"},
            {"Compounding Annual Return", "-0.154%"},
            {"Drawdown", "0.200%"},
            {"Expectancy", "-0.708"},
            {"Net Profit", "-0.155%"},
            {"Sharpe Ratio", "-1.144"},
            {"Probabilistic Sharpe Ratio", "0.000%"},
            {"Loss Rate", "80%"},
            {"Win Rate", "20%"},
            {"Profit-Loss Ratio", "0.46"},
            {"Alpha", "-0.001"},
            {"Beta", "-0"},
            {"Annual Standard Deviation", "0.001"},
            {"Annual Variance", "0"},
            {"Information Ratio", "-1.078"},
            {"Tracking Error", "0.107"},
            {"Treynor Ratio", "2.447"},
            {"Total Fees", "$19.76"},
            {"Estimated Strategy Capacity", "$2400000.00"},
            {"Lowest Capacity Asset", "DC V5E8PHPRCHJ8|DC V5E8P9SH0U0X"},
            {"Fitness Score", "0"},
            {"Kelly Criterion Estimate", "0"},
            {"Kelly Criterion Probability Value", "0"},
            {"Sortino Ratio", "-0.381"},
            {"Return Over Maximum Drawdown", "-0.995"},
            {"Portfolio Turnover", "0"},
            {"Total Insights Generated", "0"},
            {"Total Insights Closed", "0"},
            {"Total Insights Analysis Completed", "0"},
            {"Long Insight Count", "0"},
            {"Short Insight Count", "0"},
            {"Long/Short Ratio", "100%"},
            {"Estimated Monthly Alpha Value", "$0"},
            {"Total Accumulated Estimated Alpha Value", "$0"},
            {"Mean Population Estimated Insight Value", "$0"},
            {"Mean Population Direction", "0%"},
            {"Mean Population Magnitude", "0%"},
            {"Rolling Averaged Population Direction", "0%"},
            {"Rolling Averaged Population Magnitude", "0%"},
            {"OrderListHash", "c545276e7159e2b6fd1202e5a23b6190"}
        };
    }
}
