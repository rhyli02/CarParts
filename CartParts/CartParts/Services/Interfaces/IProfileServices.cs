using CartParts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartParts.Services.Interfaces
{
    public interface IProfileServices
    {

        Task<Profile> GetProfileById(int id);
        Task<IEnumerable<Profile>> GetAllProfiles();
    }
}
