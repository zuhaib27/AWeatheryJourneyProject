using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class IceGenerator : MonoBehaviour
{
    public float radius;
    public int resolution;

    Transform _waterTransform;

    Mesh _mesh;
    Vector3[] _vertices;
    List<int> _triangles;

    bool _refreshMesh = false;
    bool _refreshCollider = false;

    Vector3 _localRadius;
    int[] _gridSize;
    bool[,] _grid;

    // Start is called before the first frame update
    void Start()
    {
        _waterTransform = transform.parent;
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;

        _localRadius = new Vector3();
        _localRadius.x = radius / transform.lossyScale.x;
        _localRadius.y = radius / transform.lossyScale.y;
        _localRadius.z = radius / transform.lossyScale.z;

        _gridSize = new int[2];
        _gridSize[0] = (int)transform.lossyScale.z * resolution;
        _gridSize[1] = (int)transform.lossyScale.x * resolution;

        // Initialize the grid
        ResetGrid();

        //FillGrid();
    }

    // Reset the grid entirely
    public void ResetGrid()
    {
        _grid = new bool[_gridSize[0], _gridSize[1]];
        CreateVertices();
        _triangles = new List<int>();

        _refreshMesh = true;
        _refreshCollider = true;
        UpdateMesh();
        UpdateCollider();
    }

    // Create a radius of ice around the given position
    public void GenerateRadius(Vector3 center)
    {
        int m = _gridSize[0];
        int n = _gridSize[1];

        Vector3 localCenter = transform.InverseTransformPoint(center);

        // Check height
        if (Mathf.Abs(localCenter.y) < _localRadius.y)
        {
            // Get intersection of box around center position and the grid
            int startX = (int)(n / 2f + (localCenter.x - _localRadius.x) * n);
            int endX = (int)(n / 2f + (localCenter.x + _localRadius.x) * n);
            int startY = (int)(m / 2f + (localCenter.z - _localRadius.z) * m);
            int endY = (int)(m / 2f + (localCenter.z + _localRadius.z) * m);

            startX = (startX < 0) ? 0 : startX;
            endX = (endX > n) ? n : endX;
            startY = (startY < 0) ? 0 : startY;
            endY = (endY > m) ? m : endY;

            // Loop over intersection
            for (int i = startY; i < endY; i++)
            {
                for (int j = startX; j < endX; j++)
                {
                    if (!_grid[i, j])
                    {
                        Vector3 localPoint = new Vector3(-.5f + (float)j / n, 0f, -.5f + (float)i / m);
                        Vector3 pointFromCenter = localPoint - localCenter;

                        if (pointFromCenter.x * pointFromCenter.x / (_localRadius.x * _localRadius.x)
                            + pointFromCenter.y * pointFromCenter.y / (_localRadius.y * _localRadius.y)
                            + pointFromCenter.z * pointFromCenter.z / (_localRadius.z * _localRadius.z) < 1f)
                        {
                            SetGridCell(i, j);
                        }
                    }
                }
            }

            // Update mesh
            UpdateMesh();
        }
    }

    // Set a grid cell to ice
    private void SetGridCell(int i, int j)
    {
        int m = _gridSize[0] + 1;
        int n = _gridSize[1] + 1;

        _triangles.Add(i * n + j);
        _triangles.Add((i+1) * n + j);
        _triangles.Add(i * n + j + 1);
        _triangles.Add((i + 1) * n + j);
        _triangles.Add((i + 1) * n + j + 1);
        _triangles.Add(i * n + j + 1);

        _grid[i, j] = true;
        _refreshMesh = true;
        _refreshCollider = true;
    }

    // Update the mesh and collider
    void UpdateMesh()
    {
        if (!_refreshMesh)
            return;

        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles.ToArray();
        _mesh.RecalculateNormals();
        _mesh.RecalculateBounds();

        _refreshMesh = false;
    }

    // Update mesh collider
    public void UpdateCollider()
    {
        if (!_refreshCollider)
            return;

        MeshCollider.Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();

        _refreshCollider = false;
    }

    // Create vertices for grid
    private void CreateVertices()
    {
        int m = _gridSize[0];
        int n = _gridSize[1];

        _vertices = new Vector3[(m + 1) * (n + 1)];

        for (int i = 0; i < m + 1; i++)
        {
            for (int j = 0; j < n + 1; j++)
            {
                _vertices[i * (n + 1) + j] = new Vector3(-.5f + (float)j / n, 0f, -.5f + (float)i / m);
            }
        }
    }

    // Fill the entire grid (for debugging)
    void FillGrid()
    {
        int m = _gridSize[0];
        int n = _gridSize[1];

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                SetGridCell(i, j);
            }
        }

        UpdateMesh();
    }

    // Gizmos
    private void OnDrawGizmos()
    {
        DrawIceGrid();
    }

    // Draw the grid (remember the grid is only initialized on start up)
    void DrawIceGrid()
    {
        if (_vertices != null)
        {
            Gizmos.color = Color.white;

            int m = _gridSize[0] + 1;
            int n = _gridSize[1] + 1;

            for (int i = 0; i < m; i++)
            {
                Vector3 p1 = transform.TransformPoint(_vertices[i * n]);
                Vector3 p2 = transform.TransformPoint(_vertices[i * n + n - 1]);
                Gizmos.DrawLine(p1, p2);
            }

            for (int i = 0; i < n; i++)
            {
                Vector3 p1 = transform.TransformPoint(_vertices[i]);
                Vector3 p2 = transform.TransformPoint(_vertices[(m - 1) * n + i]);
                Gizmos.DrawLine(p1, p2);
            }

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, .1f);
        }
    }
}
