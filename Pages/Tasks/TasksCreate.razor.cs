using Blazorise.RichTextEdit;
using EviCRM.Server.Core;
using Meziantou.Framework;
using Microsoft.AspNetCore.Components.Forms;
using MySqlConnector;
using System.Diagnostics;
using System.Globalization;

namespace EviCRM.Server.Pages.Tasks
{
    public partial class TasksCreate
    {
        public class SelectData
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        protected async override Task OnInitializedAsync()
        {
            Users = _context.Users_Get();
            Divisions = _context.Divisions_Get();
            Projs = _context.Projects_Get();
            Tasks = _context.Tasks_Get();
          

            select_data = new List<SelectData>();
            select_data_users = new List<SelectData>();
            select_data_projs = new List<SelectData>();

            if (Divisions != null)
            {
                foreach(var elem in Divisions)
                {
                    SelectData sd = new SelectData();
                    sd.Id = elem.id.ToString();
                    sd.Name = elem.division_name;
                    select_data.Add(sd);
                }
            }

            if (Users != null)
            {
                foreach (var elem in Users)
                {
                    SelectData sd = new SelectData();
                    sd.Id = elem.id.ToString();
                    sd.Name = elem.fullname;
                    select_data_users.Add(sd);
                }
            }

            dt_start = DateTime.Now;
            dt_end = DateTime.Now;

            SessionUser = authStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            file_fileName = "";
            file_UploadedPercentage = 0;
            file_Size = 0;
            file_UploadedBytes = 0;

            attach_lst.Clear();
            attach_lst_links.Clear();

            ready = true;
            StateHasChanged();
        }

        public void DeleteAttachment(int j)
        {
            attach_lst.RemoveAt(j);
            attach_lst_links.RemoveAt(j);
        }

        string getUnicalFileName(string filename_we, string extension)
        {
            string guid_str = Guid.NewGuid().ToString();
            return (filename_we + "_" + Path.GetRandomFileName() + extension);
        }

        string FormatBytes(long value)
           => ByteSize.FromBytes(value).ToString("fi2", CultureInfo.CurrentCulture);

        record FileUploadProgress(string FileName, long Size)
        {
            public long UploadedBytes { get; set; }
            public double UploadedPercentage => (double)UploadedBytes / (double)Size * 100d;
        }

        public string genPersonalStatusString()
        {
            string output = "";

            if (SelectedValues_personal != null)
            {
                foreach (string str in SelectedValues_personal)
                {
                    output += str + "$===$waiting$$$";
                }
            }
            return output;
        }

        private string GetUniqueFileName(string fileName)
        {
            fileName = Path.GetFileName(fileName);
            return Path.GetFileNameWithoutExtension(fileName)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 4)
                      + Path.GetExtension(fileName);
        }
       
        //RichTextBox

        public async Task OnContentChanged_taskDescription()
        {
            contentAsHtml_taskDescription = await task_description.GetHtmlAsync();
            contentAsDeltaJson_taskDescription = await task_description.GetDeltaAsync();
            contentAsText_taskDescription = await task_description.GetTextAsync();
        }

        public async Task OnSave_taskDescription()
        {
            savedContent_taskDescription = await task_description.GetHtmlAsync();
            await task_description.ClearAsync();
        }

    }
}
