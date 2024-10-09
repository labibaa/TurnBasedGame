using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    public TemporaryStats playerCharacter;

    public List<InventoryItem> InventoryObjects = new List<InventoryItem>();
    private Dictionary<ItemClass,InventoryItem> itemDictionary = new Dictionary<ItemClass,InventoryItem>();

    private void OnEnable()
    {
        CurrencySystem.OnItemAdded += AddItem;
        CurrencySystem.OnItemRemoved += RemoveItem;
    }
    private void OnDisable()
    {
        CurrencySystem.OnItemAdded -= AddItem;
        CurrencySystem.OnItemRemoved -= RemoveItem;
    }
    private void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    public void AddItem(ItemClass item)
    {
        if (itemDictionary.TryGetValue(item, out InventoryItem inventoryItem) )
        {
            inventoryItem.AddToStack();
            Debug.Log(item + "Added to stack");
        }
        else
        {
            InventoryItem newInventoryItem = new InventoryItem(item);
            InventoryObjects.Add(newInventoryItem);
            itemDictionary.Add(item,newInventoryItem);
            Debug.Log(item + "Added to inventory");
        }
       
    }
    public void RemoveItem(ItemClass item)
    {
       if(itemDictionary.TryGetValue(item,out InventoryItem inventoryItem) )
        {
            item.UseObject(playerCharacter);
            inventoryItem.RemoveFromStack();
            if(inventoryItem.StackSize == 0)
            {
                InventoryObjects.Remove(inventoryItem);
                itemDictionary.Remove(item);
            }
            
        }
    }

    public void UseItem(ItemClass item)
    {
        if (itemDictionary.TryGetValue(item, out InventoryItem inventoryItem))
        {
            item.UseObject(playerCharacter);
            inventoryItem.RemoveFromStack();
            if (inventoryItem.StackSize == 0)
            {
                InventoryObjects.Remove(inventoryItem);
                itemDictionary.Remove(item);
            }

        }
    }
}
