using AgileProject.Entidades;
using AgileProject.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace AgileProject.Services.Contratos
{
    public interface IClaimsServices
    {
        bool validClaims(List<System.Security.Claims.Claim> claims);
        bool isAdmin(List<System.Security.Claims.Claim> claims);
        AspNetUsersModel getUserClaims(List<System.Security.Claims.Claim> claims);
        List<UserCalendarType> getUserTeamsClaims(List<System.Security.Claims.Claim> claims);
        List<AspNetUserRoles> getUserRoleClaims(List<System.Security.Claims.Claim> claims);

    }

}
