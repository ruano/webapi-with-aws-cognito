﻿using Microsoft.AspNetCore.Authorization;

namespace WebAPICongitoDemo.Api.Infra.Authorization
{
    public class CognitoGroupAuthorizationHandler : AuthorizationHandler<CognitoGroupAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CognitoGroupAuthorizationRequirement requirement)
        {
            if (!context.User.HasClaim(c => c.Type == "cognito:groups"))
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var group = context.User.FindFirst(c => c.Type == "cognito:groups").Value;

            if (group == requirement.CognitoGroup)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }

            return Task.CompletedTask;
        }
    }
}
