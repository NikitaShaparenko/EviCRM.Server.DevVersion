using Blazorise.Charts;
using EviCRM.Server.Pages.Charts.ChartListInterop;

namespace EviCRM.Server.Pages.Statistics
{
    using EviCRM.Server.Core;
    using Majorsoft.Blazor.Components.Notifications;


    using Majorsoft.Blazor.Extensions.BrowserStorage;
    using Majorsoft.Blazor.Components.GdprConsent;
    using Majorsoft.Blazor.Components;
    using Microsoft.AspNetCore.Components;
    using Microsoft.JSInterop;
    using Microsoft.AspNetCore.Components.Authorization;

    using Blazorise.Charts;
    using EviCRM.Server.Models.Statistics;
    using ChartJs.Blazor.Util;
    using EviCRM.Server.Pages.Charts.ChartListInterop;
    using EviCRM.Server.Core.EntityFramework;

    public partial class StatisticsTasks
    {
        public List<StatisticsChartElement> SortByIncreaseAvgValue(List<StatisticsChartElement> originalList)
        {
            return originalList.OrderBy(x => x.get_personaltasks_Mark() + x.get_totaltasks_Mark()).ThenBy(x => x.user_fullname).ToList();
        }
        public List<StatisticsChartElement> SortByDecreaseAvgValue(List<StatisticsChartElement> originalList)
        {
            return originalList.OrderByDescending(x => x.get_personaltasks_Mark() + x.get_totaltasks_Mark()).ThenBy(x => x.user_fullname).ToList();
        }

        public List<StatisticsChartElement> SortByIncreaseNameValue(List<StatisticsChartElement> originalList)
        {
            return originalList.OrderBy(x => x.user_fullname).ToList();
        }
        public List<StatisticsChartElement> SortByDecreaseNameValue(List<StatisticsChartElement> originalList)
        {
            return originalList.OrderByDescending(x => x.user_fullname).ToList();
        }

        bool isForwardOrder = true;

        public void ModelSort()
        {
            List<StatisticsChartElement> svmp_main_dubler = new List<StatisticsChartElement>();

            foreach (var elem in statistics_arr)
            {
                svmp_main_dubler.Add(elem);
            }

            isForwardOrder = !isForwardOrder;

            svmp_main.Clear();
            StateHasChanged();
            if (isForwardOrder)
            {
                statistics_arr = SortByIncreaseAvgValue(svmp_main_dubler);
            }
            else
            {
                statistics_arr = SortByDecreaseAvgValue(svmp_main_dubler);
            }
            StateHasChanged();
        }

        public void ModelSort(string field)
        {
            List<StatisticsChartElement> svmp_main_dubler = new List<StatisticsChartElement>();

            foreach (var elem in statistics_arr)
            {
                svmp_main_dubler.Add(elem);
            }

            isForwardOrder = !isForwardOrder;

            statistics_arr.Clear();
            StateHasChanged();
            if (isForwardOrder)
            {
                statistics_arr = SortByIncreaseNameValue(svmp_main_dubler);
            }
            else
            {
                statistics_arr = SortByDecreaseNameValue(svmp_main_dubler);
            }
            StateHasChanged();
        }

        public void ModelSort(bool isForceForward)
        {
            List<StatisticsChartElement> svmp_main_dubler = new List<StatisticsChartElement>();

            foreach (var elem in statistics_arr)
            {
                svmp_main_dubler.Add(elem);
            }

            svmp_main.Clear();
            StateHasChanged();
            statistics_arr = SortByDecreaseAvgValue(svmp_main_dubler);

            StateHasChanged();
        }
       

      

        bool isModalPieOpen { get; set; }
        bool modalAllow { get; set; }

        bool personal { get; set; }

        DateTime dt_start { get; set; }

        DateTime dt_end { get; set; }
      
        public enum StatWindow
        {
            personal,
            common
        };

        public enum InterpolationCats
        {
            disabled,
            enabled,
            stepbefore,
            stepafter,
            stepmiddle
        };

        public bool pageLoaded { get; set; }

        public string InterpolationCatsString { get; set; }

       

        public string StatWindowString { get; set; }
        public string StatWindowString2 { get; set; }

        public string isPieString
        {
            get
            {
                if (isPie)
                {
                    return "True";
                }
                else
                {
                    return "False";
                }
            }
            set
            {
                if (value == "True")
                {
                    isPie = true;
                    StateHasChanged();
                }
                else
                {
                    isPie = false;
                    StateHasChanged();
                }
            }

        }

        public bool isTop { get; set; }

        public bool isOnlyWithMarks { get; set; }

        public int TopNumber { get; set; }

        public int svp_index { get; set; }

        public List<StatisticsViewModel_Pie> svmp_main = new List<StatisticsViewModel_Pie>();

        double avg_company { get; set; }

        double academic_perfomace_company { get; set; }

        double quality_of_work_company { get; set; }

        double perfomance_company { get; set; }

        bool isPie { get; set; }
        public string modal_user { get; set; }

        public int num { get; set; }

        #region Calcs
       
        public void SeparatorStep2(List<string> users_IDs, List<double> avgs, List<string> title_dates)
        {

        }

        #endregion

        public void topnumber_changed()
        {

        }
        private List<string> borderColors = new() { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };
        private Dictionary<int, PieChartListInteropModule> pieComponents = new Dictionary<int, PieChartListInteropModule>();
        private Dictionary<int, BarChartListInteropModule> barComponents = new Dictionary<int, BarChartListInteropModule>();
      

        async Task PersonalUserChange(ChangeEventArgs cea)
        {

        }

        string _personal_users { get; set; }

        string personal_users { get; set; }

        string personal_users_k { get; set; }

        async Task PersonalUserChanger(ChangeEventArgs cea)
        {
            isPersonalStatVisible = false;
            StateHasChanged();
            personal_users_k = cea.Value.ToString();
            await Task.Delay(200);

            PersonalDataLoad(personal_users_k);
           isPersonalStatVisible = true;
            await InvokeAsync(StateHasChanged);

        }

        async Task ClickOverHandler()
        {
            _pclim_1.LoadExternalData(svmp_main[0].pieDatasetList, svmp_main[0].pieDatasetLabels);
            _pclim_2.LoadExternalData(svmp_main[1].pieDatasetList, svmp_main[1].pieDatasetLabels);
            _pclim_3.LoadExternalData(svmp_main[2].pieDatasetList, svmp_main[2].pieDatasetLabels);
            _pclim_4.LoadExternalData(svmp_main[3].pieDatasetList, svmp_main[3].pieDatasetLabels);
            _pclim_5.LoadExternalData(svmp_main[4].pieDatasetList, svmp_main[4].pieDatasetLabels);

        }

        #region InteropChartModules

        LineChartListInteropModule _lclim_1 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_2 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_3 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_4 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_5 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_6 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_7 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_8 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_9 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_10 = new LineChartListInteropModule();

        LineChartListInteropModule _lclim_11 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_12 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_13 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_14 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_15 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_16 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_17 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_18 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_19 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_20 = new LineChartListInteropModule();

        LineChartListInteropModule _lclim_21 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_22 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_23 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_24 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_25 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_26 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_27 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_28 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_29 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_30 = new LineChartListInteropModule();

        LineChartListInteropModule _lclim_31 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_32 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_33 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_34 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_35 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_36 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_37 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_38 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_39 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_40 = new LineChartListInteropModule();

        LineChartListInteropModule _lclim_41 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_42 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_43 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_44 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_45 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_46 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_47 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_48 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_49 = new LineChartListInteropModule();
        LineChartListInteropModule _lclim_50 = new LineChartListInteropModule();

        PieChartListInteropModule _pclim_1 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_2 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_3 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_4 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_5 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_6 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_7 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_8 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_9 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_10 = new PieChartListInteropModule();

        PieChartListInteropModule _pclim_11 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_12 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_13 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_14 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_15 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_16 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_17 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_18 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_19 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_20 = new PieChartListInteropModule();

        PieChartListInteropModule _pclim_21 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_22 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_23 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_24 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_25 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_26 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_27 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_28 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_29 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_30 = new PieChartListInteropModule();

        PieChartListInteropModule _pclim_31 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_32 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_33 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_34 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_35 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_36 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_37 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_38 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_39 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_40 = new PieChartListInteropModule();

        PieChartListInteropModule _pclim_41 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_42 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_43 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_44 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_45 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_46 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_47 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_48 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_49 = new PieChartListInteropModule();
        PieChartListInteropModule _pclim_50 = new PieChartListInteropModule();

        BarChartListInteropModule _bclim_1 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_2 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_3 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_4 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_5 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_6 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_7 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_8 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_9 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_10 = new BarChartListInteropModule();

        BarChartListInteropModule _bclim_11 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_12 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_13 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_14 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_15 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_16 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_17 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_18 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_19 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_20 = new BarChartListInteropModule();

        BarChartListInteropModule _bclim_21 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_22 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_23 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_24 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_25 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_26 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_27 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_28 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_29 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_30 = new BarChartListInteropModule();

        BarChartListInteropModule _bclim_31 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_32 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_33 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_34 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_35 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_36 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_37 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_38 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_39 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_40 = new BarChartListInteropModule();

        BarChartListInteropModule _bclim_41 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_42 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_43 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_44 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_45 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_46 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_47 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_48 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_49 = new BarChartListInteropModule();
        BarChartListInteropModule _bclim_50 = new BarChartListInteropModule();

        PieChartListInteropModule res_pclim_1 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_2 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_3 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_4 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_5 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_6 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_7 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_8 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_9 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_10 = new PieChartListInteropModule();

        PieChartListInteropModule res_pclim_11 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_12 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_13 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_14 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_15 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_16 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_17 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_18 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_19 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_20 = new PieChartListInteropModule();

        PieChartListInteropModule res_pclim_21 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_22 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_23 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_24 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_25 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_26 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_27 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_28 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_29 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_30 = new PieChartListInteropModule();

        PieChartListInteropModule res_pclim_31 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_32 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_33 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_34 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_35 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_36 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_37 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_38 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_39 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_40 = new PieChartListInteropModule();

        PieChartListInteropModule res_pclim_41 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_42 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_43 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_44 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_45 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_46 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_47 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_48 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_49 = new PieChartListInteropModule();
        PieChartListInteropModule res_pclim_50 = new PieChartListInteropModule();

        BarChartListInteropModule res_bclim_1 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_2 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_3 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_4 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_5 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_6 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_7 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_8 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_9 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_10 = new BarChartListInteropModule();

        BarChartListInteropModule res_bclim_11 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_12 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_13 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_14 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_15 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_16 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_17 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_18 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_19 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_20 = new BarChartListInteropModule();

        BarChartListInteropModule res_bclim_21 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_22 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_23 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_24 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_25 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_26 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_27 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_28 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_29 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_30 = new BarChartListInteropModule();

        BarChartListInteropModule res_bclim_31 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_32 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_33 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_34 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_35 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_36 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_37 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_38 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_39 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_40 = new BarChartListInteropModule();

        BarChartListInteropModule res_bclim_41 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_42 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_43 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_44 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_45 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_46 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_47 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_48 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_49 = new BarChartListInteropModule();
        BarChartListInteropModule res_bclim_50 = new BarChartListInteropModule();

        #endregion

      
        bool end_loop { get; set; }
        protected async override Task OnInitializedAsync()
        {
            
        }

        public void DoCalcs()
        {
            for (int i = 0; i < svmp_main.Count; i++)
            {
                pieComponents[i].LoadExternalData(svmp_main[i].pieDatasetList, svmp_main[i].pieDatasetLabels);
                barComponents[i].LoadExternalData(svmp_main[i].barDatasetList, svmp_main[i].barDatasetLabels);
            }
        }

        List<PieChartOptions> pieChartOptionsCopied = new List<PieChartOptions>();
        List<BarChartOptions> barChartOptionsCopied = new List<BarChartOptions>();
        bool allowed_to_load_further { get; set; }
        string user_ { get; set; }

        bool isAdmin = false;

        schema_users user_model = new();
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                dt_start = DateTime.Now;
                dt_end = DateTime.Now;

                allowed_to_load_further = false;
                personal = false;
                avg_company = 0;
                academic_perfomace_company = 0;
                perfomance_company = 0;
                quality_of_work_company = 0;

                Users = _context.Users_Get();
                Marks = _context.Marks_Get();
                Tasks = _context.Tasks_Get();

            
                int p = 0;
                if (Users != null)
                {
                    p = Users.Count;
                }

                user_ = authStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

                //Получаем карточку пользователя

                if (user_ != null)
                {
                    user_model = _context.User_Get(user_);

                    //Проверка админки
                    if (user_model.role == "admin"
                        || user_model.role == "owner"
                        || user_model.role == "secretary")
                    {
                        isAdmin = true;
                    }
                    else
                    {
                        isAdmin = false;
                    }

                }
                else
                {
                    sc.Log("Login", $"Пользователь {user_model.fullname} открыл статистику по задачам", SystemCore.LogLevels.Info);
                    sc.Syslog(_env, user_model.login, "Статистика задач", "Открыл статистику по задачам", SystemCore.LogLevels.Info);
                    return;
                }

                InterpolationCatsString = "enabled";
                isOnlyWithMarks = false;
                isTop = false;

                num++;
                
                isModalPieOpen = false;
                modalAllow = false;

                TopNumber = Users.Count;

                pageLoaded = true;

                isPie = true;

                dt_end = DateTime.Now;
                dt_start = DateTime.Now;
               
                end_loop = false;

                personal_users = "0";
                personal_users_k = "0";

                StatWindowString = StatWindow.common.ToString();
                StatWindowString2 = StatWindow.personal.ToString();

                isPersonalStatVisible = true;

                LoadStatisticsTasksData();
                ready = true;
                StateHasChanged();
            }
        }

        private string[] Labels = { "Red", "Blue", "Yellow", "Green", "Purple", "Orange" };
        ChartOptions chartOptions = new()
        {
            AspectRatio = 1.5
        };
        private Chart<double> barChart;
        List<double> RandomizeData(int min, int max)
        {
            return Enumerable.Range(0, 6).Select(x => random.Next(min, max) * random.NextDouble()).ToList();
        }
        private List<string> backgroundColors = new() { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

        private Random random = new(DateTime.Now.Millisecond);
        private BarChartDataset<double> GetBarChartDataset()
        {
            return new()
            {
                Label = "# of randoms",
                Data = RandomizeData(1, 50),
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }

        public bool ready = false;

        public void StatWindowStringChanger(ChangeEventArgs cea)
        {
            if (cea.Value.ToString() == StatWindow.common.ToString())
            {
                personal = false;
                StatWindowString2 = StatWindow.common.ToString();
            }
            else
            {
                personal = true;
                StatWindowString2 = StatWindow.personal.ToString();
            }
            StateHasChanged();
        }

        bool isPersonalStatVisible { get; set; }

        protected override void OnInitialized()
        {
            svp_index = 0;
        }

        public void SecondaryChartsConfig(int i, List<double> AverageByMonthCollection, List<string> MonthCollection)
        {
           
        }

        public void PrimaryChartsConfig(int i, int marks_1, int marks_2, int marks_3, int marks_4, int marks_5)
        {
          
        }

        public static string modal_static { get; set; }



      


    }
}
