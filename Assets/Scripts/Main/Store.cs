using UnityEngine;

public class Store : MonoBehaviour
{
    private Inventory inventory;

    public void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Inventory>();
    }

    public bool Buy(Price p)
    {
        bool sucess = inventory.Gold >= p.Gold;

        if (sucess)
        {
            inventory.DecreasesGoldBy(p.Gold);
        }

        return sucess;
    }

}
