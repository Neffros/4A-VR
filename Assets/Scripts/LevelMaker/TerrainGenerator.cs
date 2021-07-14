using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.XR.Interaction.Toolkit;



public enum ChunkSize
{
    x32=32,
    x64=64,
    x128=128,
    x256=256,
    x512=512,
    x1024=1024,
    x2048=2048
};
public class TerrainGenerator : MonoBehaviour
{
    public GameObject VRRig;
    public GameObject player;
    public float playerChunkRadius = 50f;
    public Texture2D hMap;
    public ChunkSize ChunkSize;
    public int heightScale = 100;
    public Material terrainMat;
    public Gradient gradient;
    
    private Texture2D colorText;
    private Color[] pixels;
    private float minTerrainHeight;
    private float maxTerrainHeight;
    private int chunkSize;
    private Mesh mesh; //= new Mesh();
    private Vector3 playerSpawnpoint;

    private List<GameObject> chunks;

    public Vector2 centerPosition;
    //private bool isGenerated;

    private void Start()
    {
        chunks = new List<GameObject>();
        pixels = hMap.GetPixels();
        chunkSize = (int) ChunkSize;
        GenerateAllChunks();
        VRRig.transform.position = playerSpawnpoint;
    }

    private void GenerateAllChunks()
    {
        int chunkAmount = hMap.width / chunkSize;
        Debug.Log(chunkAmount);
        for (int i = 0; i < chunkAmount; i++)
        {
            for (int j = 0; j < chunkAmount; j++)
            {
                GenerateChunk(i*chunkSize, j*chunkSize);
            }
        }
        
    }
    private void GenerateChunk(int startPointX, int startPointY)
    {
        List<Vector3> verts = new List<Vector3>();
        List<int> indices = new List<int>();
        
        Debug.Log(startPointX+chunkSize);
        colorText = new Texture2D(chunkSize, chunkSize);
        for (int x = startPointX; x < startPointX+chunkSize; x++)
        {
            for (int y = startPointY; y < startPointY+chunkSize; y++)
            {
                float height = pixels[y * hMap.width + x].grayscale * heightScale;
                Vector3 currVert = new Vector3(x - startPointX, height, y-startPointY);
                verts.Add(currVert);
                colorText.SetPixel(x, y, Color.red);
                
                if (x == startPointX || y == startPointY) continue;
                
                indices.Add(chunkSize * (y - startPointY)+ x - startPointX);
                indices.Add(chunkSize * (y - startPointY) + x - 1 - startPointX);
                indices.Add(chunkSize * (y - 1  - startPointY) + x - 1 - startPointX);
                indices.Add(chunkSize * (y - 1  - startPointY) + x - 1 - startPointX);
                indices.Add(chunkSize * (y - 1  - startPointY) + x - startPointX);
                indices.Add(chunkSize * (y - startPointY) + x - startPointX);

                if (height > maxTerrainHeight)
                    maxTerrainHeight = height;
                if (height < minTerrainHeight)
                    minTerrainHeight = height;
            }
        }
        Color[] colors = new Color[verts.Count];
        for (int i = 0; i < verts.Count; i++)
        {
            float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, verts[i].y);
            colors[i] = gradient.Evaluate(height);
        }

        GameObject plane = new GameObject("Chunk " + chunks.Count);
        chunks.Add(plane);
        plane.AddComponent<MeshFilter>();
        plane.AddComponent<MeshRenderer>();
        mesh = new Mesh();
        //mesh.indexFormat = IndexFormat.UInt32;
        Debug.Log("verts:" + verts.Count);
        Debug.Log("indices:" + indices.Count);
        Debug.Log("max indices:" + indices.Max());
        mesh.Clear();
        mesh.SetVertices(verts);
        mesh.SetIndices(indices.ToArray(), MeshTopology.Triangles, 0);
        mesh.colors = colors;
        //finalMesh.uv = uv;
        mesh.RecalculateNormals();
        
        plane.GetComponent<MeshRenderer>().material = terrainMat;
        plane.GetComponent<MeshFilter>().mesh = mesh;
        plane.AddComponent<MeshCollider>();
        plane.GetComponent<MeshCollider>().convex = true;
        
        plane.AddComponent<TeleportationArea>();
        plane.transform.position = new Vector3(startPointX, 0, startPointY);
        if(chunks.Count == (hMap.width/chunkSize * hMap.width/chunkSize)/2)
            playerSpawnpoint = new Vector3(startPointX + chunkSize/2, maxTerrainHeight/2, startPointY + chunkSize/2);
    }

    private void Update()
    {
        foreach (var chunk in chunks)
        {
            Vector3 chunkPos = chunk.transform.position;
            chunk.SetActive(Vector3.Distance(chunkPos, player.transform.position) < playerChunkRadius);
        }
    }
}