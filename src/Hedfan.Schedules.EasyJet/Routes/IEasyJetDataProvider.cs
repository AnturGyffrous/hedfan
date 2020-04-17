using System.Threading.Tasks;

namespace Hedfan.Schedules.Routes
{
    public interface IEasyJetDataProvider
    {
        Task<Route> GetRoutesAsync();
    }
}