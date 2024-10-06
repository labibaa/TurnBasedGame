using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public ItemClass itemToAdd;
    public ItemClass itemToRemove;
    public List<ItemClass> InventoryObjects = new List<ItemClass>();
    private Dictionary<ItemClass,InventoryItem> itemDictionary = new Dictionary<ItemClass,InventoryItem>();

    private void Start()
    {
        AddItem(itemToAdd);
    }

    public void AddItem(ItemClass item)
    {
        if (itemDictionary.TryGetValue(item, out InventoryItem inventoryItem) )
        {
            inventoryItem.AddToStack();  
        }
        else
        {
            InventoryItem newInventoryItem = new InventoryItem(item);
            InventoryObjects.Add(item);
            itemDictionary.Add(item,newInventoryItem);
        }
       
    }
    public void RemoveItem(ItemClass item)
    {
       if(itemDictionary.TryGetValue(item,out InventoryItem inventoryItem) )
        {
            inventoryItem.RemoveFromStack();
            if(inventoryItem.StackSize == 0)
            {
                InventoryObjects.Remove(item);
                itemDictionary.Remove(item);
            }
            
        }
    }
}
