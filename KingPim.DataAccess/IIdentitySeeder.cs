using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace KingPim.DataAccess
{
    public interface IIdentitySeeder
    {
        Task<bool> CreateRolesAndAdminUserIfEmpty();
    }
}
