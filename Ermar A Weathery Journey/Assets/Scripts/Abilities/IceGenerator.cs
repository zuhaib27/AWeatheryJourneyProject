using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class IceGenerator : MonoBehaviour
{
    public float radius;
    public int resolution;

    Transform _waterTransform;

    Vector3[] _vertices;
    Mesh _mesh;
    MeshCollider _meshCollider;

    public float _rateOfColliderUpdate = .1f;
    bool _refreshMesh = false;
    bool _refreshCollider = false;

    Vector3 _localRadius;
    int[] _gridSize;
    bool[,] _grid;

    // Initialize
    void Start()
    {
        _waterTransform = transform.parent;
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        _meshCollider = GetComponent<MeshCollider>();

        _localRadius = new Vector3
        {
            x = radius / transform.lossyScale.x,
            y = radius / transform.lossyScale.y,
            z = radius / transform.lossyScale.z
        };

        _gridSize = new int[]
        {
            (int)transform.lossyScale.z * resolution,
            (int)transform.lossyScale.x * resolution
        };
        
        ResetGrid();
        
        StartCoroutine("DoColliderUpdate");

        //FillGrid();
    }

    // Update the collider on intervals
    IEnumerator DoColliderUpdate()
    {
        for (;;)
        {
            UpdateCollider();

            yield return new WaitForSeconds(_rateOfColliderUpdate);
        }
    }

    // Reset the grid
    public void ResetGrid()
    {
        _grid = new bool[_gridSize[0], _gridSize[1]];
        CreateVertices();

        _refreshMesh = true;
        UpdateMesh();
    }

    // Create a radius of ice around the given position (or remove it)
    public void GenerateRadius(Vector3 center, bool isMelting)
    {
        int m = _gridSize[0];
        int n = _gridSize[1];

        Vector3 localCenter = transform.InverseTransformPoint(center);

        // Check height
        if (Mathf.Abs(localCenter.y) < _localRadius.y)
        {
            // Get intersection of grid with the box around the given center position
            int startX = (int)(n / 2f + (localCenter.x - _localRadius.x) * n);
            int endX = (int)(n / 2f + (localCenter.x + _localRadius.x) * n);
            int startY = (int)(m / 2f + (localCenter.z - _localRadius.z) * m);
            int endY = (int)(m / 2f + (localCenter.z + _localRadius.z) * m);

            startX = (startX < 0) ? 0 : startX;
            endX = (endX > n) ? n : endX;
            startY = (startY < 0) ? 0 : startY;
            endY = (endY > m) ? m : endY;

            bool isIce = !isMelting;

            // Loop over intersection
            for (int i = startY; i < endY; i++)
            {
                for (int j = startX; j < endX; j++)
                {
                    if (_grid[i, j] != isIce)
                    {
                        Vector3 localPoint = new Vector3(-.5f + (float)j / n, 0f, -.5f + (float)i / m);
                        Vector3 pointFromCenter = localPoint - localCenter;

                        if (pointFromCenter.x * pointFromCenter.x / (_localRadius.x * _localRadius.x)
                            + pointFromCenter.y * pointFromCenter.y / (_localRadius.y * _localRadius.y)
                            + pointFromCenter.z * pointFromCenter.z / (_localRadius.z * _localRadius.z) < 1f)
                        {
                            _grid[i, j] = isIce;
                            _refreshMesh = true;
                        }
                    }
                }
            }

            // Update mesh
            UpdateMesh();
        }
    }

    // Update the mesh
    // (call this before UpdateCollider())
    void UpdateMesh()
    {
        if (!_refreshMesh)
            return;

        int m = _gridSize[0] + 1;
        int n = _gridSize[1] + 1;

        List<int> triangles = new List<int>();

        // Can be optimized if needed so that triangles are combined when possible
        for (int i = 0; i < _gridSize[0]; i++)
        {
            for (int j = 0; j < _gridSize[1]; j++)
            {
                if (_grid[i, j])
                {
                    triangles.Add(i * n + j);
                    triangles.Add((i + 1) * n + j);
                    triangles.Add(i * n + j + 1);
                    triangles.Add((i + 1) * n + j);
                    triangles.Add((i + 1) * n + j + 1);
                    triangles.Add(i * n + j + 1);
                }
            }
        }
        
        _mesh.triangles = triangles.ToArray();
        _mesh.RecalculateBounds();

        _refreshMesh = false;
        _refreshCollider = true;
    }

    // Update mesh collider
    public void UpdateCollider()
    {
        if (!_refreshCollider)
            return;
        
        _meshCollider.sharedMesh = _mesh;

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

        // Compute normals now so that we don't have to on every update
        _mesh.Clear();
        _mesh.vertices = _vertices;
        FillGrid();
        _mesh.RecalculateNormals();
        _grid = new bool[_gridSize[0], _gridSize[1]];
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
                _grid[i, j] = true;
            }
        }

        _refreshMesh = true;
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
        if (_grid != null)
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
