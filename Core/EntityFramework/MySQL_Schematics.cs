using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EviCRM.Server.Core.EntityFramework
{
    public class MySQL_Schematics
    {

    }

    public class schema_a_health
    {
       
        public int idhealth { get; set; }

      
        public string link { get; set; }

        public string httpcode { get; set; }

        public string status { get; set; }
    }

    public class schema_alexandra_contacts
    {

        public int idalexandra_contacts { get; set; }

        public string firstname { get; set; }

        public string lastname { get; set; }

        public string phonenumber { get; set; }

        public string userId { get; set; }

        public string vcard { get; set; }

        public string login { get; set; }

    }

    public class schema_alexandra_locations
    {
        public int idalexandra_location { get; set; }

        public string lat { get; set; }

        public string lng { get; set; }

        public string name { get; set; }

        public string user { get; set; }

        public DateTime datetime { get; set; }
    }

    public class schema_alexandra_polls
    {
        public int idpolls { get; set; }

        public string multiple_answers { get; set; }

        public string explanations { get; set; }

        public string id { get; set; }

        public string is_anon { get; set; }

        public string is_closed { get; set; }

        public string question { get; set; }

        public string options { get; set; }

        public string type { get; set; }

        public string correctOptionId { get; set; }

        public DateTime adding_time { get; set; }
    }

    public class schema_aux_companies
    {
        public int id { get; set; }

        public string name { get; set; }

        public string location { get; set; }

        public int head { get; set; }
    }

    public class schema_aux_divisions
    {
        public int id { get; set; }
    }

    public class schema_aux_email_inbox
    {
        public int idaux_ { get; set; }

        public string message_ID { get; set; }

        [Column(TypeName = "VARCHAR")]
        [StringLength(100)]
        public string recepient { get; set; }
    }

    public class schema_aux_positions
    {
        public int id { get; set; }
    }

    public class schema_aux_telegrambot
    {
        public int idaux { get; set; }
        public string chatid { get; set; }

        public string login { get; set; }

        public string status { get; set; }
    }

    public class schema_aux_workingdays
    {
        public int iadux_workingdays { get; set; }

        public string user_id { get; set; }

        public string date { get; set; }

        public string day_start { get; set; }

        public string day_end { get; set; }
    }

    public class schema_calendar_schedules
    {
        public int idcalendar_schedules { get; set; }

        public string id { get; set; }

        public string calendarId { get; set; }

        public DateTime startDate { get; set; }

        public DateTime endDate { get; set; }

        public string title { get; set; }

        public string body { get; set; }
        public string users { get; set; }

        public string isinoffice { get; set; }

        public bool isnotify { get; set; }

        public string notify_period { get; set; }

        public string attachments { get; set; }

        public DateTime adding_time { get; set; }

        public string Code { get; set; }
        public string Color { get; set; }
        public string ForeColor { get; set; }
    }

    public class schema_divisions
    {
        public int id { get; set; }

        public string division_name { get; set; }

        public string division_cast { get; set; }

        public string division_responsible { get; set; }

        public string division_avatar { get; set; }
    }

    public class schema_key_store
    {
        public int idkey_store { get; set; }

        public string keys_progs { get; set; }

        public string keys_val1 { get; set; }

        public string keys_val2 { get; set; }

        public string keys_val3 { get; set; }

        public string keys_val4 { get; set; }

        public string keys_val5 { get; set; }

        public string keys_val6 { get; set; }
    }

    public class schema_main
    {
        public int id { get; set; }

        public int eval_conn { get; set; }
    }

    public class schema_maps_points
    {
        public int idmaps_points { get; set; }

        public string point_e { get; set; }

        public string point_n { get; set; }

        public string point_userlogin { get; set; }

        public string point_description { get; set; }

        public DateTime point_whenadd { get; set; }
    }

    public class schema_markdown_cards
    {
        public int idmarkdown_cards { get; set; }

        public string markdown_card_name { get; set; }

        public string markdown_card_description { get; set; }

        public DateTime markdown_card_date_start { get; set; }

        public DateTime markdown_card_date_end { get; set; }

        public string markdown_card_img_path { get; set; }

        public string markdown_card_backcolor { get; set; }

        public int markdown_card_taskid { get; set; }

        public int markdown_card_deskid { get; set; }

        public DateTime markdown_card_adding_time { get; set; }
    }

    public class schema_markdown_desks
    {
        public int idmarkdown_desks { get; set; }

        public string markdown_desk_name { get; set; }

        public int markdown_desk_personal_id { get; set; }

        public int markdown_desk_task_id { get; set; }

        public DateTime markdown_adding_time { get; set; }
    }

    public class schema_markdown_todo_list
    {
        public int idmarkdown_todo_list { get; set; }

        public int markdown_todo_taskid { get; set; }

        public bool markdown_todo_checked { get; set; }

        public string markdown_todo_name { get; set; }

        public int markdown_todo_cardid { get; set; }
        public DateTime markdown_todo_markdatetime { get; set; }
    }

    public class schema_marks
    {
        public int idmarks { get; set; }

        public int user_id { get; set; }

        public DateTime date { get; set; }

        public int first_mark { get; set; }

        public int second_mark { get; set; }

        public string first_description { get; set; }

        public string second_description { get; set; }
        public string firstAttachments { get; set; }

        public string secondAttachments { get; set; }

        public bool isTwoMarks { get; set; }

        public DateTime mark_created { get; set; }
    }

    public class schema_projects
    {
        public int id { get; set; }

        public string proj_name { get; set; }

        public string proj_description { get; set; }

        public string proj_details { get; set; }

        public DateTime proj_start { get; set; }

        public DateTime proj_end { get; set; }

        public string proj_users { get; set; }

        public string proj_divs { get; set; }

        public string proj_status { get; set; }

        public string proj_avatar_path { get; set; }

        public string proj_tasks { get; set; }

        public DateTime proj_created { get; set; }
    }

    public class schema_roles
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }

    public class schema_skills
    {
        public int idskills { get; set; }

        public string skillscol { get; set; }
    }

    public class schema_task_tracking_card
    {
        public int ID { get; set; }

        public string task_id { get; set; }

        public string card_body { get; set; }

        public string card_marksdown { get; set; }

        public string card_status { get; set; }

        public string card_action_author { get; set; }

        public DateTime card_action_datetime { get; set; }

        public string card_delegate { get; set; }

    }

    public class schema_task_tracking_main
    {
        public int id { get; set; }

        public string task_cmd { get; set; }

        public DateTime task_datetime { get; set; }

        public string task_variable1 { get; set; }

        public string task_variable2 { get; set; }

        public string task_variable3 { get; set; }

        public string task_variable4 { get; set; }

        public string task_author { get; set; }

        public string task_id { get; set; }

        public string task_card { get; set; }

    }

    public class schema_tasktracking_comments
    {
        public int idtasktracking_comments { get; set; }

        public int ttm_msg_user_id { get; set; }

        public DateTime ttm_msg_added { get; set; }

        public int ttm_id { get; set; }

        public int ttm_inreply_id { get; set; }

        public string ttm_body { get; set; }

        public bool ttm_edited { get; set; }

        public DateTime ttm_time_edited { get; set; }
    }

    public class schema_telegram_push
    {
        public int id { get; set; }

        public string chatID { get; set; }

        public string message_body { get; set; }
    }

    public class schema_tasks
    {
        public int id { get; set; }

        public string task_name { get; set; }

        public string task_description { get; set; }

        public DateTime task_startdate { get; set; }

        public DateTime task_enddate { get; set; }

        public DateTime task_enddate_real { get; set; }

        public string task_attachments { get; set; }

        public string task_budget { get; set; }

        public string task_members { get; set; }

        public string task_status { get; set; }

        public string task_responsible_member { get; set; }

        public string task_author { get; set; }

        public bool  task_notify { get; set; }

        public string task_proj { get; set; }

        public string tasks_personal_status { get; set; }

        public string tasks_members_divs { get; set; }

        public DateTime task_created { get; set; }

        public DateTime task_lastedited { get; set; }
    }

    public class schema_users
    {
        public int id { get; set; }

        public string login { get; set; }

        public string password { get; set; }

        public string token { get; set; }

        public string timetoken { get; set; }

        public string fullname { get; set; }

        public string company { get; set; }

        public string position { get; set; }

        public string department { get; set; }

        public string division { get; set; }

        public string skills { get; set; }

        public string mobilephone { get; set; }

        public string email { get; set; }

        public string location { get; set; }

        public string experience_s1 { get; set; }

        public string experience_s2 { get; set; }

        public int completed_proj { get; set; }

        public int pending_proj { get; set; }

        public double total_revenue { get; set; }

        public double revenue { get; set; }

        public string list_proj { get; set; }

        public DateTime birthday { get; set; }

        public DateTime last_login { get; set; }

        public string description { get; set; }

        public string telegram_chatid { get; set; }

        public string role { get; set; }

        public bool flag_activated { get; set; }

        public bool registered { get; set; }

        public string avatar { get; set; }

        public string thinclient_user { get; set; }
    }

    public class schema_news
    {
        public int idnews { get; set; }

        public string news_head { get; set; }

        public string news_title { get; set; }

        public string news_body { get; set; }

        public DateTime news_added { get; set; }

        public string news_tags { get; set; }

        public string news_title_img { get; set; }

        public bool news_is_archived { get; set; }

        public string news_category { get; set; }

        public int news_author { get; set; }

        public string news_mention_divs { get; set; }

        public string news_mention_users { get; set; }
    }

    public class schema_news_comments
    {
        public int idnews_comments { get; set; }

        public int news_msg_user_id { get; set; }

        public DateTime news_msg_added { get; set; }

        public int news_id { get; set; }

        public int news_inreply_id { get; set; }

        public string news_body { get; set; }

        public bool news_edited { get; set; }

        public DateTime news_time_edited { get; set; }
    }

    public class schema_news_tags
    {
        public int idnews_tags { get; set; }
        public string news_tags_body { get; set; }
    }

    public class schema_news_cats
    {
        public int idnews_categories { get; set; }
        public string news_cats_body { get; set; }
    }

}
