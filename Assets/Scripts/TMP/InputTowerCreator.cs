using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace TMP
{
    public class InputTowerCreator : MonoBehaviour
    {

        public TMP_InputField xCord;
        public TMP_InputField yCord;
        public Button submitButton;


        private void Start()
        {
            submitButton.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            // Converte os valores dos campos de entrada para números inteiros
            if (int.TryParse(xCord.text, out var value1) && int.TryParse(yCord.text, out var value2))
            {
                // Imprime os valores no console
                Debug.Log("x cord: " + value1);
                Debug.Log("y cord: " + value2);
            }
            else
            {
                // Caso a conversão falhe
                Debug.Log("Insira números válidos no campo de entrada.");
            }
        }
    }
}
