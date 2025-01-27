using System;

namespace Core.Inventory
{
    [Serializable]
    public class SlotDataArray
    {
        public SlotData[] Slots;

        public SlotDataArray(SlotData[] slots)
        {
            Slots = slots;
        }
    }
}
