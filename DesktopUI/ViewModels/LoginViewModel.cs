using Caliburn.Micro;
using DesktopUI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DesktopUI.ViewModels
{
    public class LoginViewModel : Screen
    {
        private string _username;
        private string _password;
        private IAPIHelper _APIHelper;

        public LoginViewModel(IAPIHelper _api)
        {
            _APIHelper = _api;
        }

        public string UserName { get { return _username; } set { _username = value; NotifyOfPropertyChange(() => UserName); NotifyOfPropertyChange(() => CanLogIn); } }
        public string Password { get { return _password; } set { _password = value; NotifyOfPropertyChange(() => Password); NotifyOfPropertyChange(() => CanLogIn); } }

        private bool _isErrorVisible;

        public bool IsErrorVisible
        {
            get { return ErrorMessage?.Length > 0; }
            set { _isErrorVisible = value; }
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set { _errorMessage = value; NotifyOfPropertyChange(() => ErrorMessage); NotifyOfPropertyChange(() => IsErrorVisible); }
        }



        /**
         * Works as a control which can enable and disable the button, it only works if we define the LogIn() function for the button as well.
         */
        public bool CanLogIn
        {
            get
            {
                bool output = false;
                if (UserName?.Length > 0 && Password?.Length > 0)
                {
                    output = true;
                }
                return output;
            }
        }

        public async Task LogIn()
        {
            try
            {
                ErrorMessage = "";
                var res = await _APIHelper.Authenticate(UserName, Password);
            } catch (Exception ex)
            {
                ErrorMessage = ex.Message == "OK" ? "" : ex.Message;
            }
        }
    }
}
