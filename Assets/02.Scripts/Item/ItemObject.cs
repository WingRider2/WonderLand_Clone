using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemObject : MonoBehaviour, IInteractable
{
    public ItemData itemData;
    
    private Player player;

    public void OnInteract(Player player)
    {
        this.player = player;
        //player.addItem?.Invoke(itemData);

        player.TriggerAddItem(itemData);

        Destroy(gameObject);
    }
}
