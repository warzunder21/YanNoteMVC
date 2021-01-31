using System;
using System.Collections.Generic;

#nullable disable

namespace YanNote.Models
{
    public partial class NoteTag
    {
        public int NotesId { get; set; }
        public int TagsId { get; set; }

        public virtual Note Notes { get; set; }
        public virtual Tag Tags { get; set; }
    }
}
