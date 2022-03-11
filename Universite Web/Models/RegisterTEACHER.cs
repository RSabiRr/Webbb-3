using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Universite_Web.Models
{
    public class RegisterTEACHER
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(250)]
        public string Image { get; set; }
        [NotMapped]

        public IFormFile ImageFile { get; set; }
        [MaxLength(250)]
        public string Surname { get; set; }
        [MaxLength(250)]
        public string Name { get; set; }
        [MaxLength(250)]
        public string AtaAdi { get; set; }
        public int PassportNumber { get; set; }
        public string Gender { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(150)]
        public string Adress { get; set; }
        public DateTime DateOfBirth { get; set; }
        [ForeignKey("Subject")]
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
