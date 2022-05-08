using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ObjectSpawn : MonoBehaviour, IPointerDownHandler
{

    public GameObject leaf;
    private GameObject leafClone;

    public void OnPointerDown(PointerEventData data)
    {
        //Do the thing when button pressed
        Debug.Log("leaf created");

        var mousePos = Input.mousePosition;

        leafClone = Instantiate(leaf, Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 100)), leaf.transform.rotation);
        int leafCount = GameObject.FindGameObjectsWithTag("leaf").Length;
        leafClone.name = leafClone.name + leafCount;
        Debug.Log("created leaf: " + leafClone.name);
    }

}
