using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;
using Steema.TeeChart.Tools;

namespace StandardSeriesDemo.StandardSeries.Bars
{
    public class TwoColumnData
    {
        public TwoColumnData()
        {
            this.data = new List<TwoColumnDataItem>();
        }
        public List<TwoColumnDataItem> data { get; set; }
    }

    public class TwoColumnDataItem
    {
        public double Value1 { get; set; }
        public double Value2 { get; set; }
        public long OrderId { get; set; }
    }

    public partial class StackedBars2 : Form
    {
        TwoColumnData m_TwoColumnData;
        public StackedBars2()
        {
            InitializeComponent();
            DrawAmort();
        }

        private void ClearSeries()
        {
            Chart chart = tChart1.Chart;
            foreach (Series s in tChart1.Series)
            {
                s.Clear();
            }
        }
        public void DrawAmort()
        {

            
            Chart chart = tChart1.Chart;
            chart.Import.Template.CustomType = "Steema.TeeChart.Styles.AmortBar";
            chart.Import.Template.Load(  @"c:\temp\amort.ten");
            this.ClearSeries();


            Bar bar1 = (Bar)tChart1.Series[0];
            Bar bar2 = (Bar)tChart1.Series[1];
            Bar bar3 = (Bar)tChart1.Series[2];
            Bar bar4 = (Bar)tChart1.Series[3];

            bar1.Marks.Font.Color = Color.Black;

            //bar1.Marks.ArrowLength =
            //     bar2.Marks.ArrowLength =
            //          bar3.Marks.ArrowLength =
            //              bar4.Marks.ArrowLength = -30;

            RectangleTool toolOriginal = (RectangleTool)tChart1.Tools[0];
            RectangleTool toolCurrent = (RectangleTool)tChart1.Tools[1];

            //   toolOriginal.Shape.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            //   toolCurrent.Shape.Font.Brush.Color = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));

            int i = 0;
             m_TwoColumnData = new TwoColumnData();
            m_TwoColumnData.data.Add(new TwoColumnDataItem
            {
                OrderId=3,
                Value1=27.0,
                Value2=77.0
            });
            m_TwoColumnData.data.Add(new TwoColumnDataItem
            {
                OrderId = 2,
                Value1 = 31.0,
                Value2 = 0
            });

            m_TwoColumnData.data.Add(new TwoColumnDataItem
            {
                OrderId = 1,
                Value1 = 42.0,
                Value2 = 14.0
            });



            foreach (TwoColumnDataItem item in m_TwoColumnData.data)
            {
                Bar bar = (Bar)tChart1.Series[i];
                bar.Add(m_TwoColumnData.data[i].Value1);
                bar.Add(m_TwoColumnData.data[i].Value2);
                bar.Color = DVPublicConstants.GetChartColorByOrderId(m_TwoColumnData.data[i].OrderId - 1);
                i++;


                // Can not set this value in Ten File
                bar.Marks.TextAlign = StringAlignment.Center;
                bar.MarksOnBar = true;
                bar.MarksLocation = MarksLocation.Start;
            }

            tChart1.Axes.Bottom.SetMinMax(1, 2);
            tChart1.GetNextAxisLabel += AmortizationGraphTen_GetNextAxisLabel;
            tChart1.GetAxisLabel += AmortizationGraphTen_GetAxisLabel;
            tChart1.AfterDraw += AmortizationGraphTen_AfterDraw;

            bar1.GetSeriesMark += AmortizationGraphTen_bar_GetSeriesMark;
            bar2.GetSeriesMark += AmortizationGraphTen_bar_GetSeriesMark;
            bar3.GetSeriesMark += AmortizationGraphTen_bar_GetSeriesMark;
            bar4.GetSeriesMark += AmortizationGraphTen_bar_GetSeriesMark;



            tChart1.Draw();
        }

        void AmortizationGraphTen_bar_GetSeriesMark(Series series, GetSeriesMarkEventArgs e)
        {
            double d = 5.0;
            if (Convert.ToDouble (e.MarkText) < d)
            {
                e.MarkText = string.Empty;
            }

            Bar bar1 = (Bar)series;
            bar1.Marks.Font.Color = DVPublicConstants.GetForeColorByBGColor(bar1.ValueColor(e.ValueIndex));
        }

        void AmortizationGraphTen_GetAxisLabel(object sender, GetAxisLabelEventArgs e)
        {
            if (((Steema.TeeChart.Axis)sender).Equals(tChart1.Axes.Right))
            {
                if (string.Equals(e.LabelText, "100", StringComparison.OrdinalIgnoreCase))
                {
                    e.LabelText = "100 %";
                }
            }
        }

        void AmortizationGraphTen_AfterDraw(object sender, Graphics3D g)
        {
            TChart tChart1 = (TChart)sender;

            #region Original Code
            //try
            //{
            //    Bar bar1 = (Bar)tChart1.Series[0];
            //    Bar bar2 = (Bar)tChart1.Series[1];
            //    Bar bar3 = (Bar)tChart1.Series[2];
            //    Bar bar4 = (Bar)tChart1.Series[3];


            //    Rectangle barBound = bar1.BarBounds;


            //    Bar barT = bar2;

            //    double cummulative1 = 0.0;
            //    double cummulative2 = 0.0;
            //    for (int k = 0; k < m_TwoColumnData.data.Count;k++ )
            //    {
            //        Bar bar = (Bar)tChart1.Series[k];

            //        Point p3 = bar.ValuePointToScreenPoint(0, cummulative1);
            //        Point p4 = bar.ValuePointToScreenPoint(1, cummulative2);


            //        cummulative1 = cummulative1+m_TwoColumnData.data[k].Value1;
            //        cummulative2 = cummulative2+m_TwoColumnData.data[k].Value2;


            //        Point p1 = bar.ValuePointToScreenPoint(0, cummulative1);
            //        Point p2 = bar.ValuePointToScreenPoint(1, cummulative2 );


            //        Point[] curvePoints = { p1,p3,p4,p2,p1};

            //        Color Soft = Color.FromArgb(80, bar.Color);
            //        SolidBrush blueBrush = new SolidBrush(Soft);

            //        g.GDIplusCanvas.FillPolygon(blueBrush, curvePoints);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    // swallow this exception if point does not exists
            //}

            #endregion

            // http://www.teechart.net/support/viewtopic.php?f=4&t=16229&sid=7f0cd8028b1e6b74323d18168b274239
            // To Draw shaded area between bar , refer to link above
            for (int s = 0; s < m_TwoColumnData.data.Count; s++)
            {
                Point[] poly = new Point[4];
                Color Soft = Color.Wheat;
                for (int i = 0; i < tChart1.Series[0].Count; i++)
                {
                    Bar aBar = ((Bar)tChart1[s]);
                    {
                        if (i == 0)
                        {
                            if (s == 0)
                                poly[0] = new Point(aBar.CalcXPos(i) + aBar.BarBounds.Width, tChart1.Axes.Bottom.Position + tChart1.Height);
                            else
                                poly[0] = new Point(aBar.CalcXPos(i) + aBar.BarBounds.Width, ((Bar)tChart1[s - 1]).CalcYPos(i));

                            poly[1] = new Point(aBar.CalcXPos(i) + aBar.BarBounds.Width, aBar.CalcYPos(i));
                        }
                        else
                        {
                            poly[2] = new Point(aBar.CalcXPos(i), aBar.CalcYPos(i));
                            if (s == 0)
                                poly[3] = new Point(aBar.CalcXPos(i), tChart1.Axes.Bottom.Position + tChart1.Height);
                            else
                                poly[3] = new Point(aBar.CalcXPos(i), ((Bar)tChart1[s - 1]).CalcYPos(i));
                        }
                    }
                    g.Pen.Visible = false;
                    g.Brush.Color = aBar.Color;
                    g.Brush.Transparency = 50;
                }

                //   g.ClipRectangle(tChart1.Chart.ChartRect); //don't paint polygon out of bounds on zooms
                g.Polygon(poly);
                //   g.UnClip();

                //break;
            }
        }

        private void AmortizationGraphTen_GetNextAxisLabel(object sender, GetNextAxisLabelEventArgs e)
        {
            if (((Steema.TeeChart.Axis)sender).Equals(tChart1.Axes.Right)) e.Stop = false;
            switch (e.LabelIndex)
            {
                case 0: e.LabelValue = 25; break;
                case 1: e.LabelValue = 50; break;
                case 2: e.LabelValue = 75; break;
                case 3: e.LabelValue = 100; break;
                default: e.Stop = true; break;
            }
        }
    }

    /// </summary>
    public static class DVPublicConstants
    {
        public static Color ColorWildBlueYonder = Color.FromArgb(127, 153, 185); // 7f99b9
        public static Color ColorNightShadz = Color.FromArgb(178, 53, 94); //#B2355E
        public static Color ColorEarlsGreen = Color.FromArgb(181, 188, 53);//#B5BC35
        public static Color ColorCreamCanYellow = Color.FromArgb(244, 197, 77); //#F4C54D        
        public static Color ColorMingGreen = Color.FromArgb(56, 129, 133); //#388185
        public static Color ColorJaffaOrange = Color.FromArgb(237, 133, 64); //#ed8540
        public static Color ColorIronGray = Color.FromArgb(209, 211, 212);//D1D3D4 

        public static Color ColorLightBlue = Color.FromArgb(170, 188, 209);//#aabcd1

        public static Color ColorWhite = Color.FromArgb(255, 255, 255);//#ffffff


        /* Heat map */
        public static Color ColorHeatMap_Pink = Color.FromArgb(246, 151, 133); // #f69785
        //public static Color ColorHeatMap_Orange = Color.FromArgb(237, 145, 68); // #ed9144
        //public static Color ColorHeatMap_LightOrange = Color.FromArgb(245, 194, 152); // #f5c298
        public static Color ColorHeatMap_Orange = Color.FromArgb(239, 62, 104); // #ef3e42
        public static Color ColorHeatMap_LightOrange = Color.FromArgb(244, 195, 152); // #f4c398
        public static Color ColorHeatMap_Green = Color.FromArgb(202, 215, 145); // #cad791
        public static Color ColorRed = Color.FromArgb(238, 58, 67);//ee3a43


        public static int DVCommentWidth = 505;
        public static int DVHeader = 15;


        // Specially Services Loans
        // Morningstar Watchlisted Loans (Master Serviced)








        public static Color GetChartColorByOrderId(long orderid)
        {
            Dictionary<long, Color> list = new Dictionary<long, Color>();
            list.Add(0, DVPublicConstants.ColorWildBlueYonder);
            list.Add(1, DVPublicConstants.ColorNightShadz);
            list.Add(2, DVPublicConstants.ColorEarlsGreen);
            list.Add(3, DVPublicConstants.ColorCreamCanYellow);
            list.Add(4, DVPublicConstants.ColorMingGreen);
            list.Add(5, DVPublicConstants.ColorJaffaOrange);
            list.Add(6, DVPublicConstants.ColorIronGray);
            list.Add(7, DVPublicConstants.ColorLightBlue);



            // Database order starts from 1
            list.Add(99, DVPublicConstants.ColorWhite);
            list.Add(100, DVPublicConstants.ColorWhite);

            Color outColor = Color.White;
            bool ret = list.TryGetValue(orderid, out outColor);

            return ret ? outColor : DVPublicConstants.ColorWhite;

        }


        public static Color GetForeColorByBGColor(Color bgColor)
        {
            Dictionary<Color, Color> list = new Dictionary<Color, Color>();
            list.Add(DVPublicConstants.ColorWildBlueYonder, Color.White);
            list.Add(DVPublicConstants.ColorNightShadz, Color.White);
            list.Add(DVPublicConstants.ColorEarlsGreen, Color.Black);
            list.Add(DVPublicConstants.ColorCreamCanYellow, Color.Black);
            list.Add(DVPublicConstants.ColorMingGreen, Color.Black);
            list.Add(DVPublicConstants.ColorJaffaOrange, Color.Black);
            list.Add(DVPublicConstants.ColorLightBlue, Color.White);
            list.Add(DVPublicConstants.ColorIronGray, Color.Black);


            // Database order starts from 1
            list.Add(DVPublicConstants.ColorWhite, Color.Black);
            list.Add(Color.Black, Color.White);

            Color outColor = Color.Black;
            bool ret = list.TryGetValue(bgColor, out outColor);

            return ret ? outColor : Color.Black;

        }

        public static ChartSetting DonutChartSetting = new ChartSetting
        {
            Width = 77 * 3,
            Height = 77 * 3
        };

    }

    public class ChartSetting
    {
        public int Width { get; set; }
        public int Height { get; set; }
        public string TenFileName { get; set; }
        public string TenFileNamePath
        {
            get
            {
                return string.Format("{0}/{1}", "", this.TenFileName);
            }
        }

    }
}
