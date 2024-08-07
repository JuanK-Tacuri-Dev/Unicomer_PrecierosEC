using AdoNetCore.AseClient;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;
using System.Data;
using static PrecierosEC.Core.Utiliies.Constants;

namespace PrecierosEC.Core.Service
{
    public class ServiceErrorLog : IServiceErrorLog
    {
        private readonly AseConnection Connection;
        public ServiceErrorLog()
        {
            Connection = new AseConnection(AppConfiguration.ConnectionLogsString);
            Connection.Open();
        }

        public int Execute(string SqlCmd)
        {
            using AseCommand command = new AseCommand(SqlCmd, Connection);
            return command.ExecuteNonQuery();
        }

        public void SaveErrorlog(ErrorLogModel error)
        {
            string cmd = "[sec].[spErrorInsert_Code]";

            using AseCommand command = new(cmd, Connection);
            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.Add(new AseParameter("@LogonName", AseDbType.VarChar) { Value = error.LogonName.Replace("'", "\"") });
            command.Parameters.Add(new AseParameter("@AppSource", AseDbType.VarChar) { Value = error.ApplicationSource.Replace("'", "\"") });
            command.Parameters.Add(new AseParameter("@Message", AseDbType.VarChar) { Value = error.Message.Replace("'", "\"") });
            command.Parameters.Add(new AseParameter("@AdditionalInformation", AseDbType.VarChar) { Value = Conversions.NothingToDBNULL(error.AdditionalInformation.Replace("'", "\"")) });
            command.Parameters.Add(new AseParameter("@TrackingCode", AseDbType.VarChar) { Value = Conversions.NothingToDBNULL(error.CodigoSeguimiento.Replace("'", "\"")) });     
            command.ExecuteNonQuery();
        }

        [Obsolete]
        public string SaveErrorlog(Exception Exception)
        {
            var CodigoSeguimiento = "";
            string Mensaje = Utilities.GenerateLineLog(Exception, ref CodigoSeguimiento);

            //SaveErrorlog(new ErrorLogModel(AppConfiguration.ApiData, Exception, MessageType.Error, Mensaje, Exception.StackTrace, AppConfiguration.NonUserLog, CodigoSeguimiento));

            return CodigoSeguimiento;
        }
    }
}
