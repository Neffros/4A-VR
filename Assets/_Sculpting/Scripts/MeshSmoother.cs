using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public static class MeshSmoother
    {
        public static Mesh LaplacianFilter (Mesh mesh, int times = 1) {
	        mesh.vertices = LaplacianFilter(mesh.vertices, mesh.triangles, times);
			mesh.RecalculateNormals();
			mesh.RecalculateBounds();
			return mesh;
		}

        private static Vector3[] LaplacianFilter(Vector3[] vertices, int[] triangles, int times) {
	        var network = MeshHelper.BuildNetwork(triangles);
	        for(var i = 0; i < times; i++) {
		        vertices = LaplacianFilter(network, vertices);
	        }
	        return vertices;
        }

        private static Vector3[] LaplacianFilter(IReadOnlyDictionary<int, MeshHelper> network, IReadOnlyList<Vector3> origin) {
	        var vertices = new Vector3[origin.Count];
	        for(int i = 0, n = origin.Count; i < n; i++) {
		        var connection = network[i].Connection;
		        var v = Vector3.zero;
		        foreach(var adj in connection) {
			        v += origin[adj];
		        }
		        vertices[i] = v / connection.Count;
	        }
	        return vertices;
        }
        
        public static Mesh HcFilter (Mesh mesh, int times = 5, float alpha = 0.5f, float beta = 0.75f) {
	        mesh.vertices = HcFilter(mesh.vertices, mesh.triangles, times, alpha, beta);
	        mesh.RecalculateNormals();
	        mesh.RecalculateBounds();
	        return mesh;
        }

        private static Vector3[] HcFilter(Vector3[] vertices, int[] triangles, int times, float alpha, float beta) {
	        alpha = Mathf.Clamp01(alpha);
	        beta = Mathf.Clamp01(beta);

	        var network = MeshHelper.BuildNetwork(triangles);

	        var oldVertices = new Vector3[vertices.Length];
	        Array.Copy(vertices, oldVertices, vertices.Length);
	        for(var i = 0; i < times; i++) {
		        vertices = HcFilter(network, oldVertices, vertices, alpha, beta);
	        }
	        return vertices;
        }

        private static Vector3[] HcFilter(IReadOnlyDictionary<int, MeshHelper> network, IReadOnlyList<Vector3> oldVertices, IReadOnlyList<Vector3> vertices, float alpha, float beta) {
	        var laplacianVertices = LaplacianFilter(network, vertices);
	        var buffer = new Vector3[oldVertices.Count];

	        for(var i = 0; i < laplacianVertices.Length; i++) {
		        buffer[i] = laplacianVertices[i] - (alpha * oldVertices[i] + (1f - alpha) * vertices[i]);
	        }

	        for(var i = 0; i < laplacianVertices.Length; i++) {
		        var adjacents = network[i].Connection;
		        var position = Vector3.zero;
		        foreach(var adj in adjacents) {
			        position += buffer[adj];
		        }
		        laplacianVertices[i] = laplacianVertices[i] - (beta * buffer[i] + (1 - beta) / adjacents.Count * position);
	        }

	        return laplacianVertices;
        }
    }
}