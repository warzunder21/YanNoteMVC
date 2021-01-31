using System;
using System.Collections.Generic;

#nullable disable

namespace YanNote.Models
{
    public partial class Note
    {
        public Note()
        {
            NoteTags = new HashSet<NoteTag>();
            NoteDate = DateTime.Now;
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime NoteDate { get; set; }

        public virtual Rem Rem { get; set; }
        public virtual ICollection<NoteTag> NoteTags { get; set; }
        
    }
}
