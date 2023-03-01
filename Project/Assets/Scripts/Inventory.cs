using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Item
{
    E_WOOD = 1,
    E_GEM = 2,
    E_EMPTY = 3
}

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private Sprite m_WoodSprite;

    [SerializeField]
    private Sprite m_GemSprite;

    [SerializeField]
    private Sprite m_EmptySprite;

    [SerializeField]
    private List<GameObject> m_InventorySlots;
    void Start()
    {
        EventManager.StartListening("Wood_Completed", OnWoodCompleted);
        EventManager.StartListening("Mine_Completed", OnMineCompleted);
        EventManager.StartListening("ReinitializeInventory", ReinitializeInventory);
        EventManager.StartListening("LoadInventory", OnLoadInventory);
    }

    private void OnWoodCompleted(Dictionary<string, object> param)
    {
        object hasWin = false;
        if(param.TryGetValue("hasWin", out hasWin))
        {
            bool hasWinBool = (bool)hasWin;

            if(hasWinBool)
            {
                AddItemToInventory(Item.E_WOOD);
                SaveSystem.SaveInventory(this);
            }
        }
    }

    private void OnMineCompleted(Dictionary<string, object> param)
    {
        object hasWin = false;
        if (param.TryGetValue("hasWin", out hasWin))
        {
            bool hasWinBool = (bool)hasWin;

            if (hasWinBool)
            {
                AddItemToInventory(Item.E_GEM);
                SaveSystem.SaveInventory(this);
            }
        }
    }

    private void AddItemToInventory(Item item)
    {
        switch(item)
        {
            case Item.E_WOOD:
                for (int i = 0; i < m_InventorySlots.Count; i++)
                {
                    Sprite slotSprite = m_InventorySlots[i].GetComponent<Image>().sprite;
                    if (slotSprite == m_EmptySprite)
                    {
                        m_InventorySlots[i].GetComponent<Image>().sprite = m_WoodSprite;
                        if (i == m_InventorySlots.Count - 1)
                        {
                            OnIventoryFull();
                        }
                        break;
                    }
                }
                break;
            case Item.E_GEM:
                for (int i = 0; i < m_InventorySlots.Count; i++)
                {
                    Sprite slotSprite = m_InventorySlots[i].GetComponent<Image>().sprite;
                    if (slotSprite == m_EmptySprite)
                    {
                        m_InventorySlots[i].GetComponent<Image>().sprite = m_GemSprite;
                        if (i == m_InventorySlots.Count - 1)
                        {
                            OnIventoryFull();
                        }
                        break;
                    }
                }
                break;
        }
    }

    private void OnIventoryFull()
    {
        EventManager.TriggerEvent("InventoryFull", null);
    }

    private void ReinitializeInventory(Dictionary<string, object> param)
    {
        for (int i = 0; i < m_InventorySlots.Count; i++)
        {
            m_InventorySlots[i].GetComponent<Image>().sprite = m_EmptySprite;
        }

        SaveSystem.SaveInventory(this);
    }

    public List<Item> GetItemsInInventory()
    {
        List<Item> listOfItems = new List<Item>();

        for (int i = 0; i < m_InventorySlots.Count; i++)
        {
            if (m_InventorySlots[i].GetComponent<Image>().sprite == m_WoodSprite)
            {
                listOfItems.Add(Item.E_WOOD);
            }
            else if(m_InventorySlots[i].GetComponent<Image>().sprite == m_GemSprite)
            {
                listOfItems.Add(Item.E_GEM);
            }
            else
            {
                listOfItems.Add(Item.E_EMPTY);
            }
        }

        return listOfItems;
    }

    private void OnLoadInventory(Dictionary<string, object> param)
    {
        object objData;
        if(param != null && param.TryGetValue("data", out objData))
        {
            PlayerData data = (PlayerData)objData;
            SetInventory(data);
        }
    }

    private void SetInventory(PlayerData data)
    {
        List<Item> listOfItemsInSave = new List<Item>();

        for(int i = 0; i < data.m_Items.Length; i++)
        {
            listOfItemsInSave.Add((Item)data.m_Items[i]);
        }

        for(int i = 0; i < listOfItemsInSave.Count; i++)
        {
            if (listOfItemsInSave[i] == Item.E_WOOD)
            {
                m_InventorySlots[i].GetComponent<Image>().sprite = m_WoodSprite;
            }
            else if(listOfItemsInSave[i] == Item.E_GEM)
            {
                m_InventorySlots[i].GetComponent<Image>().sprite = m_GemSprite;
            }
            else
            {
                m_InventorySlots[i].GetComponent<Image>().sprite = m_EmptySprite;
            }
        }
    }
}
