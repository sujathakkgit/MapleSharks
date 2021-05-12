using MapleSharks.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Data;

namespace MapleSharks.Services
{
    public class PoolUserDAO
    {
        String sqlstmt;

        public UserModel getPoolUser(UserModel regUsrModel,UserModel DBusrModel,string connStr)
        {
            
            sqlstmt = "select * from dbo.Pooluser where poolusername = @username";
            if (regUsrModel.Username == null) regUsrModel.Username = "";
            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {
                SqlCommand sqlcmd = new SqlCommand(sqlstmt, sqlconn);
                //    sqlcmd.Parameters.Add("@Id", System.Data.SqlDbType.int).Value = regUsrModel.Id;
                sqlcmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = regUsrModel.Username;
                //   sqlcmd.Parameters.Add("@email", System.Data.SqlDbType.VarChar, 50).Value = regUsrModel.EmailId;
                //sqlcmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = regUsrModel.Password;
                //  sqlcmd.Parameters.Add("@active", System.Data.SqlDbType.Char).Value = regUsrModel.active;

                try
                {
                    sqlconn.Open();
                    SqlDataReader sqlRdr = sqlcmd.ExecuteReader();
                    if (sqlRdr.HasRows)
                    {
                        while (sqlRdr.Read())
                        {
                            //GetValue(read.GetOrdinal("ColumnID"))


                           // DBusrModel.Id = sqlRdr.GetOrdinal("Id");
                            DBusrModel.Id = (int) sqlRdr["id"];
                            DBusrModel.Username = (string) sqlRdr["PoolUsername"];
                            DBusrModel.Password = (string) sqlRdr["Password"];
                            DBusrModel.EmailId = (string) sqlRdr["email"];
                            DBusrModel.active = ((string) sqlRdr["active"]).ToCharArray()[0];
                        } //while
                    }//copy regUsermodel
                    sqlconn.Close();
                }//try
                catch (Exception e)
                { Console.WriteLine("Errormessage : " + e.Message); }//catch
            }//using
             
            return DBusrModel;
       }//getPoolUser

        public bool PoolUserExists(UserModel usrmodel, string callerid, string connStr)
        {
            
            bool success = false;
            //usrTyp = 1 : Exists 
            //usrTyp = 2 : Doesnt exists new 
            //usrTypFail = 3 
            UserModel DBUsrmodel = new UserModel();
            getPoolUser(usrmodel,DBUsrmodel, connStr);

            if (callerid.ToUpper() == "LOGIN")
            {
                if (usrmodel.Username == null || usrmodel.Password == null)
                {
                    usrmodel.usrTyp = 4; //Fill all deatils
                }
                else if (DBUsrmodel.Username == null  || DBUsrmodel.Password == null)
                {
                    usrmodel.usrTyp = 3; //Fill all deatils
                }
                    
                else if ((usrmodel.Username.ToUpper() == DBUsrmodel.Username.ToUpper()) && (usrmodel.Password == DBUsrmodel.Password))
                {
                    usrmodel.Id = DBUsrmodel.Id;
                    usrmodel.Username = DBUsrmodel.Username;
                    usrmodel.Password = DBUsrmodel.Password;
                    usrmodel.EmailId = DBUsrmodel.EmailId;
                    usrmodel.active = DBUsrmodel.active;
                    usrmodel.usrTyp = 1;
                    success = true;
                }//if login success
                else if ((usrmodel.Username.ToUpper() != DBUsrmodel.Username.ToUpper()) || (usrmodel.Password != DBUsrmodel.Password))
                {
                    usrmodel.usrTyp = 3; //meaning fail no match
                }//all else
            }
            else if (callerid.ToUpper() == "REGISTER")
            {
                //add cehck to see whether userid/email already exists
                if (usrmodel.Username == null || usrmodel.EmailId == null || usrmodel.Password == null)
                {
                    usrmodel.usrTyp = 4; //Fill all deatils
                }
                //usrmodel.EmailId = null ? "" : usrmodel.EmailId;
              else if (DBUsrmodel.Username == null)
                {
                    success = true;
                    AddPooluser(usrmodel, connStr);
                    usrmodel.usrTyp = 2; //meaning new register
                }
              else if ((usrmodel.Username.ToUpper() == DBUsrmodel.Username.ToUpper()) || (usrmodel.EmailId.ToUpper() == DBUsrmodel.EmailId.ToUpper()))
               {
                usrmodel.usrTyp = 1;//already exists
                }
               else
               {
                usrmodel.usrTyp = 0;
               }
        }
            
        
            return success;
        }//pooluserExists
        public bool AddPooluser(UserModel usrmodel, string connStr)
        {
            bool success = false;
            //insert into dbo.PoolUser(PoolUsername, Password, email) values('sujatha', 'sue', 'sujathaa@gmail.com');
            sqlstmt = "insert into dbo.PoolUser(PoolUsername, Password,email,active) values(@username,@password,@emailid,'Y')";
            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {
                SqlCommand sqlcmd = new SqlCommand(sqlstmt, sqlconn);
                sqlcmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = usrmodel.Username;
                sqlcmd.Parameters.Add("@password", System.Data.SqlDbType.VarChar, 50).Value = usrmodel.Password;
                sqlcmd.Parameters.Add("@emailid", System.Data.SqlDbType.VarChar, 50).Value = usrmodel.EmailId;
                try
                {
                    sqlconn.Open();
                    // SqlDataReader SqlRdr = sqlcmd.ExecuteReader();
                    sqlcmd.ExecuteNonQuery();
                    sqlconn.Close();
                    success = true;
                }//try
                catch (Exception e)
                { Console.WriteLine("Errormessage : " + e.Message); }//catch
                }//using
            return success;
        }//AddnewpoolUser
    }//poolUserDAO
}//namespace
