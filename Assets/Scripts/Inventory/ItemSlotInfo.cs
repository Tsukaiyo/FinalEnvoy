[System.Serializable]
public class ItemSlotInfo
{
    public InventoryItem item;
    public string name;
    public int stacks;

    public ItemSlotInfo(InventoryItem newItem, int newStacks)
    {
        item = newItem;
        stacks = newStacks;
    }
}
