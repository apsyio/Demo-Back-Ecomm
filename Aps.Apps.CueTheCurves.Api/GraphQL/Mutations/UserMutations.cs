using Appstagram.Base.Enums;
using Appstagram.Base.Generics.Responses;
using Aps.Apps.CueTheCurves.Api.Models.Entities;
using Aps.Apps.CueTheCurves.Api.Models.Inputs;
using Aps.Apps.CueTheCurves.Api.Services.Contracts;
using HotChocolate;
using HotChocolate.Types;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Aps.Apps.CueTheCurves.Api.GraphQL.Mutations
{
    [ExtendObjectType(typeof(Mutation))]
    public class UserMutations
    {
        [GraphQLName("user_signUp")]
        public ResponseBase<Users> Signup(ClaimsPrincipal claims, [Service] IUserService userService)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Users>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            if(JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user")) != null)
            {
                return ResponseBase<Users>.Failure(ResponseStatus.ALREADY_EXIST);
            }

            string email = JsonConvert.DeserializeObject<dynamic>(claims.FindFirstValue("firebase")).identities.email[0];
            return userService.Add(new Users { Email = email, ExternalId = claims.FindFirstValue("user_id") });
        }

        [GraphQLName("user_setStyles")]
        public ResponseBase SetStyles(ClaimsPrincipal claims, [Service] IUserService userService, List<int> styleIds)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            return userService.SetStyles(styleIds, user);
        }

        [GraphQLName("user_setBrands")]
        public ResponseBase SetBrands(ClaimsPrincipal claims, [Service] IUserService userService, List<int> brandIds)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);

            return userService.SetBrands(brandIds, user);
        }

        [GraphQLName("user_updateUser")]
        public ResponseBase<Users> SetBrands(ClaimsPrincipal claims, [Service] IUserService userService, UserInput input)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase<Users>.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase<Users>.Failure(ResponseStatus.USER_NOT_FOUND);
            input.Id = user.Id;
            return userService.Update(input);
        }

        [GraphQLName("user_deactive")]
        public ResponseBase DeactiveUser(ClaimsPrincipal claims, [Service] IUserService userService)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            return userService.DeactiveUser(user);
        }

        [GraphQLName("user_activeUserAdmin")]
        public ResponseBase ActiveUserAdmin(ClaimsPrincipal claims, [Service] IUserService userService, int userId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            return userService.ActiveUserAdmin(userId);
        }

        [GraphQLName("user_deactiveUserAdmin")]
        public ResponseBase DeactiveUserAdmin(ClaimsPrincipal claims, [Service] IUserService userService, int userId)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);

            return userService.DeactiveUserAdmin(userId);
        }

        [GraphQLName("user_setSelectedInpos")]
        public ResponseBase SetSelectedInspos(ClaimsPrincipal claims, [Service] IUserService userService, List<int> userIds)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            if (!user.IsAdmin) return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);

            return userService.SetSelectedInspos(userIds);
        }

        [GraphQLName("user_support")]
        public ResponseBase Support(ClaimsPrincipal claims, EmailInput email, [Service] IConfiguration configuration)
        {
            if (!claims.Identity.IsAuthenticated) return ResponseBase.Failure(ResponseStatus.AUTHENTICATION_FAILED);
            var user = JsonConvert.DeserializeObject<Users>(claims.FindFirstValue("user"));
            if (user == null) return ResponseBase.Failure(ResponseStatus.USER_NOT_FOUND);
            try
            {
                var sendgridConfig = configuration.GetSection("SendGridConfig").Get<SendGridConfig>();
                var client = new SendGridClient(sendgridConfig.ApiKey);
                var from = new EmailAddress(sendgridConfig.EmailFrom, "");
                var to = new EmailAddress(sendgridConfig.EmailTo, "");
                var msg = MailHelper.CreateSingleEmail(from, to, email.Subject, email.PlainTextContent, email.HtmlContent);
                var response = client.SendEmailAsync(msg).ConfigureAwait(false).GetAwaiter().GetResult();

                if (response.IsSuccessStatusCode)
                    return ResponseBase.Success();
                else
                    return ResponseBase.Failure(ResponseStatus.NOT_FOUND);
            }
            catch (Exception)
            {
                return ResponseBase.Failure(ResponseStatus.NOT_ALLOWED);
            }
        }

        class SendGridConfig
        {
            public string ApiKey { get; set; }
            public string EmailFrom { get; set; }
            public string EmailTo { get; set; }
        }
    }
}
