using DesktopUI.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace DesktopUI.Helpers
{
    public class APIHelper : IAPIHelper
    {
        private HttpClient APIClient;
        private ILoggedInUser loggedInUser_;

        public APIHelper(ILoggedInUser loggedInUser_)
        {
            initializeClient();
            this.loggedInUser_ = loggedInUser_;
        }

        private void initializeClient()
        {
            /**
             * We added the base url value to the app.config and added a reference to System.configuration to be able to read the values from that app.config file here
             * Hence, we are fetching base url value from the line below
             */
            string baseUrl = ConfigurationManager.AppSettings["baseUrl"];

            APIClient = new HttpClient();
            APIClient.BaseAddress = new Uri(baseUrl);
            APIClient.DefaultRequestHeaders.Accept.Clear();
            APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<AuthenticatedUser> Authenticate(string username, string password)
        {
            FormUrlEncodedContent data = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("grant_type", "password"),
                new KeyValuePair<string, string> ( "username", username ),
                new KeyValuePair<string, string> ("password", password )
            });
            /**
             * using the (using) keyword below makes sure that unmanaged resources used by HttpResponseMessage will be disposed
             */
            using (HttpResponseMessage response = await APIClient.PostAsync("/Token", data))
            {
                if (response.StatusCode.Equals(200))
                {
                    var res = await response.Content.ReadAsAsync<AuthenticatedUser>();
                    return res;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

        public async Task GetUserInfo(string token)
        {
            // firstly, we are clearing all the header values and set our own values again, which are simple "application/json" and the authorization token.
            APIClient.DefaultRequestHeaders.Clear();
            APIClient.DefaultRequestHeaders.Accept.Clear();
            APIClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            APIClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            // secondly, we would make the api call, fetch the data, map the data, and use it for future api calls
            using (HttpResponseMessage response = await APIClient.GetAsync("/api/User"))
            {
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsAsync<ILoggedInUser>();
                    loggedInUser_.Email = result.Email;
                    loggedInUser_.FirstName = result.FirstName;
                    loggedInUser_.LastName = result.LastName;
                    loggedInUser_.token = token;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }

    }
}
