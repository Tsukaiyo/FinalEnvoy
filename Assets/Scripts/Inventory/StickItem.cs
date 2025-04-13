using UnityEngine;

public class StickItem : InventoryItem
{
    public override string GiveName()
    {
        return "Stick";
    }

    public override int MaxStacks()
    {
        return 10;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/stick");
    }
}
