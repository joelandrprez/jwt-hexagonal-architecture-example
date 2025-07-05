using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Answer.Domain
{
    public class Answer
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public char Letter { get; set; }
        public bool Correct { get; set; }
        public int AskId { get; set; }  // FK
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public Task? Ask { get; set; }
    }
}
