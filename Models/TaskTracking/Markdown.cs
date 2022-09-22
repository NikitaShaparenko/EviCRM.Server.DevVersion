using EviCRM.Server.Core.EntityFramework;

namespace EviCRM.Server.Models.TaskTracking
{
    public class Markdown_List
    {
        public string color { get; set; }
        public string name { get; set; }
    }

    public class Markdown_TwoWayDesksBindings
    {
        public schema_markdown_desks Desk { get; set; }

        public int desk_num = 0;
    }

    public class MarkdownCard_TwoWayDesksBindings
    {
        public schema_markdown_cards Card { get; set; }

        public List<schema_markdown_todo_list> Todos { get; set; }

        public int desk_num = 0;
    }


}
