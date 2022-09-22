using EviCRM.Server.Core;

using System.Collections.Generic;
using System.Net;

using EviCRM.Server.Models;
using EviCRM.Server.Models.TaskTracking;
using EviCRM.Server.ViewModels;
using System.Net.Sockets;
using System.Globalization;
using EviCRM.Server.Pages.Tasks;
using Majorsoft.Blazor.Extensions.BrowserStorage;

using System.Web.Mvc;

using Blzr.BootstrapSelect;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Majorsoft.Blazor.Components.Notifications;
using EviCRM.Server.Core.EntityFramework;
using Blazorise.RichTextEdit;

namespace EviCRM.Server.Pages.Tasks
{
    public partial class TaskTrack
    {
        public string ttm_commit_title { get; set; }
        public string dlist { get; set; }

        public List<string> workers = new List<string>();

        int task_id = -1;

        bool ready = false;
        private int _localStorageCount;
        private IList<KeyValuePair<string, string>> _localStorageItems;

        List<schema_projects> Projects = new();
        List<schema_tasks> Tasks = new List<schema_tasks>();
        schema_tasks Current_Task = new schema_tasks();

        bool ShowHideBioShown = true;
        bool TaskEditMode = false;

        RichTextEdit task_description;
        protected string contentAsHtml_taskDescription;
        protected string contentAsDeltaJson_taskDescription;
        protected string contentAsText_taskDescription;
        protected string savedContent_taskDescription;

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

        private void ShowHideBio()
        {
            ShowHideBioShown = !ShowHideBioShown;
        }
        private void TaskEditCancel()
        {
            TaskEditMode = false;
            StateHasChanged();
        }

        private void TaskEdit()
        {
            if (TaskEditMode)
            {
                Current_Task.task_description = contentAsHtml_taskDescription;
                _context.Tasks_Update(Current_Task);
                UriHelper.NavigateTo("/task_tracking", true);
            }
            else
            {
                TaskEditMode = true;
                StateHasChanged();
            }
        }

        List<schema_markdown_cards> Markdown_Cards = new();
        List<schema_markdown_desks> Markdown_Desks = new();
        List<schema_markdown_todo_list> Markdown_Todos = new();

        schema_users user_model = new();

        List<schema_users> Users = new();
        List<schema_divisions> Divisions = new();
        List<schema_users> UsersInTask = new();
        List<schema_divisions> DivisionsInTask = new();
        List<schema_task_tracking_card> TaskTrackingCards = new();
        List<schema_task_tracking_main> TaskTrackingMain = new(); 

        List<string> personal_status { get; set; }
        
        List<string> personal_status_arg1 {get;set;}
        List<string> personal_status_arg2 {get;set;}

        string task_personal_status { get; set; }

        string task_id_cookie { get; set; }
        bool isAdmin { get; set; }

        List<string> header_projs = new List<string>();

        string current_task_status { get; set; }

        string current_task_personal_status { get; set; }


        public async Task OnCloseFailClosed()
        {
            modalCloseFailedOpened = false;
            await UpdateTaskTrackingCarousel();

            StateHasChanged();
        }

        public async Task OnRemindBoardClosed()
        {
            modalRemindOpened = false;
            await UpdateTaskTrackingCarousel();

            StateHasChanged();
        }

        string header_task_author { get; set; }
        bool modalChangeOpened { get; set; }
        bool modalBeforeHeadOpened { get; set; }
        bool modalRemindOpened { get; set; }
        bool modalCloseFailedOpened { get; set; }
        bool modalRevisionOpened { get; set; }
        bool modalDateChangeOpened { get; set; }

        bool task_tracking_carousel_loaded { get; set; }
        private void ModalChangeOpen()
        {
            modalChangeOpened = true;
            StateHasChanged();
        }

       async Task OnCloseRevisionClosed(bool accepted)
        {
            modalRevisionOpened = false;
            await UpdateTaskTrackingCarousel();
            StateHasChanged();
        }

       async Task OnUpdateRevision(string str)
        {
            current_task_status = str;
            await UpdateTaskTrackingCarousel();
            StateHasChanged();
        }

        async Task ModalBeforeHeadOpen()
        {
            TaskTrackingModelCommon ttmc = new TaskTrackingModelCommon();
            ttmc.task_author = task_id_cookie;
            ttmc.task_id = task_id.ToString();

            //TOSCRIPT_COMPLETEBEFOREHAND_HANDLER(ttmc);
            //await mysqlc.MySql_ContextAsyncGeneral(mysqlc.updateTaskStatus(task_id.ToString(), "completed"));

            current_task_status = "completed";

            await UpdateTaskTrackingCarousel();
            StateHasChanged();
        }

        private async Task PersonalStatusChange(string new_personal_status)
        {
            global_personal_status = new_personal_status;
            int z = getPersonalStatusArrayIDByUsername(user_);

            await UpdateTaskTrackingCarousel();
            await UpdatePersonalStatus();
        }

        private async Task UpdatePersonalStatus()
        {
            personal_status = bc.getMultipleStringDecodingString(task_personal_status); //разбились на пары
            personal_status_arg1 = bc.getMultArgs_DeCombine_Args1(personal_status); //шапки пар
            personal_status_arg2 = bc.getMultArgs_DeCombine_Args2(personal_status); // значения пар

            int z = getPersonalStatusArrayIDByUsername(user_);
            if (personal_status_arg2.Count > 0)
            {
                global_personal_status = personal_status_arg2[z];
            }
            InvokeAsync(StateHasChanged);
        }
        private void ModalRemindOpen()
        {
            modalRemindOpened = true;
            StateHasChanged();
        }

        private void ModalCloseFailedOpen()
        {
            modalCloseFailedOpened = true;
            StateHasChanged();
        }

        private void ModalRevisionOpen()
        {
            modalRevisionOpened = true;
            StateHasChanged();
        }

        #region Toasts Notifications

        //Toasts
        private async Task ToastsNotifications_ShowAllToast()
        {
            foreach (var item in Enum.GetValues<NotificationTypes>())
            {
                _toastService.ShowToast($@"<strong>Toast оповещения:</strong> Это оповещение типа {item}", item);
            }
        }

        private Guid _lastToastId;

        private async Task ToastsNotifications_ShowCustomToast(string toastText, NotificationStyles ns, NotificationTypes ntype)
        {
            _lastToastId = _toastService.ShowToast(new ToastSettings()
            {
                Content = builder => builder.AddMarkupContent(0, toastText),
                NotificationStyle = ns,
                Type = ntype,
                AutoCloseInSec = _toastAutoCloseInSec,
                ShadowEffect = _toastShadowEffect,
                ShowCloseButton = _toastShowCloseButton,
                ShowCloseCountdownProgress = _toastShowCountdownProgress,
                ShowIcon = _toastShowIcon
            });
        }

        private async Task ToastsNotifications_ShowCustomToast_Primary(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Primary);
        }
        private async Task ToastsNotifications_ShowCustomToast_Secondary(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Secondary);
        }
        private async Task ToastsNotifications_ShowCustomToast_Info(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Info);
        }
        private async Task ToastsNotifications_ShowCustomToast_Success(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Success);
        }
        private async Task ToastsNotifications_ShowCustomToast_Warning(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Warning);
        }
        private async Task ToastsNotifications_ShowCustomToast_Danger(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Danger);
        }
        private async Task ToastsNotifications_ShowCustomToast_Light(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Light);
        }
        private async Task ToastsNotifications_ShowCustomToast_Dark(string toastText)
        {
            await ToastsNotifications_ShowCustomToast(toastText, NotificationStyles.Normal, NotificationTypes.Dark);
        }
        private async Task RemoveAllToasts()
        {
            _toastService.ClearAll();
        }
        private async Task RemoveLastToasts()
        {
            if (_lastToastId != Guid.Empty)
            {
                _toastService.RemoveToast(_lastToastId);
            }
        }
        #endregion

        public async Task ShowCustomNotification(ToastNotification toast)
        {
            switch(toast.get_ToastType())
            {
                case ToastNotification.ToastsTypes.Primary:
                    await ToastsNotifications_ShowCustomToast_Primary(toast.get_BodyText());
                    break;

                case ToastNotification.ToastsTypes.Secondary:
                    await ToastsNotifications_ShowCustomToast_Secondary(toast.get_BodyText());
                    break;

                case ToastNotification.ToastsTypes.Info:
                    await ToastsNotifications_ShowCustomToast_Info(toast.get_BodyText());
                    break;

                case ToastNotification.ToastsTypes.Success:
                    await ToastsNotifications_ShowCustomToast_Success(toast.get_BodyText());
                    break;

                case ToastNotification.ToastsTypes.Warning:
                    await ToastsNotifications_ShowCustomToast_Warning(toast.get_BodyText());
                    break;

                case ToastNotification.ToastsTypes.Danger:
                    await ToastsNotifications_ShowCustomToast_Danger(toast.get_BodyText());
                    break;

                case ToastNotification.ToastsTypes.Light:
                    await ToastsNotifications_ShowCustomToast_Light(toast.get_BodyText());
                    break;

                case ToastNotification.ToastsTypes.Dark:
                    await ToastsNotifications_ShowCustomToast_Dark(toast.get_BodyText());
                    break;
            }
        }

     
        private void On_modalChangeOpen(bool accepted)
        {
            modalChangeOpened = false;
            StateHasChanged();

        }

        public void Context_Handler_UpdateDesk(schema_markdown_desks desk)
        {
            schema_task_tracking_main scttm = new();
            scttm.task_cmd = "EDITDESK";
            scttm.task_author = user_model.id.ToString();
            scttm.task_id = task_id.ToString();
            scttm.task_variable1 = desk.markdown_desk_name;
            scttm.task_variable2 = "$null";
            scttm.task_variable3 = getDeskNameByDeskID(desk.idmarkdown_desks);
            scttm.task_card = desk.idmarkdown_desks.ToString();

            _context.TaskTrackingMain_Create(scttm);
            _context.MarkdownDesk_Update(desk);
            StateHasChanged();

        }

        public void ContextHandler_UpdateCardWithTodos(MarkdownCard_TwoWayDesksBindings mctwdb)
        {
            schema_task_tracking_main scttm = new();
            scttm.task_cmd = "EDITCARD";
            scttm.task_author = user_model.id.ToString();
            scttm.task_id = task_id.ToString();
            scttm.task_variable1 = mctwdb.Card.markdown_card_name;
            scttm.task_variable2 = mctwdb.Card.markdown_card_name;
            scttm.task_variable3 = getDeskNameByDeskID(mctwdb.Card.markdown_card_deskid);
            scttm.task_card = mctwdb.Card.idmarkdown_cards.ToString();

            if (mctwdb.Todos != null)
            {
                if (mctwdb.Todos.Count > 0)
                {
                    _context.Markdowntodo_Update(mctwdb.Todos);
                }
            }

            _context.TaskTrackingMain_Create(scttm);
            _context.MarkdownCard_Update(mctwdb.Card);


            StateHasChanged();
        }

        public void ContextHandler_UpdateCard(schema_markdown_cards card)
        {
            schema_task_tracking_main scttm = new();
            scttm.task_cmd = "EDITCARD";
            scttm.task_author = user_model.id.ToString();
            scttm.task_id = task_id.ToString();
            scttm.task_variable1 = card.markdown_card_name;
            scttm.task_variable2 = card.markdown_card_name;
            scttm.task_variable3 = getDeskNameByDeskID(card.markdown_card_deskid);
            scttm.task_card = card.idmarkdown_cards.ToString();

            _context.TaskTrackingMain_Create(scttm);
            _context.MarkdownCard_Update(card);
            StateHasChanged();
        }


        public void Context_Handler_DelegateCard(MarkdownCard_TwoWayDesksBindings mctwdb)
        {
            schema_task_tracking_main scttm = new();
            scttm.task_cmd = "DELEGATECARD";
            scttm.task_author = user_model.id.ToString();
            scttm.task_id = task_id.ToString();
            scttm.task_variable1 = mctwdb.Card.markdown_card_name;
            scttm.task_variable2 = mctwdb.Card.markdown_card_name;
            scttm.task_variable3 = getDeskNameByDeskID(mctwdb.Card.markdown_card_deskid);
            scttm.task_card = mctwdb.Card.idmarkdown_cards.ToString();

            _context.TaskTrackingMain_Create(scttm);
            _context.MarkdownCard_Update(mctwdb.Card);
            StateHasChanged();
        }

        public string getDeskNameByDeskID(int desk_id)
        {
            var elem = _context.markdown_desks.FirstOrDefault(p => p.idmarkdown_desks == desk_id);
            if (elem != null)
            {
                return elem.markdown_desk_name;
            }
            else
            {
                return "( доска без имени )";
            }
        }

        public void Context_Handler_DelegateDesk(Markdown_TwoWayDesksBindings mtdb)
        {
            schema_task_tracking_main scttm = new();
            scttm.task_cmd = "DELEGATEDESK";
            scttm.task_author = user_model.id.ToString();
            scttm.task_id = task_id.ToString();
            scttm.task_variable1 = mtdb.Desk.markdown_desk_name;
            scttm.task_variable2 = mtdb.Desk.markdown_desk_name;
            scttm.task_variable3 = getDeskNameByDeskID(mtdb.Desk.idmarkdown_desks);
            scttm.task_card = mtdb.Desk.markdown_desk_task_id.ToString();

            _context.TaskTrackingMain_Create(scttm);
            _context.MarkdownDesk_Update(mtdb.Desk);
            StateHasChanged();
        }

        public void Context_Handler_RemoveCard(MarkdownCard_TwoWayDesksBindings mctwdb)
        {
            schema_task_tracking_main scttm = new();
            scttm.task_cmd = "REMOVECARD";
            scttm.task_author = user_model.id.ToString();
            scttm.task_id = task_id.ToString();
            scttm.task_variable1 = mctwdb.Card.markdown_card_name;
            scttm.task_variable2 = mctwdb.Card.markdown_card_name;
            scttm.task_variable3 = mctwdb.Card.markdown_card_description;
            scttm.task_card = mctwdb.Card.idmarkdown_cards.ToString();

            _context.TaskTrackingMain_Create(scttm);
            _context.MarkdownDesk_Remove(mctwdb.Card.idmarkdown_cards);
            StateHasChanged();
        }

        public bool isSecondStep { get; set; }

        bool step2_allow { get; set; }


        TaskTracking.TaskTracking_DownActionBar ttd;

        async Task RogueConfirm()
        {
            if (Current_Task.task_startdate>DateTime.Now)
            {
                global_personal_status = "approved";
            }
            else if (Current_Task.task_enddate>DateTime.Now && Current_Task.task_startdate<DateTime.Now)
            {
                global_personal_status = "pending";
            }
            else
            {
                global_personal_status = "delayed";
            }
            ttd.interop_setNewGlobalPersonalStatus(global_personal_status);

            EviCRM.Server.ViewModels.TaskTrackingModelCommon ttmc = new EviCRM.Server.ViewModels.TaskTrackingModelCommon();

            ttmc.task_author = user_;
            ttmc.task_id = task_id.ToString();
            ttmc.toscript_cmd = "roger";

            string TOSCRIPT_CMD = "ROGER";

            string task_author_to = ttmc.task_author;
            string task_id_to = ttmc.task_id;

            string TOSCRIPT_VAR1 = "";
            string TOSCRIPT_VAR2 = "";

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
            push_msg += "Пользователь подтвердил, что приступил к выполнению задачи! \n";

            await bc.SendTelegramMessage(push_msg, Current_Task, Users, Divisions);

            personal_status = bc.getMultipleStringDecodingString(task_personal_status); //разбились на пары
            personal_status_arg1 = bc.getMultArgs_DeCombine_Args1(personal_status); //шапки пар
            personal_status_arg2 = bc.getMultArgs_DeCombine_Args2(personal_status); // значения пар

            int z = getPersonalStatusArrayIDByUsername(user_);
            global_personal_status = global_personal_status;

            await UpdateTaskTrackingCarousel();


            StateHasChanged();
        }

        string personal_status_updater_packer(int p,string value)
        {

            string str = "";

            for (int i = 0; i < personal_status.Count; i++)
            {
                if (p != i)
                {
                    str += personal_status[i] + "$$$";
                }
                else
                {
                    str += personal_status_arg1[i] + "$===$" + value + "$$$";
                }
            }

            personal_status_arg2[p] = value;
            global_personal_status = value;

            return str ;
        }

        async Task UpdateTaskTrackingCarousel()
        {
            task_tracking_carousel_loaded = false;

            Current_Task = _context.Task_Get(task_id);
            Projects = _context.Projects_Get(Current_Task);
            Tasks = _context.Tasks_Get();

            Markdown_Cards = _context.MarkdownCard_Get(Current_Task);
            Markdown_Desks = _context.MarkdownDesk_Get(Current_Task);
            Markdown_Todos = _context.MarkdownTodo_Get(Current_Task);

            Users = _context.Users_Get();
            Divisions = _context.Divisions_Get();
            UsersInTask = _context.Users_Get(Current_Task);
            DivisionsInTask = _context.Divisions_Get(Current_Task);
            TaskTrackingCards = _context.TaskTrackingCard_Get(Current_Task);
            TaskTrackingMain = _context.TaskTrackingMain_Get(Current_Task);

            task_personal_status = Current_Task.tasks_personal_status;

            personal_status = bc.getMultipleStringDecodingString(task_personal_status); //разбились на пары
            personal_status_arg1 = bc.getMultArgs_DeCombine_Args1(personal_status); //шапки пар
            personal_status_arg2 = bc.getMultArgs_DeCombine_Args2(personal_status); // значения пар

            task_tracking_carousel_loaded = true;
            StateHasChanged();
        }

        public schema_divisions getDivByDivID(string id)
        {
            return Divisions.FirstOrDefault(p => p.id.ToString() == id);
        }

        public schema_users getUserByUserID(string id)
        {
            return Users.FirstOrDefault(p => p.id.ToString() == id);
        }

        public string getFullnameUserByLogin(string login)
        {
            return Users.FirstOrDefault(p => p.login.Equals(login)).fullname;
        }

        async Task OnOkFailClosed()
        {
            modalCloseFailedOpened = false;
            current_task_status = "failed";
            await UpdateTaskTrackingCarousel();
            StateHasChanged();
        }

        public void OnUpdatingDtStart(TaskTrackingChangeDatesModel ttcdm)
        {
            Current_Task.task_startdate = ttcdm.dt_start;
            Current_Task.task_enddate = ttcdm.dt_end;
            StateHasChanged();
        }

        bool isMarkdownCardHistoryShown = false;

        public void OnMarkdownHistoryClosed(bool status)
        {
            isMarkdownCardHistoryShown = false;
            StateHasChanged();
        }
        schema_markdown_cards MarkdownHistoryCard = new();

        string user_ { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                new Timer(DateTimeCallback, null, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1));
            }
        }

        public async Task ModalDatesChangeOpen()
        {
            modalDateChangeOpened = true;
            await InvokeAsync(StateHasChanged);

        }

        private void DateTimeCallback(object state)
        {
            InvokeAsync(() => StateHasChanged());
        }

        string global_personal_status { get; set; }


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                task_tracking_carousel_loaded = false;
                if (await localStorage.GetItemAsStringAsync("task_tracking_id") == null)
                {
                    task_id_cookie = "";
                }
                else
                {
                    task_id_cookie = await localStorage.GetItemAsStringAsync("task_tracking_id");
                    if (int.TryParse(task_id_cookie, out var id))
                    {
                        task_id = id;
                    }
                    else
                    {
                        return;
                    }
                }

                //Clear lcl storage vars:

                if (await localStorage.GetItemAsStringAsync("extra_news") != null)
                {
                    await localStorage.RemoveItemAsync("extra_news");
                }
                if (await localStorage.GetItemAsStringAsync("extra_maps") != null)
                {
                    await localStorage.RemoveItemAsync("extra_maps");
                }
                if (await localStorage.GetItemAsStringAsync("extra_files") != null)
                {
                    await localStorage.RemoveItemAsync("extra_files");
                }
              
                //Loopback

                if (await localStorage.GetItemAsStringAsync("xtra_news") != null)
                {
                    //Xtra_SendNews(await localStorage.GetItemAsStringAsync("xtra_news"));
                    await localStorage.RemoveItemAsync("xtra_news");
                }
                if (await localStorage.GetItemAsStringAsync("xtra_maps") != null)
                {
                    //Xtra_SendMaps(await localStorage.GetItemAsStringAsync("xtra_maps"));
                    await localStorage.RemoveItemAsync("xtra_maps");
                }
                if (await localStorage.GetItemAsStringAsync("xtra_files") != null)
                {
                    //Xtra_SendFiles(await localStorage.GetItemAsStringAsync("xtra_files"));
                    await localStorage.RemoveItemAsync("xtra_files");
                }
               

                //

                user_ = authStateProvider.GetAuthenticationStateAsync().Result.User.Identity.Name;


                if (task_id_cookie != null && task_id_cookie != "")
                {
                    if (int.TryParse(task_id_cookie, out int a))
                    {
                        task_id = int.Parse(task_id_cookie);
                    }
                }

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
                    //syslog
                    //notify
                    return;
                }

                Current_Task = _context.Task_Get(task_id);
                Projects = _context.Projects_Get(Current_Task);
                Tasks = _context.Tasks_Get();

                Markdown_Cards = _context.MarkdownCard_Get(Current_Task);
                Markdown_Desks = _context.MarkdownDesk_Get(Current_Task);
                Markdown_Todos = _context.MarkdownTodo_Get(Current_Task);

                Users = _context.Users_Get();
                Divisions = _context.Divisions_Get();
                UsersInTask = _context.Users_Get(Current_Task);
                DivisionsInTask = _context.Divisions_Get(Current_Task);
                TaskTrackingCards = _context.TaskTrackingCard_Get(Current_Task);
                TaskTrackingMain = _context.TaskTrackingMain_Get(Current_Task);

                task_personal_status = Current_Task.tasks_personal_status;

                personal_status = bc.getMultipleStringDecodingString(task_personal_status); //разбились на пары
                personal_status_arg1 = bc.getMultArgs_DeCombine_Args1(personal_status); //шапки пар
                personal_status_arg2 = bc.getMultArgs_DeCombine_Args2(personal_status); // значения пар

                int z = getPersonalStatusArrayIDByUsername(user_);
                if (personal_status_arg2.Count > 0)
                { 
                     global_personal_status = personal_status_arg2[z];
                }

                task_tracking_carousel_loaded = true;

                ready = true;
                await UpdateTaskTrackingCarousel();
                StateHasChanged();


            }
        }

        public void OnDivListRefresh(bool a)
        {

        }

        public void SetNewTaskDivision(List<schema_divisions> divs_)
        {
            List<string> div_id_lst = new();
            if (divs_ != null)
            {
                if (divs_.Count > 0)
                {
                    foreach(var elem in divs_)
                    {
                        div_id_lst.Add(elem.id.ToString());
                    }
                }
            }
            Current_Task.tasks_members_divs = bc.getMultipleStringEncodingString(div_id_lst);
            _context.Tasks_Update(Current_Task);
            StateHasChanged();
        }

        public void SetNewTaskUsers(List<schema_users> users_)
        {
            List<string> users_id_lst = new();
            if (users_ != null)
            {
                if (users_.Count > 0)
                {
                    foreach (var elem in users_)
                    {
                        users_id_lst.Add(elem.id.ToString());
                    }
                }
            }
            Current_Task.task_members = bc.getMultipleStringEncodingString(users_id_lst);
            _context.Tasks_Update(Current_Task);
            StateHasChanged();
        }

        public string getTaskNameByID(int task_id)
        {
            return Tasks.FirstOrDefault(p => p.id.Equals(task_id)).task_name;
        }
        
        public string getUsernameByLogin(string login)
        {
            return Users.FirstOrDefault(p => p.login.Equals(login)).fullname;
        }

        public async Task TaskStatusUpdate(string new_status)
        {
            current_task_status = new_status;
            await UpdateTaskTrackingCarousel();
            StateHasChanged();
        }

        public int getPersonalStatusArrayIDByUsername(string username)
        {
            for (int i = 0; i < personal_status_arg1.Count; i++)
            {
                if (personal_status_arg1[i] == username)
                {
                    return i;
                }
            }
            return 0;
        }

        private async Task<string> GetLocalStorageItems(string key)
        {
            return await localStorage.GetItemAsStringAsync(key);
        }

        public void UpdatingDateChangeInterpreter(List<DateTime> dt_lst)
        {
            if (dt_lst.Count > 0)
            {
                Current_Task.task_startdate = dt_lst[0];
                Current_Task.task_enddate = dt_lst[1];
                //mysql place
            }
            StateHasChanged();
        }

        public void OnCloseDateChanger(bool accepted)
        {
            if (accepted)
            {
                modalDateChangeOpened = false;
                StateHasChanged();
            }
        }

        private string GetUniqueFileName(string fileName)
        {
            string filename = Path.GetFileNameWithoutExtension(fileName);
            filename = filename.Substring(0, 60);

            if (fileName.Length > 60)
            {
                fileName = fileName.Substring(0, 60);
            }

            return (filename)
                      + "_"
                      + Guid.NewGuid().ToString().Substring(0, 6)
                      + Path.GetExtension(fileName);

        }

        private void ErrorModalHandler(bool accepted)
        {
            UriHelper.NavigateTo("/tasks_list", true);
            StateHasChanged();

        }


        //Markdown General Handlers

        public void ContextHandler_CallHistory(schema_markdown_cards card)
        {
            MarkdownHistoryCard = card;
            isMarkdownCardHistoryShown = true;
            StateHasChanged();
        }
    }
}
