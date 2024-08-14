#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[InitializeOnLoad]
public class TerrainResetter
{
  static TerrainResetter()
  {
    EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
  }

  private static void OnPlayModeStateChanged(PlayModeStateChange state)
  {
    if (state == PlayModeStateChange.ExitingPlayMode)
    {
      var tools = GameObject.FindObjectsOfType<GroundDeform>();
      foreach (var tool in tools)
      {
        tool.ResetTerrain();
      }
    }
  }
}
#endif