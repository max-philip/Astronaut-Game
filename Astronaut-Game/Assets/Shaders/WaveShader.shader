// Shader to give the effect of clear waves. The solution provided for Lab 4, as well as
// our water shader for Project 1, was used as a base. Parameters changed to suit the
// desired wave effect.

// Wave calculation changed to model the Gerstner wave function, rather than just a sin
// wave. This gives a more visually appealing effect. Further changes were made to
// parameters such as texture tint to suit the environment.

// Clear water effect was achieved with reference to the official Unity tutorial: 
// "Making A Transparent Shader".

Shader "Unlit/WaveShader"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_TintColor("Tint", Color) = (1,1,1,1)
		_Transparency("Transparency", Range(0.5,1.0)) = 0.75
	}
	SubShader
	{
		Tags{"RenderType" = "Transparent" "Queue" = "Transparent"}
		Blend SrcAlpha OneMinusSrcAlpha

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			float4 _TintColor;
			float _Transparency;
			sampler2D _MainTex;

			struct vertIn
			{
				float4 vertex : POSITION;
				float4 uv : TEXCOORD0;
			};

			struct vertOut
			{
				float4 vertex : SV_POSITION;
				float4 uv : TEXCOORD0;
			};

			// Implementation of the vertex shader
			vertOut vert(vertIn v)
			{
				vertOut o;

				// Amplitude and frequency values for the waves
				float amp = 0.15;
				float freq = 1;

				// Gerstner wave calculation
				v.vertex.y = sin(v.vertex.x + _Time.y)  * amp;
				v.vertex.x += cos(v.vertex.x + _Time.y) * amp * 0.5;
				v.vertex.z += cos(v.vertex.x + _Time.y) * amp * 0.5;

				// Displace the original vertex in model space
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}

			// Implementation of the fragment shader
			fixed4 frag(vertOut v) : SV_Target
			{
			fixed4 col = tex2D(_MainTex, v.uv) + _TintColor;
			col.a = _Transparency;
			return col;
			}
			ENDCG
		}
	}
}