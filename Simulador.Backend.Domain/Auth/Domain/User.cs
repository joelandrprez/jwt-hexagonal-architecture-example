using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Auth.Domain
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } 
        public string LastName { get; set; }    
        public string Email { get; set; }           
        public string Password { get; set; }          
        public string PhoneNumber { get; set; } 
        public string Address { get; set; }      
        public DateTime DateOfBirth { get; set; }    
        public bool IsActive { get; set; } = true;
        public int LoginAttempts { get; set; }  
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }  
        public DateTime UpdatedAt { get; set; }  
        public string UpdatedBy { get; set; }  
        public DateTime DeletedAt { get; set; }  
        public string DeletedBy { get; set; }
        public List<Rol> Roles { get; set; }
    }

}
