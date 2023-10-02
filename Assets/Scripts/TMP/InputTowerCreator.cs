using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InputTowerCreator : MonoBehaviour
{

    public TMP_InputField xCord;
    public TMP_InputField yCord;
    public Button submitButton;


    void Start()
    {
        submitButton.onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        // Converte os valores dos campos de entrada para números inteiros
        int value1;
        int value2;

        if (int.TryParse(xCord.text, out value1) && int.TryParse(yCord.text, out value2))
        {
            // Imprime os valores no console
            Debug.Log("x cord: " + value1);
            Debug.Log("y cord: " + value2);
        }
        else
        {
            // Caso a conversão falhe
            Debug.Log("Insira números válidos nos campos de entrada.");
        }
    }
}
