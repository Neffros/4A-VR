using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    using UnityEngine;
using System;
 
//==============================================================================
public class MeshMerger : MonoBehaviour 
{ 
  public static Mesh Merge(MeshFilter filterMesh, MeshFilter currentLineMesh)
  {
    var vertCount = 0;
    var normCount = 0;
    var triCount = 0;
    var uvCount = 0;

    var meshFilters = new List<MeshFilter> {filterMesh, currentLineMesh};
 
    foreach(var mf in meshFilters)
    {
      vertCount += mf.mesh.vertices.Length; 
      normCount += mf.mesh.normals.Length;
      triCount += mf.mesh.triangles.Length; 
      uvCount += mf.mesh.uv.Length;   
    }
    
    var verts = new Vector3[vertCount];
    var norms = new Vector3[normCount];
    var aBones = new Transform[2];
    var bindPoses = new Matrix4x4[2];
    var weights = new BoneWeight[vertCount];
    var tris  = new int[triCount];
    var uvs = new Vector2[uvCount];
 
    var vertOffset = 0;
    var normOffset = 0;
    var triOffset = 0;
    var uvOffset = 0;
    var meshOffset = 0;
    
    foreach(var mf in meshFilters)
    {     
      foreach(var i in mf.mesh.triangles)
      {tris[triOffset++] = i + vertOffset;}
 
      aBones[meshOffset] = mf.transform;
      bindPoses[meshOffset] = Matrix4x4.identity;
 
      foreach(var v in mf.mesh.vertices)
      {
        weights[vertOffset].weight0 = 1.0f;
        weights[vertOffset].boneIndex0 = meshOffset;
        verts[vertOffset++] = v;
      }
 
      foreach(var n in mf.mesh.normals)
      {norms[normOffset++] = n;}
 
      foreach(Vector2 uv in mf.mesh.uv)
      {uvs[uvOffset++] = uv;}
 
      meshOffset++;
    }

    var me = new Mesh
    {
      vertices = verts,
      normals = norms,
      boneWeights = weights,
      uv = uvs,
      triangles = tris,
      bindposes = bindPoses
    };

    return me;

  }
}
}