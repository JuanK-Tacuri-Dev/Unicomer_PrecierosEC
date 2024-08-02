﻿namespace PrecierosEC.Core.Utiliies
{
    public class MensaggeErrorLog
    {
        public class ApiConstants
        {

            public const string Version = "1.0";
        }

        public const String ErrorGeneral = "Ha ocurrido una excepción, código de seguimiento => {0} ";

        
    }
    public enum DestinosLog : byte
    {
        DataBase = 1,
        AzureBlobStorage = 2,
        AmazonBucket = 3,
        Disk = 4,
    }
    public enum Ambiente
    {
        Desarrollo = 1,
        Calidad = 2,
        Produccion = 3
    }

    public enum DestinoCadenaConexion : byte
    {
        VariableLocal = 1,
        Appsettings = 2
    }

}
