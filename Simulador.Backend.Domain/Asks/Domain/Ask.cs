﻿using Simulador.Backend.Domain.Issues.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Asks.Domain
{
    public class Ask
    {
        public int Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public int IssueId { get; set; }
        public bool State { get; set; }
        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime? UpdatedAt { get; set; }
        public string? UpdatedBy { get; set; }
        public DateTime? DeletedAt { get; set; }
        public string? DeletedBy { get; set; }
        public Issue? Issue { get; set; }
    }
}
