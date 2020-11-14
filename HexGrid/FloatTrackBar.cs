using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HexGrid
{
    public class FloatTrackBar : TrackBar
    {
        private double precision = 0.01;

        public double Precision
        {
            get { return precision; }
            set
            {
                precision = value;
                // todo: update the 5 properties below
            }
        }
        public new double LargeChange
        { get { return base.LargeChange * precision; } set { base.LargeChange = (int)(value / precision); } }
        public new double Maximum
        { get { return base.Maximum * precision; } set { base.Maximum = (int)(value / precision); } }
        public new double Minimum
        { get { return base.Minimum * precision; } set { base.Minimum = (int)(value / precision); } }
        public new double SmallChange
        { get { return base.SmallChange * precision; } set { base.SmallChange = (int)(value / precision); } }
        public new double Value
        { get { return base.Value * precision; } set { base.Value = (int)(value / precision); } }
    }
}
