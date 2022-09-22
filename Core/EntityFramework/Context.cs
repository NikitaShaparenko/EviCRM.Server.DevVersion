using EviCRM.Server.Pages.Maps.Models;
using Microsoft.EntityFrameworkCore;

namespace EviCRM.Server.Core.EntityFramework
{
    public class Context : DbContext
    {

        #region News && News Comments && News Tags && News Cats

        public bool News_Cats_Remove(int news_cats_id)
        {
            var news_tags = news_cats.FirstOrDefault(p => p.idnews_categories == news_cats_id);
            Remove(news_tags);
            SaveChanges();

            return true;
        }

        public List<schema_news_cats> News_Cats_Get()
        {
            return news_cats.ToList();
        }


        public bool News_Cats_Create(schema_news_cats news_Cats)
        {
            news_cats.Add(news_Cats);
            SaveChanges();
            return true;
        }

        public bool News_Cats_Update(schema_news_cats news_Cats)
        {
            news_cats.Update(news_Cats);
            SaveChanges();
            return true;
        }


        public bool News_Tags_Create(schema_news_tags news_new_tags)
        {
            news_tags.Add(news_new_tags);
            SaveChanges();

            return true;
        }

        public bool News_Tags_Update(schema_news_tags news_new_tags)
        {
            news_tags.Update(news_new_tags);
            SaveChanges();

            return true;
        }

        public bool News_Tags_Remove(int news_tags_id)
        {
            var news_tag = news_tags.FirstOrDefault(p => p.idnews_tags == news_tags_id);
            Remove(news_tag);
            SaveChanges();

            return true;
        }

        public List<schema_news_tags> News_Tags_Get()
        {
            return news_tags.ToList();
        }

        public List<schema_news> News_Get()
        {
            return news.ToList();
        }

        public bool News_Create(schema_news news_new)
        {
            news.Add(news_new);
            SaveChanges();

            return true;
        }

        public bool News_Update(schema_news news_new)
        {
            news.Update(news_new);
            SaveChanges();

            return true;
        }

        public bool News_Remove(int news_id)
        {
            var news_elem = news.FirstOrDefault(p => p.idnews == news_id);
            Remove(news_elem);
            SaveChanges();

            return true;
        }

        public List<schema_news_comments> News_Comments_Get()
        {
            return news_comments.ToList();
        }

        public List<schema_news_comments> News_Comments_Get(schema_news in_news)
        {
            return news_comments.Where(p=>p.news_id == in_news.idnews).ToList();
        }

        public bool News_Comments_Create(schema_news_comments news_comments_new)
        {
            news_comments.Add(news_comments_new);
            SaveChanges();

            return true;
        }

        public bool News_Comments_Update(schema_news_comments news_comments_new)
        {
            news_comments.Update(news_comments_new);
            SaveChanges();

            return true;
        }

        public bool News_Comments_Remove(int news_comment_id)
        {
            var news_elem = news_comments.FirstOrDefault(p => p.idnews_comments == news_comment_id);
            Remove(news_elem);
            SaveChanges();

            return true;
        }

        public List<schema_tasktracking_comments> ttm_comm_Get()
        {
            return ttm_comm.ToList();
        }

        public List<schema_tasktracking_comments> ttm_comm_Get(schema_news in_news)
        {
            return ttm_comm.Where(p => p.ttm_id == in_news.idnews).ToList();
        }

        public bool ttm_comm_Create(schema_tasktracking_comments ttm_comm_new)
        {
            ttm_comm.Add(ttm_comm_new);
            SaveChanges();

            return true;
        }

        public bool ttm_comm_Update(schema_tasktracking_comments ttm_comm_new)
        {
            ttm_comm.Update(ttm_comm_new);
            SaveChanges();

            return true;
        }

        public bool ttm_comm_Remove(int news_comment_id)
        {
            var news_elem = ttm_comm.FirstOrDefault(p => p.idtasktracking_comments == news_comment_id);
            Remove(news_elem);
            SaveChanges();

            return true;
        }
        #endregion


        #region Alexandra Maps
        public List<schema_alexandra_locations> AlexandraMaps_Get()
        {
            return alexandra_locations.ToList();
        }

        public List<schema_alexandra_locations> AlexandraMaps_Get(string user_login)
        {
            var list = alexandra_locations.Where(p => p.user.Equals(user_login)).ToList();
            return list;
        }

        public bool AlexandraMaps_Create(MapsPoint_ViewModel mvpm)
        {
            schema_alexandra_locations schm_buffer = new schema_alexandra_locations();
            schm_buffer.lng = mvpm.Lng.ToString();
            schm_buffer.lat = mvpm.Lat.ToString();
            schm_buffer.user = mvpm.user_login;
            schm_buffer.name = mvpm.Name;
            schm_buffer.datetime = mvpm.addedDateTime;

            alexandra_locations.Add(schm_buffer);
            SaveChanges();
            return true;
        }

        public bool AlexandraMaps_Update(MapsPoint_ViewModel mvpm, int point_id)
        {
            schema_alexandra_locations schm_buffer = new schema_alexandra_locations();
            schm_buffer.lng = mvpm.Lng.ToString();
            schm_buffer.lat = mvpm.Lat.ToString();
            schm_buffer.user = mvpm.user_login;
            schm_buffer.name = mvpm.Name;
            schm_buffer.datetime = mvpm.addedDateTime;
            schm_buffer.idalexandra_location = point_id;

            alexandra_locations.Update(schm_buffer);
            SaveChanges();

            return true;
        }

        public bool AlexandraMaps_Update(schema_alexandra_locations point)
        {
            alexandra_locations.Update(point);
            SaveChanges();

            return true;
        }


        public bool AlexandaMaps_Remove(int point_id)
        {
            var point = alexandra_locations.FirstOrDefault(p => p.idalexandra_location.Equals(point_id));
            Remove(point);
            SaveChanges();
            return true;
        }

        #endregion

        #region Alexandra Contacts
        public List<schema_alexandra_contacts> AlexandraContacts_Get()
        {
            return alexandra_contacts.ToList();
        }

        public List<schema_alexandra_contacts> AlexandraContacts_Get(schema_users by_user)
        {
            return alexandra_contacts.Where(p => p.login == by_user.login).ToList();
        }

        public List<schema_alexandra_contacts> AlexandraContacts_Get(string user_login)
        {
            var list = alexandra_contacts.Where(p => p.login.Equals(user_login)).ToList();
            return list;
        }

        public bool AlexandraContacts_Create(schema_alexandra_contacts contact)
        {
            alexandra_contacts.Add(contact);
            SaveChanges();
            return true;
        }

        public bool AlexandraContacts_Update(schema_alexandra_contacts contact)
        {
            alexandra_contacts.Update(contact);
            SaveChanges();

            return true;
        }

        public bool AlexandraContacts_Remove(int contact_id)
        {
            var contact = alexandra_contacts.FirstOrDefault(p => p.idalexandra_contacts.Equals(contact_id));
            Remove(contact);
            SaveChanges();
            return true;
        }

        #endregion

        #region Maps Points

        public List<schema_maps_points> Maps_Get()
        {
            return maps_points.ToList();
        }

        public List<schema_maps_points> Maps_Get(string user_login)
        {
            var list = maps_points.Where(p => p.point_userlogin.Equals(user_login)).ToList();
            return list;
        }

        public bool Maps_Create(schema_maps_points point)
        {
            maps_points.Add(point);
            SaveChanges();
            return true;
        }

        public bool Maps_Create(MapsPoint_ViewModel point)
        {
            schema_maps_points conv_point = new schema_maps_points();
            conv_point.point_description = point.Name;
            conv_point.point_whenadd = point.addedDateTime;
            conv_point.point_n = point.Lat.ToString();
            conv_point.point_e = point.Lng.ToString();
            conv_point.point_userlogin = point.user_login;

            maps_points.Add(conv_point);
            SaveChanges();
            return true;
        }

        public bool Maps_Update(schema_maps_points point)
        {
            maps_points.Update(point);
            SaveChanges();

            return true;
        }

        public bool Maps_Update(MapsPoint_ViewModel point, int point_id)
        {
            schema_maps_points conv_point = new schema_maps_points();
            conv_point.point_description = point.Name;
            conv_point.point_whenadd = point.addedDateTime;
            conv_point.point_n = point.Lat.ToString();
            conv_point.point_e = point.Lng.ToString();
            conv_point.idmaps_points = point_id;

            maps_points.Update(conv_point);
            SaveChanges();
            return true;
        }

        public bool Maps_Remove(int point_id)
        {
            var map = maps_points.FirstOrDefault(p => p.idmaps_points.Equals(point_id));
            Remove(map);
            SaveChanges();
            return true;
        }

        #endregion

        #region Markdown Cards

        public List<schema_markdown_cards> MarkdownCard_Get()
        {
            return markdown_cards.ToList();
        }

        public schema_markdown_cards MarkdownCardSingle_Get(schema_markdown_cards card)
        {
            return markdown_cards.FirstOrDefault(p => p.markdown_card_deskid == card.markdown_card_deskid && p.markdown_card_backcolor == card.markdown_card_backcolor && p.markdown_card_taskid == card.markdown_card_taskid && p.markdown_card_name == card.markdown_card_name);
        }

        public List<schema_markdown_cards> MarkdownCard_Get(schema_tasks Task)
        {
            return markdown_cards.Where(p => p.markdown_card_taskid.Equals(Task.id)).ToList();
        }

        public List<schema_markdown_cards> MarkdownCard_Get(string ID)
        {
            var list = markdown_cards.Where(p => p.idmarkdown_cards.Equals(ID)).ToList();
            return list;
        }

        public bool MarkdownCard_Create(schema_markdown_cards card)
        {
            markdown_cards.Add(card);
            SaveChanges();
            return true;
        }

        public bool MarkdownCard_Update(schema_markdown_cards card)
        {
            markdown_cards.Update(card);
            SaveChanges();

            return true;
        }

        public bool MarkdownCard_Remove(int card_id)
        {
            var card = markdown_cards.FirstOrDefault(p => p.idmarkdown_cards.Equals(card_id));
            Remove(card);
            SaveChanges();
            return true;
        }

        #endregion

        #region Markdown Desks

        public List<schema_markdown_desks> MarkdownDesk_Get()
        {
            return markdown_desks.ToList();
        }

        public List<schema_markdown_desks> MarkdownDesk_Get(schema_tasks Task)
        {
            return markdown_desks.Where(p => p.markdown_desk_task_id.Equals(Task.id)).ToList();
        }

        public List<schema_markdown_desks> MarkdownDesk_Get(string ID)
        {
            var list = markdown_desks.Where(p => p.idmarkdown_desks.Equals(ID)).ToList();
            return list;
        }

        public bool MarkdownDesk_Create(schema_markdown_desks desk)
        {
            markdown_desks.Add(desk);
            SaveChanges();
            return true;
        }

        public bool MarkdownDesk_Update(schema_markdown_desks desk)
        {
            markdown_desks.Update(desk);
            SaveChanges();

            return true;
        }

        public bool MarkdownDesk_Remove(int desk_id)
        {
            var desk = markdown_desks.FirstOrDefault(p => p.idmarkdown_desks.Equals(desk_id));
            Remove(desk);
            SaveChanges();
            return true;
        }

        #endregion

        #region Markdown Todos

        public List<schema_markdown_todo_list> MarkdownTodo_Get(schema_tasks Task)
        {
            return markdown_todo_list.Where(p => p.markdown_todo_taskid.Equals(Task.id)).ToList();
        }

        public List<schema_markdown_todo_list> MarkdownTodo_Get()
        {
            return markdown_todo_list.ToList();
        }

        public List<schema_markdown_todo_list> MarkdownTodo_Get(string ID)
        {
            var list = markdown_todo_list.Where(p => p.idmarkdown_todo_list.Equals(ID)).ToList();
            return list;
        }

        public bool Markdowntodo_Create(schema_markdown_todo_list todo)
        {
            markdown_todo_list.Add(todo);
            SaveChanges();
            return true;
        }

        public bool Markdowntodos_Create(List<schema_markdown_todo_list> todo)
        {
            if (todo != null)
            {
                if (todo.Count > 0)
                {
                    foreach(var elem in todo)
                    {
                        markdown_todo_list.Add(elem);
                    }
                }
            }
            SaveChanges();
            return true;
        }

        public bool Markdowntodo_Update(schema_markdown_todo_list todo)
        {
            markdown_todo_list.Update(todo);
            SaveChanges();

            return true;
        }

        public bool Markdowntodo_Update(List<schema_markdown_todo_list> todos)
        {
            if (todos != null)
            {
                if (todos.Count > 0)
                {
                    foreach(var elem in todos)
                    {
                        markdown_todo_list.Update(elem);
                    }
                }
            }

          
            SaveChanges();

            return true;
        }

        public bool Markdowntodo_Remove(int todo_id)
        {
            var todo = markdown_todo_list.FirstOrDefault(p => p.idmarkdown_todo_list.Equals(todo_id));
            Remove(todo);
            SaveChanges();
            return true;
        }

        #endregion

        #region Calendar

        public List<schema_calendar_schedules> CalendarSchedules_Get()
        {
            return calendar_schedules.ToList();
        }



        public List<schema_calendar_schedules> CalendarSchedules_Get(string user_login)
        {
            var list = calendar_schedules.Where(p => p.users.Equals(user_login) || p.calendarId.Equals("corporate")).ToList();
            return list;
        }

        public bool CalendarSchedule_Create(schema_calendar_schedules schedule)
        {
            calendar_schedules.Add(schedule);
            SaveChanges();
            return true;
        }

        public bool CalendarSchedule_Update(schema_calendar_schedules schedule)
        {
            calendar_schedules.Update(schedule);
            SaveChanges();
            return true;
        }

        public bool CalendarSchedule_Remove(int calendar_id)
        {
            var calendar = calendar_schedules.FirstOrDefault(p => p.idcalendar_schedules.Equals(calendar_id));
            Remove(calendar);
            SaveChanges();
            return true;
        }

        #endregion

        #region Marks

        public List<schema_marks> Marks_Get()
        {
            return marks.ToList();
        }

        public List<schema_marks> Marks_Get(string user_login)
        {
            var list = marks.Where(p => p.user_id.Equals(user_login)).ToList();
            return list;
        }

        public bool Marks_Create(schema_marks mark)
        {
            marks.Add(mark);
            SaveChanges();
            return true;
        }

        public bool Marks_Update(schema_marks mark)
        {
            marks.Update(mark);
            SaveChanges();
            return true;
        }

        public bool Marks_Remove(int mark_id)
        {
            var mark = marks.FirstOrDefault(p => p.idmarks.Equals(mark_id));
            marks.Remove(mark);
            SaveChanges();
            return true;
        }

        #endregion

        #region Tasks

        public schema_tasks Task_Get(int task_id)
        {
            return tasks.FirstOrDefault(p => p.id.Equals(task_id));
        }
        public List<schema_tasks> Tasks_Get()
        {
            return tasks.ToList();
        }

        public List<schema_tasks> Tasks_Get(string user_login)
        {
            var list = tasks.Where(p => p.task_members.Contains(user_login) || p.tasks_personal_status.Contains(user_login) || p.task_author.Equals(user_login) || p.task_responsible_member.Equals(user_login)).ToList();
            return list;
        }

        public bool Tasks_Create(schema_tasks task)
        {
            tasks.Add(task);
            SaveChanges();
            return true;
        }

        public bool Tasks_Update(schema_tasks task)
        {
            tasks.Update(task);
            SaveChanges();
            return true;
        }

        public bool Tasks_Remove(int task_id)
        {
            var task = tasks.FirstOrDefault(p => p.id.Equals(task_id));
            tasks.Remove(task);
            SaveChanges();
            return true;
        }

        #endregion

        #region Task Tracking Main  //TOScript

        public List<schema_task_tracking_main> TaskTrackingMain_Get()
        {
            return task_tracking_main.ToList();
        }

        public List<schema_task_tracking_main> TaskTrackingMain_Get(schema_tasks cur_task)
        {
            return task_tracking_main.Where(p => p.task_id == cur_task.id.ToString()).ToList();
        }

        public List<schema_task_tracking_main> TaskTrackingMain_Get(int in_task_id)
        {
            var list = task_tracking_main.Where(p => p.task_id == in_task_id.ToString()).ToList();
            return list;
        }

        public bool TaskTrackingMain_ExecuteQueue(List<schema_task_tracking_main> queue)
        {
            foreach (var task in queue)
            {
                task_tracking_main.Add(task);
            }
            SaveChanges();
            return true;
        }

        public bool TaskTrackingMain_Create(schema_task_tracking_main task)
        {
            task_tracking_main.Add(task);
            SaveChanges();
            return true;
        }

        public bool TaskTrackingMain_Update(schema_task_tracking_main task)
        {
            task_tracking_main.Update(task);
            SaveChanges();
            return true;
        }

        public bool TaskTrackingMain_Remove(int in_task_id)
        {
            var task = task_tracking_main.FirstOrDefault(p => p.task_id.Equals(in_task_id));
            task_tracking_main.Remove(task);
            SaveChanges();
            return true;
        }

        #endregion

        #region Task Tracking Card

        public List<schema_task_tracking_card> TaskTrackingCard_Get()
        {
            return task_tracking_card.ToList();
        }

        public List<schema_task_tracking_card> TaskTrackingCard_Get(schema_tasks cur_task)
        {
            return task_tracking_card.Where(p => p.task_id.Equals(cur_task.id)).ToList();
        }

        public List<schema_task_tracking_card> TaskTrackingCard_Get(int in_task_id)
        {
            var list = task_tracking_card.Where(p => p.task_id.Equals(in_task_id)).ToList();
            return list;
        }

        public bool TaskTrackingCard_Create(schema_task_tracking_card card)
        {
            task_tracking_card.Add(card);
            SaveChanges();
            return true;
        }

        public bool TaskTrackingCard_Update(schema_task_tracking_card task)
        {
            task_tracking_card.Update(task);
            SaveChanges();
            return true;
        }

        public bool TaskTrackingCard_Remove(int in_task_id)
        {
            var card = task_tracking_card.FirstOrDefault(p => p.task_id.Equals(in_task_id));
            task_tracking_card.Remove(card);
            SaveChanges();
            return true;
        }


        #endregion

        #region Projects

        public List<schema_projects> Projects_Get(schema_tasks Task)
        {
            return projects.Where(p => p.proj_tasks.Equals(Task.id)).ToList();
        }

        public List<schema_projects> Projects_Get()
        {
            return projects.ToList();
        }

        public List<schema_projects> Projects_Get(string user_login)
        {
            var list = projects.Where(p => p.proj_users.Contains(user_login)).ToList();
            return list;
        }

        public schema_projects Project_Get(int proj_id)
        {
            var list = projects.FirstOrDefault(p => p.id == proj_id);
            return list;
        }

        public bool Projects_Create(schema_projects project)
        {
            projects.Add(project);
            SaveChanges();
            return true;
        }

        public bool Projects_Update(List<schema_projects> projs)
        {
            foreach (var elem in projects)
            {
                projects.Update(elem);
            }
            SaveChanges();
            return true;
        }

        public bool Projects_Update(schema_projects project)
        {
            projects.Update(project);
            SaveChanges();
            return true;
        }

        public bool Projects_Remove(int proj_id)
        {
            var proj = projects.FirstOrDefault(p => p.id.Equals(proj_id));
            projects.Remove(proj);
            SaveChanges();
            return true;
        }


        #endregion

        #region Users

        public List<schema_users> Users_Get(schema_tasks Task)
        {
            List<string> tasks_users = new List<string>();
            tasks_users = getMultipleStringDecodingString(Task.task_members);

            List<schema_users> users_models = new();

            if (tasks_users != null)
            {
                if (tasks_users.Count > 0)
                foreach (string str in tasks_users)
                {
                        if (str != "$null")
                        {
                           schema_users user_ = User_ByIDGet(str);
                            if (user_ != null) users_models.Add(user_);
                        }
                   
                }
            }

            return users_models;
        }


        public List<schema_users> Users_Get()
        {
            return users.ToList();
        }

        public schema_users User_ByIDGet(string id)
        {
            schema_users list = new();
            if (int.TryParse(id,out int a))
            {
                list = users.FirstOrDefault(p => p.id == a);
                if (list != null)
                {
                    return list;
                }
            }
            return list;
        }

        public schema_users User_Get(string user_login)
        {
            var list = users.FirstOrDefault(p => p.login.Equals(user_login));
            return list;
        }

        public bool User_Create(schema_users user)
        {
            users.Add(user);
            SaveChanges();
            return true;
        }

        public bool User_Update(schema_users user)
        {
            users.Update(user);
            SaveChanges();
            return true;
        }

        public bool User_Remove(int uid)
        {
            var user_id = users.FirstOrDefault(p => p.id.Equals(uid));
            users.Remove(user_id);
            SaveChanges();
            return true;
        }

        #endregion

        #region Admin Health

        public List<schema_a_health> AHealth_Get()
        {
            return a_health.ToList();
        }

        public bool AHealth_Create(schema_a_health ahealth)
        {
            a_health.Add(ahealth);
            SaveChanges();
            return true;
        }

        public bool AHealth_Update(schema_a_health ahealth)
        {
            a_health.Update(ahealth);
            SaveChanges();
            return true;
        }

        public bool AHealth_Remove(int uid)
        {
            var user_id = a_health.FirstOrDefault(p => p.idhealth.Equals(uid));
            a_health.Remove(user_id);
            SaveChanges();
            return true;
        }

        #endregion

        #region Divisions

        public List<schema_divisions> Divisions_Get()
        {
            return divisions.ToList();
        }

        public schema_divisions Division_Get(string did)
        {
            schema_divisions divs = new();
            if (int.TryParse(did,out int a))
            {
                divs = divisions.FirstOrDefault(p => p.id == a);
            }
            return divs;
        }

        public List<schema_divisions> Divisions_Get(schema_tasks current_task)
        {
            List<string> tasks_divs = new List<string>();
            tasks_divs = getMultipleStringDecodingString(current_task.tasks_members_divs);

            List<schema_divisions> divs_models = new();

            if (tasks_divs != null)
            {
                if (tasks_divs.Count > 0)
                    foreach (string str in tasks_divs)
                    {
                        if (str != "$null")
                        {
                            schema_divisions div_ = Division_Get(str);
                            if (div_ != null) divs_models.Add(div_);
                        }
                }
            }

            return divs_models;
        }

        public List<schema_divisions> Division_GetByPerson(string user)
        {
            var list = divisions.Where(p => p.division_cast.Contains(user) || p.division_responsible.Contains(user)).ToList();
            return list;
        }


        public bool Division_Create(schema_divisions div)
        {
            divisions.Add(div);
            SaveChanges();
            return true;
        }

        public bool Division_Update(schema_divisions div)
        {
            divisions.Update(div);
            SaveChanges();
            return true;
        }

        public bool Division_Remove(int uid)
        {
            var user_id = divisions.FirstOrDefault(p => p.id.Equals(uid));
            divisions.Remove(user_id);
            SaveChanges();
            return true;
        }

        #endregion

        //Telegram Push

        #region Telegram Push

        public List<schema_telegram_push> TelegramPush_Get()
        {
            return telegram_push.ToList();
        }

        public bool TelegramPush_Create(schema_telegram_push tg)
        {
            telegram_push.Add(tg);
            SaveChanges();
            return true;
        }

        public bool TelegramPush_Update(schema_telegram_push tg)
        {
            telegram_push.Update(tg);
            SaveChanges();
            return true;
        }

        public bool TelegramPush_Remove(int uid)
        {
            var user_id = telegram_push.FirstOrDefault(p => p.id.Equals(uid));
            telegram_push.Remove(user_id);
            SaveChanges();
            return true;
        }

        #endregion



        public Context(DbContextOptions<Context> options)
           : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<schema_a_health>(entity =>
            {
                entity.ToTable("a_health")
                .HasKey(p => p.idhealth);

                entity.Property(p => p.link)
                .HasColumnType("TEXT")
                .HasColumnName("link")
                .IsRequired()
                .HasMaxLength(65535);

                entity.Property(p => p.httpcode)
               .HasColumnType("VARCHAR")
               .HasColumnName("httpcode")
               .HasMaxLength(65535);

                entity.Property(p => p.status)
               .HasColumnType("TEXT")
               .HasColumnName("status")
               .HasMaxLength(65535);

            });

            modelBuilder.Entity<schema_aux_workingdays>(entity =>
            {
                entity.ToTable("aux_workingdays")
                .HasKey(p => p.iadux_workingdays);

                entity.Property(p => p.user_id)
                .HasColumnType("VARCHAR")
                .HasColumnName("user_id")
                .HasMaxLength(200);

                entity.Property(p => p.date)
                .HasColumnType("VARCHAR")
                .HasColumnName("user_id")
                .HasMaxLength(200);

                entity.Property(p => p.day_start)
                .HasColumnType("VARCHAR")
                .HasColumnName("user_id")
                .HasMaxLength(200);

                entity.Property(p => p.day_end)
                .HasColumnType("VARCHAR")
                .HasColumnName("user_id")
                .HasMaxLength(200);
            });

            modelBuilder.Entity<schema_alexandra_contacts>(entity =>
            {
                entity.ToTable("alexandra_contacts")
                .HasKey(p => p.idalexandra_contacts);

                entity.Property(p => p.firstname)
                .HasColumnType("TEXT")
                .HasColumnName("firstname")
                .HasMaxLength(65535);

                entity.Property(p => p.lastname)
                .HasColumnType("TEXT")
            .HasColumnName("lastname")
            .HasMaxLength(65535);

                entity.Property(p => p.phonenumber)
                .HasColumnType("VARCHAR")
            .HasColumnName("phonenumber")
            .HasMaxLength(65535);

                entity.Property(p => p.userId)
                .HasColumnType("TEXT")
            .HasColumnName("userId")
            .HasMaxLength(65535);

                entity.Property(p => p.vcard)
                .HasColumnType("TEXT")
            .HasColumnName("vcard")
            .HasMaxLength(65535);

                entity.Property(p => p.login)
                .HasColumnType("TEXT")
         .HasColumnName("login")
         .HasMaxLength(65535);
            });

            modelBuilder.Entity<schema_alexandra_locations>(entity =>
            {
                entity.ToTable("alexandra_locations")
                .HasKey(p => p.idalexandra_location);

                entity.Property(p => p.lat)
                 .HasColumnType("VARCHAR")
                .HasColumnName("lat")
                .HasMaxLength(50);

                entity.Property(p => p.lng)
                 .HasColumnType("VARCHAR")
               .HasColumnName("lng")
               .HasMaxLength(50);

                entity.Property(p => p.name)
                 .HasColumnType("TEXT")
            .HasColumnName("name")
            .HasMaxLength(65535);

                entity.Property(p => p.user)
                 .HasColumnType("TEXT")
            .HasColumnName("user")
            .HasMaxLength(65535);

                entity.Property(p => p.datetime)
                 .HasColumnType("DATETIME")
            .HasColumnName("datetime");
            });

            modelBuilder.Entity<schema_alexandra_polls>(entity =>
            {
                entity.ToTable("alexandra_polls")
                .HasKey(p => p.idpolls);

                entity.Property(p => p.multiple_answers)
                .HasColumnType("VARCHAR")
                .HasColumnName("multiple_answers")
                .HasMaxLength(45);

                entity.Property(p => p.explanations)
                 .HasColumnType("TEXT")
               .HasColumnName("explanation")
               .HasMaxLength(65535);

                entity.Property(p => p.id)
                 .HasColumnType("TEXT")
            .HasColumnName("id")
            .HasMaxLength(65535);

                entity.Property(p => p.is_anon)
                 .HasColumnType("VARCHAR")
            .HasColumnName("is_anon")
            .HasMaxLength(45);

                entity.Property(p => p.is_closed)
                 .HasColumnType("VARCHAR")
          .HasColumnName("is_closed")
          .HasMaxLength(45);

                entity.Property(p => p.question)
                 .HasColumnType("TEXT")
        .HasColumnName("question")
        .HasMaxLength(65535);

                entity.Property(p => p.options)
                 .HasColumnType("TEXT")
      .HasColumnName("options")
      .HasMaxLength(65535);

                entity.Property(p => p.type)
                 .HasColumnType("VARCHAR")
      .HasColumnName("type")
      .HasMaxLength(45);

                entity.Property(p => p.correctOptionId)
                 .HasColumnType("TEXT")
       .HasColumnName("correctOptionId")
       .HasMaxLength(65535);

                entity.Property(p => p.adding_time)
      .HasColumnName("adding_time")
      .HasColumnType("DATETIME");
            });

            //skip -> aux_companies

            //skip -> aux_divisions

            //skip -> aux_email_inbox

            //skip -> aux_positions

            modelBuilder.Entity<schema_aux_telegrambot>(entity =>
            {
                entity.ToTable("aux_telegrambot")
                .HasKey(p => p.idaux);

                entity.Property(p => p.chatid)
                 .HasColumnType("TEXT")
                .HasColumnName("chatid");

                entity.Property(p => p.login)
                 .HasColumnType("TEXT")
               .HasColumnName("login");

                entity.Property(p => p.status)
                 .HasColumnType("VARCHAR")
            .HasColumnName("status")
            .HasMaxLength(45);

            });

            //skip -> main

            //skip -> roles

            modelBuilder.Entity<schema_skills>(entity =>
            {
                entity.ToTable("skills")
                .HasKey(p => p.idskills);

                entity.Property(p => p.skillscol)
                 .HasColumnType("TEXT")
                .HasColumnName("skillscol");
            });

            modelBuilder.Entity<schema_divisions>(entity =>
            {
                entity.ToTable("divisions")
                .HasKey(p => p.id);

                entity.Property(p => p.division_name)
                 .HasColumnType("VARCHAR")
                .HasColumnName("division_name")
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(p => p.division_cast)
               .HasColumnType("TEXT")
              .HasColumnName("division_cast")
              .IsRequired();

                entity.Property(p => p.division_responsible)
          .HasColumnType("VARCHAR")
         .HasColumnName("division_responsible")
         .HasMaxLength(200)
         .IsRequired();

                entity.Property(p => p.division_avatar)
        .HasColumnType("VARCHAR")
       .HasColumnName("division_avatar")
       .HasMaxLength(254)
       .HasDefaultValue("division_template.png")
       .IsRequired();

            });

            modelBuilder.Entity<schema_key_store>(entity =>
            {
                entity.ToTable("key_store")
                .HasKey(p => p.idkey_store);

                entity.Property(p => p.keys_progs)
                 .HasColumnType("VARCHAR")
                .HasColumnName("keys_progs")
                .IsRequired()
                .HasMaxLength(200);

                entity.Property(p => p.keys_val1)
                 .HasColumnType("VARCHAR")
                .HasColumnName("keys_val1")
                .IsRequired()
                .HasMaxLength(512);

                entity.Property(p => p.keys_val2)
                .HasColumnType("VARCHAR")
               .HasColumnName("keys_val2")
               .IsRequired()
               .HasMaxLength(512);

                entity.Property(p => p.keys_val3)
                .HasColumnType("VARCHAR")
               .HasColumnName("keys_val3")
               .IsRequired()
               .HasMaxLength(512);

                entity.Property(p => p.keys_val4)
                .HasColumnType("VARCHAR")
               .HasColumnName("keys_val4")
               .IsRequired()
               .HasMaxLength(512);

                entity.Property(p => p.keys_val5)
                .HasColumnType("VARCHAR")
               .HasColumnName("keys_val5")
               .IsRequired()
               .HasMaxLength(512);

                entity.Property(p => p.keys_val6)
                .HasColumnType("VARCHAR")
               .HasColumnName("keys_val6")
               .IsRequired()
               .HasMaxLength(512);
            });

            modelBuilder.Entity<schema_maps_points>(entity =>
            {
                entity.ToTable("maps_points")
                .HasKey(p => p.idmaps_points);

                entity.Property(p => p.point_e)
                 .HasColumnType("VARCHAR")
                .HasColumnName("point_e")
                .HasMaxLength(100);

                entity.Property(p => p.point_n)
                 .HasColumnType("VARCHAR")
               .HasColumnName("point_n")
               .HasMaxLength(100);

                entity.Property(p => p.point_userlogin)
                 .HasColumnType("TEXT")
            .HasColumnName("point_userlogin")
            .HasMaxLength(65535);

                entity.Property(p => p.point_description)
                 .HasColumnType("TEXT")
            .HasColumnName("point_description")
            .HasMaxLength(65535);

                entity.Property(p => p.point_whenadd)
                 .HasColumnType("DATETIME")
            .HasColumnName("point_whenadd");
            });

            modelBuilder.Entity<schema_telegram_push>(entity =>
            {
                entity.ToTable("telegram_push")
                .HasKey(p => p.id);

                entity.Property(p => p.chatID)
                 .HasColumnType("VARCHAR")
                .HasColumnName("chatID")
                .HasMaxLength(200);

                entity.Property(p => p.message_body)
                 .HasColumnType("TEXT")
               .HasColumnName("message_body");
            });

            #region Fundamental Tables

            modelBuilder.Entity<schema_calendar_schedules>(entity =>
            {
                entity.ToTable("calendar_schedules")
                .HasKey(p => p.idcalendar_schedules);

                entity.Property(p => p.id)
                 .HasColumnType("VARCHAR")
                .HasColumnName("id")
                .HasMaxLength(200);

                entity.Property(p => p.calendarId)
               .HasColumnType("VARCHAR")
              .HasColumnName("calendarId")
              .HasMaxLength(200);

                entity.Property(p => p.startDate)
               .HasColumnType("DATETIME")
              .HasColumnName("startDate");

                entity.Property(p => p.endDate)
               .HasColumnType("DATETIME")
              .HasColumnName("endDate");

                entity.Property(p => p.title)
               .HasColumnType("VARCHAR")
              .HasColumnName("title")
              .HasMaxLength(300);

                entity.Property(p => p.body)
               .HasColumnType("TEXT")
              .HasColumnName("body")
              .HasMaxLength(65535);

                entity.Property(p => p.users)
               .HasColumnType("VARCHAR")
              .HasColumnName("users")
              .HasMaxLength(200);

                entity.Property(p => p.isinoffice)
               .HasColumnType("VARCHAR")
              .HasColumnName("isinoffice")
              .HasMaxLength(45);

                entity.Property(p => p.isnotify)
             .HasColumnType("TINYINT(1)")
            .HasColumnName("isnotify")
            .HasMaxLength(45);

                entity.Property(p => p.adding_time)
            .HasColumnType("DATETIME")
           .HasColumnName("adding_time")
           .HasDefaultValueSql("CURRENT_TIMESTAMP()");

                entity.Property(p => p.Code)
             .HasColumnType("TEXT")
            .HasColumnName("code")
            .HasMaxLength(65535);

                entity.Property(p => p.Color)
             .HasColumnType("TEXT")
            .HasColumnName("color")
            .HasMaxLength(65535);

                entity.Property(p => p.ForeColor)
             .HasColumnType("TEXT")
            .HasColumnName("forecolor")
            .HasMaxLength(65535);

                entity.Property(p => p.notify_period)
                .HasColumnType("TEXT")
                .HasColumnName("notify_period")
                .HasMaxLength(65535);

                entity.Property(p => p.attachments)
               .HasColumnType("TEXT")
               .HasColumnName("attachments")
               .HasMaxLength(65535);

            });

            modelBuilder.Entity<schema_users>(entity =>
            {
                entity.ToTable("users")
                .HasKey(p => p.id);

                entity.Property(p => p.login)
               .HasColumnType("TEXT")
              .HasColumnName("login")
              .IsRequired();

                entity.Property(p => p.password)
              .HasColumnType("VARCHAR")
              .HasMaxLength(512)
             .HasColumnName("password")
             .IsRequired();

                entity.Property(p => p.token)
           .HasColumnType("VARCHAR")
           .HasMaxLength(128)
          .HasColumnName("token");

                entity.Property(p => p.timetoken)
          .HasColumnType("VARCHAR")
          .HasMaxLength(128)
         .HasColumnName("timetoken");

                entity.Property(p => p.fullname)
             .HasColumnType("TEXT")
            .HasColumnName("fullname");

                entity.Property(p => p.company)
       .HasColumnType("VARCHAR")
       .HasMaxLength(100)
      .HasColumnName("company");

                entity.Property(p => p.position)
         .HasColumnType("TEXT")
        .HasColumnName("position");

                entity.Property(p => p.department)
     .HasColumnType("VARCHAR")
     .HasMaxLength(100)
    .HasColumnName("department");

                entity.Property(p => p.division)
    .HasColumnType("VARCHAR")
    .HasMaxLength(100)
    .HasColumnName("division");

                entity.Property(p => p.skills)
       .HasColumnType("LONGTEXT")
      .HasColumnName("skills");

                entity.Property(p => p.mobilephone)
    .HasColumnType("VARCHAR")
    .HasMaxLength(15)
    .HasColumnName("mobilephone");

                entity.Property(p => p.email)
     .HasColumnType("TEXT")
    .HasColumnName("email")
    .IsRequired();

                entity.Property(p => p.location)
    .HasColumnType("TEXT")
    .HasColumnName("location");

                entity.Property(p => p.experience_s1)
    .HasColumnType("MEDIUMTEXT")
    .HasColumnName("experience_s1");

                entity.Property(p => p.experience_s2)
    .HasColumnType("MEDIUMTEXT")
    .HasColumnName("experience_s2");


                entity.Property(p => p.completed_proj)
    .HasColumnType("INT")
    .HasColumnName("completed_proj");

                entity.Property(p => p.pending_proj)
    .HasColumnType("INT")
    .HasColumnName("pending_proj");

                entity.Property(p => p.total_revenue)
    .HasColumnType("DOUBLE")
    .HasColumnName("total_revenue");

                entity.Property(p => p.revenue)
    .HasColumnType("DOUBLE")
    .HasColumnName("revenue");

                entity.Property(p => p.list_proj)
    .HasColumnType("TEXT")
    .HasColumnName("list_proj");

                entity.Property(p => p.birthday)
    .HasColumnType("DATETIME")
    .HasColumnName("birthday");

                entity.Property(p => p.last_login)
    .HasColumnType("DATETIME")
    .HasColumnName("last_login")
    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(p => p.description)
    .HasColumnType("LONGTEXT")
    .HasColumnName("description");

                entity.Property(p => p.telegram_chatid)
    .HasColumnType("TEXT")
    .HasColumnName("telegram_chatid");

                entity.Property(p => p.role)
    .HasColumnType("VARCHAR")
    .HasMaxLength(45)
    .HasColumnName("role")
    .HasDefaultValue("user")
    .IsRequired();

                entity.Property(p => p.flag_activated)
    .HasColumnType("TINYINT")
    .HasMaxLength(1)
    .HasColumnName("flag_activated")
    .HasDefaultValue("false")
    .IsRequired();

                entity.Property(p => p.registered)
    .HasColumnType("TINYINT")
    .HasMaxLength(1)
    .HasColumnName("registered")
    .HasDefaultValue("false")
    .IsRequired();

                entity.Property(p => p.avatar)
    .HasColumnType("VARCHAR")
    .HasMaxLength(500)
    .HasColumnName("avatar")
        .HasDefaultValue("/assets/images/profile-img.png")
    .IsRequired();

                entity.Property(p => p.thinclient_user)
    .HasColumnType("TEXT")
    .HasColumnName("thinclient_user");

            });

            #endregion

            #region Markdown Tables

            modelBuilder.Entity<schema_markdown_todo_list>(entity =>
            {
                entity.ToTable("markdown_todo_list")
                .HasKey(p => p.idmarkdown_todo_list);

                entity.Property(p => p.markdown_todo_taskid)
                 .HasColumnType("INT")
                .HasColumnName("markdown_todo_taskid")
                .IsRequired();

                entity.Property(p => p.markdown_todo_checked)
               .HasColumnType("TINYINT(1)")
              .HasColumnName("markdown_todo_checked")
               .IsRequired();

                entity.Property(p => p.markdown_todo_name)
               .HasColumnType("TEXT")
              .HasColumnName("markdown_todo_name")
               .IsRequired();

                entity.Property(p => p.markdown_todo_cardid)
               .HasColumnType("INT")
              .HasColumnName("markdown_todo_cardid")
               .IsRequired();

                entity.Property(p => p.markdown_todo_markdatetime)
             .HasColumnType("DATETIME")
            .HasColumnName("markdown_todo_markdatetime")
             .IsRequired();

            });

            modelBuilder.Entity<schema_markdown_desks>(entity =>
            {
                entity.ToTable("markdown_desks")
                .HasKey(p => p.idmarkdown_desks);

                entity.Property(p => p.markdown_desk_name)
                 .HasColumnType("TEXT")
                .HasColumnName("markdown_desk_name")
                .IsRequired();

                entity.Property(p => p.markdown_desk_personal_id)
                .HasColumnType("INT")
               .HasColumnName("markdown_desk_personal_id")
               .IsRequired();

                entity.Property(p => p.markdown_desk_task_id)
               .HasColumnType("INT")
              .HasColumnName("markdown_desk_task_id")
              .IsRequired();

                entity.Property(p => p.markdown_adding_time)
             .HasColumnType("DATETIME")
            .HasColumnName("markdown_adding_time")
            .HasDefaultValueSql("CURRENT_TIMESTAMP()")
            .IsRequired();
            });

            modelBuilder.Entity<schema_markdown_cards>(entity =>
            {
                entity.ToTable("markdown_cards")
                .HasKey(p => p.idmarkdown_cards);

                entity.Property(p => p.markdown_card_name)
                 .HasColumnType("TEXT")
                .HasColumnName("markdown_card_name")
                .IsRequired();

                entity.Property(p => p.markdown_card_description)
                .HasColumnType("TEXT")
               .HasColumnName("markdown_card_description")
               .IsRequired();

                entity.Property(p => p.markdown_card_date_start)
               .HasColumnType("DATETIME")
              .HasColumnName("markdown_card_date_start")
              .IsRequired();

                entity.Property(p => p.markdown_card_date_end)
               .HasColumnType("DATETIME")
              .HasColumnName("markdown_card_date_end")
              .IsRequired();

                entity.Property(p => p.markdown_card_img_path)
              .HasColumnType("TEXT")
             .HasColumnName("markdown_card_img_path")
             .IsRequired();

                entity.Property(p => p.markdown_card_backcolor)
           .HasColumnType("VARCHAR")
          .HasColumnName("markdown_card_backcolor")
          .HasMaxLength(45);

                entity.Property(p => p.markdown_card_taskid)
            .HasColumnType("INT")
           .HasColumnName("markdown_card_taskid")
           .IsRequired();

                entity.Property(p => p.markdown_card_deskid)
           .HasColumnType("INT")
          .HasColumnName("markdown_card_deskid")
          .IsRequired();

                entity.Property(p => p.markdown_card_adding_time)
           .HasColumnType("DATETIME")
          .HasColumnName("markdown_card_adding_time")
          .IsRequired();


            });

            #endregion

            #region Tasks \\ Projects \\ TaskTracking

            modelBuilder.Entity<schema_tasks>(entity =>
            {
                entity.ToTable("tasks")
                .HasKey(p => p.id);

                entity.Property(p => p.task_name)
               .HasColumnType("TEXT")
              .HasColumnName("task_name")
              .IsRequired();

                entity.Property(p => p.task_description)
               .HasColumnType("LONGTEXT")
              .HasColumnName("task_description")
              .IsRequired();


                entity.Property(p => p.task_startdate)
             .HasColumnType("DATETIME")
            .HasColumnName("task_startdate")
            .IsRequired();

                entity.Property(p => p.task_enddate)
                             .HasColumnType("DATETIME")
                            .HasColumnName("task_enddate")
                            .IsRequired();

                entity.Property(p => p.task_enddate_real)
           .HasColumnType("DATETIME")
          .HasColumnName("task_enddate_real");

                entity.Property(p => p.task_attachments)
                            .HasColumnType("LONGTEXT")
                           .HasColumnName("task_attachments");

                entity.Property(p => p.task_budget)
                            .HasColumnType("TEXT")
                           .HasColumnName("task_budget");

                entity.Property(p => p.task_members)
                           .HasColumnType("LONGTEXT")
                          .HasColumnName("task_members")
                          .IsRequired();


                entity.Property(p => p.task_status)
                          .HasColumnType("VARCHAR")
                         .HasColumnName("task_status")
                         .HasMaxLength(100)
                         .IsRequired();

                entity.Property(p => p.task_responsible_member)
           .HasColumnType("VARCHAR")
           .HasMaxLength(300)
          .HasColumnName("task_responsible_member");

                entity.Property(p => p.task_author)
          .HasColumnType("TEXT")
         .HasColumnName("task_author")
         .IsRequired();


                entity.Property(p => p.task_notify)
          .HasColumnType("TINYINT(1)")
         .HasColumnName("task_notify");

                entity.Property(p => p.task_proj)
         .HasColumnType("LONGTEXT")
        .HasColumnName("task_proj");

                entity.Property(p => p.tasks_personal_status)
         .HasColumnType("TEXT")
        .HasColumnName("tasks_personal_status");

                entity.Property(p => p.tasks_members_divs)
       .HasColumnType("LONGTEXT")
      .HasColumnName("task_members_divs");

                entity.Property(p => p.task_created)
                             .HasColumnType("DATETIME")
                            .HasColumnName("task_created");

                entity.Property(p => p.task_lastedited)
                            .HasColumnType("DATETIME")
                           .HasColumnName("task_lastedited");

            });

            modelBuilder.Entity<schema_marks>(entity =>
            {
                entity.ToTable("marks")
                .HasKey(p => p.idmarks);

                entity.Property(p => p.user_id)
               .HasColumnType("VARCHAR")
               .HasMaxLength(200)
              .HasColumnName("user_id")
              .IsRequired();

                entity.Property(p => p.date)
               .HasColumnType("DATETIME")
              .HasColumnName("date")
              .IsRequired();


                entity.Property(p => p.first_mark)
             .HasColumnType("INT")
            .HasColumnName("first_mark");

                entity.Property(p => p.second_mark)
             .HasColumnType("INT")
            .HasColumnName("second_mark");



                entity.Property(p => p.first_description)
                            .HasColumnType("TEXT")
                           .HasColumnName("first_description");

                entity.Property(p => p.second_description)
                      .HasColumnType("TEXT")
                     .HasColumnName("second_description");

                entity.Property(p => p.firstAttachments)
                           .HasColumnType("TEXT")
                          .HasColumnName("firstAttachments");

                entity.Property(p => p.secondAttachments)
                      .HasColumnType("TEXT")
                     .HasColumnName("secondAttachments");


                entity.Property(p => p.isTwoMarks)
          .HasColumnType("TINYINT(1)")
         .HasColumnName("isTwoMarks");

                entity.Property(p => p.mark_created)
         .HasColumnType("DATETIME")
        .HasColumnName("mark_created")
        .HasDefaultValueSql("CURRENT_TIMESTAMP()");

            });

            modelBuilder.Entity<schema_task_tracking_card>(entity =>
            {
                entity.ToTable("task_tracking_card")
                .HasKey(p => p.ID);

                entity.Property(p => p.task_id)
               .HasColumnType("VARCHAR")
               .HasMaxLength(45)
              .HasColumnName("task_id")
              .IsRequired();

                entity.Property(p => p.card_body)
               .HasColumnType("TEXT")
              .HasColumnName("card_body")
              .IsRequired();

                entity.Property(p => p.card_marksdown)
           .HasColumnType("TEXT")
          .HasColumnName("card_marksdown")
          .IsRequired();

                entity.Property(p => p.card_status)
             .HasColumnType("VARCHAR")
             .HasMaxLength(45)
            .HasColumnName("card_status")
            .IsRequired();

                entity.Property(p => p.card_action_author)
            .HasColumnType("VARCHAR")
            .HasMaxLength(200)
           .HasColumnName("card_action_author")
           .IsRequired();

                entity.Property(p => p.card_action_datetime)
         .HasColumnType("DATETIME")
        .HasColumnName("card_action_datetime")
        .HasDefaultValueSql("CURRENT_TIMESTAMP()");

                entity.Property(p => p.card_delegate)
          .HasColumnType("TEXT")
         .HasColumnName("card_delegate");
            });

            modelBuilder.Entity<schema_task_tracking_main>(entity =>
            {
                entity.ToTable("task_tracking_main")
                .HasKey(p => p.id);

                entity.Property(p => p.task_cmd)
               .HasColumnType("VARCHAR")
               .HasMaxLength(100)
              .HasColumnName("task_cmd")
              .IsRequired();

                entity.Property(p => p.task_datetime)
               .HasColumnType("DATETIME")
              .HasColumnName("task_datetime")
              .HasDefaultValueSql("CURRENT_TIMESTAMP()")
              .IsRequired();

                entity.Property(p => p.task_variable1)
           .HasColumnType("VARCHAR(500)")
          .HasColumnName("task_variable1")
          .HasDefaultValueSql("$null")
                .IsRequired();

                entity.Property(p => p.task_variable2)
              .HasColumnType("VARCHAR(500)")
          .HasColumnName("task_variable2")
          .HasDefaultValueSql("$null")
                .IsRequired();

                entity.Property(p => p.task_variable3)
              .HasColumnType("VARCHAR(500)")
          .HasColumnName("task_variable3")
          .HasDefaultValueSql("$null")
                .IsRequired();

                entity.Property(p => p.task_variable4)
             .HasColumnType("VARCHAR(500)")
         .HasColumnName("task_variable4")
         .HasDefaultValueSql("$null")
               .IsRequired();

                entity.Property(p => p.task_author)
             .HasColumnType("TEXT")
            .HasColumnName("task_author")
            .IsRequired();

                entity.Property(p => p.task_id)
             .HasColumnType("TEXT")
            .HasColumnName("task_id")
            .IsRequired();

                entity.Property(p => p.task_card)
            .HasColumnType("VARCHAR(500)")
        .HasColumnName("task_card")
        .HasDefaultValueSql("$null")
              .IsRequired();
            });

            modelBuilder.Entity<schema_projects>(entity =>
            {
                entity.ToTable("projects")
                .HasKey(p => p.id);

                entity.Property(p => p.proj_name)
               .HasColumnType("TEXT")
              .HasColumnName("proj_name")
              .IsRequired();

                entity.Property(p => p.proj_description)
               .HasColumnType("LONGTEXT")
              .HasColumnName("proj_description");

                entity.Property(p => p.proj_details)
              .HasColumnType("LONGTEXT")
             .HasColumnName("proj_details");

                entity.Property(p => p.proj_start)
             .HasColumnType("DATETIME")
            .HasColumnName("proj_start")
            .IsRequired();

                entity.Property(p => p.proj_end)
            .HasColumnType("DATETIME")
           .HasColumnName("proj_end")
           .IsRequired();

                entity.Property(p => p.proj_users)
             .HasColumnType("LONGTEXT")
            .HasColumnName("proj_users");

                entity.Property(p => p.proj_divs)
            .HasColumnType("LONGTEXT")
           .HasColumnName("proj_divs");

                entity.Property(p => p.proj_status)
                        .HasColumnType("VARCHAR")
                       .HasColumnName("proj_status")
                       .HasMaxLength(45)
                       .HasDefaultValue("waiting")
                       .IsRequired();

                entity.Property(p => p.proj_avatar_path)
                        .HasColumnType("VARCHAR")
                       .HasColumnName("proj_avatar_path")
                        .HasDefaultValue("prj_default.jpg")
                       .HasMaxLength(300)
                       .IsRequired();

                entity.Property(p => p.proj_tasks)
                             .HasColumnType("TEXT")
                            .HasColumnName("proj_tasks");

                entity.Property(p => p.proj_created)
            .HasColumnType("DATETIME")
           .HasColumnName("proj_created")
           .HasDefaultValueSql("CURRENT_TIMESTAMP");
            });
            #endregion


            #region News 
            modelBuilder.Entity<schema_news>(entity =>
            {
                entity.ToTable("news")
                .HasKey(p => p.idnews);

                entity.Property(p => p.news_head)
               .HasColumnType("VARCHAR")
              .HasColumnName("news_head")
              .HasMaxLength(300)
              .IsRequired();

                entity.Property(p => p.news_title)
             .HasColumnType("VARCHAR")
            .HasColumnName("news_title")
            .HasMaxLength(300)
            .IsRequired();

                entity.Property(p => p.news_body)
             .HasColumnType("TEXT")
            .HasColumnName("news_body")
            .IsRequired();

                entity.Property(p => p.news_added)
               .HasColumnType("DATETIME")
              .HasColumnName("news_added")
             .HasDefaultValueSql("CURRENT_TIMESTAMP()");

                entity.Property(p => p.news_tags)
            .HasColumnType("VARCHAR")
           .HasColumnName("news_tags")
           .HasMaxLength(300)
           .IsRequired();

                entity.Property(p => p.news_title_img)
            .HasColumnType("VARCHAR")
           .HasColumnName("news_title_img")
           .HasMaxLength(300)
           .IsRequired();

                entity.Property(p => p.news_is_archived)
           .HasColumnType("TINYINT")
          .HasColumnName("news_is_archived")
          .HasMaxLength(1)
          .HasDefaultValue(false)
          .IsRequired();

                entity.Property(p => p.news_category)
          .HasColumnType("VARCHAR")
         .HasColumnName("news_category")
         .HasMaxLength(300)
         .HasDefaultValue("COMMON")
         .IsRequired();

                entity.Property(p => p.news_author)
          .HasColumnType("INT")
         .HasColumnName("news_author")
         .IsRequired();

                entity.Property(p => p.news_mention_divs)
         .HasColumnType("TEXT")
        .HasColumnName("news_mention_divs")
        .IsRequired();

                entity.Property(p => p.news_mention_users)
         .HasColumnType("TEXT")
        .HasColumnName("news_mention_users")
        .IsRequired();

            });
            #endregion

            #region News Comments
            modelBuilder.Entity<schema_news_comments>(entity =>
            {
                entity.ToTable("news_comments")
                .HasKey(p => p.idnews_comments);

                entity.Property(p => p.news_msg_user_id)
               .HasColumnType("INT")
              .HasColumnName("news_msg_user_id")
              .IsRequired();

                entity.Property(p => p.news_msg_added)
             .HasColumnType("DATETIME")
            .HasColumnName("news_msg_added")
            .HasDefaultValueSql("CURRENT_TIMESTAMP()")
            .IsRequired();

                entity.Property(p => p.news_id)
             .HasColumnType("INT")
            .HasColumnName("news_id")
            .IsRequired();

                entity.Property(p => p.news_inreply_id)
               .HasColumnType("INT")
              .HasColumnName("news_inreply_id")
             .IsRequired();

                entity.Property(p => p.news_body)
            .HasColumnType("TEXT")
           .HasColumnName("news_body")
           .IsRequired();

                entity.Property(p => p.news_edited)
            .HasColumnType("TINYINT")
            .HasDefaultValue(false)
           .HasColumnName("news_edited")
           .IsRequired();

                entity.Property(p => p.news_time_edited)
            .HasColumnType("DATETIME")
           .HasColumnName("news_time_edited")
           .HasDefaultValueSql("CURRENT_TIMESTAMP()")
           .IsRequired();


            });
            #endregion

            #region TTM Comments
            modelBuilder.Entity<schema_tasktracking_comments>(entity =>
            {
                entity.ToTable("tasktracking_comments")
                .HasKey(p => p.idtasktracking_comments);

                entity.Property(p => p.ttm_msg_user_id)
               .HasColumnType("INT")
              .HasColumnName("ttm_msg_user_id")
              .IsRequired();

                entity.Property(p => p.ttm_msg_added)
             .HasColumnType("DATETIME")
            .HasColumnName("ttm_msg_added")
            .HasDefaultValueSql("CURRENT_TIMESTAMP()")
            .IsRequired();

                entity.Property(p => p.ttm_id)
             .HasColumnType("INT")
            .HasColumnName("ttm_id")
            .IsRequired();

                entity.Property(p => p.ttm_inreply_id)
               .HasColumnType("INT")
              .HasColumnName("ttm_inreply_id")
             .IsRequired();

                entity.Property(p => p.ttm_body)
            .HasColumnType("TEXT")
           .HasColumnName("ttm_body")
           .IsRequired();

                entity.Property(p => p.ttm_edited)
            .HasColumnType("TINYINT")
            .HasDefaultValue(false)
           .HasColumnName("ttm_edited")
           .IsRequired();

                entity.Property(p => p.ttm_time_edited)
            .HasColumnType("DATETIME")
           .HasColumnName("ttm_time_edited")
           .HasDefaultValueSql("CURRENT_TIMESTAMP()")
           .IsRequired();


            });
            #endregion

            #region News Tags
            modelBuilder.Entity<schema_news_tags>(entity =>
            {
                entity.ToTable("news_tags")
                .HasKey(p => p.idnews_tags);

                entity.Property(p => p.news_tags_body)
               .HasColumnType("VARCHAR")
               .HasMaxLength(300)
              .HasColumnName("news_tags_body")
              .IsRequired();
            });

            #endregion

            #region News Cats
            modelBuilder.Entity<schema_news_cats>(entity =>
            {
                entity.ToTable("news_categories")
                .HasKey(p => p.idnews_categories);

                entity.Property(p => p.news_cats_body)
               .HasColumnType("VARCHAR")
               .HasMaxLength(300)
              .HasColumnName("news_cats_body")
              .IsRequired();
            });
            #endregion

        }

        public void EntityFramework_Add(DbSet<schema_a_health> dbset)
        {
            Add(dbset);
            SaveChanges();
        }


        #region Admin Tables
        public DbSet<schema_a_health> a_health { get; set; }

        #endregion

        #region Addictional User eXtension Tables 
        //public DbSet<schema_aux_companies> aux_companies { get; set; }
        //public DbSet<schema_aux_divisions> aux_divisions { get; set; }
        //public DbSet<schema_aux_email_inbox> aux_email_inbox { get; set; }
        //public DbSet<schema_aux_positions> aux_positions { get; set; }
        public DbSet<schema_aux_telegrambot> aux_telegrambot { get; set; }
        public DbSet<schema_aux_workingdays> aux_workingdays { get; set; }


        #endregion

        #region User Tables

        public DbSet<schema_calendar_schedules> calendar_schedules { get; set; }

        public DbSet<schema_divisions> divisions { get; set; }

        public DbSet<schema_key_store> key_store { get; set; }
        //public DbSet<schema_main> main { get; set; }
        public DbSet<schema_maps_points> maps_points { get; set; }

        public DbSet<schema_marks> marks { get; set; }

        public DbSet<schema_projects> projects { get; set; }
        //public DbSet<schema_roles> roles { get; set; }

        public DbSet<schema_skills> skills { get; set; }
        public DbSet<schema_task_tracking_card> task_tracking_card { get; set; }

        public DbSet<schema_task_tracking_main> task_tracking_main { get; set; }
        public DbSet<schema_telegram_push> telegram_push { get; set; }

        public DbSet<schema_tasks> tasks { get; set; }

        public DbSet<schema_users> users { get; set; }

        #endregion

        #region Markdown Tables
        public DbSet<schema_markdown_cards> markdown_cards { get; set; }
        public DbSet<schema_markdown_desks> markdown_desks { get; set; }

        public DbSet<schema_markdown_todo_list> markdown_todo_list { get; set; }

        #endregion

        #region DbSet Alexandra

        public DbSet<schema_alexandra_contacts> alexandra_contacts { get; set; }
        public DbSet<schema_alexandra_locations> alexandra_locations { get; set; }
        public DbSet<schema_alexandra_polls> alexandra_polls { get; set; }

        #endregion

        #region News

        public DbSet<schema_news> news { get; set; }

        public DbSet<schema_news_comments> news_comments { get; set; }

        public DbSet<schema_news_tags> news_tags { get; set; }

        public DbSet<schema_news_cats> news_cats { get; set; }
        public DbSet<schema_tasktracking_comments> ttm_comm { get; set; }


        #endregion


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
        public List<string> getMultipleStringDecodingString(string input_str)
        {
            List<string> encoded = new List<string>();
            if (input_str != null)
            {
                string[] subs = input_str.Split("$$$", StringSplitOptions.RemoveEmptyEntries);

                foreach (string str in subs)
                {
                    encoded.Add(str);
                }
                return encoded;
            }
            else
            {
                encoded.Add("");
                return encoded;
            }
        }



    }
}
