using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetDeform : MonoBehaviour
{
    public void CallResetDeform()
    {
        var tools = GameObject.FindObjectsOfType<MeshDeformTool>();
        foreach (var tool in tools)
        {
            tool.ResetMesh();
        }
    }
}
