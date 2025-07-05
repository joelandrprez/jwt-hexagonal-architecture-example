using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simulador.Backend.Domain.Courses.Dto
{
    public class CourseResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public int EntityId { get; set; }
        public int QuestionCount { get; set; }
        public int TimeMinute { get; set; }
    }
}
