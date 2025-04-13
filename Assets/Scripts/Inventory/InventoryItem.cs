using UnityEngine;

[System.Serializable]
public abstract class InventoryItem
{
    public abstract string GiveName();
    public virtual int MaxStacks()
    {
        return 30;
    }
    public virtual Sprite GiveItemImage()
    {
        return Resources.Load<Sprite>("UI/Item Images/unknown");
    }
}