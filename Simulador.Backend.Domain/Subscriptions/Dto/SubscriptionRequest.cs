using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Subscriptions.Dto
{
    public class SubscriptionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public DateTime? ExpirationDate { get; set; }
        public DateTime SubscriptionDate { get; set; }
        public int CourseId { get; set; }
        public int StatusId { get; set; }
        public Guid UserId { get; set; }
    }
}
