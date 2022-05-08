using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class FollowMouse : MonoBehaviour
{

    public float zDistance;
    private bool firstClick = true;
    private bool following = true;
    private bool snapped = false;

    private Collider targetCollider;
    private Collider myCollider;
    private float snapDistance = 50.0f;

    private void Start()
    {
        targetCollider = GameObject.FindGameObjectWithTag("stalk").GetComponent(typeof(Collider)) as Collider;
        myCollider = GetComponent(typeof(SphereCollider)) as Collider;
    }



    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var mousePos = Input.mousePosition;
            Vector3 v3Pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z);
            v3Pos = Camera.main.ScreenToWorldPoint(v3Pos);
            var distance = (Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position)).magnitude;

            //Debug.Log("following offset: " + distance);

            if (firstClick)
            {
                //Debug.Log("following");
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
            }

            else if (!firstClick & distance < 602)
            {
                //Debug.Log("following");
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
                following = true;
            }
        }


        if (Input.GetMouseButton(0))
        {

            if (following)
            {
                var mousePos = Input.mousePosition;
                //Debug.Log("following");
                transform.position = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, zDistance));
            }

        }

        else if(Input.GetMouseButtonUp(0))
        {
            firstClick = false;
            Vector3 myClosestPoint = myCollider.ClosestPoint(targetCollider.transform.position);
            Vector3 targetClosestPoint = targetCollider.ClosestPoint(myClosestPoint);
            Vector3 offset = targetClosestPoint - myClosestPoint;
            //Debug.Log("offset magnitude: " + offset.magnitude + "snapdistance: " + snapDistance);

            if (offset.magnitude < snapDistance)
            {
               // Debug.Log("snapping");
                transform.position += offset;
                snapped = true;
            }

            else
            {
                snapped = false;
            }

            if(!snapped)
            {
              //  Debug.Log("Destroy leaf");
                Destroy(gameObject);
            }

            following = false;
        }

        //Debug.Log("state of following: " + following);
    }
}
