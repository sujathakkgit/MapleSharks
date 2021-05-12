using MapleSharks.Models;
using MapleSharks.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace MapleSharks.Services
{
    public class DBservice
    {
        PoolUserDAO poolUsr = new PoolUserDAO();
        SwimmerDAO swimmerUsr = new SwimmerDAO();
        ComboDAO comboUsr = new ComboDAO();
        //tmpDBComboModel 

        static String connStr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=MapleSharkPool;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public bool PoolUserExists(UserModel usrmodel, string callerid)
        {
         
            return poolUsr.PoolUserExists(usrmodel, callerid, connStr);
           
        } //pooluserexists

        public bool UpdateAccount(ComboModel cmbModel)
        {

            return comboUsr.UpdateComboUser(cmbModel, connStr);

        } //UpdateAccount

        public ComboModel FetchAccount(ComboModel cmbModel,int usrSessionId)
        {

            return comboUsr.getComboUser(cmbModel,usrSessionId, connStr);

        } //UpdateAccount

        public List<SwimmerModel> getSwimmerDetails(SwimmerModel swimmodel, int usrSessionId)
        {
            //List<SwimmerModel> retlist = new List<SwimmerModel>();
            //retlist = swimmerUsr.getSwimmerDetails(swimmodel, usrSessionId, connStr).ToList();
            //return retlist;
            return swimmerUsr.getSwimmerDetails(swimmodel, usrSessionId, connStr);
        } //isValidswimmer

        public bool swimmerExists(SwimmerModel swimmodel, int usrSessionId)
        {
           // swimmodel.parentid = usrSessionId;
            return swimmerUsr.swimmerExists(swimmodel, usrSessionId, connStr);
        } //isValidswimmer


        public bool addNewSwimmer(SwimmerModel swimmodel, int usrSessionId)
        {
            return swimmerUsr.addNewSwimmer( swimmodel, usrSessionId,connStr);
        } //addNewSwimmer





    }
}
