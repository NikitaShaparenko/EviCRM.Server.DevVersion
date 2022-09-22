using EviCRM.Server.Core;
using Majorsoft.Blazor.Components.Notifications;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace EviCRM.Server.Pages.Calendar
{
    public partial class CalendarTasks
    {
      
        protected async override Task OnInitializedAsync()
        {

            switch (operationType)
            {
                case "CREATE":
                    modal_title = "Создание события";

                    schedule_model.title = "";
                    schedule_model.body = "";
                    schedule_model.calendarId = "personal";
                    schedule_model.isnotify = false;
                    schedule_model.isinoffice = "office";

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
                attach_lst = bc.getMultipleStringDecodingString(schedule_model.attachments);

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

        private async Task ModalSaveChanges()
        {
           
            if (schedule_model.endDate < schedule_model.startDate)
            {
                await ToastErrorShow.InvokeAsync("Дата конца не может быть раньше даты начала!");
            }
            else
            {
                switch (schedule_model.calendarId)
                {
                    case "corporate":
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#ff0000";
                        schedule_model.Code = "Работа";
                        break;

                    case "notes":
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#9966cc";
                        schedule_model.Code = "Заметки";
                        break;

                    case "personal":
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#ffe5b4";
                        schedule_model.Code = "Личное";
                        break;

                    default:
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#556832";
                        schedule_model.Code = "Всё остальное";
                        break;
                }

                if (schedule_model.attachments == null)
                {
                    schedule_model.attachments = "$null";
                }
               if (schedule_model.isinoffice == null)
                {
                    schedule_model.isnotify = false;
                }
               if (schedule_model.notify_period == null)
                {
                    schedule_model.notify_period = "$null";
                }


                await OnUpdate.InvokeAsync(schedule_model);
                await OnClose.InvokeAsync(true);
            }
        }

        private async Task ModalSave()
        {
            if (schedule_model.endDate < schedule_model.startDate)
            {
                await ToastErrorShow.InvokeAsync("Дата конца не может быть раньше даты начала!");
            }
            else
            {
                switch (schedule_model.calendarId)
                {
                    case "corporate":
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#ff0000";
                        schedule_model.Code = "Работа";
                        break;

                    case "notes":
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#9966cc";
                        schedule_model.Code = "Заметки";
                        break;

                    case "personal":
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#ffe5b4";
                        schedule_model.Code = "Личное";
                        break;

                    default:
                        schedule_model.ForeColor = "#ffffff";
                        schedule_model.Color = "#556832";
                        schedule_model.Code = "Всё остальное";
                        break;
                }

                if (schedule_model.attachments == null)
                {
                    schedule_model.attachments = "$null";
                }
                if (schedule_model.isinoffice == null)
                {
                    schedule_model.isnotify = false;
                }
                if (schedule_model.notify_period == null)
                {
                    schedule_model.notify_period = "$null";
                }

                await OnCreate.InvokeAsync(schedule_model);
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
