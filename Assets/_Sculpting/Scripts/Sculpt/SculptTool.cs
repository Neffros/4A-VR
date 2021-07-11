using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class SculptTool : Tool
    {
        [SerializeField] private Dropdown addOrSubtract;
        [SerializeField] private Toggle symmetryToggle;
        [SerializeField] private Slider strengthLevel;
        [SerializeField] private Slider radiusLevel;
        [SerializeField] private ToolVisualizer toolVisualizer;
        
        private MeshFilter _filter;
        private float _strength;
        private float _radius;
        

        public override void OnRightGripBegin()
        {
            base.OnRightGripBegin();
            _filter.transform.parent.SetParent(RightHand.transform, true);
        }

        public override void OnRightGripEnd()
        {
            base.OnRightGripEnd();
            _filter.transform.parent.SetParent(null, true);
        }

        [SerializeField] private GameObject sphere;
        protected override void CustomUpdate()
        {
            base.CustomUpdate();
            _strength = addOrSubtract.value switch
            {
                0 => strengthLevel.value,
                1 => -strengthLevel.value,
                _ => 2.0f
            };
            _radius = radiusLevel.value;
            toolVisualizer.SetSize(radiusLevel.value);
            var handTransform = RightHand.transform;
            Debug.DrawRay(handTransform.position,  handTransform.forward, Color.red);
            var parent = _filter.transform.parent;
            Debug.DrawRay(parent.position,  parent.forward, Color.red);
        }

        protected override void CustomStart()
        {
            base.CustomStart();
            _strength = strengthLevel.value;
            _radius = radiusLevel.value;
            _filter = sphere.GetComponent<MeshFilter>();
        }

        public override void OnRightTrigger()
        {
            base.OnRightTrigger();
            if (!_filter) return;
            var localPoint = _filter.transform.InverseTransformPoint(RightHand.transform.position);
            var strength = _strength * Time.deltaTime;
            
            DeformMesh(_filter.mesh, localPoint, strength, _radius);
                
            if (symmetryToggle.isOn)
            {
                localPoint.x = -localPoint.x;
                DeformMesh(_filter.mesh, localPoint, strength, _radius);
            }
        }

        private static float Linear(float dis, float radius)
        {
            return Mathf.Clamp01(- dis / radius + 1.0f);
        }

        private static float Squared(float sqrDist, float sqrRadius)
        {
            return - sqrDist / sqrRadius + 1.0f;
        }
        
        private static void DeformMesh(Mesh mesh, Vector3 position, float strength, float radius){
            var vertices = mesh.vertices;
            var normals = mesh.normals;
            var sqrRadius = radius * radius;

            var averageNormal = ComputeAverageNormal(position, radius, vertices, sqrRadius, normals);

            UpdateVertices(position, strength, vertices, sqrRadius, averageNormal);
            
            mesh.vertices = vertices;
            mesh.RecalculateNormals ();
            mesh.RecalculateBounds ();
        }

        private static void UpdateVertices(Vector3 position, float strength, IList<Vector3> vertices, float sqrRadius, Vector3 averageNormal)
        {
            for (var i = 0; i < vertices.Count; i++)
            {
                var sqrMagnitude = (vertices[i] - position).sqrMagnitude;

                if (sqrMagnitude > sqrRadius)
                    continue;
                
                var fallOff = Squared(sqrMagnitude, sqrRadius);

                vertices[i] += averageNormal * (fallOff * strength);
            }
        }

        private static Vector3 ComputeAverageNormal(Vector3 position, float radius, IReadOnlyList<Vector3> vertices, float sqrRadius,
            IReadOnlyList<Vector3> normals)
        {
            var averageNormal = Vector3.zero;
            for (var i = 0; i < vertices.Count; i++)
            {
                var sqrMagnitude = (vertices[i] - position).sqrMagnitude;
                if (sqrMagnitude > sqrRadius)
                    continue;

                var distance = Mathf.Sqrt(sqrMagnitude);
                var fallOff = Linear(distance, radius);
                averageNormal += fallOff * normals[i];
            }

            averageNormal = averageNormal.normalized;
            return averageNormal;
        }
    }
}
