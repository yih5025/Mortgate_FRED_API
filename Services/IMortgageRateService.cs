using System.Threading.Tasks;

namespace MortgageWebProject.Services
{
    public interface IMortgageRateService
    {
        Task<double> GetLatestMortgageRateAsync();
    }
}
