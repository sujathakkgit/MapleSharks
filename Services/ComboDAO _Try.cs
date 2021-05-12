using MapleSharks.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MapleSharks.Services
{
    public class ComboDAO_Try
    {
        

        ComboModel_try  DBTryCmboModel = new ComboModel_try();
        string sqlstmt;

        public ComboModel_try getComboDATAUser(ComboModel_try frmComboModel,ComboModel_try DBCmboModel, string connStr)
        {

            sqlstmt = "select * from dbo.Pooluser where id=@id";
            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {
                SqlCommand sqlcmd = new SqlCommand(sqlstmt, sqlconn);
                sqlcmd.Parameters.Add("@Id", System.Data.SqlDbType.Int).Value = frmComboModel.poolUsrModel.Id;
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
                            DBCmboModel.poolUsrModel.Id = (int)sqlRdr["id"];
                            DBCmboModel.poolUsrModel.Username = (string)sqlRdr["PoolUsername"];
                            DBCmboModel.poolUsrModel.Password = (string)sqlRdr["Password"];
                            DBCmboModel.poolUsrModel.EmailId = (string)sqlRdr["email"];
                            DBCmboModel.poolUsrModel.active = ((string)sqlRdr["active"]).ToCharArray()[0];
                            //DBCmboModel.ImagePath = (string)sqlRdr["PicPath"];
                            DBCmboModel.poolUsrModel.CPicture = System.Convert.ToBase64String((byte[])sqlRdr["accimage"]);
                            DBCmboModel.poolUsrModel.cUsrAddressNo = (int)sqlRdr["uno"];
                            DBCmboModel.poolUsrModel.cUsrAddressStreet = (string)sqlRdr["street"];
                            DBCmboModel.poolUsrModel.cUsrAddressCity = (string)sqlRdr["city"];
                            DBCmboModel.poolUsrModel.cUsrAddressZipcode = (int)sqlRdr["zipcode"];
                            //DBCmboModel.ImagePath = (string)sqlRdr["PicPath"];
                            DBCmboModel.poolUsrModel.CPicture = System.Convert.ToBase64String((byte[])sqlRdr["accimage"]);

                        } //while
                    }//copy regUsermodel
                    sqlconn.Close();
                }//try
                catch (Exception e)
                { Console.WriteLine("Errormessage : " + e.Message); }//catch
            }//using

            return DBCmboModel;
        }//getPoolUser

        //public ComboModel PoolUsrComboData(ComboModel cmboModel, string connStr)
        //{

        //}//poolUsrComboData

    }
}
