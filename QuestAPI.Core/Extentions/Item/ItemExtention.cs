using QuestAPI.Core.Data.Models.Item;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuestAPI.Core.Extentions.Item
{
    public static class ItemExtention
    {
        public static void Update(this ItemEntry item, ItemUpdateModel updateModel)
        {
            item.Description = updateModel.Description != null ? updateModel.Description : item.Description;
            item.Name = updateModel.Name !=null ? updateModel.Name : item.Name;
        }
    }
}
