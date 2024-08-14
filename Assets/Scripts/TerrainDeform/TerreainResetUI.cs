using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerreainResetUI : MonoBehaviour
{
    public void ResetTerrainCall()
    {
      var tools = GameObject.FindObjectsOfType<GroundDeform>();
        foreach (var tool in tools)
        {
        tool.ResetTerrain();
        }
    }
}
