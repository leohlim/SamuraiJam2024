Shader "Samurai Jam 2024/Simple Depth Gradient"
{
    Properties
    {
        _ColorA ("ColorA", Color) = (1, 1, 1, 1)
        _ColorB ("ColorB", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
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
                float depth : TEXCOORD1;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                o.depth = COMPUTE_DEPTH_01;
                // COMPUTE_EYEDEPTH(o.depth);

                return o;
            }

            sampler2D _CameraDepthTexture;
            fixed4 _ColorA, _ColorB;

            fixed4 frag (v2f i) : SV_Target
            {
                // return lerp(_ColorA, _ColorB, depth);
                return fixed4(i.depth, i.depth , i.depth, 1.0);
            }
            ENDCG
        }
    }
}
