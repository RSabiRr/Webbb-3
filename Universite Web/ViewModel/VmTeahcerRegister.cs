using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Universite_Web.Models;

namespace Universite_Web.ViewModel
{
    public class VmTeahcerRegister
    {
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Surname { get; set; }
        [MaxLength(50)]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [MaxLength(30)]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [MaxLength(30)]
        [DataType(DataType.Password)]
        [Compare(nameof(Password))]
        public string RepaetPassword { get; set; }
        [MaxLength(250)]
        public string AtaAdi { get; set; }
        public int PassportNumber { get; set; }
        public string Gender { get; set; }
        [MaxLength(20)]
        public string Phone { get; set; }
        [MaxLength(150)]
        public string Adress { get; set; }
        public DateTime DateOfBirth { get; set; }

        public RegisterTEACHER RegisterTEACHER { get; set; }
    }
}
