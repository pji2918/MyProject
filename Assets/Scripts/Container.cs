using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Container", menuName = "ScriptableObjects/Item/Container")]
public class Container : Item, IComparable<Item>
{
    [SerializeField] private uint bulletCapacity;

    public uint BulletCapacity
    {
        get => bulletCapacity;
    }

    private uint bulletCount;

    public uint BulletCount
    {
        get => bulletCount;
        set => bulletCount = value;
    }

    void OnEnable()
    {
        bulletCount = bulletCapacity;
    }

    void OnDisable()
    {
        bulletCount = bulletCapacity;
    }
}
