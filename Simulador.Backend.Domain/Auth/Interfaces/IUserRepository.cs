using Simulador.Backend.Domain.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Auth.Interfaces
{
    public interface IUserRepository
    {
        Task Save(User user);
        Task<User?> FindByEmail(string email);
        Task UpdateLoginAttempts(int attempts, string id);
        Task SaveSession(Session session);
        Task DisableActiveSession(Guid usersId, bool isEnabled);
    }
}
