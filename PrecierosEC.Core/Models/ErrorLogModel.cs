using static PrecierosEC.Core.Utiliies.Constants;

namespace PrecierosEC.Core.Models
{
    public class ErrorLogModel
    {
        public ErrorLogModel()
        {

        }
        public ErrorLogModel(string ApplicationSource, Exception Exception, MessageType Type, string Message, string AdditionalInformation = "", string LogonName = "", string CodigoSeguimiento = "")
        {

            this.ApplicationSource = ApplicationSource;
            this.Exception = Exception;
            this.Type = Type;
            this.Message = Message;
            this.AdditionalInformation = AdditionalInformation;
            this.LogonName = LogonName;
            this.CodigoSeguimiento = CodigoSeguimiento;

        }
        public ErrorLogModel(string ApplicationSource, Exception Exception, string LogonName, MessageType Type, string mensaje)
        {

            this.ApplicationSource = ApplicationSource;
            this.Exception = Exception;
            this.Type = Type;
            this.LogonName = LogonName;
            this.Message = mensaje;

        }


        public ErrorLogModel(string ApplicationSource, MessageType Type, string Message, string AdditionalInformation = "", string LogonName = "", string CodigoSeguimiento = "")
        {

            this.ApplicationSource = ApplicationSource;
            this.Type = Type;
            this.Message = Message;
            this.AdditionalInformation = AdditionalInformation;
            this.LogonName = LogonName;
            this.CodigoSeguimiento = CodigoSeguimiento;

        }

        public string ApplicationSource { get; set; }
        public Exception Exception { get; set; }

        public MessageType Type { get; set; }

        public string Message { get; set; }

        public string AdditionalInformation { get; set; }

        public string LogonName { get; set; }
        public string CodigoSeguimiento { get; set; }
    }
}

