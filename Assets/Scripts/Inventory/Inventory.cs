using System.Collections.Generic;
using UnityEngine;
using System.Linq;
 
public class Inventory : MonoBehaviour
{
    [SerializeReference] public List<ItemSlotInfo> items = 
        new List<ItemSlotInfo>();

    [Space]
    [Header("Inventory Menu Components")]
    public GameObject inventoryMenu;
    public GameObject itemPanel;
    public GameObject itemPanelGrid;

    private List<ItemPanel> existingPanels = new List<ItemPanel>();

    [Space]
    public int inventorySize = 28;

    void Start()
    {
        for (int i = 0; i < inventorySize; i++)
        {
            items.Add(new ItemSlotInfo(null, 0));
        }

        // TESTING: add items
        AddItem(new StickItem(), 40);
        AddItem(new AppleItem(), 40);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) // TODO: map keybindings to a config file
        {
            if (inventoryMenu.activeSelf)
            {
                inventoryMenu.SetActive(false);
            }
            else
            {
                inventoryMenu.SetActive(true);
                RefreshInventory();
            }
        }
    }

    public void RefreshInventory()
    {
        existingPanels = 
            itemPanelGrid.GetComponentsInChildren<ItemPanel>().ToList();

        // ensure inventory contains the correct amount of panels
        if (existingPanels.Count < inventorySize)
        {
            int amountToCreate = inventorySize - existingPanels.Count;
            for (int i = 0; i < amountToCreate; i++)
            {
                GameObject newPanel = Instantiate(itemPanel, itemPanelGrid.transform);
                existingPanels.Add(newPanel.GetComponent<ItemPanel>());
            }
        }

        int index = 0;
        foreach (ItemSlotInfo i in items)
        {
            // name list elements
            i.name = "" + (index + 1);
            if (i.item != null) i.name += ": " + i.item.GiveName();
            else i.name += ": -";

            // update panels
            ItemPanel panel = existingPanels[index];
            if (panel != null)
            {
                panel.name = i.name + " Panel";
                panel.inventory = this;
                panel.itemSlot = i;

                if (i.item != null)
                {
                    panel.itemImage.gameObject.SetActive(true);
                    panel.itemImage.sprite = i.item.GiveItemImage();
                    panel.stacksText.gameObject.SetActive(true);
                    panel.stacksText.text = "" + i.stacks;
                }
                else
                {
                    panel.itemImage.gameObject.SetActive(false);
                    panel.stacksText.gameObject.SetActive(false);
                }
            }
            index++;
        }
    }

    public int AddItem(InventoryItem item, int amount)
    {
        // check if item is already in inventory
        foreach (ItemSlotInfo i in items)
        {
            if (i.item != null)
            {
                if (i.item.GiveName() == item.GiveName())
                {
                    // check if threre is enough space in current stack
                    if (amount > i.item.MaxStacks() - i.stacks)
                    {
                        amount -= i.item.MaxStacks() - i.stacks;
                        i.stacks = i.item.MaxStacks();
                    }
                    else
                    {
                        i.stacks += amount;
                        if (inventoryMenu.activeSelf) RefreshInventory();
                        return 0;
                    }
                }
            }
        }
        
        // fill empty slots with remainder
        foreach (ItemSlotInfo i in items)
        {
            if (i.item == null)
            {
                // create new stacks
                if (amount > item.MaxStacks())
                {
                    i.item = item;
                    i.stacks = item.MaxStacks();
                    amount -= item.MaxStacks();
                }
                else
                {
                    i.item = item;
                    i.stacks = amount;
                    if (inventoryMenu.activeSelf) RefreshInventory();
                    return 0;
                }
            }
        }

        // inventory is full
        Debug.Log("No space in inventory for: " + item.GiveName());
        if (inventoryMenu.activeSelf) RefreshInventory();
        return amount;
    }

    public void ClearSlot(ItemSlotInfo slot)
    {
        slot.item = null;
        slot.stacks = 0;
    }
}