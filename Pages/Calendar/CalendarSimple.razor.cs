using BlazorCalendar;
using BlazorCalendar.Models;
using EviCRM.Server.Core;
using EviCRM.Server.Core.EntityFramework;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.EntityFrameworkCore;

namespace EviCRM.Server.Pages.Calendar
{

    public partial class CalendarSimple
  {
		public async Task CallbackDeleteInterpreter(int idc)
		{
			string connection_string = "server = 82.146.44.138;user = mysql_c;password = 512Dcfbd28#;database=evicrm";

			var contextOptions = new DbContextOptionsBuilder<Context>()
		.UseMySql(connection_string, ServerVersion.AutoDetect(connection_string))
		.Options;

			using (var dbContext = new Context(contextOptions))
			{
				dbContext.CalendarSchedule_Remove(idc);
			}
		}

		public async Task CallbackCreateInterpreter(schema_calendar_schedules model)
		{
			string connection_string = "server = 82.146.44.138;user = mysql_c;password = 512Dcfbd28#;database=evicrm";
			var contextOptions = new DbContextOptionsBuilder<Context>()
		.UseMySql(connection_string, ServerVersion.AutoDetect(connection_string))
		.Options;

			using (var dbContext = new Context(contextOptions))
			{
				dbContext.CalendarSchedule_Create(model);
			}
		}

		public async Task CallbackUpdateInterpeter(schema_calendar_schedules model)
		{
			string connection_string = "server = 82.146.44.138;user = mysql_c;password = 512Dcfbd28#;database=evicrm";

			var contextOptions = new DbContextOptionsBuilder<Context>()
		.UseMySql(connection_string, ServerVersion.AutoDetect(connection_string))
		.Options;

			using (var dbContext = new Context(contextOptions))
			{
				dbContext.CalendarSchedule_Update(model);
			}
		}

	

        protected override async Task OnInitializedAsync()
        {
			await LoadScheludeData();

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
				if (user == User.login)
				{
					CallLoadData();
					StateHasChanged();
				}
			});
			await hubConnection.StartAsync();

			await InvokeAsync(StateHasChanged);
		}

		private void DayClick(ClickEmptyDayParameter clickEmptyDayParameter)
		{
			fakeConsole = "Empty day :" + clickEmptyDayParameter.Day.ToShortDateString();
			isNewHandler = true;
			modal_event_date_click = clickEmptyDayParameter.Day.ToShortDateString();
			operationModal = "CREATE";

			Calendar_Schedule = new()
			{
				startDate = clickEmptyDayParameter.Day,
				endDate = clickEmptyDayParameter.Day,
				users = user_,
				id=Guid.NewGuid().GetHashCode().ToString(),
			};

			OpenDialog();
		}

		schema_calendar_schedules getScheduleByTaskID(int taskID)
		{
			return Calendar_SchedulesList.FirstOrDefault(p => p.idcalendar_schedules.Equals(taskID));
		}

		private void TaskClick(ClickTaskParameter clickTaskParameter)
		{
			operationModal = "EDIT";

			modal_ID = clickTaskParameter.IDList[0];
			schema_calendar_schedules task_clicked = getScheduleByTaskID(modal_ID);

			if (!isAdmin && task_clicked.calendarId == "corporate")
			{
				operationModal = "VIEW";
			}

			Calendar_Schedule = task_clicked;

			OpenDialog();
			StateHasChanged();
		}

		

	}
}
