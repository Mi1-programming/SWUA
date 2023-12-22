using UnityEngine;
using System.Collections.Generic;
using Subdiv;

public class Generation : MonoBehaviour
{
    public int size;

    [HideInInspector] public bool[][][] vox;

    private MeshFilter meshFilter;
    private Vector3[][] voxFaces = new Vector3[][]
    {
        new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(0, 1, 0),
            new Vector3(1, 1, 0),
            new Vector3(1, 0, 0),
        },
        new Vector3[] {
            new Vector3(0, 0, 1),
            new Vector3(1, 0, 1),
            new Vector3(1, 1, 1),
            new Vector3(0, 1, 1),
        },
        new Vector3[] {
            new Vector3(0, 0, 0),
            new Vector3(1, 0, 0),
            new Vector3(1, 0, 1),
            new Vector3(0, 0, 1),
        },
        new Vector3[] {
            new Vector3(0, 1, 1),
            new Vector3(1, 1, 1),
            new Vector3(1, 1, 0),
            new Vector3(0, 1, 0),
        },
        new Vector3[] {
            new Vector3(0, 0, 1),
            new Vector3(0, 1, 1),
            new Vector3(0, 1, 0),
            new Vector3(0, 0, 0),
        },
        new Vector3[] {
            new Vector3(1, 1, 1),
            new Vector3(1, 0, 1),
            new Vector3(1, 0, 0),
            new Vector3(1, 1, 0),
        },
    };
    private void Awake()
    {
        meshFilter = GetComponent<MeshFilter>();
    }
    private void Start()
    {
        vox = MuchClean.Create3DMatrix(size);
    }
    public Vector3Int ClampToBounds(Vector3Int value)
    {
        return new Vector3Int(Mathf.Clamp(value.x, 0, size - 1), Mathf.Clamp(value.y, 0, size - 1), Mathf.Clamp(value.z, 0, size - 1));
    }
    public Mesh Export(bool forProduction)
    {
        Mesh model = new Mesh();
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        int x;
        for (x = 0; x < size; x++)
        {
            int y;
            for (y = 0; y < size; y++)
            {
                int z;
                for (z = 0; z < size; z++)
                {
                    BuildFace(x, y, z, 5, 1, 0, 0, ref vertices, ref triangles, ref uvs);
                    BuildFace(x, y, z, 4, -1, 0, 0, ref vertices, ref triangles, ref uvs);
                    BuildFace(x, y, z, 3, 0, 1, 0, ref vertices, ref triangles, ref uvs);
                    BuildFace(x, y, z, 2, 0, -1, 0, ref vertices, ref triangles, ref uvs);
                    BuildFace(x, y, z, 1, 0, 0, 1, ref vertices, ref triangles, ref uvs);
                    BuildFace(x, y, z, 0, 0, 0, -1, ref vertices, ref triangles, ref uvs);
                }
            }
        }

        if (forProduction)
        {
            Vector3 origin = Vector3.zero;
            foreach (var vertice in vertices)
            {
                origin += vertice;
            }
            origin /= vertices.Count * size;
            int i = 0;
            for (i = 0; i < vertices.Count; i++)
            {
                vertices[i] /= size;
                vertices[i] -= origin;
            }
        }
        
        model.vertices = vertices.ToArray();
        model.triangles = triangles.ToArray();
        model.uv = uvs.ToArray();

        if (model.vertexCount > 0)
        {
            Mesh welded = SubdivisionSurface.Weld(model, float.Epsilon, model.bounds.size.x);
            model = SubdivisionSurface.Subdivide(
                welded,
                1,
                false
            );
        }

        model.RecalculateNormals();
        return model;
    }
    public bool IsOutOfBounds(int x, int y, int z)
    {
        return x < 0 || x >= size || y < 0 || y >= size || z < 0 || z >= size;
    }
    private bool GetFace(int x, int y, int z, int dx, int dy, int dz)
    {
        if (!vox[x][y][z]) return false;
        Vector3Int pos = new Vector3Int(x + dx, y + dy, z + dz);
        if (IsOutOfBounds(pos.x, pos.y, pos.z)) return true;
        return !vox[pos.x][pos.y][pos.z];
    }
    private void BuildFace(int x, int y, int z, int face, int dx, int dy, int dz, ref List<Vector3> vertices, ref List<int> triangles, ref List<Vector2> uvs)
    {
        if (GetFace(x, y, z, dx, dy, dz))
        {
            int vertLen = vertices.Count;
            triangles.Add(vertLen + 0);
            triangles.Add(vertLen + 1);
            triangles.Add(vertLen + 2);
            triangles.Add(vertLen + 2);
            triangles.Add(vertLen + 3);
            triangles.Add(vertLen + 0);

            foreach (var f in voxFaces[face])
            {
                vertices.Add(f - Vector3.one * 0.5f + new Vector3(x, y, z));
            }

            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
            uvs.Add(new Vector2(0.0f, 0.0f));
        }
    }
    public void Generate()
    {
        Mesh model = Export(false);
        meshFilter.mesh = model;
    }
    public void Clear()
    {
        vox = MuchClean.Create3DMatrix(size);
        Generate();
    }
}