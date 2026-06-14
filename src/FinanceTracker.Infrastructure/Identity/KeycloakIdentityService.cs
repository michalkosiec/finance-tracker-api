using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using FinanceTracker.Application.Common.Interfaces;
using Microsoft.Extensions.Configuration;

namespace FinanceTracker.Infrastructure.Identity
{
    public class KeycloakIdentityService(HttpClient httpClient, IConfiguration config)
        : IIdentityService
    {
        public async Task<string?> RegisterUserAsync(string email, string password)
        {
            var adminToken = await GetAdminTokenAsync();
            if (string.IsNullOrEmpty(adminToken))
                return null;

            var realm = config["Keycloak:Realm"];
            var baseUrl = config["Keycloak:BaseUrl"];

            var userPayload = new
            {
                username = email,
                email,
                enabled = true,
                emailVerified = true,
            };

            var content = new StringContent(
                JsonSerializer.Serialize(userPayload),
                Encoding.UTF8,
                "application/json"
            );
            var createUserUrl = $"{baseUrl}/admin/realms/{realm}/users";

            using var createRequest = new HttpRequestMessage(HttpMethod.Post, createUserUrl);
            createRequest.Content = content;
            createRequest.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                adminToken
            );

            var response = await httpClient.SendAsync(createRequest);
            if (!response.IsSuccessStatusCode)
                return null;

            var getUserUrl = $"{baseUrl}/admin/realms/{realm}/users?email={email}";
            using var getRequest = new HttpRequestMessage(HttpMethod.Get, getUserUrl);
            getRequest.Headers.Authorization = new AuthenticationHeaderValue("Bearer", adminToken);

            var getResponse = await httpClient.SendAsync(getRequest);
            if (!getResponse.IsSuccessStatusCode)
                return null;

            var jsonString = await getResponse.Content.ReadAsStringAsync();
            using var jsonDocument = JsonDocument.Parse(jsonString);
            var usersArray = jsonDocument.RootElement;

            if (usersArray.GetArrayLength() == 0)
                return null;

            var userId = usersArray[0].GetProperty("id").GetString();

            var passwordPayload = new
            {
                type = "password",
                value = password,
                temporary = false,
            };

            var setPasswordUrl = $"{baseUrl}/admin/realms/{realm}/users/{userId}/reset-password";
            using var passwordRequest = new HttpRequestMessage(HttpMethod.Put, setPasswordUrl);
            passwordRequest.Content = new StringContent(
                JsonSerializer.Serialize(passwordPayload),
                Encoding.UTF8,
                "application/json"
            );
            passwordRequest.Headers.Authorization = new AuthenticationHeaderValue(
                "Bearer",
                adminToken
            );

            var passwordResponse = await httpClient.SendAsync(passwordRequest);

            if (!passwordResponse.IsSuccessStatusCode)
                return null;

            return userId;
        }

        async Task<string?> IIdentityService.LoginAsync(
            string email,
            string password,
            CancellationToken cancellationToken
        )
        {
            var baseUrl = config["Keycloak:BaseUrl"];
            var realm = config["Keycloak:Realm"];
            var tokenEndpoint = $"{baseUrl}/realms/{realm}/protocol/openid-connect/token";

            var requestedData = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("client_id", "finance-api"),
                new KeyValuePair<string, string>("client_secret", "finance-api-secret"),
                new KeyValuePair<string, string>("username", email),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("grant_type", "password"),
            ]);

            var response = await httpClient.PostAsync(
                tokenEndpoint,
                requestedData,
                cancellationToken
            );

            if (!response.IsSuccessStatusCode)
                return null;

            var jsonString = await response.Content.ReadAsStringAsync(cancellationToken);

            using var jsonDocument = JsonDocument.Parse(jsonString);
            return jsonDocument.RootElement.GetProperty("access_token").GetString();
        }

        private async Task<string?> GetAdminTokenAsync()
        {
            var baseUrl = config["Keycloak:BaseUrl"];

            var request = new FormUrlEncodedContent([
                new KeyValuePair<string, string>("client_id", "admin-cli"),
                new KeyValuePair<string, string>(
                    "username",
                    config["Keycloak:AdminUser"] ?? "admin"
                ),
                new KeyValuePair<string, string>(
                    "password",
                    config["Keycloak:AdminPassword"] ?? "admin"
                ),
                new KeyValuePair<string, string>("grant_type", "password"),
            ]);

            var response = await httpClient.PostAsync(
                $"{baseUrl}/realms/master/protocol/openid-connect/token",
                request
            );
            if (!response.IsSuccessStatusCode)
                return null;

            var jsonString = await response.Content.ReadAsStringAsync();

            using var jsonDocument = JsonDocument.Parse(jsonString);
            return jsonDocument.RootElement.GetProperty("access_token").GetString();
        }
    }
}
