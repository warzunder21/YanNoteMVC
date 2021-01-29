using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace YanNote.Models
{
    public class Rem
    {
        [Key]
        public int Id { get; set; }
        
        [Required]
        public DateTime EventDate { get; set; }

        public int NoteId { get; set; }
        [ForeignKey("NoteId")]
        public Note Note { get; set; }
    }
}
