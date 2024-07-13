using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace backend_api.Extensions
{
    public static class ClaimsExtensions
    {
        // 在你的代码中，Type 被设置为 "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"，这是一个标准的 URI，表示用户的名字。这些 URI 是微软和其他组织在定义身份声明标准时使用的。常见的声明类型包括：

        // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname：表示用户的名字（Given Name）。
        // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname：表示用户的姓氏（Surname）。
        // http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress：表示用户的电子邮件地址（Email Address）。

        public static string GetUsername(this ClaimsPrincipal user)
        {
            var claim = user.Claims.SingleOrDefault(x => x.Type.Equals("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname"));
            return claim?.Value ?? string.Empty; // 如果找不到声明，返回空字符串
        }

    }
}