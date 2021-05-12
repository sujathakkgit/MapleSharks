using MapleSharks.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace MapleSharks.Services
{
    public class SwimmerDAO
    {

        public string sqlstmt="";
        public List<SwimmerModel> getSwimmerDetails(SwimmerModel regSwimModel, int UsrSessionid, string connStr)
        {
            List<SwimmerModel> lstSwimModel= new List<SwimmerModel>();
            if (regSwimModel.swrFname == "" || regSwimModel.swrFname is null)
                {
                sqlstmt = "select * from dbo.Swimmers where parentid = @parentid";
            }
            else
            { sqlstmt = "select * from dbo.Swimmers where parentid = @parentid and swrFirstname= @swrFname"; }
            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {
                SqlCommand sqlcmd = new SqlCommand(sqlstmt, sqlconn);
                sqlcmd.Parameters.Add("@parentid", System.Data.SqlDbType.Int).Value = UsrSessionid;
                if (!(regSwimModel.swrFname is null))
                   { sqlcmd.Parameters.Add("@swrFname", System.Data.SqlDbType.VarChar, 50).Value = regSwimModel.swrFname; }
                try
                {
                    sqlconn.Open();
                    SqlDataReader sqlRdr = sqlcmd.ExecuteReader();
                    if (sqlRdr.HasRows)
                    {
                        while (sqlRdr.Read())
                        {
                            regSwimModel = new SwimmerModel();
                            //GetValue(read.GetOrdinal("ColumnID"))
                            // regUsrModel.Id = sqlRdr.GetOrdinal("Id");
                            regSwimModel.Id = (int)sqlRdr["id"];
                            regSwimModel.parentid = (int)sqlRdr["parentid"];
                            regSwimModel.swrFname = (string)sqlRdr["swrFirstname"];
                            regSwimModel.swrLname = (string)sqlRdr["swrLastname"];
                            regSwimModel.swimgroup = (string)sqlRdr["swimgroup"];
                            regSwimModel.dob = (DateTime)sqlRdr["dob"];
                            regSwimModel.active = ((string)sqlRdr["active"]).ToCharArray()[0];
                            //copy to list
                            lstSwimModel.Add(regSwimModel);
                        } //while (sqlRdr.Read());//while
                       

                    }//copy regSwimmodel
                    sqlconn.Close();
                }//try
                catch (Exception e)
                { Console.WriteLine("Errormessage : " + e.Message); }//catch
            }//using
            
            return lstSwimModel;
        }//getSwimmerDetails

        public bool swimmerExists(SwimmerModel swimmodel, int UsrSessionId, string connStr)
        {
            //swmType =1 exists
            //swmtype = 2 new
            //swmtype = 3 failed
            bool swrExists = false;
            SwimmerModel DBSwimmermodel = new SwimmerModel();

            //     DBSwimmermodel = getSwimmerDetails(swimmodel, UsrSessionId, connStr).FirstOrDefault();// GetRange(0,1);
            DBSwimmermodel = getSwimmerDetails(swimmodel, UsrSessionId, connStr).Find(x=> x.swrFname.ToUpper() == swimmodel.swrFname.ToUpper());                                                                                    //     DBSwimmermodel = getSwimmerDetails(swimmodel, UsrSessionId, connStr, "add");
            if ((swimmodel.swrFname is not null) && (DBSwimmermodel is null))//meaning new record
            {
                swrExists = addNewSwimmer(swimmodel, UsrSessionId, connStr);
               if (swrExists) { swimmodel.swrTyp = 2; }

            }
            else if ((swimmodel.swrFname.ToUpper() == DBSwimmermodel.swrFname.ToUpper()) && (swimmodel.parentid == DBSwimmermodel.parentid) && (DBSwimmermodel.Id > 0 ))
            {
                swimmodel.Id = DBSwimmermodel.Id;
                swimmodel.swrFname = DBSwimmermodel.swrFname;
                swimmodel.swrLname = DBSwimmermodel.swrLname;
                swimmodel.swimgroup = DBSwimmermodel.swimgroup;
                swimmodel.parentid = DBSwimmermodel.parentid;
                swimmodel.active = DBSwimmermodel.active;
                //if (DateTime.TryParse(swimmodel.dob,out "1/1/1900"))
                swimmodel.dob = DBSwimmermodel.dob; 
                swimmodel.swrTyp = 1;
                swrExists = true;
            }//if login success
            return swrExists;
        }//swimmerExists

        public bool addNewSwimmer(SwimmerModel swimmodel, int usrSessionId, string connStr)
        {
            bool success = false;

                        //move items from swimgroup to new list
            // List<SwimmerModel> swimgroup = new List<SwimmerModel>;

            populateswimgroup(swimmodel);

            //insert into dbo.PoolUser(PoolUsername, Password, email) values('sujatha', 'sue', 'sujathaa@gmail.com');
           string sqlstmt = "insert into dbo.swimmers(parentid, swimgroup,dob,active,swrFirstname,swrLastname)" +
                " values(@usrid,@swimgroup,@dob,'Y',@swrFname,@swrLname)";
            using (SqlConnection sqlconn = new SqlConnection(connStr))
            {
                SqlCommand sqlcmd = new SqlCommand(sqlstmt, sqlconn);
                sqlcmd.Parameters.Add("@usrid", System.Data.SqlDbType.VarChar, 50).Value = usrSessionId;
                sqlcmd.Parameters.Add("@swimgroup", System.Data.SqlDbType.VarChar, 50).Value = swimmodel.swimgroup;
                sqlcmd.Parameters.Add("@dob", System.Data.SqlDbType.VarChar, 50).Value = swimmodel.dob;
                sqlcmd.Parameters.Add("@swrFname", System.Data.SqlDbType.VarChar, 40).Value = swimmodel.swrFname;
                sqlcmd.Parameters.Add("@swrLname", System.Data.SqlDbType.VarChar, 40).Value = swimmodel.swrLname;
                try
                {
                    sqlconn.Open();
                    // SqlDataReader SqlRdr = sqlcmd.ExecuteReader();
                    sqlcmd.ExecuteNonQuery();
                    sqlconn.Close();
                    success = true;
                    swimmodel.swrTyp = 2;
                }//try
                catch (Exception e)
                {
                    Console.WriteLine("Errormessage : " + e.Message); }//catch
                
                return success;
            }//using
        }//AddnewpoolUser

        public void populateswimgroup(SwimmerModel swimmodel)
        {
            DateTime Dob;
            Dob = swimmodel.dob;
            int age = Convert.ToInt32((Dob.Subtract(DateTime.Today).TotalDays / 365));
            age = Math.Abs(age);
            switch (age)
            {
                case int kidage when (age > 0) && (age <= 5):
                    swimmodel.swimgroup = "Tigers";
                    break;
                case int kidage when (age >= 6) && (age <= 10):
                    swimmodel.swimgroup = "Lemon";
                    break;
                case int kidage when (age >= 11) && (age <= 13):
                    swimmodel.swimgroup = "Angels";
                    break;
                case int kidage when (age >= 14) && (age <= 16):
                    swimmodel.swimgroup = "Whales";
                    break;
                case int kidage when (age >= 17) && (age <= 21):
                    swimmodel.swimgroup = "Hammerheads";
                    break;
                case int kidage when (age >= 22) && (age <=100):
                    swimmodel.swimgroup = "Boneheads";
                    break;
                default:
                    swimmodel.swimgroup = "John Doe";
                    break;
            }//switch

        }//swimgroup
                

            }//swimmerDAO
        }//namespace
