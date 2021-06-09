using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using ConsumerApi.Client;
using ConsumerApi.Contracts;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace ConsumerApi.HealthCheck
{
    /// <summary>
    /// Testing health of the API
    /// </summary>
    public class UserApiHealthcheck : IHealthCheck
    {
        private readonly IUserClient _userClient;
        private const string UserApiHealthyText = "User API is healthy";
        private const string UserApiUnhealthyText = "User API is unhealthy. Reason: ";

        /// <inheritdoc cref="IHealthCheck"/>
        public UserApiHealthcheck(IUserClient userClient)
        {
            _userClient = userClient;
        }

        /// <summary>
        /// Runs the health check, returning the status of the component being checked.
        /// </summary>
        /// <param name="context">A context object associated with the current execution.</param>
        /// <param name="cancellationToken">A <see cref="T:System.Threading.CancellationToken" /> that can be used to cancel the health check.</param>
        /// <returns>A <see cref="T:System.Threading.Tasks.Task`1" /> that completes when the health check has finished, yielding the status of the component being checked.</returns>
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new())
        {
            try
            {
                await _userClient.HealthCheck(cancellationToken);
                return HealthCheckResult.Healthy(UserApiHealthyText);
            }
            catch (HttpRequestException)
            {
                return HealthCheckResult.Unhealthy("User API is not healthy");
            }

            //// await CheckHealthViaRegularEndpoints(cancellationToken);
        }

        private async Task<HealthCheckResult> CheckHealthViaRegularEndpoints(CancellationToken cancellationToken)
        {
            try
            {
                await _userClient.Login(new UserModel
                {
                    Login = "consumerApiUser",
                    Password = "consumerApiPassword",
                }, cancellationToken);
                return HealthCheckResult.Healthy(UserApiHealthyText);
            }
            catch (HttpRequestException httpRequestException)
            {
                var unAuthorized = httpRequestException.StatusCode == HttpStatusCode.Unauthorized;
                var badRequest = httpRequestException.StatusCode == HttpStatusCode.BadRequest;
                var notFound = httpRequestException.StatusCode == HttpStatusCode.NotFound;
                var forbidden = httpRequestException.StatusCode == HttpStatusCode.Forbidden;
                if (unAuthorized || badRequest || notFound || forbidden)
                {
                    return HealthCheckResult.Healthy(UserApiHealthyText);
                }

                return HealthCheckResult.Unhealthy(UserApiUnhealthyText + httpRequestException.Message);
            }
        }
    }
}
