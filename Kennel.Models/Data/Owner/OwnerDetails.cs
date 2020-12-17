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
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string BackupName { get; set; }

        public string BackupPhone { get; set; }

        public string BackupEmail { get; set; }
    }
}
