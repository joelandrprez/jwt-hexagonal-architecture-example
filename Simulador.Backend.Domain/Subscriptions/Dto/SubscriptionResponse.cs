using Simulador.Backend.Domain.Auth.Domain;
using Simulador.Backend.Domain.Auth.Dto;
using Simulador.Backend.Domain.Courses.Domain;
using Simulador.Backend.Domain.Courses.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Subscriptions.Dto
{
    public class SubscriptionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public int CourseId { get; set; }
        public int StatusId { get; set; }
        public Guid UserId { get; set; }
        public CourseResponse? Course { get; set; }
        public UserResponse? User { get; set; }
        public StatusSubscriptionResponse? Status { get; set; }
    }
}
