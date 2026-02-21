#region Using declarations
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Windows.Media;
using System.Xml.Serialization;
using NinjaTrader.Gui;
using NinjaTrader.Gui.Chart;
using NinjaTrader.Data;
using NinjaTrader.NinjaScript;
using NinjaTrader.Core.FloatingPoint;
#endregion

namespace NinjaTrader.NinjaScript.Indicators
{
    [Description("Dual moving average warm-up indicator. Plots a fast and slow MA; colors the fast MA green when trending up and red when trending down.")]
    public class WarmUpMA : Indicator
    {
        private SMA fastSMA;
        private SMA slowSMA;

        protected override void OnStateChange()
        {
            if (State == State.SetDefaults)
            {
                Description = "Dual SMA warm-up indicator with trend coloring";
                Name        = "WarmUpMA";

                FastPeriod  = 9;
                SlowPeriod  = 21;

                IsOverlay          = true;
                DisplayInDataBox   = true;
                DrawOnPricePanel   = true;
                ScaleJustification = ScaleJustification.Right;

                // Plot 0 = Fast MA, Plot 1 = Slow MA
                AddPlot(new Stroke(Brushes.DodgerBlue, 2), PlotStyle.Line, "FastMA");
                AddPlot(new Stroke(Brushes.Orange,     2), PlotStyle.Line, "SlowMA");
            }
            else if (State == State.DataLoaded)
            {
                fastSMA = SMA(FastPeriod);
                slowSMA = SMA(SlowPeriod);
            }
        }

        protected override void OnBarUpdate()
        {
            if (CurrentBar < SlowPeriod)
                return;

            double fast = fastSMA[0];
            double slow = slowSMA[0];

            Values[0][0] = fast;
            Values[1][0] = slow;

            // Color the fast MA based on trend direction
            if (fast > slow)
                PlotBrushes[0][0] = Brushes.LimeGreen;
            else if (fast < slow)
                PlotBrushes[0][0] = Brushes.Red;
            else
                PlotBrushes[0][0] = Brushes.DodgerBlue;
        }

        #region Properties

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "Fast Period", Description = "Period for the fast moving average", Order = 1, GroupName = "Parameters")]
        public int FastPeriod { get; set; }

        [NinjaScriptProperty]
        [Range(1, int.MaxValue)]
        [Display(Name = "Slow Period", Description = "Period for the slow moving average", Order = 2, GroupName = "Parameters")]
        public int SlowPeriod { get; set; }

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> FastMA => Values[0];

        [Browsable(false)]
        [XmlIgnore]
        public Series<double> SlowMA => Values[1];

        #endregion
    }
}
