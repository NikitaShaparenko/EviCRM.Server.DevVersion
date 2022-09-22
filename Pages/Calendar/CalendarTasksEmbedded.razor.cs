using EviCRM.Server.Core;
using Microsoft.AspNetCore.Components.Forms;

namespace EviCRM.Server.Pages.Calendar
{
    public partial class CalendarTasksEmbedded
    {
        protected async override Task OnInitializedAsync()
        {

            switch (operationType)
            {
                case "CREATE":
                    modal_title = "Создание события";
                    schedule_model.scsh = new();
                    schedule_model.scsh.title = "";
                    schedule_model.scsh.body = "";
                    schedule_model.scsh.calendarId = "personal";
                    schedule_model.scsh.isnotify = false;
                    schedule_model.scsh.isinoffice = "office";

                    break;
                case "EDIT":
                    modal_title = "Редактирование события";
                    break;
                case "VIEW":
                    modal_title = "Просмотр события";
                    break;
            }

            isFileLoaded = false;

            if (operationType == "EDIT" || operationType == "VIEW")
            {
                attach_lst = bc.getMultipleStringDecodingString(schedule_model.scsh.attachments);

                foreach (string att_lnk in attach_lst)
                {
                    attach_lst_links.Add("https://evicrm.store/uploads/calendar/" + att_lnk);
                }

            }

            file_fileName = "";
            file_UploadedPercentage = 0;
            file_Size = 0;
            file_UploadedBytes = 0;

            StateHasChanged();

        }

        public DateTime WhatIsDay()
        {
            return schedule_model.StartDate;
        }

        private async Task ModalSaveChanges()
        {
            if (schedule_model.EndDate < schedule_model.StartDate)
            {
                await ToastErrorShow.InvokeAsync("Дата конца не может быть раньше даты начала!");
            }
            if (schedule_model.scsh.body == null || schedule_model.scsh.title == null)
            {
                await ToastErrorShow.InvokeAsync("Необходимо заполнить карточку!");
            }
            else
            {
                schedule_model.scsh.startDate = schedule_model.StartDate;
                schedule_model.scsh.endDate = schedule_model.EndDate;

                if (schedule_model.scsh.attachments == null)
                {
                    schedule_model.scsh.attachments = "$null";
                }

                switch (schedule_model.scsh.calendarId)
                {
                    case "corporate":
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#ff0000";
                        schedule_model.scsh.Code = "Работа";
                        break;

                    case "notes":
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#9966cc";
                        schedule_model.scsh.Code = "Заметки";
                        break;

                    case "personal":
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#ffe5b4";
                        schedule_model.scsh.Code = "Личное";
                        break;

                    default:
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#556832";
                        schedule_model.scsh.Code = "Всё остальное";
                        break;
                }

                if (schedule_model.scsh.attachments == null)
                {
                    schedule_model.scsh.attachments = "$null";
                }
                if (schedule_model.scsh.isinoffice == null)
                {
                    schedule_model.scsh.isnotify = false;
                }
                if (schedule_model.scsh.notify_period == null)
                {
                    schedule_model.scsh.notify_period = "$null";
                }

                schedule_model.Subject = schedule_model.scsh.title;

                await OnUpdate.InvokeAsync(schedule_model.scsh);
                await OnClose.InvokeAsync(true);

            }
        }

        private async Task ModalSave()
        {
            if (schedule_model.EndDate < schedule_model.StartDate)
            {
                await ToastErrorShow.InvokeAsync("Дата конца не может быть раньше даты начала!");
            }
            if (schedule_model.scsh.body == null || schedule_model.scsh.title == null)
            {
                await ToastErrorShow.InvokeAsync("Необходимо заполнить карточку!");
            }
            else
            {

                schedule_model.scsh.users = user_model.login;
                schedule_model.scsh.id = Guid.NewGuid().GetHashCode().ToString();
                schedule_model.scsh.startDate = schedule_model.StartDate;
                schedule_model.scsh.endDate = schedule_model.EndDate;

                if (schedule_model.scsh.attachments == null)
                {
                    schedule_model.scsh.attachments = "$null";
                }

                switch (schedule_model.scsh.calendarId)
                {
                    case "corporate":
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#ff0000";
                        schedule_model.scsh.Code = "Работа";
                        break;

                    case "notes":
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#9966cc";
                        schedule_model.scsh.Code = "Заметки";
                        break;

                    case "personal":
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#ffe5b4";
                        schedule_model.scsh.Code = "Личное";
                        break;

                    default:
                        schedule_model.scsh.ForeColor = "#ffffff";
                        schedule_model.scsh.Color = "#556832";
                        schedule_model.scsh.Code = "Всё остальное";
                        break;
                }

                if (schedule_model.scsh.attachments == null)
                {
                    schedule_model.scsh.attachments = "$null";
                }
                if (schedule_model.scsh.isinoffice == null)
                {
                    schedule_model.scsh.isnotify = false;
                }
                if (schedule_model.scsh.notify_period == null)
                {
                    schedule_model.scsh.notify_period = "$null";
                }

                schedule_model.Subject = schedule_model.scsh.title;

                await OnCreate.InvokeAsync(schedule_model.scsh);

                schedule_model.scsh = new();
                await OnClose.InvokeAsync(true);

            }
        }

        async Task OnInputFileChange(InputFileChangeEventArgs e, string unique_filename, int FileNumber)
        {
            var startIndex = uploadedFiles.Count;

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
                        {
                            chunkSize = totalBytes - uploadedBytes;
                        }
                        var chunk = new byte[chunkSize];
                        await inStream.ReadAsync(chunk, 0, chunk.Length);
                        using var formFile = new MultipartFormDataContent();
                        var fileContent = new StreamContent(new MemoryStream(chunk));
                        formFile.Add(fileContent, "file", unique_filename);
                        var response = await clientlocal.PostAsync($"api/CalendarUploadFile/{fragment++}", formFile);
                        uploadedBytes += chunkSize;
                        percent = uploadedBytes * 100 / totalBytes;

                        file_UploadedPercentage = percent;
                        file_Size = totalBytes;
                        file_UploadedBytes = uploadedBytes;
                        file_fileName = file.Name;

                        sc.Log("CalendarTasks", "Начинается загрузка большого файла '" + unique_filename + "'", SystemCore.LogLevels.Info);
                        sc.Log("CalendarTasks", $"Загружено {percent}%  {uploadedBytes} из {totalBytes} | Фрагмент: {fragment}", SystemCore.LogLevels.Debug);

                        if (percent >= 100)
                        {
                            //Загрузка завершена

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
                        sc.Log("CalendarTasks", "Произошла ошибка при загрузке большого файла '" + unique_filename + "'", SystemCore.LogLevels.Error);
                    }
                }
            }
        }
    }
}
