using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Reflection;
using System.Activities;



namespace scs_datarepository
{
    
    internal class DB_UTILITY
    {
        internal static IDbDataParameter CreateParameter(string PName, DbType PType, ParameterDirection PDirection, Object PValue)
        {
            SqlCommand Command = new SqlCommand();
            IDbDataParameter DP = Command.CreateParameter();
            DP.ParameterName = PName;
            DP.DbType = PType;
            DP.Direction = PDirection;
            DP.Value = PValue;
            return DP;
        }

        internal  static DataTable RunSP(string SPName, IDbDataParameter[] ParamsList)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConStr);
            SqlCommand Command = new SqlCommand(SPName, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            foreach (IDbDataParameter Params in ParamsList)
            {
                Command.Parameters.Add(Params);
            }
            DataTable DTResultTable = new DataTable();
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(Command);
                DA.Fill(DTResultTable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DTResultTable;
        }

        internal static DataTable RunSP(string SPName)
        {
            string ConStr = ConfigurationManager.ConnectionStrings["Con"].ConnectionString;
            SqlConnection Connection = new SqlConnection(ConStr);
            SqlCommand Command = new SqlCommand(SPName, Connection);
            Command.CommandType = CommandType.StoredProcedure;
            DataTable DTResultTable = new DataTable();
            try
            {
                SqlDataAdapter DA = new SqlDataAdapter(Command);
                DA.Fill(DTResultTable);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return DTResultTable;
        }
    }

    //common components
    //mail sending
    #region Mail sending
    internal class COMMON_UTILITY
    {
        //getting database format from sys format
        internal static string GetDBDate(DateTime dtDate)
        {
            return dtDate.ToString("yyyy/MM/dd");
        }

        //mail sending
        public void SendMail(string FromID, string FromPWD, string DestinationID, string Subject, string Details, Char Mode)
        {
            try
            {
                MailMessage _objMailMessage = new MailMessage();
                _objMailMessage.From = new MailAddress(FromID);

                switch (Mode)
                {
                    case 'T': _objMailMessage.To.Add(DestinationID);
                        break;
                    case 'C': _objMailMessage.CC.Add(DestinationID);
                        break;
                    case 'B': _objMailMessage.Bcc.Add(DestinationID);
                        break;
                }

                _objMailMessage.Subject = Subject;
                //_objMailMessage.AlternateViews.Add(Mail_Body(Details));
                _objMailMessage.Body = Details;
                _objMailMessage.IsBodyHtml = true;
                _objMailMessage.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                
                //sending client
                SendClientMail(_objMailMessage, FromPWD);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

//        private AlternateView Mail_Body(string Details)
//        {
//            LinkedResource Img = new LinkedResource(@"E:\Images\sfs-logo.jpg", MediaTypeNames.Image.Jpeg);
//            Img.ContentId = "MyImage";
//            string str = @"  
//            <table>  
//                <tr>  
//                </tr>  
//                <tr>  
//                    <td>
//                    </td>
//                    <td>  
//                      <img src=cid:MyImage  id='img' alt='' width='400px' height='300px'/>   
//                    </td>  
//                </tr>
//            </table>  
//            ";
//            AlternateView AV =
//            AlternateView.CreateAlternateViewFromString(str, null, MediaTypeNames.Text.Html);
//            AV.LinkedResources.Add(Img);
//            return AV;
//        }  

        private void SendClientMail(MailMessage _MailContent, string Password)
        {
            try
            {
                SmtpClient client = new SmtpClient();
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Timeout = 20000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = false;
                client.Credentials = new System.Net.NetworkCredential(_MailContent.From.ToString(), Password);
                client.Send(_MailContent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
    #endregion




    //Reflection
    //data table to object converter
    #region Object Converter
    internal class OBJECT_UTILITY
    {
        //individual object converter
        public static T GetConvert<T>(DataTable dtData) where T : new()
        {
            Type objType = typeof(T);
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            T newObject = new T();
            foreach (DataRow _EachRow in dtData.Rows)
            {
                for (int Index = 0; Index < dtData.Columns.Count; Index++)
                {
                    string ColumnName = dtData.Columns[Index].ColumnName.ToUpper();
                    PropertyInfo info = (PropertyInfo)hashtable[ColumnName];
                    if ((info != null) && info.CanWrite && _EachRow[ColumnName] != DBNull.Value)
                    {
                        info.SetValue(newObject, _EachRow[ColumnName], null);
                    }
                }
            }
            return newObject;
        }

        //for list of objects
        public static List<T> GetConvertCollection<T>(DataTable dtData) where T : new()
        {
            Type objType = typeof(T);
            Hashtable hashtable = new Hashtable();
            PropertyInfo[] properties = objType.GetProperties();
            foreach (PropertyInfo info in properties)
            {
                hashtable[info.Name.ToUpper()] = info;
            }
            List<T> lstT = new List<T>();
            foreach (DataRow _EachRow in dtData.Rows)
            {
                T newObject = new T();
                for (int Index = 0; Index < dtData.Columns.Count; Index++)
                {
                    string ColumnName = dtData.Columns[Index].ColumnName.ToUpper();
                    PropertyInfo info = (PropertyInfo)hashtable[ColumnName];
                    if ((info != null) && info.CanWrite && _EachRow[ColumnName] != DBNull.Value)
                    {
                        info.SetValue(newObject, _EachRow[ColumnName], null);
                    }
                }
                lstT.Add(newObject);
            }
            return lstT;
        }

    }
    #endregion
}

