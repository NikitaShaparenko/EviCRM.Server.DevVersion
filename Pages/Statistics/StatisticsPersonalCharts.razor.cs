using Blazorise.Charts;
using EviCRM.Server.Core.EntityFramework;
using EviCRM.Server.Models.Statistics;
using EviCRM.Server.Pages.Charts.ChartListInterop;
using Microsoft.AspNetCore.Components;

namespace EviCRM.Server.Pages.Statistics
{
    public partial class StatisticsPersonalCharts
    {

        public bool isReady { get; set; }

        ChartOptions chartOption = new()
        {
            Plugins = new ChartPlugins()
            {
                Legend = new ChartLegend()
                {
                    Display = false,
                },
            },
            AspectRatio = 1.5,
        };

        PieChartDataset<int> pieDatasetList = new PieChartDataset<int>();
        BarChartDataset<int> barDatasetList = new BarChartDataset<int>();

        string[] pieDatasetLabels { get; set; }
        string[] barDatasetLabels { get; set; }

        PieChartListInteropModule _pclim = new PieChartListInteropModule();
        BarChartListInteropModule _bclim = new BarChartListInteropModule();
        LineChartListInteropModule _lclim = new LineChartListInteropModule();


        public enum InterpolationCats
        {
            disabled,
            enabled,
            stepbefore,
            stepafter,
            stepmiddle
        };

        public string InterpolationCatsString { get; set; }

        public async Task InterpolationCatsStringHandler(ChangeEventArgs cea)
        {
            string str = "";
            InterpolationCatsString = cea.Value.ToString();
            if (dt_start.Date == DateTime.Now.Date && dt_end.Date == DateTime.Now.Date)
            {
                await LoadDataList();
            }
            else
            {
                await LoadDataList(dt_start, dt_end);
            }
            _lclim.UpdateInterpolationCase(InterpolationCatsString);
            await InvokeAsync(StateHasChanged);
        }

        [Parameter]
        public List<schema_marks> Marks { get; set; }

        [Parameter]
        public List<schema_users> Users { get; set; }

        [Parameter]
        public string current_user_id { get; set; }

        DateTime dt_start { get; set; }
        DateTime dt_start_sys { get; set; }

        DateTime dt_end { get; set; }
        DateTime dt_end_sys { get; set; }


        #region Chart Generator
        double avg_company { get; set; }

        double academic_perfomace_company { get; set; }

        double quality_of_work_company { get; set; }

        double perfomance_company { get; set; }

        double avg_personal { get; set; }

        double academic_perfomace_personal { get; set; }

        double quality_of_work_personal { get; set; }

        double perfomance_personal { get; set; }

        int personal_top_place { get; set; }


        private Dictionary<int, PieChartListInteropModule> pieComponents = new Dictionary<int, PieChartListInteropModule>();
        private Dictionary<int, BarChartListInteropModule> barComponents = new Dictionary<int, BarChartListInteropModule>();

        public List<StatisticsViewModel_Pie> svmp_main = new List<StatisticsViewModel_Pie>();


        async Task<double> getCompanyEvaluations()
        {


            List<double> avgs = new List<double>();
            double avg = 0;
            foreach (var elem in svmp_main)
            {
                if (!(elem.marks1 == 0 && elem.marks2 == 0 && elem.marks3 == 0 && elem.marks4 == 0 && elem.marks5 == 0))
                {
                    avgs.Add(Double.Parse(elem.getAverage()));
                }
            }

            avg = 0;
            for (int z = 0; z < avgs.Count; z++)
            {
                avg += avgs[z];
            }

            if (avgs.Count != 0)
            {
                avg_company = (double)((double)avg / avgs.Count);
            }

            double t_v = 0;

            foreach (var elem in svmp_main)
            {
                if (!(elem.marks1 == 0 && elem.marks2 == 0 && elem.marks3 == 0 && elem.marks4 == 0 && elem.marks5 == 0))
                {
                    t_v += elem.marks3 + elem.marks4 + elem.marks5;
                }
            }

            t_v = (double)(t_v) / svmp_main.Count;
            academic_perfomace_company = t_v;

            t_v = 0;

            foreach (var elem in svmp_main)
            {
                if (!(elem.marks1 == 0 && elem.marks2 == 0 && elem.marks3 == 0 && elem.marks4 == 0 && elem.marks5 == 0))
                {
                    t_v += elem.marks4 + elem.marks5;
                }
            }
            t_v = (double)(t_v) / svmp_main.Count;
            quality_of_work_company = t_v;

            t_v = 0;
            double t_v2 = 0;
            double t_v3 = 0;

            foreach (var elem in svmp_main)
            {
                if (!(elem.marks1 == 0 && elem.marks2 == 0 && elem.marks3 == 0 && elem.marks4 == 0 && elem.marks5 == 0))
                {
                    t_v += elem.marks5 + elem.marks4 * 0.64 + elem.marks3 * 0.36 + elem.marks2 * 0.16 + elem.marks1 * 0.08;
                }
            }
            perfomance_company = t_v / svmp_main.Count;
            return 0;
        }

        async Task<double> getPersonalEvaluations(StatisticsViewModel_Pie svmp)
        {

            double personal_avgs = 0;

            double diff_ch = svmp.marks1 + svmp.marks2 + svmp.marks3 + svmp.marks4 + svmp.marks5;
            if (diff_ch != 0)
            {
                personal_avgs = (double)(svmp.marks1 * 1 + svmp.marks2 * 2 + svmp.marks3 * 3 + svmp.marks4 * 4 + svmp.marks5 * 5) / (diff_ch);
            }
            avg_personal = personal_avgs;


            double t_v = 0;

            t_v = svmp.marks3 + svmp.marks4 + svmp.marks5;

            if (diff_ch != 0)
            {
                t_v = (double)(t_v) / diff_ch;
            }
            else
            {
                t_v = 0;
            }

            academic_perfomace_personal = t_v;

            t_v = 0;

            t_v = svmp.marks4 + svmp.marks5;

            if (diff_ch != 0)
            {
                t_v = (double)(t_v) / diff_ch;
            }
            else
            {
                t_v = 0;
            }

            quality_of_work_personal = t_v;

            t_v = 0;

            t_v += svmp.marks5 + svmp.marks4 * 0.64 + svmp.marks3 * 0.36 + svmp.marks2 * 0.16 + svmp.marks1 * 0.08;

            if (diff_ch != 0)
            {
                t_v = (double)t_v / diff_ch;
            }


            perfomance_personal = t_v;

            List<double> avgs = new List<double>();
            double avg = 0;
            foreach (var elem in svmp_main)
            {
                if (!(elem.marks1 == 0 && elem.marks2 == 0 && elem.marks3 == 0 && elem.marks4 == 0 && elem.marks5 == 0))
                {
                    avgs.Add(Double.Parse(elem.getAverage()));
                }
            }

            var args_sorted = from s in avgs orderby avgs descending select s;

            List<StatisticsViewModel_Pie> sorted_lst = new List<StatisticsViewModel_Pie>();

            foreach (var elem in svmp_main)
            {
                sorted_lst.Add(elem);
            }

            sorted_lst = SortByDecreaseAvgValue(sorted_lst);

            personal_top_place = getTopPositionByValue(sorted_lst, int.Parse(current_user_id));

            StateHasChanged();
            return 0;
        }

        public List<StatisticsViewModel_Pie> SortByIncreaseAvgValue(List<StatisticsViewModel_Pie> originalList)
        {
            return originalList.OrderBy(x => x.getAverage(true)).ThenBy(x => x.user_fullname).ToList();
        }
        public List<StatisticsViewModel_Pie> SortByDecreaseAvgValue(List<StatisticsViewModel_Pie> originalList)
        {
            return originalList.OrderByDescending(x => x.getAverage(true)).ThenBy(x => x.user_fullname).ToList();
        }

        bool isForwardOrder = true;

        public void ModelSort()
        {
            List<StatisticsViewModel_Pie> svmp_main_dubler = new List<StatisticsViewModel_Pie>();

            foreach (var elem in svmp_main)
            {
                svmp_main_dubler.Add(elem);
            }

            isForwardOrder = !isForwardOrder;

            svmp_main.Clear();
            StateHasChanged();
            if (isForwardOrder)
            {
                svmp_main = SortByIncreaseAvgValue(svmp_main_dubler);
            }
            else
            {
                svmp_main = SortByDecreaseAvgValue(svmp_main_dubler);
            }
            StateHasChanged();
        }

        int getTopPositionByValue(List<StatisticsViewModel_Pie> sorted_lst, int user_arr_id)
        {
            int zp = 0;
            for (int p = 0; p < sorted_lst.Count; p++)
            {
                if (sorted_lst[p].user_arr_id == user_arr_id)
                {
                    return (p + 1);
                }
            }
            return zp;
        }

        int get_All_MarksByUser(schema_users User, int mark)
        {
            int marks = 0;

            if (Marks.Count > 0)
            {
                foreach (var elem in Marks)
                {
                    if (elem.isTwoMarks)
                    {
                        if (elem.first_mark == mark && elem.user_id == User.id) marks++;
                        if (elem.second_mark == mark && elem.user_id == User.id) marks++;
                    }
                    else
                    {
                        if (elem.first_mark == mark && elem.user_id == User.id) marks++;
                    }
                }
            }

            return marks;
        }

        double getAverage(schema_users User, bool isOverload)
        {
            double a = 0;

            int one = get_All_MarksByUser(User, 1);
            int two = get_All_MarksByUser(User, 2);
            int three = get_All_MarksByUser(User, 3);
            int four = get_All_MarksByUser(User, 4);
            int five = get_All_MarksByUser(User, 5);

            if ((one + two + three + four + five) != 0)
            {
                a = (double)(one + two + three + four + five);
                double b = (double)(one * 1 + two * 2 + 3 * three + 4 * four + 5 * five);
                a = (double)(b / a);
            }
            else
            {
                a = 0;
            }

            return a;
        }

        public enum LoadingSource
        {
            IndexPage,
            StatisticsPage,
        }

        [Parameter]
        public LoadingSource loaded_from { get; set; }

        bool isPie { get; set; }

        string table_activity_page_workdaily { get; set; }

        string table_activity_page_workdaily_duration { get; set; }


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

        public async Task DateStartChangeHandler(ChangeEventArgs cea)
        {
            dt_start_sys = DateTime.Parse(cea.Value.ToString());

            if (dt_start_sys.Date == DateTime.Now.Date && dt_end_sys.Date == DateTime.Now.Date)
            {
                await LoadDataList();
            }
            else
            {
                await LoadDataList(dt_start_sys, dt_end_sys);
            }

            // _pclim.ReDrawHandler();
            //_bclim.ReDrawHandler();
            await ReDrawHandler();

            StateHasChanged();

        }

        public async Task ReDrawHandler()
        {
            isPie = !isPie;
            StateHasChanged();
            await Task.Delay(100);
            isPie = !isPie;
            StateHasChanged();
        }

        public async Task DateEndChangeHandler(ChangeEventArgs cea)
        {
            dt_end_sys = DateTime.Parse(cea.Value.ToString());

            if (dt_start_sys.Date == DateTime.Now.Date && dt_end_sys.Date == DateTime.Now.Date)
            {
                await LoadDataList();
            }
            else
            {
                await LoadDataList(dt_start_sys, dt_end_sys);
            }
            StateHasChanged();
        }

        public double avg_diff(double avg_personal, double avg_company)
        {
            double dbl = 0.0;

            if (avg_personal > avg_company && avg_company != 0)
            {
                dbl = (avg_personal - avg_company) / avg_company * 100;
            }
            else
            {
                dbl = (avg_company - avg_personal) / avg_company * 100;
            }

            return dbl;
        }

        protected async override Task OnInitializedAsync()
        {
            isReady = false;

            dt_start = DateTime.Now;
            dt_end = DateTime.Now;

            dt_start_sys = DateTime.Now;
            dt_end_sys = DateTime.Now;

            if (dt_start_sys.Date == DateTime.Now.Date && dt_end_sys.Date == DateTime.Now.Date)
            {
                await LoadDataList();
            }
            else
            {
                await LoadDataList(dt_start_sys, dt_end_sys);
            }
            getCompanyEvaluations();

            int svmp_arr_num = getSVMP_ElementNumber();

            getPersonalEvaluations(svmp_main[svmp_arr_num]);


            isPie = true;
            isReady = true;

            InvokeAsync(StateHasChanged);
        }
        private List<string> backgroundColors = new() { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
        private List<string> borderColors = new() { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };


        public int getSVMP_ElementNumber()
        {
            int number = 0;

            for (int j = 0; j < svmp_main.Count; j++)
            {
                if (svmp_main[j].user_arr_id.ToString() == current_user_id)
                {
                    return j;
                }
            }
            return number;
        }
        private BarChartDataset<int> GetBarChartDataset()
        {
            return new()
            {
                Label = " таких оценок",
                //Data = ChartDataSetList,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                BorderWidth = 1
            };
        }

        public async Task LoadDataList(DateTime? dt_start = null, DateTime? dt_end = null)
        {
            svmp_main.Clear();
            pieComponents.Clear();
            barComponents.Clear();

            if (Users != null)
            {

                foreach (var elem in Users)
                {
                    pieComponents.Add(elem.id, new PieChartListInteropModule());
                    barComponents.Add(elem.id, new BarChartListInteropModule());

                    StatisticsViewModel_Pie statisticsViewModel_Pie = new StatisticsViewModel_Pie();
                    statisticsViewModel_Pie.user_arr_id = elem.id;
                    statisticsViewModel_Pie.user_fullname = elem.fullname;

                    ChartOptions pieOptions = new ChartOptions
                    {
                        Responsive = true,
                        Plugins = new ChartPlugins
                        {
                            Legend = new ChartLegend
                            {
                                Display = false,
                            },

                            Title = new ChartTitle
                            {
                                Display = true,
                                Text = elem.id.ToString(),
                            }
                        },
                    };

                    statisticsViewModel_Pie.pieOption = pieOptions;

                    ChartOptions barOptions = new ChartOptions
                    {
                        Responsive = true,
                        Plugins = new ChartPlugins
                        {
                            Legend = new ChartLegend
                            {
                                Display = false,
                            },

                            Title = new ChartTitle
                            {
                                Display = false,
                                Text = elem.id.ToString(),
                            }
                        },
                    };

                    statisticsViewModel_Pie.barOption = barOptions;

                    PieChart<int> pieRefs = new PieChart<int>();
                    BarChart<int> barRefs = new BarChart<int>();

                    statisticsViewModel_Pie.barRef = barRefs;
                    statisticsViewModel_Pie.pieRef = pieRefs;

                    LineChartOptions lineChartOptions = new Blazorise.Charts.LineChartOptions
                    {
                        Responsive = true,

                        Plugins = new ChartPlugins
                        {
                            Legend = new ChartLegend
                            {
                                Display = false,
                            },
                            Title = new ChartTitle
                            {
                                Display = false,
                                Text = "Тренды среднего балла",
                            },

                        },
                        Parsing = new ChartParsing
                        {
                            XAxisKey = "num",
                            YAxisKey = "date",
                        },

                    };

                    LineChartDataset<StatisticsLineData> _line_ch_dataset = new LineChartDataset<StatisticsLineData>();

                    _line_ch_dataset.Data = new List<StatisticsLineData>();

                    if (Marks != null)
                    {
                        List<schema_marks> user_marks = new();

                        if (!dt_start.HasValue || !dt_end.HasValue)
                        {
                            user_marks = Marks.Where(p => p.user_id.Equals(elem.id)).ToList();
                        }
                        else
                        {
                            user_marks = Marks.Where(p => p.user_id.Equals(elem.id) && p.date >= dt_start && p.date <= dt_end).ToList();
                        }

                        if (user_marks != null)
                        {
                            List<double> lst_average = new List<double>();
                            List<DateTime> lst_dates = new List<DateTime>();

                            foreach (var mark in user_marks)
                            {

                                if (mark.isTwoMarks)
                                {
                                    //Учитываем обе оценки

                                    double avg = (double)(((double)mark.first_mark + (double)mark.second_mark) / 2);
                                    lst_average.Add(avg);
                                    lst_dates.Add(mark.date);
                                }
                                else
                                {
                                    //Учитываем только первую

                                    lst_average.Add(mark.first_mark);
                                    lst_dates.Add(mark.date);
                                }

                            }

                            if (lst_average.Count > 0)
                            {
                                for (int azp = 0; azp < lst_average.Count; azp++)
                                {
                                    StatisticsLineData sld = new StatisticsLineData();
                                    sld.num = lst_average[azp];
                                    sld.date_mark = lst_dates[azp].ToShortDateString();

                                    _line_ch_dataset.Data.Add(sld);
                                };
                            }

                        }
                    }

                    _line_ch_dataset.Stepped = true;
                    statisticsViewModel_Pie._lineDatasetList = _line_ch_dataset;

                    _line_ch_dataset.Stepped = true;
                    statisticsViewModel_Pie._lineDatasetList2 = _line_ch_dataset;

                    _line_ch_dataset.Stepped = "before";
                    statisticsViewModel_Pie._lineDatasetList3 = _line_ch_dataset;

                    _line_ch_dataset.Stepped = "after";
                    statisticsViewModel_Pie._lineDatasetList4 = _line_ch_dataset;

                    _line_ch_dataset.Stepped = "middle";
                    statisticsViewModel_Pie._lineDatasetList5 = _line_ch_dataset;

                    statisticsViewModel_Pie.marks1 = getCountMarks(1, elem, dt_start, dt_end);

                    statisticsViewModel_Pie.marks2 = getCountMarks(2, elem, dt_start, dt_end);

                    statisticsViewModel_Pie.marks3 = getCountMarks(3, elem, dt_start, dt_end);

                    statisticsViewModel_Pie.marks4 = getCountMarks(4, elem, dt_start, dt_end);

                    statisticsViewModel_Pie.marks5 = getCountMarks(5, elem, dt_start, dt_end);

                    PieChartDataset<int> pieDatasetList = new PieChartDataset<int>();
                    BarChartDataset<int> barDatasetList = new BarChartDataset<int>();

                    double percentage1 = 0;
                    double percentage2 = 0;
                    double percentage3 = 0;
                    double percentage4 = 0;
                    double percentage5 = 0;


                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage1 = ((double)statisticsViewModel_Pie.marks1 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage1 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage2 = ((double)statisticsViewModel_Pie.marks2 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage2 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage3 = ((double)statisticsViewModel_Pie.marks3 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage3 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage4 = ((double)statisticsViewModel_Pie.marks4 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage4 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage5 = ((double)statisticsViewModel_Pie.marks5 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage5 = 0;
                    }

                    List<int> pieDatalistCommon = new List<int>();

                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks1);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks2);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks3);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks4);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks5);

                    pieDatasetList = new PieChartDataset<int>()
                    {
                        Data = new List<int>(),
                        BackgroundColor = new List<ChartColor>()
                    {
                        ChartColor.FromRgba(255, 99, 132, 255), // Slice 1 aka "Red"

                        ChartColor.FromRgba(255, 205, 86, 255),
                        ChartColor.FromRgba(75, 192, 192, 255),
                        ChartColor.FromRgba(54, 162, 235, 255),
                        ChartColor.FromRgba(255, 255, 0, 255),

                    },
                        Label = "График оценок",
                        BorderColor = borderColors,
                        BorderWidth = 1
                    };

                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks1);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks2);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks3);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks4);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks5);


                    barDatasetList = new BarChartDataset<int>()
                    {
                        Data = new List<int>(),
                        BackgroundColor = new List<ChartColor>()
                    {
                    ChartColor.FromRgba(255, 99, 132,255), // Slice 1 aka "Red"
                    
                    ChartColor.FromRgba(255, 205, 86,255),
                    ChartColor.FromRgba(75, 192, 192,255),
                    ChartColor.FromRgba(54, 162, 235,255),
                    ChartColor.FromRgba(255, 255, 0,255),

                    },
                        Label = "График оценок",
                        BorderColor = borderColors,
                        BorderWidth = 1
                    };

                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks1);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks2);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks3);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks4);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks5);

                    statisticsViewModel_Pie.barDatasetList = barDatasetList;
                    statisticsViewModel_Pie.pieDatasetList = pieDatasetList;

                    List<string> labels = new List<string>();

                    labels.Add("1 [ " + percentage1.ToString("N2") + "% ]");
                    labels.Add("2 [ " + percentage2.ToString("N2") + "% ]");
                    labels.Add("3 [ " + percentage3.ToString("N2") + "% ]");
                    labels.Add("4 [ " + percentage4.ToString("N2") + "% ]");
                    labels.Add("5 [ " + percentage5.ToString("N2") + "% ]");

                    foreach (string marks in labels)
                    {
                        //statisticsViewModel_Pie.pieRef.Data.Labels = new();
                        //statisticsViewModel_Pie.pieRef.Data.Labels.Add(marks);
                        //statisticsViewModel_Pie.barRef.Data.Labels = new();
                        //statisticsViewModel_Pie.barRef.Data.Labels.Add(marks);
                    }

                    string[] mark_labels = new[] { "1 [ " + percentage1.ToString("N2") + "% ]", "2 [ " + percentage2.ToString("N2") + "% ]", "3 [ " + percentage3.ToString("N2") + "% ]", "4 [ " + percentage4.ToString("N2") + "% ]", "5 [ " + percentage5.ToString("N2") + "% ]" };

                    await statisticsViewModel_Pie.pieRef.AddLabelsDatasetsAndUpdate(mark_labels, statisticsViewModel_Pie.pieDatasetList);
                    await statisticsViewModel_Pie.barRef.AddLabelsDatasetsAndUpdate(mark_labels, statisticsViewModel_Pie.barDatasetList);

                    statisticsViewModel_Pie.pieDatasetLabels = mark_labels;
                    statisticsViewModel_Pie.barDatasetLabels = mark_labels;

                    await statisticsViewModel_Pie.line1.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList);
                    await statisticsViewModel_Pie.line2.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList2);
                    await statisticsViewModel_Pie.line3.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList3);
                    await statisticsViewModel_Pie.line4.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList4);
                    await statisticsViewModel_Pie.line5.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList5);

                    statisticsViewModel_Pie.pieOption = pieOptions;
                    statisticsViewModel_Pie.barOption = barOptions;

                    svmp_main.Add(statisticsViewModel_Pie);
                }
            }


            StateHasChanged();

        }

        public async Task LoadDataList()
        {
            svmp_main.Clear();
            pieComponents.Clear();
            barComponents.Clear();
            StateHasChanged();

            if (Users != null)
            {

                foreach (var elem in Users)
                {
                    pieComponents.Add(elem.id, new PieChartListInteropModule());
                    barComponents.Add(elem.id, new BarChartListInteropModule());
                    StatisticsViewModel_Pie statisticsViewModel_Pie = new StatisticsViewModel_Pie();
                    statisticsViewModel_Pie.user_arr_id = elem.id;
                    statisticsViewModel_Pie.user_fullname = elem.fullname;

                    ChartOptions pieOptions = new ChartOptions
                    {
                        Responsive = true,
                        Plugins = new ChartPlugins
                        {
                            Legend = new ChartLegend
                            {
                                Display = false,
                            },

                            Title = new ChartTitle
                            {
                                Display = false,
                                Text = elem.id.ToString(),
                            }
                        },
                    };

                    statisticsViewModel_Pie.pieOption = pieOptions;

                    ChartOptions barOptions = new ChartOptions
                    {
                        Responsive = true,
                        Plugins = new ChartPlugins
                        {
                            Legend = new ChartLegend
                            {
                                Display = false,
                            },

                            Title = new ChartTitle
                            {
                                Display = false,
                                Text = elem.id.ToString(),
                            }
                        },
                    };

                    statisticsViewModel_Pie.barOption = barOptions;

                    PieChart<int> pieRefs = new PieChart<int>();
                    BarChart<int> barRefs = new BarChart<int>();

                    statisticsViewModel_Pie.barRef = barRefs;
                    statisticsViewModel_Pie.pieRef = pieRefs;

                    LineChartOptions lineChartOptions = new Blazorise.Charts.LineChartOptions
                    {
                        Responsive = true,

                        Plugins = new ChartPlugins
                        {
                            Legend = new ChartLegend
                            {
                                Display = false,
                            },
                            Title = new ChartTitle
                            {
                                Display = false,
                                Text = "Тренды среднего балла",
                            },

                        },
                        Parsing = new ChartParsing
                        {
                            XAxisKey = "num",
                            YAxisKey = "date",
                        },

                    };

                    LineChartDataset<StatisticsLineData> _line_ch_dataset = new LineChartDataset<StatisticsLineData>();

                    _line_ch_dataset.Data = new List<StatisticsLineData>();

                    if (Marks != null)
                    {
                        foreach (var elem2 in Marks)
                        {
                            List<double> lst_average = new List<double>();
                            List<string> lst_dates = new List<string>();

                            if (elem2.user_id == elem.id)
                            {
                                if (elem2.isTwoMarks)
                                {
                                    int a = elem2.first_mark;
                                    int b = elem2.second_mark;

                                    double avg = (double)(((double)a + (double)b) / 2);
                                    lst_average.Add(avg);
                                    lst_dates.Add(elem2.date.ToShortDateString());
                                    //2 оценки
                                }
                                else
                                {
                                    //1 оценка, нет среднего
                                    lst_average.Add(elem2.first_mark);
                                    lst_dates.Add(elem2.date.ToShortDateString());
                                }
                            }
                            if (lst_average.Count > 0)
                            {
                                for (int azp = 0; azp < lst_average.Count; azp++)
                                {
                                    StatisticsLineData sld = new StatisticsLineData();
                                    sld.num = lst_average[azp];
                                    sld.date_mark = DateTime.Parse(lst_dates[azp]).ToShortDateString();

                                    _line_ch_dataset.Data.Add(sld);
                                };
                            }
                        }
                    }

                    statisticsViewModel_Pie._lineDatasetList = _line_ch_dataset;

                    _line_ch_dataset.Stepped = true;
                    statisticsViewModel_Pie._lineDatasetList2 = _line_ch_dataset;

                    _line_ch_dataset.Stepped = "before";
                    statisticsViewModel_Pie._lineDatasetList3 = _line_ch_dataset;

                    _line_ch_dataset.Stepped = "after";
                    statisticsViewModel_Pie._lineDatasetList4 = _line_ch_dataset;

                    _line_ch_dataset.Stepped = "middle";
                    statisticsViewModel_Pie._lineDatasetList5 = _line_ch_dataset;

                    statisticsViewModel_Pie.marks1 = getCountMarks(1, elem);

                    statisticsViewModel_Pie.marks2 = getCountMarks(2, elem);

                    statisticsViewModel_Pie.marks3 = getCountMarks(3, elem);

                    statisticsViewModel_Pie.marks4 = getCountMarks(4, elem);

                    statisticsViewModel_Pie.marks5 = getCountMarks(5, elem);

                    PieChartDataset<int> pieDatasetList = new PieChartDataset<int>();
                    BarChartDataset<int> barDatasetList = new BarChartDataset<int>();

                    double percentage1 = 0;
                    double percentage2 = 0;
                    double percentage3 = 0;
                    double percentage4 = 0;
                    double percentage5 = 0;


                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage1 = ((double)statisticsViewModel_Pie.marks1 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage1 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage2 = ((double)statisticsViewModel_Pie.marks2 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage2 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage3 = ((double)statisticsViewModel_Pie.marks3 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage3 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage4 = ((double)statisticsViewModel_Pie.marks4 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage4 = 0;
                    }

                    if ((statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) != 0)
                    {
                        percentage5 = ((double)statisticsViewModel_Pie.marks5 / (statisticsViewModel_Pie.marks1 + statisticsViewModel_Pie.marks2 + statisticsViewModel_Pie.marks3 + statisticsViewModel_Pie.marks4 + statisticsViewModel_Pie.marks5) * 100);
                    }
                    else
                    {
                        percentage5 = 0;
                    }

                    List<int> pieDatalistCommon = new List<int>();

                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks1);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks2);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks3);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks4);
                    pieDatalistCommon.Add(statisticsViewModel_Pie.marks5);

                    pieDatasetList = new PieChartDataset<int>()
                    {
                        Data = new List<int>(),
                        BackgroundColor = new List<ChartColor>()
                    {
                        ChartColor.FromRgba(255, 99, 132, 255), // Slice 1 aka "Red"

                        ChartColor.FromRgba(255, 205, 86, 255),
                        ChartColor.FromRgba(75, 192, 192, 255),
                        ChartColor.FromRgba(54, 162, 235, 255),
                        ChartColor.FromRgba(255, 255, 0, 255),

                    },
                        Label = "График оценок",
                        BorderColor = borderColors,
                        BorderWidth = 1
                    };

                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks1);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks2);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks3);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks4);
                    pieDatasetList.Data.Add(statisticsViewModel_Pie.marks5);


                    barDatasetList = new BarChartDataset<int>()
                    {
                        Data = new List<int>(),
                        BackgroundColor = new List<ChartColor>()
                    {
                    ChartColor.FromRgba(255, 99, 132,255), // Slice 1 aka "Red"
                    
                    ChartColor.FromRgba(255, 205, 86,255),
                    ChartColor.FromRgba(75, 192, 192,255),
                    ChartColor.FromRgba(54, 162, 235,255),
                    ChartColor.FromRgba(255, 255, 0,255),

                    },
                        Label = "График оценок",
                        BorderColor = borderColors,
                        BorderWidth = 1
                    };

                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks1);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks2);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks3);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks4);
                    barDatasetList.Data.Add(statisticsViewModel_Pie.marks5);

                    statisticsViewModel_Pie.barDatasetList = barDatasetList;
                    statisticsViewModel_Pie.pieDatasetList = pieDatasetList;

                    List<string> labels = new List<string>();

                    labels.Add("1 [ " + percentage1.ToString("N2") + "% ]");
                    labels.Add("2 [ " + percentage2.ToString("N2") + "% ]");
                    labels.Add("3 [ " + percentage3.ToString("N2") + "% ]");
                    labels.Add("4 [ " + percentage4.ToString("N2") + "% ]");
                    labels.Add("5 [ " + percentage5.ToString("N2") + "% ]");


                    string[] mark_labels = new[] { "1 [ " + percentage1.ToString("N2") + "% ]", "2 [ " + percentage2.ToString("N2") + "% ]", "3 [ " + percentage3.ToString("N2") + "% ]", "4 [ " + percentage4.ToString("N2") + "% ]", "5 [ " + percentage5.ToString("N2") + "% ]" };

                    await statisticsViewModel_Pie.pieRef.AddLabelsDatasetsAndUpdate(mark_labels, statisticsViewModel_Pie.pieDatasetList);
                    await statisticsViewModel_Pie.barRef.AddLabelsDatasetsAndUpdate(mark_labels, statisticsViewModel_Pie.barDatasetList);

                    statisticsViewModel_Pie.pieDatasetLabels = mark_labels;
                    statisticsViewModel_Pie.barDatasetLabels = mark_labels;

                    await statisticsViewModel_Pie.line1.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList);
                    await statisticsViewModel_Pie.line2.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList2);
                    await statisticsViewModel_Pie.line3.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList3);
                    await statisticsViewModel_Pie.line4.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList4);
                    await statisticsViewModel_Pie.line5.AddDatasetsAndUpdate(statisticsViewModel_Pie._lineDatasetList5);

                    statisticsViewModel_Pie.pieOption = pieOptions;
                    statisticsViewModel_Pie.barOption = barOptions;


                    svmp_main.Add(statisticsViewModel_Pie);
                }
            }


            StateHasChanged();

        }

        public int getCountMarks(int eval_mark, schema_users user, DateTime? dt_start = null, DateTime? dt_end = null)
        {
            int counts = 0;

            if (Marks != null)
            {
                List<schema_marks> Marks_Local = new();

                if (!dt_start.HasValue || !dt_end.HasValue)
                {
                    Marks_Local = Marks.Where(p => p.user_id.Equals(user.id)).ToList();
                }
                else
                {
                    Marks_Local = Marks.Where(p => p.date <= dt_end && p.date >= dt_start && p.user_id.Equals(user.id)).ToList();
                }

                return Marks_Local.Where(p => (p.first_mark.Equals(eval_mark) && p.isTwoMarks == false) || (p.first_mark.Equals(eval_mark) && p.second_mark.Equals(eval_mark) && p.isTwoMarks)).Count();
            }

            return counts;
        }

        #endregion

        ChartOptions chartOptions = new()
        {
            Plugins = new ChartPlugins()
            {
                Legend = new ChartLegend()
                {
                    Display = false,
                },
            },
            AspectRatio = 1.5,
        };


    }
}