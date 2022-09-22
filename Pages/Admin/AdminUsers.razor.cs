using EviCRM.Server.Core.EntityFramework;

namespace EviCRM.Server.Pages.Admin
{
    public partial class AdminUsers
    {
        void ResetPassword(schema_users user) //Сброс пароля
        {
            selected_User = user;
            OpenModal();
        }

        async Task OnHandlerResetPassword(string password)
        {
            selected_User.password = password;
            _context.users.Update(selected_User);
            CloseModal();
            await InvokeAsync(StateHasChanged);

        }

        void OpenModal()
        {
            isModalOpened = true;
            StateHasChanged();
        }

        void CloseModal()
        {
            isModalOpened = false;
            StateHasChanged();
        }

        protected override async Task OnInitializedAsync()
        {
            Users = _context.Users_Get();

            ready = true;
            await InvokeAsync(StateHasChanged);
        }

        public async Task Activate(schema_users user)
        {
            user.flag_activated = true;
            _context.User_Update(user);

            sc.Log("AdminUsers", "Пользователь " + sentinel.TripleDesDecrypt(user.login) + " был активирован!", Core.SystemCore.LogLevels.Info);

            await InvokeAsync(StateHasChanged);
        }

        public async Task DowngradeToUser(schema_users user)
        {
            user.role= "user";
            _context.User_Update(user);
            sc.Log("AdminUsers", "Пользователь " + sentinel.TripleDesDecrypt(user.login) + " был понижен до пользователя!", Core.SystemCore.LogLevels.Info);
            await InvokeAsync(StateHasChanged);
        }

        public async Task UpgradeToAdmin(schema_users user)
        {
            user.role = "admin";
            _context.User_Update(user);
            sc.Log("AdminUsers", "Пользователь " + sentinel.TripleDesDecrypt(user.login) + " был повышен до администратора!", Core.SystemCore.LogLevels.Info);
            await InvokeAsync(StateHasChanged);
        }


        public async Task Deactivate(schema_users user)
        {
            user.flag_activated = false;
            _context.User_Update(user);
            sc.Log("AdminUsers", "Пользователь " + sentinel.TripleDesDecrypt(user.login) + " был деактивирован!", Core.SystemCore.LogLevels.Info);
            await InvokeAsync(StateHasChanged);
        }
    }
}
