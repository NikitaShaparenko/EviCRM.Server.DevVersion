using EviCRM.Server.Core.EntityFramework;
using Microsoft.AspNetCore.Components;

namespace EviCRM.Server.Core
{
    public class LiveNature
    {
        public enum livenature_categories
        {
              notifications_admin,
        notifications_index,
        notifications_tasks,
        notifications_projs,
        notifications_users,
        notifications_calendar,
        notifcations_markschart,
        notifications_chat,
        notifications_video,
        notifications_store,
        notifications_news,
        notifications_maps,
        notifications_email,
    }
        public void LiveNature_AddNotify(schema_users user, livenature_categories lnc, int sum_i = 1)
        {
            LiveNatureModel lnm = new(user);

            if (isUserInList(user))
            {
                lnm = GetLNM_ModelByUser(user);
            }
            else
            {
                return;
            }

            switch(lnc)
            {
                case livenature_categories.notifications_admin:
                    lnm.notifications_admin+=sum_i;
                    break;

                case livenature_categories.notifications_index:
                    lnm.notifications_index += sum_i;
                    break;

                case livenature_categories.notifications_tasks:
                    lnm.notifications_tasks += sum_i;
                    break;

                case livenature_categories.notifications_projs:
                    lnm.notifications_projs += sum_i;
                    break;

                case livenature_categories.notifications_users:
                    lnm.notifications_users += sum_i;
                    break;

                case livenature_categories.notifications_calendar:
                    lnm.notifications_calendar += sum_i;
                    break;

                case livenature_categories.notifcations_markschart:
                    lnm.notifcations_markschart += sum_i;
                    break;

                case livenature_categories.notifications_chat:
                    lnm.notifications_chat += sum_i;
                    break;

                case livenature_categories.notifications_video:
                    lnm.notifications_video += sum_i;
                    break;

                case livenature_categories.notifications_store:
                    lnm.notifications_store += sum_i;
                    break;

                case livenature_categories.notifications_news:
                    lnm.notifications_news += sum_i;
                    break;

                case livenature_categories.notifications_maps:
                    lnm.notifications_maps += sum_i;
                    break;

            }

            UpdateUserNotifications(user, lnm);
        }
        public void LiveNature_RemoveNotify(schema_users user_, livenature_categories cat)
        {
            LiveNature_AddNotify(user_, cat, -1);
        }
        public int LiveNature_GetNotifyCount(schema_users user_, livenature_categories cat)
        {
            int sum = 0;

            LiveNatureModel lnm = new(user_);

            if (isUserInList(user_))
            {
                lnm = GetLNM_ModelByUser(user_);
            }
            else
            {
                return 0;
            }

            switch (cat)
            {
                case livenature_categories.notifications_admin: return lnm.notifications_admin;
                case livenature_categories.notifications_index: return lnm.notifications_index;
                case livenature_categories.notifications_tasks: return lnm.notifications_tasks;
                case livenature_categories.notifications_projs: return lnm.notifications_projs;
                case livenature_categories.notifications_users: return lnm.notifications_users;
                case livenature_categories.notifications_calendar: return lnm.notifications_calendar;
                case livenature_categories.notifcations_markschart:return lnm.notifcations_markschart;
                case livenature_categories.notifications_chat: return lnm.notifications_chat;
                case livenature_categories.notifications_video: return lnm.notifications_video;
                case livenature_categories.notifications_store: return lnm.notifications_store;
                case livenature_categories.notifications_news: return lnm.notifications_news;
                case livenature_categories.notifications_maps: return lnm.notifications_maps;
            }

            return 0;
        }
        public void UpdateUserNotifications(schema_users user, LiveNatureModel lnm)
        {
            if (lnm_list != null)
            {
                var list_without_user = lnm_list.Where(p => !p.lnm_user.Equals(user)).ToList() ;
                list_without_user.Add(lnm);

                lnm_list.Clear();
                foreach(var elem in list_without_user)
                {
                    lnm_list.Add(elem);
                }

            }
        }
        public List<LiveNatureModel> lnm_list { get; set; }
        public void InitializeIfNeeded(List<schema_users> context,IWebHostEnvironment _env)
        {
            if (lnm_list == null) //Ещё не запускалась инициализация
            {
               lnm_list = new List<LiveNatureModel>();

                List<schema_users> users_lst = new List<schema_users>();
                users_lst = context;

                if (users_lst != null)
                {
                    foreach(var elem in users_lst)
                    {
                        LiveNatureModel lnm_new = new LiveNatureModel(elem);
                        lnm_list.Add(lnm_new);
                    }
                }

                BackendController bc = new();
                bc.SendAlexandraEnvironment(_env);
            }
        }
        public bool isUserInList(schema_users _user)
        {
            if (lnm_list != null)
            {
                if (lnm_list.Count > 0)
                {
                    foreach (var item in lnm_list)
                    {
                        if (item.lnm_user.login == _user.login)
                        {
                            return true;
                        }
                    }
                }

            }
            return false;
        }
        LiveNatureModel? GetLNM_ModelByUser(schema_users _user)
        {
            if (lnm_list != null)
            {
                if (lnm_list.Count > 0)
                {
                    foreach (var item in lnm_list)
                    {
                        if (item.lnm_user.login == _user.login)
                        {
                            return item;
                        }
                    }
                }
            }
            return null;
        }
        public class LiveNatureModel
        {
            #region Variables
            public schema_users lnm_user { get; set; }

            public int notifications_admin { get; set; }

            public int notifications_index { get; set; }

            public int notifications_tasks { get; set; }
            public int notifications_projs { get; set; }

            public int notifications_users { get; set; }

            public int notifications_calendar { get; set; }

            public int notifcations_markschart { get; set; }

            public int notifications_chat { get; set; }

            public int notifications_video { get; set; }

            public int notifications_store { get; set; }

            public int notifications_news { get; set; }

            public int notifications_maps { get; set; }

            #endregion

           public LiveNatureModel(schema_users user_n)
            {
                lnm_user = user_n;

                notifications_admin = 0;
                notifications_index = 0;
                notifications_tasks = 0;
                notifications_projs = 0;
                notifications_users = 0;
                notifications_calendar = 0;
                notifcations_markschart = 0;
                notifications_video = 0;
                notifications_store = 0;
                notifications_news = 0;
                notifications_maps = 0;
            }

        }
    }
}