﻿using Kennel.Data.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.Owner
{
    public class OwnerEdit
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string BackupName { get; set; }

        [Required]
        public string BackupPhone { get; set; }

        [Required]
        public string BackupEmail { get; set; }
    }
}
