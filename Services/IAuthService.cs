using System.Threading.Tasks;
using chatApplication.Models;
using chatApplication.ViewModels;

namespace chatApplication.Services
{
    public interface SigInManager
    {
        Task<AuthModel> RegisterAsync(VM_CreateUser user,string roleName);
        Task<AuthModel> GetTokenAsync(TokenRequestModel model);
        Task<string> AddRoleAsync(AddRoleModel model);
        //Task SininAsync(string PhoneNumber);
        //Task SinoutAsync();
    }
}