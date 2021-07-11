using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class SculptTarget : MonoBehaviour
    {
        [SerializeField] private MeshFilter filter;

        public MeshFilter Filter => filter;
    }
}
