using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Gun", menuName = "ScriptableObjects/Item/Gun", order = 2)]
public class Gun : Item, IComparable<Item>
{
    private int ammo;

    public int Ammo
    {
        get
        {
            return ammo;
        }
        set
        {
            ammo = value;
        }
    }

    [SerializeField] private int maxAmmo;

    public int MaxAmmo
    {
        get
        {
            return maxAmmo;
        }
    }

    [SerializeField] private float reloadTime;

    public float ReloadTime
    {
        get
        {
            return reloadTime;
        }
    }

    void OnEnable()
    {
        ammo = 0;
    }

    void OnDisable()
    {
        ammo = 0;
    }
}