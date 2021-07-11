using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    [RequireComponent(typeof(MeshFilter), typeof(MeshRenderer))]
    public class GraphicsLineRenderer : MonoBehaviour
    {
        private Mesh _mesh;
        private Vector3 _oldPoint;

        private bool _firstQuad = true;

        private Material LineMaterial { get; set; }
        private float LineSize { get; set; }
        public MeshFilter Filter => GetComponent<MeshFilter>();


        public void AddPoint(Vector3 point)
        {
            if (_oldPoint != Vector3.zero)
            {
                AddLine(_mesh, MakeQuad(point));
                _firstQuad = false;
            }

            _oldPoint = point;
        }

        private void AddLine(Mesh mesh, IReadOnlyList<Vector3> quad)
        {
            var vertices = mesh.vertices;
            var verticesLength = vertices.Length;

            
            var doubleQuadVertexCount = 2 * quad.Count;
            vertices = ResizeArray(vertices, doubleQuadVertexCount);

            for (var i = 0; i < doubleQuadVertexCount; i += 2)
            {
                vertices[verticesLength + i] = quad[i / 2];
                vertices[verticesLength + i + 1] = quad[i / 2];
            }

            var uvs = mesh.uv;
            uvs = ResizeArray(uvs, doubleQuadVertexCount);

            if (_firstQuad)
            {
                uvs[verticesLength] = Vector2.zero;
                uvs[verticesLength + 1] = Vector2.zero;
                uvs[verticesLength + 2] = Vector2.right;
                uvs[verticesLength + 3] = Vector2.right;
                uvs[verticesLength + 4] = Vector2.up;
                uvs[verticesLength + 5] = Vector2.up;
                uvs[verticesLength + 6] = Vector2.one;
                uvs[verticesLength + 7] = Vector2.one;
            }
            else
            {
                if (verticesLength % 8 == 0)
                {
                    uvs[verticesLength] = Vector2.zero;
                    uvs[verticesLength + 1] = Vector2.zero;
                    uvs[verticesLength + 2] = Vector2.right;
                    uvs[verticesLength + 3] = Vector2.right;
                }
                else
                {
                    uvs[verticesLength] = Vector2.up;
                    uvs[verticesLength + 1] = Vector2.up;
                    uvs[verticesLength + 2] = Vector2.one;
                    uvs[verticesLength + 3] = Vector2.one;
                }
            }
            
            var triangles = mesh.triangles;
            var trianglesLength = triangles.Length;

            triangles = ResizeArray(triangles, 12);
            
            if (!_firstQuad)
            {
                verticesLength -= 4;
            }

            // front facing quad
            triangles[trianglesLength] = verticesLength;
            triangles[trianglesLength + 1] = verticesLength + 2;
            triangles[trianglesLength + 2] = verticesLength + 4;
            
            triangles[trianglesLength + 3] = verticesLength + 2;
            triangles[trianglesLength + 4] = verticesLength + 6;
            triangles[trianglesLength + 5] = verticesLength + 4;
            
            // back facing quad
            triangles[trianglesLength + 6] = verticesLength + 5;
            triangles[trianglesLength + 7] = verticesLength + 3;
            triangles[trianglesLength + 8] = verticesLength + 1;
            
            triangles[trianglesLength + 9] = verticesLength + 5;
            triangles[trianglesLength + 10] = verticesLength + 7;
            triangles[trianglesLength + 11] = verticesLength + 3;

            mesh.vertices = vertices;
            mesh.uv = uvs;
            mesh.triangles = triangles;
            mesh.RecalculateBounds();
            mesh.RecalculateNormals();
        }

        private static T[] ResizeArray<T>(IReadOnlyList<T> data, int delta)
        {
            var newData = new T[data.Count + delta];
            for (var i = 0; i < data.Count; i++)
            {
                newData[i] = data[i];
            }

            return newData;
        }

        private Vector3[] MakeQuad(Vector3 point)
        {
            var vertexCount = _firstQuad ? 4 : 2;
            var quad = new Vector3[vertexCount];

            var n = Vector3.Cross(_oldPoint, point);
            var displacement = Vector3.Cross(n, point - _oldPoint).normalized;

            displacement *= LineSize / 2;

            if (_firstQuad)
            {
                quad[0] = transform.InverseTransformPoint(_oldPoint + displacement);
                quad[1] = transform.InverseTransformPoint(_oldPoint - displacement);
                quad[2] = transform.InverseTransformPoint(point + displacement);
                quad[3] = transform.InverseTransformPoint(point - displacement);
            }
            else
            {
                quad[0] = transform.InverseTransformPoint(point + displacement);
                quad[1] = transform.InverseTransformPoint(point - displacement);
            }

            return quad;
        }

        public void Init(float lineSize, Material material)
        {
            LineMaterial = material;
            LineSize = lineSize;
            
            _mesh = GetComponent<MeshFilter>().mesh;
            GetComponent<MeshRenderer>().material = LineMaterial;
        }
    }
}
