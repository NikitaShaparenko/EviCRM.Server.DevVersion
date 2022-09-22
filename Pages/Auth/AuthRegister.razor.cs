using EviCRM.Server.Core;
using EviCRM.Server.Core.EntityFramework;
using Majorsoft.Blazor.Components.Notifications;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace EviCRM.Server.Pages.Auth
{
    public partial class AuthRegister
    {
        bool IsContainsNumbers(string input)
        {
            if (input != null && input != "")
            {
                foreach (char c in input)
                    if (Char.IsNumber(c))
                        return true;
                return false;
            }
            return false;

        }
        bool isAnyNonAlphabetic(string s)
        {
            if (System.Text.RegularExpressions.Regex.IsMatch(s, @"^[а-яА-ЯёЁa-zA-Z0-9]+$"))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        bool isAnyUpperCase(string s)
        {
            if (s != null && s != "")
            {
                if (s.Equals(s.ToLower()))
                    return false;
                return true;
            }
            return false;
        }

        public async Task<bool> isPasswordStrong(string password)
        {
            try
            {
                if (password.Length >= 6)
                {
                    if (IsContainsNumbers(password))
                    {
                        if (isAnyUpperCase(password))
                        {
                            if (isAnyNonAlphabetic(password))
                            {
                                return true;
                            }
                            else
                            {
                                login_message = "Пароль не содержит символов!";
                                login_status = "failed";
                                await ToastsNotifications_ShowCustomToast_Danger("Пароль должен содержать хотя бы один символ!");
                                sc.Log("AuthRegister", "Пароль должен содержать хотя бы один символ", SystemCore.LogLevels.Error);
                                return false;
                            }
                        }
                        else
                        {   
                            login_message = "Пароль не содержит заглавных букв!";
                            login_status = "failed";
                            await ToastsNotifications_ShowCustomToast_Danger("Пароль должен содержать хотя бы одну заглавную букву!");
                            sc.Log("AuthRegister", "Пароль не содержит заглавных букв", SystemCore.LogLevels.Error);
                            return false;
                        }
                    }
                    else
                    {
                        login_message = "Пароль не содержит цифр!";
                        login_status = "failed";
                        await ToastsNotifications_ShowCustomToast_Danger("Пароль должен содержать хотя бы одну цифру!");
                        sc.Log("AuthRegister", "Пароль не содержит цифр", SystemCore.LogLevels.Error);
                        return false;
                    }

                }
                else
                {
                    login_message = "Пароль слишком короткий!";
                    login_status = "failed";
                    await ToastsNotifications_ShowCustomToast_Danger("Пароль слишком короткий. Надо ещё хотя бы " + (6 - password.Length).ToString() + " символов!");
                    sc.Log("AuthRegister", "Пароль слишком короткий!", SystemCore.LogLevels.Error);
                    return false;
                }

            }
            catch(Exception ex)
            {
                await ToastsNotifications_ShowCustomToast_Warning("Произошла неизвестная ошибка! " + ex.Message);
                sc.Log("AuthRegister", "Обработчик ошибок",ex.Message, SystemCore.LogLevels.Error);
                return false;
            }
            
            return false;
        }

        public async Task Register(string password)
        {
            if (register_email is null)
            {
                sc.Log("AuthRegister", "Электронная почта не может быть пустой!", SystemCore.LogLevels.Error);
                await ToastsNotifications_ShowCustomToast_Danger("Необходимо ввести электронную почту!");
                login_status = "failed";
            }
            else
            {
                if (register_email == "")
                {
                    sc.Log("AuthRegister", "Электронная почта не может быть пустой!", SystemCore.LogLevels.Error);
                    await ToastsNotifications_ShowCustomToast_Danger("Необходимо ввести электронную почту!");
                    login_status = "failed";
                }
                else if (!register_email.Contains("@"))
                {
                    sc.Log("AuthRegister", "Электронная почта должна иметь правильный формат!", SystemCore.LogLevels.Error);
                    await ToastsNotifications_ShowCustomToast_Danger("Электронная почта должна иметь правильный формат!");
                    login_status = "failed";
                }
            }

            if (register_login is null)
            {
                sc.Log("AuthRegister", "Имя пользователя не может быть пусто!", SystemCore.LogLevels.Error);
                await ToastsNotifications_ShowCustomToast_Danger("Необходимо ввести имя пользователя!");
                login_status = "failed";
            }
            else
            {
                if (register_login == "")
                {
                    sc.Log("AuthRegister", "Имя пользователя не может быть пусто!", SystemCore.LogLevels.Error);
                    await ToastsNotifications_ShowCustomToast_Danger("Необходимо ввести имя пользователя!");
                    login_status = "failed";
                }
                else if (register_login.Contains("@"))
                {
                    sc.Log("AuthRegister", "Электронная почта должна иметь правильный формат!", SystemCore.LogLevels.Error);
                    await ToastsNotifications_ShowCustomToast_Danger("Электронная почта должна иметь правильный формат!");
                    login_status = "failed";
                }
            }

            if (password is null)
            {
                sc.Log("AuthRegister", "Пароль не может быть пустым!", SystemCore.LogLevels.Error);
                await ToastsNotifications_ShowCustomToast_Danger("Необходимо ввести пароль!");
                login_status = "failed";
            }
            else
            {
                if (password == "")
                {
                    sc.Log("AuthRegister", "Пароль не может быть пустым!", SystemCore.LogLevels.Error);
                    await ToastsNotifications_ShowCustomToast_Danger("Необходимо ввести пароль!");
                    login_status = "failed";
                }
                else if (password.Length<6)
                {
                    sc.Log("AuthRegister", "Пароль слишком короткий! Нужно ещё " + (6-password.Length).ToString() + " символов", SystemCore.LogLevels.Error);
                    await ToastsNotifications_ShowCustomToast_Danger("Пароль слишком короткий! Нужно ещё " + (6 - password.Length).ToString() + " символов");
                    login_status = "failed";
                }
            }

            bool password_status = await isPasswordStrong(password);

            if (password_status == true)
            {
                //IME Supress:
                var value = _context.User_Get(register_login);

                if (value != null)
                {
                    login_message = "Пользователь уже существует!";
                    login_status = "failed";
                    await ToastsNotifications_ShowCustomToast_Danger("Пользователь уже существует!");
                    sc.Log("AuthRegister", "Пользователь уже существует", SystemCore.LogLevels.Error);
                }
                else
                {
                    schema_users NewUser = new()
                    {
                        login = sentinel.TripleDesEncrypt(register_login),
                        password = sentinel.SHA512(register_password),
                        email = sentinel.TripleDesEncrypt(register_email),
                        flag_activated = false,
                        registered = false,
                    };

                    _context.User_Create(NewUser);

                    login_message = "Регистрация прошла успешно!";
                    sc.Log("AuthRegister", "Пользователь успешно зарегистрирован - " + register_login, SystemCore.LogLevels.Info);
                    login_status = "success";
                    UriHelper.NavigateTo("/login",true);

                }
            }
          
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

        public async Task DoRegister(string login, string password)
        {

            var user = new IdentityUser { UserName = login, Email = login };
            var result = await userManager.CreateAsync(user, password);

            await signInManager.SignInAsync(user, false);
            if (result.Succeeded)
            {
                Console.WriteLine("[AuthRegister.DoRegister - (OK)]: Производится регистрация!");
            }

        }

    }
}
