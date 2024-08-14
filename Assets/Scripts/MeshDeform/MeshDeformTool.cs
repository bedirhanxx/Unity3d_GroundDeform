using UnityEngine;

public class MeshDeformTool : MonoBehaviour
{
    public float deformRadius = 5f;
    public float deformAmount = 1f;
    public bool increase = true;

    private Mesh mesh;
    private Vector3[] originalVertices;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        originalVertices = (Vector3[])mesh.vertices.Clone();
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            DeformMesh();
        }
    }

    void DeformMesh()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            Vector3 hitPoint = hit.point;
            Vector3[] vertices = mesh.vertices;
            Vector3 planePosition = transform.position;
            Vector3 planeNormal = transform.up; // Assuming the plane is upright

            for (int i = 0; i < vertices.Length; i++)
            {
                Vector3 worldVertex = transform.TransformPoint(vertices[i]);
                float distance = Vector3.Distance(hitPoint, worldVertex);

                if (distance < deformRadius)
                {
                    float effect = Mathf.Lerp(deformAmount, 0, distance / deformRadius);
                    if (increase)
                    {
                        vertices[i] += planeNormal * effect;
                    }
                    else
                    {
                        vertices[i] -= planeNormal * effect;
                    }
                }
            }

            mesh.vertices = vertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }

    public void ResetMesh()
    {
        if (originalVertices != null)
        {
            mesh.vertices = originalVertices;
            mesh.RecalculateNormals();
            mesh.RecalculateBounds();
        }
    }
}