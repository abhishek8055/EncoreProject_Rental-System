using AutoMapper;
using EncoreBL.Interfaces;
using EncoreDAL;
using EncoreDAL.Entities;
using EncoreML;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EncoreBL.Repositories
{
    public class UserActions : IUser
    {
        UserActionDAL db = new UserActionDAL();

        public bool AddUser(UserLoginModel mUser)
        {
            UserLogin user = new UserLogin();
            bool status = false;
            Mapper.Map(mUser, user);
            try
            {
                status = db.RegisterUser(user);
            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }

        public UserModel GetUserById(int userId)
        {
            UserModel user = null;
            string query = "SELECT * FROM Users WHERE UId=" + userId;
            DataSet ds = null;

            try
            {
                ds = db.dbContext.GetData(query);
                user.UName = Convert.ToString(ds.Tables[0].Rows[0]["UName"]);
                user.UEmail = Convert.ToString(ds.Tables[0].Rows[0]["UEmail"]);
                user.UContact = Convert.ToString(ds.Tables[0].Rows[0]["UContact"]);
                user.UAge = Convert.ToInt32(ds.Tables[0].Rows[0]["UAge"]);
                user.UAddress = Convert.ToString(ds.Tables[0].Rows[0]["UAddress"]);
                user.UPaymentId = Convert.ToInt32(ds.Tables[0].Rows[0]["UPaymentId"]);
                user.UValid = Convert.ToBoolean(ds.Tables[0].Rows[0]["UValid"]);
                user.UPhoto = Convert.ToString(ds.Tables[0].Rows[0]["UPhoto"]);
            }
            catch(Exception e)
            {
                throw e;
            }
            return user;
        }

        public IEnumerable<UserModel> GetUsers()
        {
            string query = "SELECT * FROM Users WITH (NOLOCK)";
            List<UserModel> userList = new List<UserModel>();
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    userList.Add(new UserModel
                    {
                        UName = Convert.ToString(dr["UName"]),
                        UEmail = Convert.ToString(dr["UEmail"]),
                        UContact = Convert.ToString(dr["UContact"]),
                        UAge = Convert.ToInt32(dr["UAge"]),
                        UAddress = Convert.ToString(dr["UAddress"]),
                        UPaymentId = Convert.ToInt32(dr["UPaymentId"]),
                        UValid = Convert.ToBoolean(dr["UValid"]),
                        UPhoto = Convert.ToString(dr["UPhoto"])
                });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            finally
            {

            }
            return userList;
        }

        public UserLoginModel Login(string email, string password)
        {
            UserLoginModel user = new UserLoginModel();
            DataSet ds = new DataSet();
            try
            {
                ds = db.ValidUser(email, password);
                if(ds == null)
                {
                    return null;
                }
                user.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                user.Password = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                user.RoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleId"]);
            }
            catch (Exception e)
            {
                throw e;
            }
            return user;
        }

        public bool FeedbackBL(FeedbackModel mFeedback)
        {
            Feedback feedback = new Feedback();
            bool status = false;
            Mapper.Map(mFeedback, feedback);
            try
            {
                status = db.dbContext.Feedback(feedback);
            }
            catch (Exception e)
            {
                throw e;
            }
            return status;
        }

        public IEnumerable<FeedbackModel> GetFeedbacks()
        {
            string query = "SELECT * FROM Feedback WITH (NOLOCK)";
            List<FeedbackModel> feedbackList = new List<FeedbackModel>();
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    feedbackList.Add(new FeedbackModel
                    {
                        Id = Convert.ToInt32(dr["Id"]),
                        Email = Convert.ToString(dr["Email"]),
                        Name = Convert.ToString(dr["Name"]),
                        Subject = Convert.ToString(dr["Subject"]),
                        Description = Convert.ToString(dr["Description"])
                    });
                }
            }
            catch (Exception e)
            {

                throw e;
            }
            return feedbackList;
        }
    }
}
