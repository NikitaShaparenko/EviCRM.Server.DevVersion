using Blazorise.RichTextEdit;
using EviCRM.Server.ViewModels;
using Majorsoft.Blazor.Components.Tabs;
using Meziantou.Framework;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Globalization;
using EviCRM.Server.Core.EntityFramework;

namespace EviCRM.Server.Pages.Tasks.TaskTracking
{
    public partial class TaskTracking_DownActionBar
    {
        public Task DeleteAttachment(int j)
        {
            attach_lst.RemoveAt(j);
            attach_lst_links.RemoveAt(j);
            return Task.CompletedTask;
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

        bool _uploading;
        string echo;

        string SelectedValues2 { get; set; }

        string SelectedValues { get; set; }

        List<string> SelectedValues_rtfWarningWhom_personal { get; set; }
        List<string> SelectedValues_rtfWarningWhom_divs { get; set; }

        List<string> SelectedValues_ttm_message_whom_personal { get; set; }
        List<string> SelectedValues_ttm_message_whom_div { get; set; }

        List<string> SelectedValues_ttm_question_whom_personal { get; set; }
        List<string> SelectedValues_ttm_question_whom_div { get; set; }

        List<string> SelectedValues_ttm_commit_personal { get; set; }
        List<string> SelectedValues_ttm_commit_divs { get; set; }

        [Parameter]
        public List<string> personal_status_arg1 { get; set; }

        [Parameter]
        public List<string> personal_status_arg2 { get; set; }

        [Parameter]
        public List<string> personal_status { get; set; }

        [Parameter]
        public EventCallback<bool> OnCarouselRefresh { get; set; }

        protected override async Task OnInitializedAsync()
        {
            select_data = new List<SelectData>();
            select_data_divs = new List<SelectData>();
            select_data_personal = new List<SelectData>();

            if (Divisions != null)
            {
                foreach(var elem in Divisions)
                {
                    SelectData sd = new SelectData();
                    sd.Id = elem.id.ToString();
                    sd.Name = elem.division_name;
                    select_data_divs.Add(sd);
                }
            }

            if (Users != null)
            {
                foreach (var elem in Users)
                {
                    SelectData sd = new SelectData();
                    sd.Id = elem.id.ToString();
                    sd.Name = elem.fullname;
                    select_data_personal.Add(sd);
                }
            }

            //foreach (string att_lnk in _attachments)
            //{
            //    attach_lst_links.Add("https://evicrm.store/uploads/tasktracking/" + att_lnk);
            //}
            file_fileName = "";
            file_UploadedPercentage = 0;
            file_Size = 0;
            file_UploadedBytes = 0;

            StateHasChanged();
        }

        

        public async Task OnSave()
        {
            savedContent = await richTextEditRef.GetHtmlAsync();
            await richTextEditRef.ClearAsync();
        }

        public void OnCloseRefuseDoing(bool accepted)
        {
            modal_RefuseDoingOpened = false;
            
            if (accepted)
            {
                OnPersonalStatusChange.InvokeAsync("failed");
            }

            OnCarouselRefresh.InvokeAsync(true);
            StateHasChanged();
        }

        public void OnCloseFinishTask(bool accepted)
        {
            modal_finishTaskOpened = false;

            if (accepted)
            {
                OnPersonalStatusChange.InvokeAsync("completed");
            }

            OnCarouselRefresh.InvokeAsync(true);
            StateHasChanged();
        }

        public void OnCloseDelay(bool accepted)
        {
            modal_DelayOpened = false;

            if (accepted)
            {
                OnPersonalStatusChange.InvokeAsync("delayed");
            }

            OnCarouselRefresh.InvokeAsync(true);
            StateHasChanged();
        }

        bool isAllPointsCompleted = false;

        public string getUsernameByLogin(string login)
        {
            string fullname = "";

            var elem = Users.FirstOrDefault(p => p.login == login);

            if (elem != null)
            {
                fullname = elem.fullname;
            }
            return fullname;
        }

        async Task post_message()
        {
            ViewModels.TaskTrackingModelInformation ttmi = new ViewModels.TaskTrackingModelInformation();
            ttmi.task_author = Current_Task.task_author;
            ttmi.ttm_info_body = contentAsHtml_rtfMessage;
            ttmi.ttm_info_forwhom = SelectedValues_ttm_message_whom_div;
            ttmi.task_id = Current_Task.id.ToString();

            string TOSCRIPT_CMD = "MESSAGE";

            string task_author_to = ttmi.task_author;
            string task_id_to = ttmi.task_id;

            List<string> str_lst = new List<string>();


            if (ttmi.ttm_info_forwhom != null)
            {
                if (ttmi.ttm_info_forwhom.Count > 0)
                    foreach (string elem in ttmi.ttm_info_forwhom)
                    {
                        str_lst.Add(elem);
                    }

            }

            string TOSCRIPT_VAR1 = ttmi.ttm_info_body;
            string TOSCRIPT_VAR2 = bc.getMultipleStringEncodingString(str_lst);

            schema_task_tracking_main sttm = new()
            {
                task_variable1 = TOSCRIPT_VAR1,
                task_variable2 = TOSCRIPT_VAR2,
                task_variable3 = "",
                task_author = task_author_to,
                task_id = task_id_to,
                task_cmd = TOSCRIPT_CMD,
                task_datetime = DateTime.Now,
            };

            _context.TaskTrackingMain_Create(sttm);

            string push_msg = "[Задача - " + Current_Task.task_name + "] \n";
            push_msg += "(Пользователь: " + getUsernameByLogin(task_author_to) + ") \n \n";
            push_msg += "Написал новое сообщение: \n";
            push_msg += ttmi.ttm_info_body + "\n";
            push_msg += "\n Совместно с: \n";
            if (ttmi.ttm_info_forwhom != null)
            {
                if (ttmi.ttm_info_forwhom.Count > 0)
                    foreach (string elem in ttmi.ttm_info_forwhom)
                    {
                        push_msg += getUsernameByLogin(elem) + ", ";
                    }
            }

            await bc.SendTelegramMessage(push_msg, Current_Task, Users, Divisions);
            await rtfMessage.ClearAsync();
            await OnCarouselRefresh.InvokeAsync(true);
            StateHasChanged();
        }

        async Task post_commit()
        {
            ViewModels.TaskTrackingModelCardCommit ttmcc = new ViewModels.TaskTrackingModelCardCommit();
            ttmcc.task_author = Current_Task.task_author;
            ttmcc.task_id = Current_Task.id.ToString();
            ttmcc.ttm_commit_id = Current_Task.id.ToString();
            ttmcc.ttm_commit_author = Current_Task.task_author;

            ttmcc.ttm_commit_body = contentAsHtml_rtfMessage;
            ttmcc.ttm_commit_title = ttm_commit_title;
            ttmcc.ttm_cast_coauthors = SelectedValues_ttm_commit_personal;

            string TOSCRIPT_CMD = "COMMIT";

            string task_author_to = ttmcc.task_author;
            string task_id_to = ttmcc.task_id;

            List<string> str_lst = new List<string>();


            if (ttmcc.ttm_cast_coauthors != null)
            {
                if (ttmcc.ttm_cast_coauthors.Count > 0)
                    foreach (string elem in ttmcc.ttm_cast_coauthors)
                    {
                        str_lst.Add(elem);
                    }

            }
            string TOSCRIPT_VAR1 = ttmcc.ttm_commit_title;
            string TOSCRIPT_VAR2 = ttmcc.ttm_commit_body;
            string TOSCRIPT_VAR3 = bc.getMultipleStringEncodingString(str_lst);

            schema_task_tracking_main sttm = new()
            {
                task_variable1 = TOSCRIPT_VAR1,
                task_variable2 = TOSCRIPT_VAR2,
                task_variable3 = TOSCRIPT_VAR3,
                task_author = task_author_to,
                task_id = task_id_to,
                task_cmd = TOSCRIPT_CMD,
                task_datetime = DateTime.Now,
            };

_context.TaskTrackingMain_Create(sttm);

            string push_msg = "[Задача - " + Current_Task.task_name + "] \n";
            push_msg += "(Пользователь: " + getUsernameByLogin(task_author_to) + ") \n \n";
            push_msg += "Произошли новые изменения в задаче: \n";
            push_msg += ttmcc.ttm_commit_title + "\n";
            push_msg += ttmcc.ttm_commit_body + "\n";
            push_msg += "\n Совместно с: \n";
            if (ttmcc.ttm_cast_coauthors != null)
            {
                foreach (string elem in ttmcc.ttm_cast_coauthors)
                {
                    push_msg += getUsernameByLogin(elem) + ", ";
                }
            }
            await bc.SendTelegramMessage(push_msg, Current_Task, Users, Divisions);

            await rtfCommit.ClearAsync();
            ttm_commit_title = "";
            await OnCarouselRefresh.InvokeAsync(true);
            StateHasChanged();
        }

        async Task post_warning()
        {
            ViewModels.TaskTrackingModelWarning ttmw = new ViewModels.TaskTrackingModelWarning();
            ttmw.task_author = Current_Task.task_author;
            ttmw.task_id = Current_Task.id.ToString();
            ttmw.ttm_cast_coauthors = SelectedValues_rtfWarningWhom_personal;
            ttmw.ttm_warning_body = contentAsHtml_rtfWarning;
            ttmw.ttm_warning_title = ttm_warning_title;
            ttmw.ttm_whom_warning = SelectedValues_rtfWarningWhom_personal;

            string TOSCRIPT_CMD = "WARNING";

            string task_author_to = ttmw.task_author;
            string task_id_to = ttmw.task_id;

            List<string> str_lst = new List<string>();

            if (ttmw.ttm_whom_warning.Count > 0)
                foreach (string elem in ttmw.ttm_whom_warning)
                {
                    str_lst.Add(elem);
                }

            string TOSCRIPT_VAR1 = ttmw.ttm_warning_title;
            string TOSCRIPT_VAR2 = ttmw.ttm_warning_body;
            string TOSCRIPT_VAR3 = bc.getMultipleStringEncodingString(str_lst);

            schema_task_tracking_main sttm = new()
            {
                task_variable1 = TOSCRIPT_VAR1,
                task_variable2 = TOSCRIPT_VAR2,
                task_variable3 = TOSCRIPT_VAR3,
                task_author = task_author_to,
                task_id = task_id_to,
                task_cmd = TOSCRIPT_CMD,
                task_datetime = DateTime.Now,
            };

             _context.TaskTrackingMain_Create(sttm);

            string push_msg = "[Задача - " + Current_Task.task_name + "] \n";
            push_msg += "(Пользователь: " + getUsernameByLogin(task_author_to) + ") \n \n";
            push_msg += "ПРЕДУПРЕЖДЕНИЕ! : \n";
            push_msg += ttmw.ttm_warning_title + "\n";
            push_msg += ttmw.ttm_warning_body + "\n";
            push_msg += "\n Совместно с: \n";
            if (ttmw.ttm_cast_coauthors != null)
            {
                foreach (string elem in ttmw.ttm_cast_coauthors)
                {
                    push_msg += getUsernameByLogin(elem) + ", ";
                }
            }
            await bc.SendTelegramMessage(push_msg, Current_Task, Users, Divisions);

            await rtfWarning.ClearAsync();
            ttm_warning_title = "";
            await OnCarouselRefresh.InvokeAsync(true);
            StateHasChanged();
        }

        async Task post_attachments()
        {
            ViewModels.TaskTrackingModelAttachments ttma = new ViewModels.TaskTrackingModelAttachments();
            ttma.task_author = Current_Task.task_author;
            ttma.task_id = Current_Task.id.ToString();
            ttma.ttm_sendattachments = attach_lst_links;

            await rtfSenderFile.ClearAsync();
            attach_lst.Clear();

            string TOSCRIPT_CMD = "ADDATTACHMENTS";

          
            List<string> attach_list = new List<string>();
            string attach_u = "";

            if (ttma.ttm_sendattachments.Count > 0)
                foreach (string file in ttma.ttm_sendattachments)
                {

                    //    var uniqueFileName = GetUniqueFileName(file.FileName);
                    //    var uploads = Path.Combine(hostingEnvironment.WebRootPath, "task_uploads");
                    //    var filePath = Path.Combine(uploads, uniqueFileName);
                    attach_list.Add(file);
                    //    file.CopyTo(new FileStream(filePath, FileMode.Create));
                }

            attach_u = bc.getMultipleStringEncodingString(attach_list);

            //string TOSCRIPT_VAR1 = ttmcm.ttm_sendattachments_body;
            string TOSCRIPT_VAR1 = "В процессе выполнения загружены новые файлы";

            string TOSCRIPT_VAR2 = attach_u;

            schema_task_tracking_main sttm = new()
            {
                task_variable1 = TOSCRIPT_VAR1,
                task_variable2 = TOSCRIPT_VAR2,
                task_variable3 = "",
                task_author = ttma.task_author,
                task_id = ttma.task_id,
                task_cmd = TOSCRIPT_CMD,
                task_datetime = DateTime.Now,
            };

            _context.TaskTrackingMain_Create(sttm);

            string push_msg = "[Задача - " + Current_Task.task_name + "] \n";
            push_msg += "(Пользователь: " + getUsernameByLogin(ttma.task_author) + ") \n \n";
            push_msg += "В задаче добавились файлы!: \n \n";

            foreach (string attach in attach_list)
            {
                push_msg += "http://evicrm.store/task_uploads/" + attach + "\n";
            }

            await bc.SendTelegramMessage(push_msg, Current_Task, Users, Divisions);


            await OnCarouselRefresh.InvokeAsync(true);
            StateHasChanged();
        }

        async Task post_question()
        {
            ViewModels.TaskTrackingModelQuestion ttmq = new ViewModels.TaskTrackingModelQuestion();

            ttmq.task_author = Current_Task.task_author;
            ttmq.task_id = Current_Task.id.ToString();
            ttmq.ttm_questioncard_id = Current_Task.id.ToString();
            ttmq.ttm_question_body = contentAsHtml_rtfQuestion;
            ttmq.ttm_question_title = ttm_question_title;
            ttmq.ttm_whom_question = SelectedValues_ttm_question_whom_personal;

            string TOSCRIPT_CMD = "QUESTION";

            string task_author_to = ttmq.task_author;
            string task_id_to = ttmq.task_id;

            List<string> str_lst = new List<string>();

            if (ttmq.ttm_whom_question.Count > 0)
                foreach (string elem in ttmq.ttm_whom_question)
                {
                    str_lst.Add(elem);
                }

            string TOSCRIPT_VAR1 = ttmq.ttm_question_title;
            string TOSCRIPT_VAR2 = ttmq.ttm_question_body;
            string TOSCRIPT_VAR3 = bc.getMultipleStringEncodingString(str_lst);

            schema_task_tracking_main sttm = new()
            {
                task_variable1 = TOSCRIPT_VAR1,
                task_variable2 = TOSCRIPT_VAR2,
                task_variable3 = TOSCRIPT_VAR3,
                task_author = task_author_to,
                task_id = task_id_to,
                task_cmd = TOSCRIPT_CMD,
                task_datetime = DateTime.Now,
            };

            _context.TaskTrackingMain_Create(sttm);

            string push_msg = "[Задача - " + Current_Task.task_name + "] \n";
            push_msg += "(Пользователь: " + getUsernameByLogin(task_author_to) + ") \n \n";
            push_msg += "В чате задан вопрос : \n";
            push_msg += ttmq.ttm_question_title + "\n";
            push_msg += ttmq.ttm_question_body + "\n";
            await bc.SendTelegramMessage(push_msg, Current_Task, Users, Divisions);

            await OnCarouselRefresh.InvokeAsync(true);
            await rtfQuestion.ClearAsync();
            ttm_question_title = "";

            StateHasChanged();
        }
   
        async Task Xtra_SendNews(string news_id)
        {
            if (int.TryParse(news_id,out int a))
            {
                schema_task_tracking_main ttm = new();
                ttm.task_author = Current_User.login;
                ttm.task_id = Current_Task.id.ToString();
                ttm.task_variable1 = news_id;
                ttm.task_cmd = "ADDNEWS";
                _context.TaskTrackingMain_Create(ttm);
            }
        }

        async Task Xtra_SendMaps(string maps_id)
        {
            bool alexandra = false;

            string pure_id;

            if (maps_id.Contains("alexandra"))
            {
                alexandra = true;
                pure_id = maps_id.Replace("alexandra", "");
            }
            else
            {
                pure_id =maps_id;
            }

            if (int.TryParse(pure_id, out int a))
            {
                schema_task_tracking_main ttm = new();
                ttm.task_author = Current_User.login;
                ttm.task_id = Current_Task.id.ToString();
                ttm.task_variable1 = pure_id;

                if (alexandra)
                {
                    ttm.task_variable2 = "alexandra";
                }
                else
                {
                    ttm.task_variable2 = "exchange";
                }

                ttm.task_cmd = "ADDMAPS";
                _context.TaskTrackingMain_Create(ttm);
            }
        }

        async Task Xtra_SendFiles(string path)
        {
            bool alexandra = false;

            string pure_path = "";

            if (path.Contains("alexandra"))
            {
                alexandra = true;
                path = path.Replace("alexandra", "");
            }
            else
            {
                pure_path = path;
            }

            schema_task_tracking_main ttm = new();
            ttm.task_author = Current_User.login;
            ttm.task_id = Current_Task.id.ToString();
            ttm.task_variable1 = pure_path;

            if (alexandra)
            {
                ttm.task_variable2 = "alexandra";
            }
            else
            {
                ttm.task_variable2 = "exchange";
            }

            ttm.task_cmd = "ADDFILES";
            _context.TaskTrackingMain_Create(ttm);

        }
       
   
        async Task Extra_AddNews()
        {
            await SaveCustomLocalStorageItem("extra_news", Current_Task.id.ToString());
            UriHelper.NavigateTo("/news_list");
        }

        async Task Extra_AddMaps()
        {
            await SaveCustomLocalStorageItem("extra_maps", Current_Task.id.ToString());
            UriHelper.NavigateTo("/maps");
        }

        async Task Extra_AddFile()
        {
            await SaveCustomLocalStorageItem("extra_files", Current_Task.id.ToString());
            UriHelper.NavigateTo("/store");
        }

        async Task Extra_AddContact()
        {
            await SaveCustomLocalStorageItem("extra_contacts", Current_Task.id.ToString());
            UriHelper.NavigateTo("/users_list");
        }

        #region Local Storage
        private int _localStorageCount;
        private IList<KeyValuePair<string, string>> _localStorageItems;

        List<int> users_in_lst = new List<int>();
        List<int> users_auth_lst = new List<int>();

        private async Task InsertLocalStorageItems(string key, string val)
        {
            await _localStorageService.SetItemAsync(key, val);
        }
        private async Task RefreshLocalStorageItems()
        {
            _localStorageItems = new List<KeyValuePair<string, string>>();
            await foreach (var key in _localStorageService.GetAllKeysAsync())
            {
                if (key is null)
                    continue;

                _localStorageItems.Add(new KeyValuePair<string, string>(key, await _localStorageService.GetItemAsStringAsync(key)));
            }

        }
        private async Task SaveCustomLocalStorageItem(string key, string val)
        {
            await _localStorageService.SetItemAsync(key, val);
            await RefreshLocalStorageItems();
        }
        private async Task<string> GetLocalStorageItems(string key)
        {
            return await _localStorageService.GetItemAsStringAsync(key);
        }

        #endregion



        async Task OnInputFileChange(InputFileChangeEventArgs e, string unique_filename, int FileNumber)
    {
       
        await using var timer = new Timer(_ => InvokeAsync(() => StateHasChanged()));
        timer.Change(TimeSpan.FromMilliseconds(500), TimeSpan.FromMilliseconds(500));

        const long CHUNKSIZE = 1024 * 400; // subjective

        IBrowserFile file = null;

        if (e.FileCount > 1)
        {
            int file_c = 0;
            var file_dump_col = e.GetMultipleFiles(e.FileCount);
            foreach (IBrowserFile file_in in file_dump_col)
            {
                if (file_c == FileNumber)
                {
                    file = file_dump_col[file_c];
                }
                file_c++;
            }
        }
        else
        {
            file = e.File;
        }

        long uploadedBytes = 0;
        long totalBytes = file.Size;
        long percent = 0;
        int fragment = 0;
        long chunkSize;

        var clientlocal = ClientFactory.CreateClient("LocalApi");

        using (var inStream = file.OpenReadStream(long.MaxValue))
        {
            _uploading = true;
            while (_uploading)
            {
                try
                {
                    chunkSize = CHUNKSIZE;
                    if (uploadedBytes + CHUNKSIZE > totalBytes)
                    {// remainder
                        chunkSize = totalBytes - uploadedBytes;
                    }
                    var chunk = new byte[chunkSize];
                    await inStream.ReadAsync(chunk, 0, chunk.Length);
                    // upload this fragment
                    using var formFile = new MultipartFormDataContent();
                    var fileContent = new StreamContent(new MemoryStream(chunk));
                    formFile.Add(fileContent, "file", unique_filename);
                    // post 

                    var response = await clientlocal.PostAsync($"api/AppendFileTaskTracking/{fragment++}", formFile);
                    // Update our progress data and UI
                    uploadedBytes += chunkSize;
                    percent = uploadedBytes * 100 / totalBytes;

                    file_UploadedPercentage = percent;
                    file_Size = totalBytes;
                    file_UploadedBytes = uploadedBytes;
                    file_fileName = file.Name;

                    echo = $"Uploaded {percent}%  {uploadedBytes} of {totalBytes} | Fragment: {fragment}";
                    if (percent >= 100)
                    {// upload complete
                        _uploading = false;
                        file_UploadedPercentage = 0;
                        file_Size = 0;
                        file_UploadedBytes = 0;
                        file_fileName = "";
                    }
                    await InvokeAsync(StateHasChanged);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }

    private async Task LoadFilesAttachments(InputFileChangeEventArgs e)
    {
        string filename = "";
        isLoading = true;
        loadedFiles.Clear();

        int file_num = -1;

        foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
        {
            file_num++;
            try
            {
                loadedFiles.Add(file);
                filename = file.Name;
                var trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath, "wwwroot","uploads","tasktracking", filename);

                //if (System.IO.File.Exists(Path.Combine(_env.ContentRootPath,_env.EnvironmentName, "uploads/calendar", filename)) || filename.Length > 100)

                if (System.IO.File.Exists(Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "tasktracking", filename)) || filename.Length > 100)
                {
                    string filename_we = Path.GetFileNameWithoutExtension(file.Name);
                    string extension = Path.GetExtension(file.Name);
                    filename = getUnicalFileName(filename_we, extension);
                }

                if (file.Size > maxFileSize)
                {
                    await OnInputFileChange(e, filename, file_num);
                }
                else
                {
                    await using FileStream fs = new(path, FileMode.Create);
                    await file.OpenReadStream(maxFileSize).CopyToAsync(fs);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("File: {Filename} Error: {Error}",
                    file.Name, ex.Message);
            }
            finally
            {
                attach_lst.Add(filename);
                attach_lst_links.Add("https://evicrm.store/uploads/tasktracking/" + filename);
                StateHasChanged();
            }
        }


    }

    public void interop_setNewGlobalPersonalStatus(string gps)
    {
        global_personal_status = gps;
        StateHasChanged();
    }

 

        public bool isTaskContainsSelf()
    {
        bool res = true;

        return res;
    }

    public class SelectData
    {
        public string Id { get; set; }

        public string Name { get; set; }
    }
}

    }