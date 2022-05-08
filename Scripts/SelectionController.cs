using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionController : MonoBehaviour
{

    public string selection;

    private void OnMouseDown()
    {
        Debug.Log("Selected object: " + gameObject.name);
        selection = gameObject.name;
    }
}
