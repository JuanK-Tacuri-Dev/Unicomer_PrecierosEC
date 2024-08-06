namespace PrecierosEC.Core.Interface.Service
{
    public interface IServiceErrorLog
    {
        string SaveErrorlog(Exception ex);
    }
}
