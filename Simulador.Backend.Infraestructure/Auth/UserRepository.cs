using Azure;
using Dapper;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using Simulador.Backend.Domain.Auth.Domain;
using Simulador.Backend.Domain.Auth.Interfaces;
using Simulador.Backend.Domain.Config.Dto;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Infraestructure.Auth
{
    public class UserRepository : IUserRepository
    {
        private readonly DatabaseConfig _databaseConfig;

        public UserRepository(DatabaseConfig databaseConfig)
        {
            _databaseConfig = databaseConfig;
        }

        public async Task<User?> FindByEmail(string email)
        {
            User? users = null;
            using (IDbConnection con = new SqlConnection(this._databaseConfig.SqlServerConnection))
            {
                users = await con.QueryFirstOrDefaultAsync<User>("dbo.sp_Find_Users_By_Email", new
                {
                    email
                }, commandType: CommandType.StoredProcedure);
            }
            return users;
        }

        public async Task Save(User user)
        {
            User? response = new User();
            using (IDbConnection con = new SqlConnection(this._databaseConfig.SqlServerConnection))
            {
                response = await con.QueryFirstOrDefaultAsync<User>("dbo.sp_Insert_User", new {
                     user.Id,
                     user.FirstName,
                     user.LastName,
                     user.Email,
                     user.Password,
                     user.PhoneNumber,
                     user.Address,
                     user.DateOfBirth,
                     user.IsActive,
                     user.LoginAttempts,
                     user.CreatedBy
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task SaveSession(Session session)
        {
            using (IDbConnection con = new SqlConnection(this._databaseConfig.SqlServerConnection))
            {
                await con.QueryAsync("dbo.sp_Insert_Session", new
                {
                    session.IpSession,
                    session.Token,
                    session.IssueDate,
                    session.ExpirationDate,
                    session.IsEnabled,
                    session.UsersId,
                    session.CreatedAt,
                    session.CreatedBy
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task UpdateLoginAttempts(int attempts, string id)
        {
            using (IDbConnection con = new SqlConnection(this._databaseConfig.SqlServerConnection))
            {
                await con.QueryFirstOrDefaultAsync<User>("dbo.sp_Update_Attempts_Users_By_Id", new
                {
                    id,
                    attempts
                }, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task DisableActiveSession(Guid usersId,bool isEnabled)
        {
            using (IDbConnection con = new SqlConnection(this._databaseConfig.SqlServerConnection))
            {
                await con.QueryFirstOrDefaultAsync<User>("dbo.sp_Update_Enabled_Session", new
                {
                    usersId,
                    isEnabled
                }, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
