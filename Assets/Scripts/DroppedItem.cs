using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;
using pji2918.Functions;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private Item item;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            FunctionCollections.AddItem(other.GetComponent<PlayerController>().inventory, item);

            Destroy(gameObject);
        }
    }
}
