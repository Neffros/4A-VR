using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace paint
{
    public class PaintTest : MonoBehaviour
    {

        private Mesh mesh;
        public MeshFilter meshFilter;
        private Vector3[] vertices;

        private int[] triangles;

        private int[] indices;

        // Start is called before the first frame update
        void Start()
        {
            mesh = new Mesh();
            meshFilter.mesh = mesh;
            CreateShape();
            mesh.Clear();
            mesh.SetIndices(triangles, MeshTopology.Lines, 0);
            //.vertices = vertices;
            //mesh.triangles = triangles;
        }

        void CreateShape()
        {
            vertices = new Vector3[]
            {
                new Vector3(0, 0, 0),
                new Vector3(1, 0, 0),
                new Vector3(2, 0, 0)
            };

            triangles = new int[]
            {
                0, 1, 2
            };
        }
    }
}