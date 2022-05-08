using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 2F;
    public float sensitivityY = 2F;
    public float minimumX = -360F;
    public float maximumX = 360F;
    public float minimumY = -90F;
    public float maximumY = 90F;
    public float zoomSpeed = 20f;
    float rotationY = -60F;

    // For camera movement
    float CameraPanningSpeed = 10.0f;


    void Update()
    {
        MouseInput();
    }

    void MouseInput()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        if (Input.GetMouseButton(1))
        {
            MouseRightClick();
        }
        else if (Input.GetMouseButton(2))
        {
            MouseMiddleButtonClicked();
        }
        else if (Input.GetMouseButtonUp(1))
        {
            ShowAndUnlockCursor();
        }
        else if (Input.GetMouseButtonUp(2))
        {
            ShowAndUnlockCursor();
        }
        else
        {
            MouseWheeling();
        }
    }

    void ShowAndUnlockCursor()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void HideAndLockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void MouseMiddleButtonClicked()
    {
        HideAndLockCursor();
        Vector3 NewPosition = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));
        Vector3 pos = transform.position;
        if (NewPosition.x > 0.0f)
        {
            pos += transform.right;
        }
        else if (NewPosition.x < 0.0f)
        {
            pos -= transform.right;
        }
        if (NewPosition.z > 0.0f)
        {
            pos += transform.up;
        }
        if (NewPosition.z < 0.0f)
        {
            pos -= transform.up;
        }
        pos.z = transform.position.z;
        transform.position = pos;
    }

    void MouseRightClick()
    {
        HideAndLockCursor();
        Vector3 NewPosition = new Vector3(Input.GetAxis("Mouse X"), 0, Input.GetAxis("Mouse Y"));

        if (NewPosition.x > 0.0f)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, 0.2f);
        }

        else if (NewPosition.x < 0.0f)
        {
            transform.RotateAround(Vector3.zero, Vector3.up, -0.2f);
        }

        else if (NewPosition.z > 0.0f)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, 0.2f);
        }


        else if (NewPosition.z < 0.0f)
        {
            transform.RotateAround(Vector3.zero, Vector3.right, -0.2f);
        }

        Quaternion quaternion = transform.rotation;
        quaternion.eulerAngles = new Vector3(quaternion.eulerAngles.x, quaternion.eulerAngles.y, 0);
        transform.rotation = quaternion;
    }

    void MouseWheeling()
    {
        Vector3 pos = transform.position;
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            pos = pos - transform.forward * zoomSpeed;
            transform.position = pos;
        }
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            pos = pos + transform.forward * zoomSpeed;
            transform.position = pos;
        }
    }
}