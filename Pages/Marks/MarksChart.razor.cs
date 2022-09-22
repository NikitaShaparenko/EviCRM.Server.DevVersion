using EviCRM.Server.Core;
using System.Collections.Generic;
using System.Net;
using EviCRM.Server.Models.MarksChart;
using EviCRM.Server.Pages.Tasks.Components;
using EviCRM.Server.ViewModels;
using System.IO;
using EviCRM.Server.Pages.Modals;
using EviCRM.Server.Models;
using EviCRM.Server.Core.EntityFramework;
using Microsoft.AspNetCore.SignalR.Client;

namespace EviCRM.Server.Pages.Marks
{
    public partial class MarksChart
    {

        public void CreateMarkInterpreter(schema_marks mcm)
        {
            //Обработчик создания оценки

            _context.Marks_Create(mcm);

            int user_id = mcm.user_id;
            var elem = schs_users.FirstOrDefault(p => p.id == user_id);

            liven.LiveNature_AddNotify(elem, LiveNature.livenature_categories.notifcations_markschart);

            if(elem != null)
            {
                hubConnection.SendAsync("SendMessageDirectly", elem.login);
            }
            else
            {
                hubConnection.SendAsync("SendMessage", "username", "message");
            }

            if (elem != null)
            {
                if (!mcm.isTwoMarks)
                {
                    Task.Run(async () =>
                    {
                        bc.SendTelegramMessage($"{user_model.fullname} поставил тебе за {mcm.date.ToShortDateString()} оценку {mcm.first_mark}, с комментарием: {mcm.first_description}", elem);
                    });
                 }
                else
                {
                    Task.Run(async () =>
                    {
                        bc.SendTelegramMessage($"{user_model.fullname} поставил тебе за {mcm.date.ToShortDateString()} оценки {mcm.first_mark} / {mcm.second_mark}, с комментариями: {mcm.first_description},{mcm.second_description}", elem);
                    });
                    }
            }

            liven.LiveNature_AddNotify(user_model, LiveNature.livenature_categories.notifcations_markschart);
            sc.Log("MarksChart", $"Пользователь {user_model.fullname} поставил оценку в дневнике", SystemCore.LogLevels.Info);
            sc.Syslog(_env, user_model.login, "Дневник", $"Поставил оценку #{mcm.idmarks} в дневнике", SystemCore.LogLevels.Info);

            StateHasChanged();
        }

        public void UpdateInterpreter(schema_marks mcm)
        {
            //Обработчик изменения оценки

            _context.Marks_Update(mcm);

            int user_id = mcm.user_id;
            var elem = schs_users.FirstOrDefault(p => p.id == user_id);

            liven.LiveNature_AddNotify(elem, LiveNature.livenature_categories.notifcations_markschart);

            if (elem != null)
            {
                hubConnection.SendAsync("SendMessageDirectly", elem.login);
            }
            else
            {
                hubConnection.SendAsync("SendMessage", "username", "message");
            }

            liven.LiveNature_AddNotify(user_model, LiveNature.livenature_categories.notifcations_markschart);
            sc.Log("Login", $"Пользователь {user_model.fullname} изменил оценку в дневнике", SystemCore.LogLevels.Info);
            sc.Syslog(_env, user_model.login, "Дневник", $"Изменил оценку #{mcm.idmarks} в дневнике", SystemCore.LogLevels.Info);

            StateHasChanged();
        }

        public void DeleteInterpreter(schema_marks mcm)
        {

            //Обработчик изменения оценки

            _context.Marks_Remove(mcm.idmarks);

            int user_id = mcm.user_id;
            var elem = schs_users.FirstOrDefault(p => p.id == user_id);

            liven.LiveNature_AddNotify(elem, LiveNature.livenature_categories.notifcations_markschart);

            if (elem != null)
            {
                hubConnection.SendAsync("SendMessageDirectly", elem.login);
            }
            else
            {
                hubConnection.SendAsync("SendMessage", "username", "message");
            }

            liven.LiveNature_AddNotify(user_model, LiveNature.livenature_categories.notifcations_markschart);
            sc.Log("Login", $"Пользователь {user_model.fullname} удалил оценку в дневнике", SystemCore.LogLevels.Info);
            sc.Syslog(_env, user_model.login, "Дневник", $"Удалил оценку #{mcm.idmarks} в дневнике", SystemCore.LogLevels.Info);
            StateHasChanged();
        }

        protected override void OnInitialized()
        {
            //Текущая дата в журнале
            _global_year = DateTime.Now;
            _global_month = DateTime.Now;

            //Получение имени пользователя
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

                //Загрузка данных

                if (isAdmin)
                {
                    schs_marks = _context.Marks_Get();
                    schs_users = _context.Users_Get();
                }
                else
                {
                    schs_marks = _context.Marks_Get(user_);
                    schs_users = _context.Users_Get();
                }

                liven.LiveNature_RemoveNotify(user_model, LiveNature.livenature_categories.notifcations_markschart); //Очистка уведомлений от дневника
                sc.Log("MarksChart", $"Пользователь {user_model.fullname} открыл дневник", SystemCore.LogLevels.Info);
                sc.Syslog(_env, user_model.login, "Дневник", "Открыл дневник", SystemCore.LogLevels.Info);
            }
            else
            {
                return;
            }

            day_num = DateTime.DaysInMonth(_global_year.Year, _global_month.Month);
            DayCounter();

            modal_operationType = "";
            mcm = new();
            modal_Date = DateTime.Now;
            modal_ID = 0;
            modal_Name = "";

            ready = true;

            StateHasChanged();
        }

        protected async override Task OnInitializedAsync()
        {
            #region ALive
            hubConnection = new HubConnectionBuilder()
        .WithUrl(NavigationManager.ToAbsoluteUri("/alive"))
        .Build();

            hubConnection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                CallLoadData();
                StateHasChanged();
            });

            hubConnection.On<string>("ReceiveMessageDirectly", (user) =>
            {
                if (user == user_model.login)
                {
                    CallLoadData();
                    StateHasChanged();
                }
            });
            await hubConnection.StartAsync();
            #endregion
        }

        public void ShowInfo(schema_users user, int day, bool isPatched)
        {
            modal_ID = 0;
            modal_Date = new DateTime(_global_year.Year, _global_month.Month, day); //Заполнение модального окна под оценку
            modal_Name = user.fullname.ToString();

            if (isAdmin) //Проверка админки
            {
                if (isPatched) //Создание оценки, обход заглушки
                {
                    DateTime dt_n = new DateTime(_global_year.Year, _global_month.Month, day);

                    mcm = new();

                    modal_operationType = "CREATE";
                    mcm.date = dt_n;
                    mcm.first_description = "";
                    mcm.first_mark = 5;
                    mcm.second_description = "";
                    mcm.second_mark = 5;
                    mcm.user_id = user_model.id;
                    mcm.isTwoMarks = true;
                    mcm.firstAttachments = "";
                    mcm.secondAttachments = "";
                    OpenDeleteDialog();
                }
                else //Редактирование оценки, админка
                {
                    modal_operationType = "EDIT";
                    DateTime dt_n = new DateTime(_global_year.Year, _global_month.Month, day);

                    //Оценку по этому пользователю
                    var mark = sql_getMarkByUserAndDate(user, dt_n);

                    mcm = mark;
                    OpenDeleteDialog();
                }
            }
            else
            {
                if (isPatched) //Невозможное событие, просмотр от админки
                {


                }
                else //Просмотр оценки, нет админки
                {
                    modal_operationType = "VIEW";
                    DateTime dt_n = new DateTime(_global_year.Year, _global_month.Month, day);

                    //Оценку по этому пользователю
                    var mark = sql_getMarkByUserAndDate(user, dt_n);

                    mcm = mark;
                    OpenDeleteDialog();
                }
            }
        }
    }

}
