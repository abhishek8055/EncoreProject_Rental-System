using AutoMapper;
using EncoreBL.Interfaces;
using EncoreDAL;
using EncoreDAL.Entities;
using EncoreML;
using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


//@AUTHOR ABHISHEK DWIVEDI
//USER_ACTIONS CLASS IS A BUSINESS LAYER CLASS WHICH IMPLEMENTS IUSER INTERFACE 
//USER_ACTIONS CLASS MAPS ALL PRODUCT RELATED QUERIES TO THE PRODUCT_ACTIONS_DAL (DAL) CLASS


namespace EncoreBL.Repositories
{
    public class UserActions : IUser
    {

        //LOGGER INITIALIZATION
        readonly ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        //GET: GETTING INSTANCE OF USER_ACTIONS_DAL (DAL CLASS)
        UserActionDAL db = new UserActionDAL();

        //POST: REGISTER NEW USER TO DATABASE
        public bool AddUser(UserLoginModel mUser)
        {
            UserLogin user = new UserLogin();
            bool status = false;
            Mapper.Map(mUser, user);
            try
            {
                status = db.RegisterUser(user);
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("AddUser() in UserActions Class of Business Layer", e);
                return false;
            }
            return status;
        }

        //GET: GET USER BY EMAIL ID (UNIQUE)
        public UserModel GetUserByEmail(string emailId, int roleId)
        {
            UserModel user = new UserModel();
            string query = "SELECT * FROM Users WHERE UEmail='"+emailId+"' AND URoleId="+roleId;
            DataSet ds = null;
            try
            {
                ds = db.dbContext.GetData(query);
                user.UId = Convert.ToInt32(ds.Tables[0].Rows[0]["UId"]);
                user.URoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["URoleId"]);
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
                //LOG EXCEPTION
                logger.Fatal("GetUserByEmailId() in UserActions Class of Business Layer : ", e);
                return null;
            }
            return user;
        }

        //GET: GET ALL USERS FROM DATABASE
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
                //LOG EXCEPTION
                logger.Fatal("GetUser() in UserActions Class of Business Layer : ", e);
                return null;
            }
            return userList;
        }

        //GET: VERIFY USER CREDENTIALS BY EMAIL AND PASSWORD
        public UserLoginModel Login(string email, string password)
        {
            UserLoginModel user = new UserLoginModel();
            DataSet ds = new DataSet();
            try
            {
                ds = db.ValidUser(email, password);
                if(ds.Tables[0].Rows.Count == 0)
                {
                    return null;
                }
                user.Email = Convert.ToString(ds.Tables[0].Rows[0]["Email"]);
                user.Password = Convert.ToString(ds.Tables[0].Rows[0]["Password"]);
                user.RoleId = Convert.ToInt32(ds.Tables[0].Rows[0]["RoleId"]);
                user.Id = Convert.ToInt32(ds.Tables[0].Rows[0]["Id"]);
            }
            catch (Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("Login() in UserActions Class of Business Layer : ", e);
                return null;
            }
            return user;
        }

        //POST: ADD FEEDBACK TO DATABASE
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
                //LOG EXCEPTION
                logger.Fatal("EFeedbackBL() in UserActions Class of Business Layer", e);
                return false;
            }
            return status;
        }

        //GET: GET ALL FEEDBACKS FROM DATABASE
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
                //LOG EXCEPTION
                logger.Fatal("GetFeedbacks() in UserActions Class of Business Layer : ", e);
                return null;
            }
            return feedbackList;
        }

        //POST: ADD USER'S PERSONAL DETAILS TO DATABASE
        public bool AddUserDetails(UserModel mUser)
        {
            User user = new User();
            bool status = false;
            Mapper.Map(mUser, user);
            try
            {
                status = db.AddUserDetails(user);
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("AddUserDetails() in UserActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }

        //POST: UPDATE USER'S PERSONAL DETAILS
        public bool UpdateUserDetails(UserModel mUser)
        {
            User user = new User();
            bool status = false;
            Mapper.Map(mUser, user);
            try
            {
                status = db.UpdateUserDetails(user);
            }
            catch(Exception e)
            {
                //LOG EXCEPTION
                logger.Fatal("UpdateUserDetails() in UserActions Class of Business Layer : ", e);
                return false;
            }
            return status;
        }
    }
}
