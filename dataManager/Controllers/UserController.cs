﻿using dataManager.Library.DataAccess;
using dataManager.Library.Model;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace dataManager.Controllers
{
    [Authorize]
    public class UserController : ApiController
    {
        public UserModel getUserById()
        {
            string userId = RequestContext.Principal.Identity.GetUserId();
            UserData data = new UserData();
            return data.GetUserById(userId).First();
        }
    }
}
