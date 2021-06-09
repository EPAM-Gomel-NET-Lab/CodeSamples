using RestEase;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace WebUI.Client
{
    public interface IUserRestClient
    {
        [Get("/health/live")]
        public Task HealthCheck(CancellationToken cancellationToken = default);

        [Post("users/register")]
        public Task<string> Register([Body] HttpContent content, CancellationToken cancellationToken = default);

        [Post("users/login")]
        public Task<string> Login([Body] HttpContent content, CancellationToken cancellationToken = default);

        [Get("users/validate")]
        public Task ValidateToken([Query]string token, CancellationToken cancellationToken = default);
    }

    public static class UserClientExtensions
    {
        public static async Task<string> Register(this IUserRestClient userClient, UserModel userModel, CancellationToken cancellationToken = default)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(userModel.Login), nameof(UserModel.Login) },
                { new StringContent(userModel.Password), nameof(UserModel.Password) }
            };
            var result = await userClient.Register(form, cancellationToken);
            return result;
        }

        public static async Task<string> Login(this IUserRestClient userClient, UserModel userModel, CancellationToken cancellationToken = default)
        {
            var form = new MultipartFormDataContent
            {
                { new StringContent(userModel.Login), nameof(UserModel.Login) },
                { new StringContent(userModel.Password), nameof(UserModel.Password) }
            };
            var result = await userClient.Login(form, cancellationToken);
            return result;
        }
    }
}
