using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdocaoPetApi.Models;

namespace AdocaoPetApi.Extensions
{
    public static class RoleClaimExtension
    {
        public static IEnumerable<Claim> GetClaim (this Usuario usuario)
        {
            var result = new List<Claim>
            {
                new (ClaimTypes.Name, usuario.Nome),
                new (ClaimTypes.Email, usuario.Email)
            };
            result.AddRange(
                usuario.Roles.Select(role => new Claim(ClaimTypes.Role, role.Nome))
            );

            return result;
        }
    }
}