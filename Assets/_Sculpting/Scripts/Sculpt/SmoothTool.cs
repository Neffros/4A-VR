using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace ESGI.ProjetAnnuel.Sculpting
{
    public class SmoothTool : Tool
    {
	    [SerializeField] private Dropdown smoothAlgorithmValue;
        [SerializeField] private Slider times;
        [SerializeField] private Text iterationCount;
        [SerializeField] private GameObject sphere;
		[SerializeField, Range(0f, 1f)] private float hcAlpha = 0.5f;
		[SerializeField, Range(0f, 1f)] private float hcBeta = 0.5f;

		private MeshFilter _filter;
		
		private MeshFilter Filter {
			get {
				if(_filter == null) {
					_filter = sphere.GetComponent<MeshFilter>();
				}
				return _filter;
			}
		}
		
		protected override void CustomStart()
		{
			base.CustomStart();
			_filter = sphere.GetComponent<MeshFilter>();
		}

		protected override void CustomUpdate()
		{
			base.CustomUpdate();
			iterationCount.text = ((int) (times.value)).ToString();
		}

		public void Smooth(){
			switch (smoothAlgorithmValue.value) {
				case 0:
					LaplacianSmooth ();
					break;
				case 1:
					HumphreySmooth ();
					break;
			}
		}

		private void LaplacianSmooth(){
			Filter.mesh = MeshSmoother.LaplacianFilter (Filter.mesh, (int)times.value);
		}

		private void HumphreySmooth(){
			Filter.mesh = MeshSmoother.HcFilter (Filter.mesh, (int)times.value, hcAlpha, hcBeta);
		}
    }
}
