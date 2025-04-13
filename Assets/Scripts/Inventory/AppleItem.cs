using UnityEngine;

public class AppleItem : InventoryItem
{
    public override string GiveName()
    {
        return "Apple";
    }

    public override int MaxStacks()
    {
        return 20;
    }

    public override Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/apple");
    }
}
