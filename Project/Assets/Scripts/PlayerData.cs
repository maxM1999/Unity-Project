using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int[] m_Items;

    public PlayerData(Inventory inventory)
    {
        List<Item> listOfItem = inventory.GetItemsInInventory();
        m_Items = new int[listOfItem.Count];

        for(int i = 0; i < listOfItem.Count; i++)
        {
            m_Items[i] = (int)listOfItem[i];
        }
    }
}
