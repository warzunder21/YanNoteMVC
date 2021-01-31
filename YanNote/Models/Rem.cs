using System;
using System.Collections.Generic;

#nullable disable

namespace YanNote.Models
{
    public partial class Rem
    {
        public int Id { get; set; }
        public DateTime EventDate { get; set; }
        public int NoteId { get; set; }

        public virtual Note Note { get; set; }
    }
}
