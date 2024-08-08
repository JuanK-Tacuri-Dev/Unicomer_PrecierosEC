namespace PrecierosEC.Core.Utiliies
{
    public class MensaggeErrorLog
    {
        public class ApiConstants
        {

            public const string Version = "1.0";
        }

        public const String ErrorGeneral = "Ha ocurrido una excepción, código de seguimiento => {0} ";


    }
    public class Constants
    {
        public enum MessageType
        {
            Information,
            Error,
            Warning,
            Confirmation,
            Success
        }
        public enum DestinosLog : byte
        {
            DataBase = 1,
            AzureBlobStorage = 2,
            AmazonBucket = 3,
            Disk = 4,
        }


        public enum DestinoCadenaConexion : byte
        {
            VariableLocal = 1,
            Appsettings = 2
        }

    }
}