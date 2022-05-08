using System.Collections;
using System.Collections.Generic;
using UnityEditor.Events;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class BlendshapeSlider : MonoBehaviour
{
    private string _blendShapeName;
    public Slider slider;
    public CameraRayCast cameraRayCast;

    // Start is called before the first frame update
    private void Start()
    {
        slider = GetComponent<Slider>();
    }

    public void AssignBlendShape(string blendShapeName)
    {
        _blendShapeName = blendShapeName;

        try
        {
            UnityEventTools.RemovePersistentListener(slider.onValueChanged, 0);
        }
        catch (System.Exception)
        {
        }

        slider.onValueChanged.AddListener(value => cameraRayCast.ChangeBlendshapeValue(_blendShapeName, value));
    }


}
