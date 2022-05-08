using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CameraRayCast : MonoBehaviour
{
    Ray ray;
    RaycastHit hit;
    public BlendshapeSlider slider1;
    public BlendshapeSlider slider2;
    public BlendshapeSlider slider3;

    public GameObject target;

    private SkinnedMeshRenderer skmr;
    private Mesh mesh;
    private Dictionary<string, int> blendShapeDatabase = new Dictionary<string, int>();


    private string selection;

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Debug.Log(hit.collider.name);
                selection = hit.collider.name;
                Debug.Log("selected item: " + selection);

                blendShapeDatabase.Clear();

                target = GameObject.Find(selection);

                skmr = GetSkinnedMeshRenderer();
                mesh = skmr.sharedMesh;

                ParseBlendshapesToDictionary();

                slider1.AssignBlendShape(blendShapeDatabase.ElementAt(0).Key);
                slider2.AssignBlendShape(blendShapeDatabase.ElementAt(1).Key);
                slider3.AssignBlendShape(blendShapeDatabase.ElementAt(2).Key);
            }
        }
    }



    private SkinnedMeshRenderer GetSkinnedMeshRenderer()
    {
        SkinnedMeshRenderer _skmr = target.GetComponentInChildren<SkinnedMeshRenderer>();

        return _skmr;
    }

    private void ParseBlendshapesToDictionary()
    {
        List<string> blendShapeNames = Enumerable.Range(0, mesh.blendShapeCount).Select(x => mesh.GetBlendShapeName(x)).ToList();

        for (int i = 0; i < blendShapeNames.Count; i++)
        {
            Debug.Log("blendshapenames: " + blendShapeNames[i]);
        }

        for (int i = 0; i < blendShapeNames.Count; i++)
        {
            int index = mesh.GetBlendShapeIndex(blendShapeNames[i]);
            blendShapeDatabase.Add(blendShapeNames[i], index);
        }
    }

    public void ChangeBlendshapeValue(string blendShapename, float value)
    {
        if (!blendShapeDatabase.ContainsKey(blendShapename))
        {
            Debug.LogError("Blendshape " + blendShapename + " does not exist!");
            return;
        }

        value = Mathf.Clamp(value, 0, 100);

        skmr.SetBlendShapeWeight(blendShapeDatabase[blendShapename], value);
    }
}
