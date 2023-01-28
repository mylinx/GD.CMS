using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using GD.CMS.Common;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;

namespace GD.CMS.WebApi
{
    public class JwtService : IJwtService
    {
        public string DeclearToken(string token)
        {
            Console.WriteLine(token);
            return null;
        }

        /// <summary>
        /// 创建双token
        /// </summary>
        /// <param name="model">用户信息</param>
        /// <returns></returns>
        public JwtTokenModel GetToken(UsersIndentityModel model)
        {
            return new JwtTokenModel
            {
                access_token = CreateToken(model, JwtTokenEnum.AccessToken),
                refresh_token = CreateToken(model, JwtTokenEnum.RefreshToken)
            };
        }




        public string RefreshToken()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// 生成token
        /// </summary>
        /// <param name="model"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        private string CreateToken(UsersIndentityModel model, JwtTokenEnum token) 
        {
            //定义发行人issuer
            string iss = ConfigHelper.GetValue("JwtBear:Issurer");
            //定义受众人audience
            string aud = token.Equals(JwtTokenEnum.AccessToken)? ConfigHelper.GetValue("JwtBear:Audience") : ConfigHelper.GetValue("RefreshToken:RefreshAudience");

            //定义许多种的声明Claim,信息存储部分,Claims的实体一般包含用户和一些元数据
            IEnumerable<Claim> claims = new Claim[]
            {
                    new Claim(JwtClaimTypes.Id,model.ID),
                    new Claim(JwtClaimTypes.Name,model.Account),
                    new Claim(JwtClaimTypes.Role,model.Roles),
                    //new Claim("Permission",model.)
                    new Claim("isAdmin",model.IsAdmin.ToString())
            };
            //notBefore  生效时间
            var nbf = DateTime.UtcNow;

            var dtnow = DateTime.Now;
            //expires   //过期时间
            var Expire = token.Equals(JwtTokenEnum.AccessToken) ? dtnow.AddHours(ConfigHelper.GetValue("JwtBear:ExpireHours").ToInt())
                : dtnow.AddDays(ConfigHelper.GetValue("RefreshToken:RefreshTokenExpiresDay").ToInt());


            //signingCredentials  签名凭证
            string sign = ConfigHelper.GetValue("JwtBear:secretCredentials"); //SecurityKey 的长度必须 大于等于 16个字符
            var secret = Encoding.UTF8.GetBytes(sign);
            var key = new SymmetricSecurityKey(secret);
            var signcreds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var jwt = new JwtSecurityToken(issuer: iss, audience: aud, claims: claims, notBefore: nbf, expires: Expire, signingCredentials: signcreds);
            var JwtHander = new JwtSecurityTokenHandler();
            return JwtHander.WriteToken(jwt);
        }
    }
}
