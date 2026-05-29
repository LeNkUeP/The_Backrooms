using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshCreator
{
    public MeshCreator()
    {
        
    }

    public GameObject CreateRectangle(float width, float height, Vector3 position, float rotation)
    {
        // inits
        Mesh mesh = new Mesh();
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[vertices.Length];

        // vertices
        vertices[0] = new Vector3(0, 0, 0);
        vertices[1] = new Vector3(0, height, 0);
        vertices[2] = new Vector3(width, height, 0);
        vertices[3] = new Vector3(width, 0, 0);
        mesh.vertices = vertices;

        // triangles
        mesh.triangles = new int[]
        {
            // front
            0,1,2,
            0,2,3,
        };

        // uvs
        for (int i = 0; i < uv.Length; i++)
        {
            uv[i] = new Vector2(vertices[i].x, vertices[i].y);
        }
        mesh.uv = uv;
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));

        // front
        if(rotation == 0){
            gameObject.name = "front";
        // back
        }else if(rotation == 180){
            gameObject.name = "back";
        // left
        }else if (rotation == 90){
            gameObject.name = "left";
        // right
        }else if (rotation == -90){
            gameObject.name = "right";
        }

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        gameObject.transform.position = position;
        gameObject.transform.RotateAround(position, new Vector3(0, 1, 0), rotation);
        return gameObject;
    }

    public GameObject CreateFrontRectangle(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = MeshData.roomTris[0, i];
            vertices.Add(Vector3.Scale(MeshData.roomVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
            triangles.Add(vertexIndex);

             uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(width, height)));

            vertexIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("front", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateBackRectangle(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = MeshData.roomTris[1, i];
            vertices.Add(Vector3.Scale(MeshData.roomVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
            triangles.Add(vertexIndex);

            uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(width, height)));

            vertexIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("back", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateLeftRectangle(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = MeshData.roomTris[2, i];
            vertices.Add(Vector3.Scale(MeshData.roomVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
            triangles.Add(vertexIndex);

            uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(depth, height)));

            vertexIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("left", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateRightRectangle(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = MeshData.roomTris[3, i];
            vertices.Add(Vector3.Scale(MeshData.roomVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
            triangles.Add(vertexIndex);

            uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(depth, height)));

            vertexIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("right", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateBottomRectangle(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = MeshData.roomTris[5, i];
            vertices.Add(Vector3.Scale(MeshData.roomVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
            triangles.Add(vertexIndex);

            uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(width, depth)));

            vertexIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("bottom", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateTopRectangle(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int i = 0; i < 6; i++)
        {
            int triangleIndex = MeshData.roomTris[4, i];
            vertices.Add(Vector3.Scale(MeshData.roomVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
            triangles.Add(vertexIndex);

            uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(width, depth)));

            vertexIndex++;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("top", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateCube(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int p = 0; p < 6; p++)
        {
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = MeshData.cubeTris[p,i];
                vertices.Add(Vector3.Scale(MeshData.cubeVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
                triangles.Add(vertexIndex);

                if (p == 0 || p == 1)
                {
                    uvs.Add(Vector3.Scale(MeshData.cubeUvs[i], new Vector2(width, height)));
                }
                else if (p == 4 || p == 5)
                {
                    uvs.Add(Vector3.Scale(MeshData.cubeUvs[i], new Vector2(width, depth)));
                }
                else
                {
                    uvs.Add(Vector3.Scale(MeshData.cubeUvs[i], new Vector2(depth, height)));
                }

                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("Cube", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<BoxCollider>();
        return gameObject;
    }

    public GameObject CreateWall(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int p = 0; p < 4; p++)
        {
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = MeshData.wallTris[p, i];
                vertices.Add(Vector3.Scale(MeshData.wallVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
                triangles.Add(vertexIndex);

                if (p == 2 || p == 3)
                {
                    uvs.Add(Vector3.Scale(MeshData.wallUvs[i], new Vector2(height, depth)));
                }
                else
                {
                    uvs.Add(Vector3.Scale(MeshData.wallUvs[i], new Vector2(height, width)));
                }

                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("Cube", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateRoom(float width, float height, float depth, Vector3 pos)
    {
        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        for (int p = 0; p < 4; p++)
        {
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = MeshData.roomTris[p, i];
                vertices.Add(Vector3.Scale(MeshData.roomVerts[triangleIndex], new Vector3(width, height, depth)) + pos);
                triangles.Add(vertexIndex);

                // /2 because of scaling of texture !!!!!!!!!!
                if (p == 2 || p == 3)
                {
                    uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(depth/2, height/2)));
                }
                else
                {
                    uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(width/2, height/2)));
                }

                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("Room", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateFrontCurve(float width, float height, float depth, float gradient1, float gradient2, int subdivision, Vector3 pos)
    {
        List<Vector3> verts = new List<Vector3>();
        List<int[]> tris = new List<int[]>();

        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        // top vertices
        verts.Add(new Vector3(0,height,0) + pos);
        verts.Add(new Vector3(width, height, 0) + pos);

        // all vertices between top and bottom, calculated by subdivision
        for (int i = 1; i < subdivision+1; i++)
        {
            float heightcoord = getCurve((subdivision + 1 - i) * (1f / (subdivision + 1)), height, gradient1, gradient2);
            verts.Add(new Vector3(0, heightcoord, i * depth / (subdivision+1)) + pos);
            verts.Add(new Vector3(width, heightcoord, i * depth/(subdivision+1)) + pos);
        }

        // bottom vertices
        verts.Add(new Vector3(0, 0, depth) + pos);
        verts.Add(new Vector3(width, 0, depth) + pos);

        // add correct vertices to tris
        int counter = 0;
        for (int i = 0; i < subdivision+1; i++)
        {
            int[] currentTris = new int[6];
            currentTris[0] = counter + 0;
            currentTris[1] = counter + 2;
            currentTris[2] = counter + 1;
            currentTris[3] = counter + 1;
            currentTris[4] = counter + 2;
            currentTris[5] = counter + 3;
            counter += 2;
            tris.Add(currentTris);
        }

        for (int p = 0; p < subdivision+1; p++)
        {
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = tris[p][i];
                vertices.Add(verts[triangleIndex]);
                triangles.Add(vertexIndex);

                uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(width, height)));

                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("front", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public GameObject CreateBackCurve(float width, float height, float depth, float gradient1, float gradient2, int subdivision, Vector3 pos)
    {
        List<Vector3> verts = new List<Vector3>();
        List<int[]> tris = new List<int[]>();

        int vertexIndex = 0;
        List<Vector3> vertices = new List<Vector3>();
        List<int> triangles = new List<int>();
        List<Vector2> uvs = new List<Vector2>();

        // top vertices
        verts.Add(new Vector3(0, height, 0) + pos);
        verts.Add(new Vector3(width, height, 0) + pos);

        // all vertices between top and bottom, calculated by subdivision
        for (int i = 1; i < subdivision + 1; i++)
        {
            float heightcoord = getCurve((subdivision + 1 - i) * (1f / (subdivision + 1)), height, gradient1, gradient2);
            verts.Add(new Vector3(0, heightcoord, -i * depth / (subdivision + 1)) + pos);
            verts.Add(new Vector3(width, heightcoord, -i * depth / (subdivision + 1)) + pos);
        }

        // bottom vertices
        verts.Add(new Vector3(0, 0, -depth) + pos);
        verts.Add(new Vector3(width, 0, -depth) + pos);

        // add correct vertices to tris
        int counter = 0;
        for (int i = 0; i < subdivision + 1; i++)
        {
            int[] currentTris = new int[6];
            currentTris[0] = counter + 0;
            currentTris[1] = counter + 1;
            currentTris[2] = counter + 2;
            currentTris[3] = counter + 2;
            currentTris[4] = counter + 1;
            currentTris[5] = counter + 3;
            counter += 2;
            tris.Add(currentTris);
        }

        for (int p = 0; p < subdivision + 1; p++)
        {
            for (int i = 0; i < 6; i++)
            {
                int triangleIndex = tris[p][i];
                vertices.Add(verts[triangleIndex]);
                triangles.Add(vertexIndex);

                uvs.Add(Vector3.Scale(MeshData.roomUvs[i], new Vector2(width, height)));

                vertexIndex++;
            }
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.uv = uvs.ToArray();
        mesh.RecalculateNormals();

        // gameobject
        GameObject gameObject = new GameObject("back", typeof(MeshFilter), typeof(MeshRenderer));

        // stuff
        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.AddComponent<MeshCollider>();
        return gameObject;
    }

    public float getCurve(float x, float b, float p, float q)
    {
        return (p + q - 2 * b) * Mathf.Pow(x, 3) + (3 * b - 2 * p - q) * Mathf.Pow(x, 2) + p * x;
    }
}
