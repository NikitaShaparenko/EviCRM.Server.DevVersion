namespace EviCRM.Server.Pages.Marks
{
    public partial class MarksChartModal
    {
        public Task DeleteAttachment(int j)
        {
            attach_first_lst.RemoveAt(j);
            attach_first_lst_links.RemoveAt(j);
            return Task.CompletedTask;
        }
        public Task DeleteSecondAttachment(int j)
        {
            attach_second_lst.RemoveAt(j);
            attach_second_lst_links.RemoveAt(j);
            return Task.CompletedTask;
        }

        void OnMarkNumberChanged(object args)
        {
            if (string.IsNullOrEmpty(args.ToString()))
            {
                mcm.isTwoMarks = true;
                return;
            }

            if (bool.TryParse(args.ToString(), out var result))
            {
                mcm.isTwoMarks = result;
            }
        }

        static String BytesToString(long byteCount)
        {
            string[] suf = { "Байт", "кб", "мб", "гб", "тб", "пб", "эксб" }; //
            if (byteCount == 0)
                return "0" + suf[0];
            long bytes = Math.Abs(byteCount);
            int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
            double num = Math.Round(bytes / Math.Pow(1024, place), 1);
            return (Math.Sign(byteCount) * num).ToString() + suf[place];
        }
    }
}
