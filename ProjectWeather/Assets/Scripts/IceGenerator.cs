using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class IceGenerator : MonoBehaviour
{
    public float radius;
    public int resolution;

    public GameObject waterObject;

    Mesh _mesh;
    Vector3[] _vertices;
    List<int> _triangles;

    bool[,] _grid;
    Vector3 _gridOrigin;
    Vector3 _gridX;
    Vector3 _gridY;
    Vector3 _gridNorm;
    float _gridWidth;
    float _gridHeight;

    // Start is called before the first frame update
    void Start()
    {
        _mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = _mesh;
        
        _gridX = transform.right;
        _gridY = transform.forward;
        _gridNorm = transform.up;
        _gridWidth = waterObject.GetComponent<Renderer>().bounds.size.x;
        _gridHeight = waterObject.GetComponent<Renderer>().bounds.size.z;
        _gridOrigin = transform.position - _gridX * _gridWidth / 2 - _gridY * _gridHeight / 2;
        
        _grid = new bool[(int)(_gridHeight * resolution), (int)(_gridWidth * resolution)];
        CreateVertices();
        _triangles = new List<int>();
    }

    // Create a radius of ice around the given position
    public void GenerateRadius(Vector3 center)
    {
        int m = (int)(_gridHeight * resolution);
        int n = (int)(_gridWidth * resolution);

        // Get box surrounding radius
        Vector3 boundX1 = center - radius * _gridX;
        Vector3 boundX2 = center + radius * _gridX;
        Vector3 boundY1 = center - radius * _gridY;
        Vector3 boundY2 = center + radius * _gridY;
        Vector3 boundZ1 = center - radius * _gridNorm;
        Vector3 boundZ2 = center + radius * _gridNorm;

        // Compute position of box relative to grid
        float x1 = Vector3.Dot((boundX1 - _gridOrigin), _gridX);
        float x2 = Vector3.Dot((boundX2 - _gridOrigin), _gridX);
        float y1 = Vector3.Dot((boundY1 - _gridOrigin), _gridY);
        float y2 = Vector3.Dot((boundY2 - _gridOrigin), _gridY);
        float z1 = Vector3.Dot((boundZ1 - _gridOrigin), _gridNorm);
        float z2 = Vector3.Dot((boundZ2 - _gridOrigin), _gridNorm);

        // Compute intersection of box and grid
        float minX = Mathf.Max(x1, 0);
        float maxX = Mathf.Min(x2, _gridWidth);
        float minY = Mathf.Max(y1, 0);
        float maxY = Mathf.Min(y2, _gridHeight);

        // Convert intersection to grid indeces
        int startX = (int)((minX / _gridWidth) * n);
        int endX = (int)((maxX / _gridWidth) * n);
        int startY = (int)((minY / _gridHeight) * m);
        int endY = (int)((maxY / _gridHeight) * m);

        float resY = _gridHeight / (m - 1);
        float resX = _gridWidth / (n - 1);

        // Check if z in range
        if (z1 * z2 < 0)
        {
            // Loop over intersection
            for (int i = startY; i < endY; i++)
            {
                for (int j = startX; j < endX; j++)
                {
                    if (!_grid[i, j])
                    {
                        Vector3 realWorldPoint = _gridOrigin + (float)i * resY * _gridY + (float)j * resX * _gridX;

                        if (Vector3.SqrMagnitude(realWorldPoint - center) < radius * radius)
                        {
                            SetGridCell(i, j);
                        }
                    }
                }
            }

            // Update mesh
            if (startY < endY && startX < endX)
            {
                UpdateMesh();
            }
        }
    }

    // Set a grid cell to ice
    private void SetGridCell(int i, int j)
    {
        int m = (int)(_gridHeight * resolution) + 1;
        int n = (int)(_gridWidth * resolution) + 1;

        _triangles.Add(i * n + j);
        _triangles.Add((i+1) * n + j);
        _triangles.Add(i * n + j + 1);
        _triangles.Add((i + 1) * n + j);
        _triangles.Add((i + 1) * n + j + 1);
        _triangles.Add(i * n + j + 1);

        _grid[i, j] = true;
    }

    // Update the mesh and collider
    void UpdateMesh()
    {
        _mesh.Clear();

        _mesh.vertices = _vertices;
        _mesh.triangles = _triangles.ToArray();
        _mesh.RecalculateNormals();

        // Update mesh collider
        MeshCollider.Destroy(GetComponent<MeshCollider>());
        gameObject.AddComponent<MeshCollider>();
    }

    // Create vertices for grid
    private void CreateVertices()
    {
        int m = (int)(_gridHeight * resolution) + 1;
        int n = (int)(_gridWidth * resolution) + 1;

        float resY = _gridHeight / (m - 1);
        float resX = _gridWidth / (n - 1);

        _vertices = new Vector3[m * n];

        for (int i = 0; i < m; i++)
        {
            for (int j = 0; j < n; j++)
            {
                _vertices[i * n + j] = transform.InverseTransformPoint(_gridOrigin + (float)i * resY * _gridY + (float)j * resX * _gridX);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (_gridHeight != 0 && _gridWidth != 0)
        {
            int m = (int)(_gridHeight * resolution) + 1;
            int n = (int)(_gridWidth * resolution) + 1;

            for (int i = 0; i < m; i++)
            {
                Gizmos.DrawLine(_vertices[i * n], _vertices[i * n + n - 1]);
            }

            for (int i = 0; i < n; i++)
            {
                Gizmos.DrawLine(_vertices[i], _vertices[(m - 1) * n + i]);
            }

            Gizmos.DrawWireSphere(_gridOrigin, .1f);
        }
    }
}
