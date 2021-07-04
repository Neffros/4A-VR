using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;

public class TerrainGenerator : MonoBehaviour
{
    public Texture2D hMap;
    //public bool updateRealTime;
    public int heightScale = 100;
    public Material terrainMat;
    public Gradient gradient;
    private Texture2D colorText;
    private Color[] colors;
    private List<Vector3> verts = new List<Vector3>();
    List<int> indices =new List<int>();

    private float minTerrainHeight;
    private float maxTerrainHeight;
    Mesh mesh = new Mesh();

    //private bool isGenerated;

    private void Start()
    {

        Color[] pixels = hMap.GetPixels();
        
        colorText = new Texture2D(hMap.width, hMap.height);
        for (int x = 0; x < hMap.width; x++)
        {
            for (int y = 0; y < hMap.height; y++)
            {
                float height = pixels[y * hMap.width + x].grayscale * heightScale;
                Vector3 currVert = new Vector3(x, height, y);
                verts.Add(currVert);
                colorText.SetPixel(x, y, Color.red);
                if (x == 0 || y == 0) continue;
                    
                indices.Add(hMap.width * y + x);
                indices.Add(hMap.width * y + x - 1);
                indices.Add(hMap.width * (y - 1) + x - 1);
                indices.Add(hMap.width * (y - 1) + x - 1);
                indices.Add(hMap.width * (y - 1) + x);
                indices.Add(hMap.width * y + x);

                if (height > maxTerrainHeight)
                    maxTerrainHeight = height;
                if (height < minTerrainHeight)
                    minTerrainHeight = height;
            }
        }
        //Vector2[] uv = new Vector2[verts.Count];
        colors = new Color[verts.Count];
        for (int i = 0; i < verts.Count; i++)
        {
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, verts[i].y);
            colors[i] = gradient.Evaluate(height);
            //uv[i] = new Vector2(verts[i].x, verts[i].z);
        }

        GameObject plane = new GameObject("plane from heightmap");
        plane.AddComponent<MeshFilter>();
        plane.AddComponent<MeshRenderer>();
        mesh = new Mesh();
        mesh.indexFormat = IndexFormat.UInt32;
        UpdateMesh();
        
        plane.GetComponent<MeshRenderer>().material = terrainMat;
        plane.GetComponent<MeshFilter>().mesh = mesh;
        plane.AddComponent<TeleportationArea>();
        //isGenerated = true;
    }

    private void UpdateMesh()
    {
        mesh.Clear();
        mesh.SetVertices(verts);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        mesh.colors = colors;
        //finalMesh.uv = uv;
        mesh.RecalculateNormals();

    }

}