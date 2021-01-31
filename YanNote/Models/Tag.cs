using System;
using System.Collections.Generic;

#nullable disable

namespace YanNote.Models
{
    public partial class Tag
    {
        public Tag()
        {
            NoteTags = new HashSet<NoteTag>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NoteTag> NoteTags { get; set; }
    }
}
