using UnityEngine;
using UnityEngine.Rendering;

namespace Paint
{
    public class PaintGameManager : MonoBehaviour
    {
        public Shader texturePaint;
        //public Shader extendIslands;
        private int _prepareUVID = Shader.PropertyToID("_PrepareUV");
        private int _positionID = Shader.PropertyToID("_PainterPosition");
        private int _hardnessID = Shader.PropertyToID("_Hardness");
        private int _strengthID = Shader.PropertyToID("_Strength");
        private int _radiusID = Shader.PropertyToID("_Radius");
        private int _blendOpID = Shader.PropertyToID("_BlendOp");
        private int _colorID = Shader.PropertyToID("_PainterColor");
        private int _textureID = Shader.PropertyToID("_MainTex");
        private int _uvOffsetID = Shader.PropertyToID("_OffsetUV");
        private int _uvIslandsID = Shader.PropertyToID("_UVIslands");
    
    
    
        private Material paintMaterial;
        //private Material extendMaterial;

        private CommandBuffer buffer;
        #region Singleton

        public static PaintGameManager _instance;    
    
        public static PaintGameManager Instance => _instance;
        private void Awake()
        {
            if (_instance != null && _instance != this)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            paintMaterial = new Material(texturePaint);
            //extendMaterial = new Material(extendIslands);
            buffer = new CommandBuffer();
            buffer.name = "CommmandBuffer - " + gameObject.name;
        }
    

        #endregion


        public void InitTextures(ToolTarget2D toolTarget)
        {
            
            buffer.SetRenderTarget(toolTarget.GetMask());
            //buffer.SetRenderTarget(extend);
            buffer.SetRenderTarget(toolTarget.GetSupport());

            paintMaterial.SetFloat(_prepareUVID, 1);
            buffer.SetRenderTarget(_uvIslandsID);
            buffer.DrawRenderer(toolTarget.GetRenderer(), paintMaterial, 0);

            Graphics.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }

        public void Paint(ToolTarget2D toolTarget, Vector3 pos, float radius = 1f, float hardness = .5f, float strength = .5f,
            Color? color = null)
        {

            Debug.Log("apply paint");
            RenderTexture mask = toolTarget.GetMask();
            //RenderTexture uvIslands = toolTarget.GetUVIslands();
            //RenderTexture extend = toolTarget.GetExtend();
            RenderTexture support = toolTarget.GetSupport();
            Renderer rend = toolTarget.GetRenderer();

            paintMaterial.SetFloat(_prepareUVID, 0);
            paintMaterial.SetVector(_positionID, pos);
            paintMaterial.SetFloat(_hardnessID, hardness);
            paintMaterial.SetFloat(_strengthID, strength);
            paintMaterial.SetFloat(_radiusID, radius);
            paintMaterial.SetTexture(_textureID, support);
            paintMaterial.SetColor(_colorID, color ?? Color.red);
            
            //extendMaterial.SetFloat(_uvOffsetID, toolTarget.extendsIslandOffset);
            //extendMaterial.SetTexture(uvIslandsID, uvIslands);

            buffer.SetRenderTarget(mask);
            buffer.DrawRenderer(rend, paintMaterial, 0);

            buffer.SetRenderTarget(support);
            buffer.Blit(mask, support);

            //buffer.SetRenderTarget(extend);
            //buffer.Blit(mask, extend, extendMaterial);

            Graphics.ExecuteCommandBuffer(buffer);
            buffer.Clear();
        }

    }
}
