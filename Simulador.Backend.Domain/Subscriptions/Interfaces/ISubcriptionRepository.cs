using Simulador.Backend.Domain.Auth.Domain;
using Simulador.Backend.Domain.Subscriptions.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Subscriptions.Interfaces
{
    public interface ISubcriptionRepository
    {
        Task<List<Subscription>> FindByUserId(Guid userId);
    }
}
