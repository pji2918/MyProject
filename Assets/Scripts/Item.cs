using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "ScriptableObjects/Item", order = 1)]
public class Item : ScriptableObject, IComparable<Item>
{
    [SerializeField] private string itemName;
    public string ItemName
    {
        get
        {
            return itemName;
        }
    }
    [SerializeField] private string description;
    public string Description
    {
        get
        {
            return description;
        }
    }
    [SerializeField] private Sprite icon;
    public Sprite Icon
    {
        get
        {
            return icon;
        }
    }

    [SerializeField] private float equipTime;
    public float EquipTime
    {
        get
        {
            return equipTime;
        }
    }

    [SerializeField] private uint amount;
    public uint Amount
    {
        get
        {
            return amount;
        }
        set
        {
            amount = value;
        }
    }

    int IComparable<Item>.CompareTo(Item other)
    {
        return ItemName.CompareTo(other.ItemName);
    }
}
