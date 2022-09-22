using EviCRM.Server.Pages.Maps.Models;
using EviCRM.Server.ViewModels;

namespace EviCRM.Server.Core.EntityFramework
{
    public class MySQL_EntityFramework_Controller
    {
        private readonly Context _context;

        public MySQL_EntityFramework_Controller(Context context)
        {
            _context = context;
        }

    }
}
