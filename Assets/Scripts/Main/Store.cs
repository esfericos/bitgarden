using TMPro;
using UnityEngine;

public class Store : MonoBehaviour
{
    private Inventory inventory;
    [SerializeField] private TMP_Text goldText;

    public void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("StoreHandler").GetComponent<Inventory>();
        goldText.text = inventory.Gold.ToString();
    }

    public bool Buy(Price p)
    {
        bool sucess = inventory.Gold >= p.Gold;

        if (sucess)
        {
            inventory.DecreasesGoldBy(p.Gold);
            goldText.text = inventory.Gold.ToString();
        }

        return sucess;
    }

}
