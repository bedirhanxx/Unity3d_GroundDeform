using UnityEngine;

public class GroundDeform : MonoBehaviour
{
    public Terrain terrain;
    public float deformRadius = 5f;
    public float deformAmount = 1f;
    public bool increase = true;

    private TerrainData terrainData;
    private float[,] originalHeights;

    void Start()
    {
        if (terrain == null)
        {
            Debug.LogError("Terrain not assigned!");
            return;
        }

        terrainData = terrain.terrainData;
        originalHeights = (float[,])terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution).Clone();
    }

    void Update()
    {
        if (Input.GetMouseButton(0)) // Left mouse button
        {
            DeformGround();
        }
    }

    void DeformGround()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            float x = hitPoint.x / terrainData.size.x * terrainData.heightmapResolution;
            float z = hitPoint.z / terrainData.size.z * terrainData.heightmapResolution;

            int radius = Mathf.RoundToInt(deformRadius);
            int xMin = Mathf.Clamp(Mathf.RoundToInt(x - radius), 0, terrainData.heightmapResolution - 1);
            int xMax = Mathf.Clamp(Mathf.RoundToInt(x + radius), 0, terrainData.heightmapResolution - 1);
            int zMin = Mathf.Clamp(Mathf.RoundToInt(z - radius), 0, terrainData.heightmapResolution - 1);
            int zMax = Mathf.Clamp(Mathf.RoundToInt(z + radius), 0, terrainData.heightmapResolution - 1);

            float[,] heights = terrainData.GetHeights(xMin, zMin, xMax - xMin, zMax - zMin);

            for (int i = 0; i < heights.GetLength(0); i++)
            {
                for (int j = 0; j < heights.GetLength(1); j++)
                {
                    float dist = Vector2.Distance(new Vector2(i, j), new Vector2(x - xMin, z - zMin));
                    if (dist < deformRadius)
                    {
                        float effect = Mathf.Lerp(deformAmount, 0, dist / deformRadius);
                        if (increase)
                        {
                            heights[i, j] += effect;
                        }
                        else
                        {
                            heights[i, j] -= effect;
                        }
                    }
                }
            }

            terrainData.SetHeights(xMin, zMin, heights);
        }
    }

    public void ResetTerrain()
    {
        if (originalHeights != null)
        {
            terrainData.SetHeights(0, 0, originalHeights);
        }
    }
}
