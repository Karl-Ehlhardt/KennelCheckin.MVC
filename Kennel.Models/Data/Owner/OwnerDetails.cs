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
    public class OwnerDetails
    {
        public int OwnerId { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        [Display(Name = "Backup Name")]
        public string BackupName { get; set; }

        [Display(Name = "Backup Phone")]
        public string BackupPhone { get; set; }

        [Display(Name = "Backup Email")]
        public string BackupEmail { get; set; }
    }
}
