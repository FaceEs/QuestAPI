﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Data.Models.Item
{
    public class ItemEntry : BaseEntry
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ItemEntry() { }
    }
}