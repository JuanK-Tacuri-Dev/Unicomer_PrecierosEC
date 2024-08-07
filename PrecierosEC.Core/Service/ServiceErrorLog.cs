
using Microsoft.Data.Sqlite;
using PrecierosEC.Core.Interface.Service;
using PrecierosEC.Core.Models;
using PrecierosEC.Core.Utiliies;
using System.Data;

namespace PrecierosEC.Core.Service
{
    public class ServiceErrorLog : IServiceErrorLog
    {
        private readonly SqliteConnection Connection;
        public ServiceErrorLog()
        {
            //Connection = new SqliteConnection(AppConfiguration.ConnectionLogsString);
            //Connection.Open();
        }

        public int Execute(string SqlCmd)
        {
            using SqliteCommand command = new SqliteCommand(SqlCmd, Connection);
            return command.ExecuteNonQuery();
        }

        public void SaveErrorLog(ErrorLogModel error)
        {
            // Define the SQL insert command
            string cmdText = @"
            INSERT INTO ErrorLog (  LogonName,  AppSource,  Message,  AdditionalInformation,  TrackingCode) 
                          VALUES ( @LogonName, @AppSource, @Message, @AdditionalInformation, @TrackingCode);";

            using var command = new SqliteCommand(cmdText, Connection);
            // Add parameters to the command
            command.Parameters.Add(new SqliteParameter("@LogonName", DbType.String) { Value = error.LogonName?.Replace("'", "\"") });
            command.Parameters.Add(new SqliteParameter("@AppSource", DbType.String) { Value = error.ApplicationSource?.Replace("'", "\"") });
            command.Parameters.Add(new SqliteParameter("@Message", DbType.String) { Value = error.Message?.Replace("'", "\"") });
            command.Parameters.Add(new SqliteParameter("@AdditionalInformation", DbType.String) { Value = Conversions.NothingToDBNULL(error.AdditionalInformation?.Replace("'", "\"")) });
            command.Parameters.Add(new SqliteParameter("@TrackingCode", DbType.String) { Value = Conversions.NothingToDBNULL(error.CodigoSeguimiento?.Replace("'", "\"")) });

            // Execute the command
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
