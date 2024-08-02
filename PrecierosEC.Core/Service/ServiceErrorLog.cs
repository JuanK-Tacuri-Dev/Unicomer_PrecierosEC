using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Utiliies;

namespace PrecierosEC.Core.Service
{
    public class ServiceErrorLog : IServiceErrorLog
    {
        private readonly SqlConnection Connection;
        public ServiceErrorLog()
        {
            Connection = new SqlConnection(AppConfiguration.ConnectionLogsString);
            Connection.Open();
        }

        public int Execute(string SqlCmd)
        {
            using SqlCommand command = new(SqlCmd, Connection);
            return command.ExecuteNonQuery();
        }
        public void SaveErrorlog(Modelos.ErrorLogModel error)
        {
            string cmd = string.Format("EXEC [sec].[spErrorInsert_Code] @LogonName = '{0}',@AppSource = '{1}',@Message='{2}',@AdditionalInformation='{3}', @TrackingCode='{4}'",
                error.LogonName.Replace("'", "\""), $"{error.ApplicationSource.Replace("'", "\"")}", error.Message.Replace("'", "\""), NothingToDBNULL(error.AdditionalInformation.Replace("'", "\"")), NothingToDBNULL(error.CodigoSeguimiento.Replace("'", "\"")));
            Execute(cmd);
        }
        [Obsolete]
        public string SaveErrorlog(Exception Exception)
        {
            var CodigoSeguimiento = "";
            string Mensaje = InfraUtilities.GenerateLineLog(Exception, ref CodigoSeguimiento);

            SaveErrorlog(new Modelos.ErrorLogModel(ErrorLogSource.ApiData, Exception, MessageType.Error, Mensaje, Exception.StackTrace, ErrorLogSource.NonUserLog, CodigoSeguimiento));

            return CodigoSeguimiento;
        }
    }
}
