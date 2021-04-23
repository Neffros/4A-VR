Shader "Paint/PaintShaderbackup"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        //_Touched (boolean and such list)
        //_newColor ("get color of it")
        _Position("Position", Vector) = (.0, .0, .0, .0)
        _appliedColor("_appliedColor", Color) = (1,1,1,1)
        [MaterialToggle] _isToggled("isToggle", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };  

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
                float3 worldPos : TEXCOORD1;

            };

            uniform float4 _Position;
            sampler2D _MainTex;
            float4 _MainTex_ST;
            int _positionsCount = 50;
            fixed4 _appliedColor;

            float3 _positions[10000];
             
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                o.worldPos = mul (unity_ObjectToWorld, v.vertex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                
                fixed4 col = tex2D(_MainTex, i.uv);
                for(int j = 0; j < _positionsCount; i++)
                {
                    if(_positions[j] != i.worldPos)
                    {
                        col = _appliedColor;
                        break;
                    }
                }
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
