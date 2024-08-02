using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace PrecierosEC.Core.Utiliies
{
    public static class ApplicationService
    {
        public static void Configure(this IServiceCollection services, IConfiguration configuration)
        {
            string mensaje = null;
            //Rp3.Security.Cryptography.KeyFileName = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "key");
            //_ = new DatabaseCredential();
            //_ = new DatabaseCredentialLogs();
            var settings = Utilities.LeerAppSettings<ApiSettings>(typeof(ApiSettings), ref mensaje);

            AppConfiguration.NameApi_Title = settings.NameApi.Title;
            AppConfiguration.NameApi_Version = settings.NameApi.Version;
            AppConfiguration.NameApi_Name = settings.NameApi.Name;
            AppConfiguration.NameApi_Description = settings.NameApi.Description;
            AppConfiguration.NameApi_TermsOfService = settings.NameApi.TermsOfService;
            AppConfiguration.NameApi_ContactName = settings.NameApi.Contact.Name;
            AppConfiguration.NameApi_ContactEmail = settings.NameApi.Contact.Email;
            AppConfiguration.NameApi_ContactUrl = settings.NameApi.Contact.Url;


            AppConfiguration.ApiData = settings.ErrorLogSource.ApiData;
            AppConfiguration.NonUserLog = settings.ErrorLogSource.NonUserLog;
            AppConfiguration.Custom = settings.ErrorLogSource.Custom;


            //DatabaseCredential Credential;
            //if (settings.Credentials is not null)
            //{
            //    var Destinocredential = settings.Credentials.Destino == 0 ? settings.Credentials.DefaultConexion : settings.Credentials.Destino;

            //    switch ((DestinoCadenaConexion)Destinocredential)
            //    {
            //        case DestinoCadenaConexion.VariableLocal:

            //            _ = new DatabaseCredential
            //            {
            //                Server = settings.Credentials.Appsettings.Server,
            //                Database = configuration["Credentials:Appsettings:Database"],
            //                User = configuration["Credentials:Appsettings:User"],
            //                Password = configuration["Credentials:Appsettings:Pwd"],
            //                //Encripter = configuration["Credentials:Appsettings:Encripter"]
            //            };
            //            break;
            //        case DestinoCadenaConexion.Appsettings:

            //            Credential = new DatabaseCredential
            //            {
            //                Server = settings.Credentials.Appsettings.Server,
            //                Database = settings.Credentials.Appsettings.Database,
            //                User = settings.Credentials.Appsettings.User,
            //                Password = settings.Credentials.Appsettings.Pwd,
            //                Desencriptado = settings.Credentials.Appsettings.Desencriptado,
            //            };
            //            AppConfiguration.ConnectionString = SqlServerConnectionConstructor.Build(Credential);
            //            break;

            //        default: throw new Exception("Destino de conexió no reconocido: " + Destinocredential);
            //    }
            //}
            //if (settings.LOG is not null)
            //{
            //    var DESTINO = settings.LOG.Destino == 0 ? settings.LOG.DefaultLogs : settings.LOG.Destino;
            //    switch ((DestinosLog)DESTINO)
            //    {
            //        case DestinosLog.DataBase:

            //            #region GetConexionDatabaseLogs
            //            var DestinocredentialLog = settings.Credentials.Destino == 0 ? settings.Credentials.DefaultConexion : settings.Credentials.Destino;
            //            switch ((DestinoCadenaConexion)DestinocredentialLog)
            //            {
            //                case DestinoCadenaConexion.VariableLocal:

            //                    _ = new DatabaseCredential
            //                    {
            //                        Server = settings.Credentials.Appsettings.Server,
            //                        Database = configuration["Credentials:Appsettings:Database"],
            //                        User = configuration["Credentials:Appsettings:User"],
            //                        Password = configuration["Credentials:Appsettings:Pwd"],
            //                        //Encripter = configuration["Credentials:Appsettings:Encripter"]
            //                    };
            //                    break;
            //                case DestinoCadenaConexion.Appsettings:

            //                    Credential = new DatabaseCredential
            //                    {
            //                        Server = settings.LOG.DataBase.Credentials.Appsettings.Server,
            //                        Database = settings.LOG.DataBase.Credentials.Appsettings.Database,
            //                        User = settings.LOG.DataBase.Credentials.Appsettings.User,
            //                        Password = settings.LOG.DataBase.Credentials.Appsettings.Pwd,

            //                    };

            //                    AppConfiguration.ConnectionLogsString = SqlServerConnectionConstructor.Build(Credential);
            //                    break;

            //                default: throw new Exception("Destino de conexió no reconocido: " + DestinocredentialLog);
            //            }
            //            #endregion
            //            break;


            //        default: throw new Exception("Destino LOG no reconocido: " + settings.LOG.Destino);
            //    }

            //}

            //AppConfiguration.DefaultApiServer = configuration["Authorization:DefaultApiServer"];
            //AppConfiguration.CertificateName = configuration["Authorization:CertificateName"];



            //if (!(string.IsNullOrEmpty(configuration["Authorization:CertificatePassword"])))
            //    AppConfiguration.CertificatePassword = Cryptography.Decrypt(configuration["Authorization:CertificatePassword"]);










            ////Endpoint
            //AppConfiguration.EndpointApiWhatsappCloud = configuration["Endpoint:WhatsappService"];

            ////Headers
            //AppConfiguration.HeaderType = configuration["Oauth:Headers:HeaderType"];
            //AppConfiguration.HeaderCredential = configuration["Oauth:Headers:HeaderCredential"];
            //AppConfiguration.User = configuration["Oauth:Credentials:User"];
            //AppConfiguration.Password = configuration["Oauth:Credentials:Password"];
            //AppConfiguration.ApiOauth = configuration["Oauth:ApiOauth"];
            //AppConfiguration.AmbientePublicacion = Convert.ToBoolean(configuration["Ambiente:Production"]);


            //string environment = configuration["ASPNETCORE_ENVIRONMENT"];
            //if (environment == Environments.Development)
            //    AppConfiguration.Ambientes = Ambiente.Desarrollo;
            //else
            //    AppConfiguration.Ambientes = Ambiente.Produccion;


        }

    }
}
