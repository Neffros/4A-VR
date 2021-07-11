using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
public class Sphere : MonoBehaviour
{
	public float radius = 0.5f;
	public int nbLong = 200;	
	public int nbLat = 200;	

	private void Awake()
	{
		CreateSphere();
	}

	private void CreateSphere()
	{
		var filter = GetComponent<MeshFilter>();
		var mesh = filter.mesh;
		mesh.Clear();

		var vertices = CreateVertices();
		var normals = CreateNormals(vertices);
		var uvs = CreateUVs(vertices);
		var triangles = CreateTriangles(vertices);

		UpdateMesh(mesh, vertices, normals, uvs, triangles);
	}

	private static void UpdateMesh(Mesh mesh, Vector3[] vertices, Vector3[] normals, Vector2[] uvs, int[] triangles)
	{
		mesh.vertices = vertices;
		mesh.normals = normals;
		mesh.uv = uvs;
		mesh.triangles = triangles;

		mesh.RecalculateBounds();
	}

	private int[] CreateTriangles(IReadOnlyCollection<Vector3> vertices)
	{
		var nbFaces = vertices.Count;
		var nbTriangles = nbFaces * 2;
		var nbIndexes = nbTriangles * 3;
		var triangles = new int[nbIndexes];

		//Top Cap
		var i = 0;
		for (var lon = 0; lon < nbLong; lon++)
		{
			triangles[i++] = lon + 2;
			triangles[i++] = lon + 1;
			triangles[i++] = 0;
		}

		//Middle
		for (var lat = 0; lat < nbLat - 1; lat++)
		{
			for (var lon = 0; lon < nbLong; lon++)
			{
				var current = lon + lat * (nbLong + 1) + 1;
				var next = current + nbLong + 1;

				triangles[i++] = current;
				triangles[i++] = current + 1;
				triangles[i++] = next + 1;

				triangles[i++] = current;
				triangles[i++] = next + 1;
				triangles[i++] = next;
			}
		}

		//Bottom Cap
		for (var lon = 0; lon < nbLong; lon++)
		{
			triangles[i++] = vertices.Count - 1;
			triangles[i++] = vertices.Count - (lon + 2) - 1;
			triangles[i++] = vertices.Count - (lon + 1) - 1;
		}

		return triangles;
	}

	private Vector2[] CreateUVs(IReadOnlyCollection<Vector3> vertices)
	{
		var uvs = new Vector2[vertices.Count];
		uvs[0] = Vector2.up;
		uvs[uvs.Length - 1] = Vector2.zero;
		for (var lat = 0; lat < nbLat; lat++)
		for (var lon = 0; lon <= nbLong; lon++)
			uvs[lon + lat * (nbLong + 1) + 1] = new Vector2((float) lon / nbLong, 1f - (float) (lat + 1) / (nbLat + 1));
		return uvs;
	}

	private static Vector3[] CreateNormals(IReadOnlyList<Vector3> vertices)
	{
		var normals = new Vector3[vertices.Count];
		for (var n = 0; n < vertices.Count; n++)
		{
			normals[n] = vertices[n].normalized;
		}
		return normals;
	}

	private Vector3[] CreateVertices()
	{
		var vertices = new Vector3[(nbLong + 1) * nbLat + 2];
		const float pi = Mathf.PI;
		const float _2pi = pi * 2f;

		vertices[0] = Vector3.up * radius;
		for (var lat = 0; lat < nbLat; lat++)
		{
			var a1 = pi * (lat + 1) / (nbLat + 1);
			var sin1 = Mathf.Sin(a1);
			var cos1 = Mathf.Cos(a1);

			for (var lon = 0; lon <= nbLong; lon++)
			{
				var a2 = _2pi * (lon == nbLong ? 0 : lon) / nbLong;
				var sin2 = Mathf.Sin(a2);
				var cos2 = Mathf.Cos(a2);

				vertices[lon + lat * (nbLong + 1) + 1] = new Vector3(sin1 * cos2, cos1, sin1 * sin2) * radius;
			}
		}

		vertices[vertices.Length - 1] = Vector3.up * -radius;
		return vertices;
	}
}
}
