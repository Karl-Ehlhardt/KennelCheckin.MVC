﻿using KennelData.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kennel.Models.Data.DisplayOnly
{
    public class DogInfoIndexView
    {
        //Dogs List
        public IEnumerable<KennelData.Data.DogBasic> DogBasic { get; set; }
        public KennelData.Data.Owner Owner { get; set; }
        public IEnumerable<KennelData.JoiningData.DogInfo> DogInfo { get; set; }

        public DogInfoIndexView(IEnumerable<KennelData.Data.DogBasic> dogBasic, KennelData.Data.Owner owner, IEnumerable<KennelData.JoiningData.DogInfo> dogInfo)
        {
            DogBasic = dogBasic;
            Owner = owner;
            DogInfo = dogInfo;
        }

    }
}
