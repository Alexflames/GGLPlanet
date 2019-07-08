using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CurrentItem : MonoBehaviour,IPointerClickHandler
{
    private GameObject player;
    private Inventory inventory;
    [HideInInspector]
    public int index;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        inventory = player.GetComponent<Inventory>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        inventory.PointerEffect(eventData, index);

    }
}
