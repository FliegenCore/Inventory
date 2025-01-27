using System;
using System.Collections.Generic;

namespace Core.Inventory
{
    [Serializable]
    public class Data
    {
        public List<ItemWrapper> ItemsWrapper = new List<ItemWrapper>();
    }
}
