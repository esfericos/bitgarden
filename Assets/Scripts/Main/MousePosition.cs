using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MousePosition : MonoBehaviour
{
    public TextMeshProUGUI text;    

    void Update()
    {
        Vector3 mouse_position = Input.mousePosition;

        float x = mouse_position.x; 
        float y = mouse_position.y;

        text.text = $"({x}, {y})";

    }
}
