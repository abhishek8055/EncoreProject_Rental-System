using EncoreML;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//@AUTHOR ABHISHEK DWIVEDI
//IPRODUCT INTERFACE DECLEARS ALL THE USEER SPECIFIC METHODS
//ALL METHODS ARE IMPLEMENTED BY USER_ACTIONS CLASS

namespace EncoreBL.Interfaces
{
    interface IUser
    {
        bool AddUser(UserLoginModel user);
        UserModel GetUserByEmail(string emailId, int roleId);
        IEnumerable<UserModel> GetUsers();
        UserLoginModel Login(string email, string password);
        bool FeedbackBL(FeedbackModel mFeedback);
        IEnumerable<FeedbackModel> GetFeedbacks();
        bool AddUserDetails(UserModel mUser);
        bool UpdateUserDetails(UserModel mUser);
    }
}
