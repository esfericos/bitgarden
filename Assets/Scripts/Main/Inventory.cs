using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int Gold { get; private set; }
    // public int Iron { get; private set; }
    // public int Coal { get; private set; }
    // public int Steel { get; private set; }

    void Start()
    {
        Gold = 50;
    }

    public void IncreasesGoldBy(int x)
    {
        Gold += x;
    }
    public void DecreasesGoldBy(int x)
    {
        Gold -= x;
    }
}
