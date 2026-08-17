using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    public GameObject inventoryPanel;
    public GameObject slotPrefab;
    public int slotCount;
    public GameObject[] itemPrefabs;

    private ItemDictionary itemDictionary;

    void Start()
    {
        itemDictionary = FindAnyObjectByType<ItemDictionary>();
 
        for(int i = 0; i< slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefab, inventoryPanel.transform).GetComponent<Slot>();
            if(i< itemPrefabs.Length)
            {
                GameObject item = Instantiate(itemPrefabs[i], slot.transform);
                item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = item;
            }    
                
        }
    }

    public List<InventorySaveData> GetInventoryItems()
    {
        List<InventorySaveData> invData = new List<InventorySaveData>();
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot.currentItem != null)
            {
                Item item = slot.currentItem.GetComponent<Item>();
                invData.Add(new InventorySaveData{itemId = item.ID, slotIndex = slotTransform.GetSiblingIndex()});
            }
        }
        return invData;
    }
    public void SetInventoryItems(List<InventorySaveData> invSaveData)
    {
        foreach(Transform child in inventoryPanel.transform)
        {
            Destroy(child.gameObject);
        }
        for(int i = 0; i < slotCount; i++)
        {
            Instantiate(slotPrefab, inventoryPanel.transform);
        }
        foreach(InventorySaveData data in invSaveData)
        {
            if(data.slotIndex < slotCount)
            {
                Slot slot = inventoryPanel.transform.GetChild(data.slotIndex).GetComponent<Slot>();
                GameObject itemPrefab = itemDictionary.GetItemPrefab(data.itemId);
                if (itemPrefab != null)
                {
                    GameObject item = Instantiate(itemPrefab, slot.transform);
                    item.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                    slot.currentItem = item;
                }
            }
        }

    }
    public bool AddItem(GameObject ItemPrefab)
    {
        foreach(Transform slotTransform in inventoryPanel.transform)
        {
            Slot slot = slotTransform.GetComponent<Slot>();
            if(slot != null && slot.currentItem == null)
            {
                GameObject newItem = Instantiate(ItemPrefab, slotTransform);
                newItem.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                slot.currentItem = newItem;
                return true;
            }
        }
     Debug.Log("inventory is full");
     return false;   
    }
}
