using Dapper;
using Microsoft.Data.SqlClient;
using Simulador.Backend.Domain.Auth.Domain;
using Simulador.Backend.Domain.Config.Dto;
using Simulador.Backend.Domain.Courses.Domain;
using Simulador.Backend.Domain.Subscriptions.Domain;
using Simulador.Backend.Domain.Subscriptions.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Infraestructure.Subscriptions
{
    public class SubcriptionRepository : ISubcriptionRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public SubcriptionRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<List<Subscription>> FindByUserId(Guid userId)
        {
            List<Subscription> listado = new List<Subscription>();
            using (IDbConnection con = new SqlConnection(this._databaseConfig.SqlServerConnection))
            {
                listado = (await con.QueryAsync<Subscription, StatusSubscription,Course, Subscription>("dbo.sp_Find_Subscriptions_By_UserId",
                (subscription, status, course) =>
                {
                    subscription.StatusSubscription = status;
                    subscription.Course = course;
                    return subscription;
                }, new
                {
                    userId
                },
                splitOn: "Id", 
                commandType: CommandType.StoredProcedure)).ToList();
            }
            return listado;
        }
    }
}
