using Simulador.Backend.Domain.Auth.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Auth.Dto
{
    public static class UserMapper
    {
        public static UserResponse ToDto(User user)
        {
            if (user == null) return null;

            return new UserResponse
            {
                Id = user.Id,
                FullName = $"{user.FirstName} {user.LastName}",
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Address = user.Address,
                IsActive = user.IsActive,
            };
        }

        public static User ToDomain(UserRequest userRequest)
        {
            return new User
            {
                Id = userRequest.Id,
                FirstName = userRequest.FirstName,
                LastName = userRequest.LastName,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
                Address = userRequest.Address,
                IsActive = userRequest.IsActive,
                Password = userRequest.Password
            };
        }
    }
}
