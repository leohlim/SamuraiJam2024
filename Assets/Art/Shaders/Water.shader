Shader "Samurai Jam 2024/Water"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _ColorA ("Color A", Color) = (1, 1, 1, 1)
        _ColorB ("Color B", Color) = (0, 0, 0, 1)
        _Depth  ("Depth", Range(-100, 100)) = 20
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            sampler2D _CameraDepthTexture;
            float _Depth;

            fixed4 _ColorA, _ColorB;

            bool colCmp(fixed4 colA, fixed4 colB)
            {
                if(colA.r == colB.r && colA.g == colB.g && colA.b == colB.b) return true;
                return false;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                fixed4 colWaterMatch = fixed4(0, 0, 1, 1);
                
                if (colCmp(col, colWaterMatch)) // Solid blue == water
                {
                    fixed4 depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, i.uv));
                    col = lerp(_ColorA, _ColorB, depth*_Depth);
                    return col;
                }

                return col;
            }
            ENDCG
        }
    }
}
