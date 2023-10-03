using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class TowerScript : MonoBehaviour
{

    [Header("Attribute")]
    [SerializeField] private float range = 5f;

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }

}


