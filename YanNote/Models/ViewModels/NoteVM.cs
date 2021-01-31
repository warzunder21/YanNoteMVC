using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace YanNote.Models.ViewModels
{
    public class NoteVM
    {
        public Note Note { get; set; }
        public IEnumerable<int> NoteTags { get; set; }
        public List<SelectListItem> TagSelectList { get; set; }
    }
}
