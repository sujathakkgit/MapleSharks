using MapleSharks.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MapleSharks.Services
{
    public class ComboDAO
    {
        PoolUserDAO poolusr = new PoolUserDAO();
        SwimmerDAO swrUsr = new SwimmerDAO();

        ComboModel DBCmboModel = new ComboModel();
        string sqlstmt;

        public ComboModel getComboUser(ComboModel cmbModel, int usrSessionId,string connStr)
        {
            cmbModel.Id = usrSessionId;
            cmbModel = getComboUser(cmbModel, DBCmboModel, connStr);
            return cmbModel;
        }//
        public ComboModel getComboUser(ComboModel regUsrModel, ComboModel DBCmboModel, string connStr)
        {

            sqlstmt = "select * from dbo.Pooluser where id=@id";
            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {
                SqlCommand sqlcmd = new SqlCommand(sqlstmt, sqlconn);
                sqlcmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = regUsrModel.Id;
              //  sqlcmd.Parameters.Add("@username", System.Data.SqlDbType.VarChar, 50).Value = regUsrModel.Username;
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
                            DBCmboModel.Id = (int)sqlRdr["id"];
                            DBCmboModel.Username = (string)sqlRdr["PoolUsername"];
                            DBCmboModel.Password = (string)sqlRdr["Password"];
                            DBCmboModel.EmailId = (string)sqlRdr["email"];
                            DBCmboModel.active = ((string)sqlRdr["active"]).ToCharArray()[0];
                            //DBCmboModel.ImagePath = (string)sqlRdr["PicPath"];
                            DBCmboModel.cPicture = System.Convert.ToBase64String((byte[])sqlRdr["accimage"]);
                            DBCmboModel.cUsrAddressNo = (int)sqlRdr["uno"];
                            DBCmboModel.cUsrAddressStreet = (string)sqlRdr["street"];
                            DBCmboModel.cUsrAddressCity = (string)sqlRdr["city"];
                            DBCmboModel.cUsrAddressZipcode = (int)sqlRdr["zipcode"];
                            //DBCmboModel.ImagePath = (string)sqlRdr["PicPath"];
                            DBCmboModel.cPicture = System.Convert.ToBase64String((byte[])sqlRdr["accimage"]);

                        } //while
                    }//copy regUsermodel
                    sqlconn.Close();
                }//try
                catch (Exception e)
                { Console.WriteLine("Errormessage : " + e.Message); }//catch
            }//using

            return DBCmboModel;
        }//getPoolUser

        public bool UpdateComboUser(ComboModel cmboModel, string connStr)
        {
            bool success = false;

            DBCmboModel = getComboUser(cmboModel, DBCmboModel, connStr);
            //insert into dbo.PoolUser(PoolUsername, Password, email) values('sujatha', 'sue', 'sujathaa@gmail.com');
            //sqlstmt = "update dbo.PoolUser set Firstname=@fname,lastname=@lname," +
            //    " uno=@uno, street=@street, city=@city, zipcode=@zipcode, picpath=@imgdir," +
            //    "Accimage=(SELECT a.* from openrowset(BULK N@imgdir,SINGLE_BLOB) as a)" +
            //    " where PoolUserName = @poolusrname and ID = @ID";
            //using (SqlConnection sqlconn = new SqlConnection(connStr))

            sqlstmt = "update dbo.PoolUser set Firstname=@fname,lastname=@lname," +
               " uno=@uno, street=@street, city=@city, zipcode=@zipcode, " +
                "Accimage=@imgdata" +
                " where PoolUserName = @poolusrname and ID = @ID";
            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {

                SqlCommand sqlcmd = new SqlCommand(sqlstmt, sqlconn);
                //where cond
                sqlcmd.Parameters.Add("@poolusrname", System.Data.SqlDbType.VarChar, 50).Value = DBCmboModel.Username;
                sqlcmd.Parameters.Add("@id", System.Data.SqlDbType.Int).Value = DBCmboModel.Id;
                //update cmd
                sqlcmd.Parameters.Add("@fname", System.Data.SqlDbType.VarChar, 50).Value = cmboModel.firstName;
                sqlcmd.Parameters.Add("@lname", System.Data.SqlDbType.VarChar, 50).Value = cmboModel.lastName;
                sqlcmd.Parameters.Add("@uno", System.Data.SqlDbType.Int).Value = cmboModel.cUsrAddressNo;
                sqlcmd.Parameters.Add("@street", System.Data.SqlDbType.VarChar, 50).Value = cmboModel.cUsrAddressStreet;
                sqlcmd.Parameters.Add("@city", System.Data.SqlDbType.VarChar, 50).Value = cmboModel.cUsrAddressCity;
                sqlcmd.Parameters.Add("@zipcode", System.Data.SqlDbType.Int).Value = cmboModel.cUsrAddressZipcode ;
                //sqlcmd.Parameters.Add("@imgdir", System.Data.SqlDbType.VarChar,100).Value = cmboModel.ImagePath;
                if (!(cmboModel.ImageData == null))
                 { 
                using var fileStream = cmboModel.ImageData.OpenReadStream();
                byte[] bytes = new byte[cmboModel.ImageData.Length];
                fileStream.Read(bytes, 0, (int)cmboModel.ImageData.Length);

                sqlcmd.Parameters.Add("@imgdata", System.Data.SqlDbType.VarBinary).Value = bytes;
                }
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
        }//updatecombouser

      

    }
}
