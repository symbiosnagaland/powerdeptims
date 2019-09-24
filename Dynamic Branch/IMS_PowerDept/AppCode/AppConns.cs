using System.Configuration;


namespace IMS_PowerDept.AppCode
{
    public class AppConns
    {
        public static string GetConnectionString() //Database connectionString
        {
            return (ConfigurationManager.ConnectionStrings["PowerDeptNagalandIMSConnectionString"].ToString());
        }


        public static string GetMasterConnectionString() //Database connectionString
        {
            return (ConfigurationManager.ConnectionStrings["MasterConnectionString"].ToString());
        }
        public static string GetUploadFolderPath()
        {
            return (ConfigurationManager.AppSettings["UPLOADFOLDERPATH"].ToString());
        }


    }
}