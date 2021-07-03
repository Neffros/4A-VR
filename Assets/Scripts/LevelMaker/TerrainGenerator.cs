using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainGenerator : MonoBehaviour
{
    public Texture2D hMap;

    public int heightScale = 100;
    public Material terrainMat;
    
    private List<Vector3> verts = new List<Vector3>();
    List<int> indices =new List<int>();

    private void Start()
    {
        Debug.Log(hMap.GetPixels().Length);
        Debug.Log("height" + hMap.height);
        Debug.Log("width" + hMap.width);

        Color[] pixels = hMap.GetPixels();
        for (int x = 0; x < hMap.width; x++)
        {
            for (int y = 0; y < hMap.height; y++)
            {
                verts.Add(new Vector3(x, pixels[y * hMap.width + x].grayscale * heightScale, y));

                if (x == 0 || y == 0) continue;
                    
                indices.Add(hMap.width * y + x);
                indices.Add(hMap.width * y + x - 1);
                indices.Add(hMap.width * (y - 1) + x - 1);
                indices.Add(hMap.width * (y - 1) + x - 1);
                indices.Add(hMap.width * (y - 1) + x);
                indices.Add(hMap.width * y + x);
            }
        }
        
        Vector2[] uv = new Vector2[verts.Count];
        for (int i = 0; i < verts.Count; i++)
        {
            uv[i] = new Vector2(verts[i].x, verts[i].z);
        }

        GameObject plane = new GameObject("plane from heightmap");
        plane.AddComponent<MeshFilter>();
        plane.AddComponent<MeshRenderer>();
        Mesh finalMesh = new Mesh();
        finalMesh.SetVertices(verts);
        finalMesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        //finalMesh.uv = uv;
        finalMesh.RecalculateNormals();
        plane.GetComponent<MeshRenderer>().material = terrainMat;
        plane.GetComponent<MeshFilter>().mesh = finalMesh;

        
    }


}