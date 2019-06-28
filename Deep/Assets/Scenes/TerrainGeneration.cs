using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGeneration : MonoBehaviour
{

    Vector2[] coordinates;

    // Start is called before the first frame update
    void Start()
    {
        coordinates = new Vector2[2000];

        // Sets the coordinates of the terrain
        for (int x = 0; x < 2000; x++)
        {
            if(x < 11)
            {
                coordinates[x] = new Vector2(x, (Mathf.Pow(x-10, 2f)/100)+2000);
            }
            else if (x < 634)
            {
                coordinates[x] = new Vector2(x, (Mathf.Pow(x-10, 2f)/-750)+2000);
            }
            else if(x > 634)
            {
                coordinates[x] = new Vector2(x, (Mathf.Pow(x-1850, 2f)/1000)+1);
            }
        }

        Vector3[] vertices = new Vector3[3*coordinates.Length];
        int[] triangles = new int[12*(coordinates.Length-1)];

        vertices[0] = new Vector3(coordinates[0].x, coordinates[0].y, 1);
        vertices[1] = new Vector3(coordinates[0].x, coordinates[0].y, -1);
        vertices[2] = new Vector3(coordinates[0].x, 0, -1);

        for (int verts = 3, tris = 0, coord = 1; coord < coordinates.Length; coord++)
        {
            // Vertices for current coordinate
            vertices[verts] = new Vector3(coordinates[coord].x, coordinates[coord].y, 1);
            vertices[verts+1] = new Vector3(coordinates[coord].x, coordinates[coord].y, -1);
            vertices[verts+2] = new Vector3(coordinates[coord].x, 0, -1);

            // Front Bottom Triangle
            triangles[tris] = verts-1;
            triangles[tris+1] = verts-2;
            triangles[tris+2] = verts+2;

            // Front Top Triangle
            triangles[tris+3] = verts-2;
            triangles[tris+4] = verts+1;
            triangles[tris+5] = verts+2;

            // Close Top Triangle
            triangles[tris+6] = verts-3;
            triangles[tris+7] = verts+1;
            triangles[tris+8] = verts-2;

            // Far Top Triangle
            triangles[tris+9] = verts-3;
            triangles[tris+10] = verts;
            triangles[tris+11] = verts+1;

            verts += 3;
            tris += 12;
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;

        MeshFilter meshFilter = GetComponent<MeshFilter>();
        meshFilter.mesh = mesh;

        MeshCollider meshCollider = GetComponent<MeshCollider>();
        meshCollider.sharedMesh = mesh;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
