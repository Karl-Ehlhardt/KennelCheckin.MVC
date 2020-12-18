using Kennel.Data.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Owner
{
    public class OwnerCreate
    {
        [Required]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [Display(Name = "Backup Name")]
        public string BackupName { get; set; }

        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Backup Phone")]
        public string BackupPhone { get; set; }

        [Required]
        [Display(Name = "Backup Email")]
        public string BackupEmail { get; set; }
    }
}
