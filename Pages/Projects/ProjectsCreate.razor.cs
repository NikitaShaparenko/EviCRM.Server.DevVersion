using Blazor.Cropper;
using EviCRM.Server.Core;
using EviCRM.Server.Core.EntityFramework;
using Microsoft.AspNetCore.Components.Forms;
using MySqlConnector;
using System.Diagnostics;

namespace EviCRM.Server.Pages.Projects
{
    public partial class ProjectsCreate
    {
        
        protected override async Task OnInitializedAsync()
        {
            pcm.proj_start = DateTime.Now;
            pcm.proj_end = DateTime.Now;

            user_ = authStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;

            if (user_ != null)
            {
                 User = _context.User_Get(user_);

                //Проверка админки
                if (User.role == "admin"
                    || User.role == "owner"
                    || User.role == "secretary")
                {
                    isAdmin = true;
                }
                else
                {
                    isAdmin = false;
                }

                //Загрузка данных

                Users = _context.Users_Get();
                Tasks = _context.Tasks_Get();
                Projects = _context.Projects_Get();
                Divisions = _context.Divisions_Get();
            }
            else
            {
                //syslog
                //notify
                return;
            }

            select_data_personal = new List<SelectData>();
            select_data_divs = new List<SelectData>();
            select_data_free_tasks = new List<SelectData>();

            //Загрузка персонала

            if (Users != null)
            {
                foreach(var elem in Users)
                {
                    SelectData sd = new SelectData();

                    sd.Name = elem.fullname;
                    sd.Id = elem.id.ToString();

                    select_data_personal.Add(sd);
                }
            }

            if (Divisions != null)
            {
                foreach (var elem in Divisions)
                {
                    SelectData sd = new SelectData();

                    sd.Name = elem.division_name;
                    sd.Id = elem.id.ToString();

                    select_data_divs.Add(sd);
                }
            }

            if (Tasks != null)
            {
                foreach (var elem in Tasks)
                {
                    SelectData sd = new SelectData();

                    sd.Name = elem.task_name;
                    sd.Id = elem.id.ToString();

                    select_data_free_tasks.Add(sd);
                }
            }
        }

        string avatar_path = "";

        private long maxFileSize = 1024 * 15 * 1024;
        private int maxAllowedFiles = 100;


        private async Task LoadAvatar(InputFileChangeEventArgs e)
        {
            string filename = "";
            int file_num = -1;

            file_num++;
            try
            {

                filename = e.File.Name;
                var trustedFileNameForFileStorage = Path.GetRandomFileName();
                var path = Path.Combine(_env.ContentRootPath, "wwwroot","uploads","users","avatars", filename);

                //if (System.IO.File.Exists(Path.Combine(_env.ContentRootPath,_env.EnvironmentName, "uploads/calendar", filename)) || filename.Length > 100)

                if (System.IO.File.Exists(Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "users", "avatars", filename)) || filename.Length > 100)
                {
                    string filename_we = Path.GetFileNameWithoutExtension(e.File.Name);
                    string extension = Path.GetExtension(e.File.Name);
                    filename = getUnicalFileName(filename_we, extension);
                }

                path = Path.Combine(_env.ContentRootPath, "wwwroot", "uploads", "users", "avatars", filename);

                if (e.File.Size > maxFileSize)
                {
                    await ToastsNotifications_ShowCustomToast_Danger("Файл слишком большой для аватарки, его нельзя загрузить!");
                }
                else
                {
                    await using FileStream fs = new(path, FileMode.Create);
                    await e.File.OpenReadStream(maxFileSize).CopyToAsync(fs);
                    avatar_path = filename;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("File: {Filename} Error: {Error}",
                    e.File.Name, ex.Message);
            }
            finally
            {
                StateHasChanged();
            }


        }

        public async Task CreateProject()
        {
            if (pcm.proj_name == "" || pcm.proj_name == null)
            {
                await ToastsNotifications_ShowCustomToast_Danger("У проекта не может быть пустое имя!");
                return;
            }
            if (pcm.proj_short_desc == "" || pcm.proj_short_desc == null)
            {
                await ToastsNotifications_ShowCustomToast_Danger("У проекта не может быть пустое краткое имя!");
                return;
            }

            string body_text = await proj_description.GetHtmlAsync();

            if (body_text == "" || body_text == null)
            {
                await ToastsNotifications_ShowCustomToast_Danger("Необходимо ввести описание проекта!");
                return;
            }
            
            if (pcm.proj_end < DateTime.Now)
            {
                await ToastsNotifications_ShowCustomToast_Warning("Пытаешься создать проект задним числом. Это запрещено!");
                return;
            }

            if (pcm.proj_start > pcm.proj_end)
            {
                DateTime dt = new DateTime();
                dt = pcm.proj_start;
                pcm.proj_start = pcm.proj_end;
                pcm.proj_end = dt;
                await ToastsNotifications_ShowCustomToast_Danger("Проект не может начаться позже, чем закончится! Я поменял введённые даты местами, необходимо проверить правильность или продолжи так");
            }
            else
            {

                var value = _context.projects.FirstOrDefault(p => p.proj_name == pcm.proj_name);

                List<string> proj_members_list = new List<string>();
                List<string> proj_divs_list = new List<string>();
                List<string> proj_tasks_list = new List<string>();

                string filename = "";
                string filename_without_extension = "";
                string extension = "";

                string attach_u = "";
                string members_u = "";

                string filePath = "";

                if (value == null)
                {
                    int user_id = 0;

                    if (SelectedValues_personal != null)
                    foreach (string member in SelectedValues_personal)
                    {
                        proj_members_list.Add(member);
                    }

                    if (SelectedValues_divs != null)
                    foreach (string div in SelectedValues_divs)
                    {
                        proj_divs_list.Add(div);
                    }

                    if (SelectedValues_free_tasks != null)
                    foreach (string task in SelectedValues_free_tasks)
                    {
                        proj_tasks_list.Add(task);
                    }

                    string str_compiled_members = "";
                    string str_compiled_divs = "";
                    string str_compiled_tasks = "";

                    if (proj_members_list.Count > 0)
                    {
                        str_compiled_members = bc.getMultipleStringEncodingString(proj_members_list);
                    }
                    if (proj_divs_list.Count > 0)
                    {
                        str_compiled_divs = bc.getMultipleStringEncodingString(proj_divs_list);
                    }
                    if (proj_members_list.Count > 0)
                    {
                        str_compiled_tasks = bc.getMultipleStringEncodingString(proj_tasks_list);
                    }

                    string uniqueFileName_avatar = "";

                    body_text = Base64Encode(body_text);

                    schema_projects new_proj = new()
                    {
                        proj_name = pcm.proj_name,
                        proj_description = pcm.proj_short_desc,
                        proj_details = body_text,
                        proj_start = pcm.proj_start,
                        proj_end = pcm.proj_end,
                        proj_divs = str_compiled_divs,
                        proj_users = str_compiled_members,
                        proj_tasks = str_compiled_tasks,
                        proj_status = ProjectStatus.waiting.ToString(),
                        proj_avatar_path = avatar_path,
                    };

                    _context.Projects_Create(new_proj);

                    UriHelper.NavigateTo("/proj_list");

                    await ToastsNotifications_ShowCustomToast_Info("Проект успешно создан!");
                    await InvokeAsync(StateHasChanged);
                }
                else
                {
                    await ToastsNotifications_ShowCustomToast_Danger("Проект с таким имененм уже существует!");
                    Debug.WriteLine("Проект с таким именем уже существует!");
                }
            }
        }
    }
}