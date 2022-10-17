using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace Program
{
    public partial class Form1 : Form
    {
        TideData tideData;
        public Form1()
        {
            InitializeComponent();
            tideData = new TideData();
            tideData.Request();
            Generate();
        }

        public void Generate()
        {
            var objChart = chart.ChartAreas[0];

            //TimeDate Axis X
            objChart.AxisX.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Days;
            objChart.AxisX.Minimum = tideData.response.meta.start.ToOADate();
            objChart.AxisX.Maximum = tideData.response.meta.end.ToOADate();

            //Tide Axis Y
            objChart.AxisY.IntervalType = System.Windows.Forms.DataVisualization.Charting.DateTimeIntervalType.Number;
            objChart.AxisY.Minimum = -3;
            objChart.AxisY.Maximum = 3;

            //Clear
            chart.Series.Clear();

            Series newSeries = new Series("TideChart");
            newSeries.ChartType = SeriesChartType.Spline;
            newSeries.BorderWidth = 2;
            newSeries.Color = Color.IndianRed;
            newSeries.XValueType = ChartValueType.DateTime;
            chart.Series.Add(newSeries);

            for (int i = 0; i < tideData.response.data.Count; i++)
            {
                chart.Series[0].Points.AddXY(tideData.response.data[i].time, tideData.response.data[i].height);
                Refresh();

            }
        }

    }
}
