using Dapper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace TakipSiparis.Models
{
    public class DataLayer
    {
        SqlConnection Con; string s_Con;

        public DataLayer()
        {
            Connectin();
        }

        private void Connectin()
        {
            s_Con = ConfigurationManager.ConnectionStrings["SiparisTakipASPEntities"].ToString();

            Con = new SqlConnection(s_Con);
            if (Con.State == System.Data.ConnectionState.Open)
            {
                Con.Close();
            }
            Con.Open();
        }

        public List<Attachment> GetFileList()
        {
            List<Attachment> DetList = new List<Attachment>();
            DetList = SqlMapper.Query<Attachment>(Con, "GetFileDetails", commandType: CommandType.StoredProcedure).ToList();
            return DetList;
        }

        public bool SaveFileDetails(Attachment objDet)
        {
            try
            {
                DynamicParameters Parm = new DynamicParameters();
                Parm.Add("@FileName", objDet.FileName);
                Parm.Add("@FileContent", objDet.FileContent);
                Con.Execute("AddFileDetails", Parm, commandType: System.Data.CommandType.StoredProcedure);
                return true;
            }
            catch (Exception E) {
                Console.WriteLine(E.Message);
                return false; }
        }
    }
}