using System;
using System.Collections.Generic;

namespace BYU_EGYPT_INTEX.Models
{
    public partial class Masterfilter
	{
        public long Id { get; set; }
        public string? Burialid { get; set; }
        public string? Depth { get; set; }
        public string? Ageatdeath { get; set; }
        public string? Sex { get; set; }
        public string? Haircolor { get; set; }
        public string? Headdirection { get; set; }
        public int? Estimatestature { get; set; }
        public string? Color { get; set; }
        public string? TextileStructure { get; set; }
        public string? Textilefunction { get; set; }
    }
}