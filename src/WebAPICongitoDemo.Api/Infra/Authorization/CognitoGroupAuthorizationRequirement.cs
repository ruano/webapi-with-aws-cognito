using Microsoft.AspNetCore.Authorization;

namespace WebAPICongitoDemo.Api.Infra.Authorization
{
    public class CognitoGroupAuthorizationRequirement : IAuthorizationRequirement
    {
        public string CognitoGroup { get; private set; }

        public CognitoGroupAuthorizationRequirement(string cognitoGroup)
        {
            CognitoGroup = cognitoGroup;
        }
    }
}
