using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Universite_Web.Models
{
    public class TeacherPanel
    {
        [Key]
        public int Id { get; set; }
        public DateTime DersDay { get; set; }
        public DateTime JurnalDay { get; set; }
       [ForeignKey("Dersnovu")]
        public int DersnovuId { get; set; }
        public Dersnovu Dersnovu { get; set; }
        public List<StudentPanel> StudentPanel { get; set; }
    }
}
