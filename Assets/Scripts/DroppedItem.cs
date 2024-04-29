using UnityEngine;
using pji2918.Functions;

public class DroppedItem : MonoBehaviour
{
    [SerializeField] private Item item;

    public Item GetItem
    {
        get
        {
            return item;
        }
    }

    private GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void ToPlayerInventory()
    {
        FunctionCollections.AddItem(player.GetComponent<PlayerController>().inventory, item);

        Destroy(gameObject);
    }
}
