using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ParityService.Models.View
{
    public sealed class QuestradeLinkViewModel
    {
        [Required]
        public string RefreshToken { get; set; }

        public bool IsPractice { get; set; }
    }
}