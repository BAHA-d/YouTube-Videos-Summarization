using System;
using System.Collections.Generic;

namespace VideoSummarization.Models
{
    public partial class Exam
    {
        public int Id { get; set; }
        public string Result { get; set; } = null!;
        public string Video { get; set; } = null!;
        public int UserId { get; set; }
        public DateTime DateTime { get; set; }
        public virtual User? User { get; set; } = null!;
    }
}
