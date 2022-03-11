using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Universite_Web.Models
{
    public class StudentPanel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(500)]
        public string Content { get; set; }
         
        [ForeignKey("TeacherPanel")]
        public int TeacherPanelId { get; set; }
        public TeacherPanel TeacherPanel { get; set; }

    }
}
