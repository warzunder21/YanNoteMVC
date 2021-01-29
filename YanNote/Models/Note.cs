using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace YanNote.Models
{
    public class Note
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Text { get; set; }

        public List<Tag> Tags { get; set; } = new List<Tag>();

        public DateTime NoteDate { get; set; }
        public Rem Rem { get; set; }

        public Note()
        {
            NoteDate = DateTime.Now;
        }
    }
}
