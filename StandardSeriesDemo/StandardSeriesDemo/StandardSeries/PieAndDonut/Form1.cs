
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DealViewChartLibrary;
using Steema.TeeChart;
using Steema.TeeChart.Drawing;
using Steema.TeeChart.Styles;

namespace StandardSeriesDemo.StandardSeries.PieAndDonut
{
    public partial class Form1 : Form
    {

        DonutChartData m_IChartData;
        public Form1()
        {
            InitializeComponent();
        }

        private void DonutChart_Load(object sender, EventArgs e)
        {
            m_IChartData = new DonutChartData();

            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId=1,
                Text="Long Text 1",
                Value=14.93m
            });
            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId = 1,
                Text = "Long Text 1",
                Value = 14.93m
            });
            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId =2,
                Text = "Long Text 2",
                Value = 10.5m
            });
            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId = 3,
                Text = "Long Text 1",
                Value = 7.11m
            });
            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId = 4,
                Text = "Long Text 1",
                Value = 5.41m
            });
            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId =5,
                Text = "Long Text 1",
                Value = 4.19m
            });
            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId = 6,
                Text = "Long Text 1",
                Value = 50.66m
            });
            m_IChartData.Data.Add(new DonutChartItem
            {
                OrderId = 7,
                Text = "Long Text 1",
                Value = 7.101m
            });


            DoughnutChartTen(m_IChartData);
        }

        #region Donut Chart
        private void DoughnutChartTen(DonutChartData donutData)
        {
            Chart chart = tChart1.Chart;
            chart.Import.Template.Load(@"c:\temp\donut.ten");
            this.ClearSeries();

            Donut donut1 = (Donut)tChart1.Series[0];

            if (donutData != null)
            {
                foreach (DonutChartItem chi in donutData.Data)
                {
                    if (chi.Value <= 0.0M)
                    {
                        // do not add Zero value 
                        // chart can not handle zero
                    }
                    else
                    {
                        donut1.Add(Math.Round(Decimal.ToDouble(chi.Value), 0), DVPublicConstants.GetChartColorByOrderId(chi.OrderId - 1));
                    }
                }
            }
            else
            {
                // donut1.FillSampleValues(8);
            }

            donut1.Marks.Callout.Distance = 60;
            donut1.Marks.Font.Color = Color.White;


            Steema.TeeChart.Tools.Annotation tAnnot1 = (Steema.TeeChart.Tools.Annotation)tChart1.Tools[0];
            Steema.TeeChart.Tools.Annotation tAnnot2 = (Steema.TeeChart.Tools.Annotation)tChart1.Tools[1];

            // this is Ten file config center position
            tAnnot1.Text = donutData.ChartHeader1;
            tAnnot2.Text = donutData.ChartHeader2;




            // To Avoid Cut off at border set radius and -5 for slight margin
            donut1.CustomXRadius = DVPublicConstants.DonutChartSetting.Width / 2 - 5;
            donut1.CustomYRadius = DVPublicConstants.DonutChartSetting.Height / 2 - 5;


            donut1.GetSeriesMark += donut1_GetSeriesMark;
            donut1.BeforeDrawValues += donut1_BeforeDrawValues;


            donut1.MarksPie.VertCenter = true;
            // http://stackoverflow.com/questions/20545931/teechart-pie-or-donut-labelling-the-inside-pie-of-a-donut

            //donut1.Marks.Visible = true;
            //donut1.Marks.Transparent = true;
            //donut1.Marks.Arrow.Visible = false;
            //donut1.Marks.ArrowLength = -10;

            tChart1.Draw();


        }

        void donut1_BeforeDrawValues(object sender, Graphics3D g)
        {
            Donut donut1 = (Donut)sender;


            Steema.TeeChart.Tools.Annotation tAnnot1 = (Steema.TeeChart.Tools.Annotation)donut1.Chart.Tools[0];
            Steema.TeeChart.Tools.Annotation tAnnot2 = (Steema.TeeChart.Tools.Annotation)donut1.Chart.Tools[1];

            // tAnnot1 = this is Ten file config center position
            tAnnot2.Left = tAnnot1.Left;
            tAnnot2.Top = tAnnot1.Top + 40;
        }

        void donut1_GetSeriesMark(Series series, GetSeriesMarkEventArgs e)
        {
            double d = Convert.ToDouble(ConfigurationManager.AppSettings["DONUT_CHART_MIN_MARK_VALUE"]);
            if (Convert.ToDouble(e.MarkText) < d)
            {
                e.MarkText = string.Empty;
            }

            Donut donut1 = (Donut)series;
            DonutChartData donutData = (DonutChartData)m_IChartData;

            //if (donutData != null)
            //{
            //    foreach (DonutChartItem chi in donutData.Data)
            //    {
            //        if (Math.Round(Decimal.ToDouble(chi.Value), 0) == ParseUtility.TryParseInt(e.MarkText)
            //            && String.Equals("Vacant", chi.Text, StringComparison.OrdinalIgnoreCase))
            //        {
            //            donut1.Marks.Font.Color = Color.Black;
            //        }
            //        else
            //        {
            //            donut1.Marks.Font.Color = Color.White;
            //        }
            //    }
            //}

            donut1.Marks.Font.Color = DVPublicConstants.GetForeColorByBGColor(donut1.ValueColor(e.ValueIndex));
        }

        #endregion
        private void tChart1_DoubleClick(object sender, EventArgs e)
        {
          (sender as Steema.TeeChart.TChart).Zoom.Active = false;
          (sender as Steema.TeeChart.TChart).ShowEditor();
          (sender as Steema.TeeChart.TChart).Chart.CancelMouse = true;
          (sender as Steema.TeeChart.TChart).Zoom.Active = true;
        }

        private void ClearSeries()
        {
            Chart chart = tChart1.Chart; 
            foreach (Series s in tChart1.Series)
            {
                s.Clear();
            }
        }
    }
}


namespace DealViewChartLibrary
{


    public enum ChartType : int
    {
        TopLoansAndCohort = 0,
        MorningStarTrendAnalysis = 1,
        DonutChart = 2,
        AmortizationType = 3
    }

    public interface IChartData
    {
    }

    public class DonutChartItem
    {
        public string Text { get; set; }
        public decimal Value { get; set; }
        public long OrderId { get; set; }
    }
    public class DonutChartData : IChartData
    {
        public DonutChartData()
        {
            this.Data = new List<DonutChartItem>();
            this.ChartHeader1 = string.Empty;
            this.ChartHeader2 = string.Empty;
        }
        public List<DonutChartItem> Data { get; set; }

        public string ChartHeader1 { get; set; }
        public string ChartHeader2 { get; set; }
    }




    /// <summary>
    /// Summary description for PublicConstants
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