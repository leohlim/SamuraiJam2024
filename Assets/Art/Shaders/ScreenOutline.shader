Shader "Samurai Jam 2024/ScreenOutline"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_OutlineColor ("OutlineColor", Color) = (0, 0, 0, 1)
		_Thickness ("Thickness", Range(0, 1)) = 0

		_DepthStrength ("Depth Strength", Range(0, 1)) = 0
		_DepthThickness ("Depth Thickness", Range(0, 1)) = 0
		_DepthThreshold ("Depth Threshold", Range(0, 1)) = 0

		_XWobbleFrequency ("X Wobble Frequency", Range(0, 100)) = 0
		_XWobbleAmplitude ("X Wobble Amplitude", Range(0, 100)) = 0
		_XWobbleShift ("X Wobble Shift", Range(0, 100)) = 0

		_YWobbleFrequency ("X Wobble Frequency", Range(0, 100)) = 0
		_YWobbleAmplitude ("X Wobble Amplitude", Range(0, 100)) = 0
		_YWobbleShift ("X Wobble Shift", Range(0, 100)) = 0
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
			};

			v2f vert(appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			sampler2D _MainTex;
			sampler2D _CameraDepthTexture;

			fixed4 _OutlineColor;
			float _Thickness;

			float _DepthStrength;
			float _DepthThickness;
			float _DepthThreshold;

			float _XWobbleFrequency;
			float _XWobbleAmplitude;
			float _XWobbleShift;

			float _YWobbleFrequency;
			float _YWobbleAmplitude;
			float _YWobbleShift;

			static float2 SobelSamplePoints[9] = {
				float2(-1, 1), float2(0, 1), float2(1, 1),
				float2(1, 0), float2(0, 0), float2(1, 1),
				float2(-1, -1), float2(0, -1), float2(1, -1)
			};

			static float SobelXMatrix[9] = {
				1, 0, -1,
				2, 0, -2,
				1, 0, -1
			};

			static float SobelYMatrix[9] = {
				1, 2, 1,
				0, 0, 0,
				-1, -2, -1
			};

			float rand(float2 co)
			{
				return frac(sin(dot(co.xy, float3(12.9898, 78.233, 45.5432))) * 43758.5453);
			}

			void DepthSobelf(float2 uv, float thickness, out float _out)
			{
				float2 sobel = 0;
				[unroll] for(int i = 0; i < 9; ++i)
				{
					float depth = Linear01Depth(SAMPLE_DEPTH_TEXTURE(_CameraDepthTexture, uv + SobelSamplePoints[i] * thickness));
					sobel += depth * float2(SobelXMatrix[i], SobelYMatrix[i]);
				}
				_out = length(sobel);
			}

			void Blend(fixed4 color1, fixed4 color2, float opacity, out fixed4 color)
			{
				color = lerp(color1, color2, opacity);
			}

			fixed4 frag(v2f i) : SV_TARGET
			{
				const float pi = 3.14159;
				fixed4 col = tex2D(_MainTex, i.uv);

				float yNew = i.uv.y + sin(_YWobbleFrequency * 2 * pi * i.uv.x * _YWobbleShift) * _YWobbleAmplitude;
				i.uv.y = yNew;

				float xNew = i.uv.x + sin(_XWobbleFrequency * 2 * pi * i.uv.y * _XWobbleShift) * _XWobbleAmplitude;
				i.uv.x = xNew;

				float sobel;
				DepthSobelf(i.uv, _Thickness, sobel);

				float SmoothstepResult = smoothstep(0, _DepthThreshold, sobel);
				float PowerResult = pow(SmoothstepResult, _DepthThickness);
				float Opacity = mul(PowerResult, _DepthStrength);

				return lerp(col, _OutlineColor, Opacity);
			}
			ENDCG
		}
	}
}