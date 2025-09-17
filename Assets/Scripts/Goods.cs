using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Goods", menuName = "ScriptableObjects/Item/Goods", order = 2)]
public class Goods : Item, IComparable<Item>
{

}
