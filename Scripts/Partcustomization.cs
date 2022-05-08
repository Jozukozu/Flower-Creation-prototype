using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Partcustomization : Singleton<Partcustomization>
{
    public GameObject target;

    private Partcustomization() { }

    private SkinnedMeshRenderer skmr;
    private Mesh mesh;
    private Dictionary<string, int> blendShapeDatabase = new Dictionary<string, int>();

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        skmr = GetSkinnedMeshRenderer();
        mesh = skmr.sharedMesh;

        ParseBlendshapesToDictionary();
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
