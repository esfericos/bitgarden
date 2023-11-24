using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public int Gold { get; private set; }
    [SerializeField] private TMP_Text goldText;
    // public int Iron { get; private set; }
    // public int Coal { get; private set; }
    // public int Steel { get; private set; }

    void Start()
    {
        Gold = 50;
        goldText.text = Gold.ToString();
    }

    public void IncreasesGoldBy(int x)
    {
        Console.print("teste");
        Gold += x;
        goldText.text = Gold.ToString();
    }
    public void DecreasesGoldBy(int x)
    {
        Gold -= x;
    }
}
