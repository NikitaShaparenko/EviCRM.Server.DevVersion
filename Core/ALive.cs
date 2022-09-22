using Abp.Dependency;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.SignalR;

namespace EviCRM.Server.Core
{
    public class ALive : Hub, ITransientDependency
    {
        public IAbpSession AbpSession { get; set; }
        protected IHubContext<ALive> _context;
        public ILogger Logger { get; set; }


        public ALive(IHubContext<ALive> context)
        {
            AbpSession = NullAbpSession.Instance;
            _context = context;
        }


        public enum ALive_Database
        {
            calendar_schedules,
            divisions,
            key_store,
            //main,
            maps_points,
            marks,
            projects,
            roles,
            skills,
            task_tracking_card,
            task_tracking_main,
            //telegram_push,
            tasks,
            users,
            alexandra_contacts,
            alexandra_locations,
            alexandra_polls,
            markdown_cards,
            markdown_desks,
            markdown_todos,
        }

        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task SendMessageDirectly(string user)
        {
            await Clients.All.SendAsync("ReceiveMessageDirectly", user);
        }

        public async Task SendAll(List<ALive_Database> alives, string name = "alive_update_lst")
        {
            List<string> lavised = new List<string>();

            if (alives != null)
            {
                foreach(var elem in alives)
                {
                    lavised.Add(elem.ToString());
                }
            }

            string message = getMultipleStringEncodingString(lavised);

            await _context.Clients.All.SendAsync("broadcastMessage", name, message);
        }

        public async Task SendAll_Single(ALive_Database alive, string name = "alive_update_single")
        {
            string message = alive.ToString();
            await Clients.Others.SendAsync("broadcastMessage", name, message);
        }

        public string getMultipleStringEncodingString(List<string> input_str)
        {
            if (input_str != null)
            {

                string encoded = "";
                foreach (string str in input_str)
                {
                    encoded += str + "$$$";
                }
                return encoded;
            }
            return "";

        }

    }
}
