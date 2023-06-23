using dataManager.Library.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dataManager.Library.DataAccess
{
    public class UserData
    {   
        public List<UserModel> GetUserById(string Id)
        {
            sqlDataAccess sql_ = new sqlDataAccess();
            var p = new { Id = Id };
            return sql_.LoadData<UserModel, dynamic>("[dbo].[spUser]", p, "DataConnection");
            /**
             * a couple of notes here which will be improved along the way
             * 1- the connection name is being hard coded for now
             * 2- we have a dependancy of the class sqlDataAccess
             * 3- we have a dynamic type and a useless p variable
             */
        }
    }
}
