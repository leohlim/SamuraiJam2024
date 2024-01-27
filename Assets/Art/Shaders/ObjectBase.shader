Shader "Samurai Jam 2024/ObjectBase"
{
	Properties
	{
		_Color ("Diffuse Material Color", Color) = (1, 1, 1, 1)
		_SpecColor ("Specular Material Color", Color) = (1, 1, 1, 1)
		_Shininess ("Shininess", Float) = 10
		_ShadowColor ("Shadow Color", Color) = (0, 0, 0, 1)
	}

	SubShader
	{
		Pass
		{
			Tags { "LightMode" = "ForwardBase" }
			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#include "UnityCG.cginc"
			#include "AutoLight.cginc"

			uniform float4 _LightColor0;
			uniform float4 _Color;
			uniform float4 _SpecColor;
			uniform float4 _Shininess;
			uniform float4 _ShadowColor;

			struct vertexInput
			{
				float4 vertex : POSITION;
				float3 normal : NORMAL;
			};

			struct vertexOutput
			{
				float4 pos : SV_POSITION;
				float4 posWorld : TEXCOORD0;
				float3 normalDir : TEXCOORD1;
				LIGHTING_COORDS(2,3)
			};

			vertexOutput vert(vertexInput input)
			{
				vertexOutput output;

				float4x4 modelMatrix = unity_ObjectToWorld;
				float4x4 modelMatrixInverse = unity_WorldToObject;

				output.posWorld = mul(modelMatrix, input.vertex);
				output.pos = UnityObjectToClipPos(input.vertex);
				output.normalDir = normalize(mul(float4(input.normal, 0.0), modelMatrixInverse).xyz);
				TRANSFER_VERTEX_TO_FRAGMENT(output);
				return output;
			}

			float4 frag(vertexOutput input) : COLOR
			{
			
				float3 normalDirection = normalize(input.normalDir);
				float3 viewDirection = normalize(_WorldSpaceCameraPos - input.posWorld.xyz);
				float3 lightDirection;
				float attenuation = LIGHT_ATTENUATION(input);
				
				if(0.0 == _WorldSpaceLightPos0.w) // dir light?
				{
					attenuation = 1.0;
					lightDirection = normalize(_WorldSpaceLightPos0.xyz);
				}

				float3 diffuseReflection = attenuation * _LightColor0.rgb * _Color.rgb * max(0.0, dot(normalDirection, lightDirection));
				float dotVal = max(0.0, dot(normalDirection, lightDirection));

				if(dotVal < 0.25)
					diffuseReflection = _ShadowColor.rgb * attenuation;
				else
					diffuseReflection = _Color.rgb;
			
				return float4(diffuseReflection.rgb, 1.0);
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
}