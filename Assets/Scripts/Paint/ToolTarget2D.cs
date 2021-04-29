using UnityEngine;

namespace Paint
{
    public class ToolTarget2D : ToolTarget
    {
        private const int TextureSize = 1024;
    
        //public float extendsIslandOffset = 1;
    
        //private RenderTexture _extendIslandsRenderTexture;
        //private RenderTexture _uvIslandsRenderTexture;
        private RenderTexture _maskRenderTexture;
        private RenderTexture _supportTexture;

        private Renderer _renderer;
    
        private int _maskTextureID = Shader.PropertyToID("_MaskTexture");
        //public RenderTexture GetExtend() => _extendIslandsRenderTexture;
        //public RenderTexture GetUVIslands() => _uvIslandsRenderTexture;
        public RenderTexture GetMask() => _maskRenderTexture;
        public RenderTexture GetSupport() => _supportTexture;
        public Renderer GetRenderer() => _renderer;
    
    
        // Start is called before the first frame update
        void Start()
        {
            _maskRenderTexture = new RenderTexture(TextureSize, TextureSize, 0);
            _maskRenderTexture.filterMode = FilterMode.Bilinear;

            //_extendIslandsRenderTexture = new RenderTexture(TextureSize, TextureSize, 0);
            //_extendIslandsRenderTexture.filterMode = FilterMode.Bilinear;

            //_uvIslandsRenderTexture = new RenderTexture(TextureSize, TextureSize, 0);
            //_uvIslandsRenderTexture.filterMode = FilterMode.Bilinear;

            _supportTexture = new RenderTexture(TextureSize, TextureSize, 0);
            _supportTexture.filterMode =  FilterMode.Bilinear;

            _renderer = GetComponent<Renderer>();
            //_renderer.material.SetTexture(_maskTextureID, _extendIslandsRenderTexture);

            PaintGameManager.Instance.InitTextures(this);
        }
    
        void OnDisable(){
            _maskRenderTexture.Release();
            //_uvIslandsRenderTexture.Release();
            //extendIslandsRenderTexture.Release();
            _supportTexture.Release();
        }
        /*
   public List<LayerMask> layers;
   public Material targetMaterial;
   private static readonly int AppliedColor = Shader.PropertyToID("_appliedColor");
   private static readonly int Positions = Shader.PropertyToID("_positions");

  

   private void OnCollisionStay(Collision other)
   {
       Debug.Log("collision");
       foreach (var layer in layers)
       {
           if ((layer & 1 << other.gameObject.layer) != 0)
           {
               other.gameObject.GetComponent<Brush2D>().ApplyEffect(this);
               Color color = Color.black;
               Shader.SetGlobalColor(AppliedColor, color);
                
              
               Vector4[] contactPositions = new Vector4[other.contactCount];
               //Vector3[] co = new Vector3[other.contactCount];
               foreach (var contact in other.contacts)
               {
                   contactPositions.Append(new Vector4(contact.point.x, contact.point.y, contact.point.z, 0));
                   Debug.Log("point:" + contact.point);
               }
               targetMaterial.SetVectorArray(Positions, contactPositions);
           
               renderer.material = targetMaterial;
               break;
           }
       }
   }*/



    }
}
