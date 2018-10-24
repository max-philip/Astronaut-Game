Shader "Unlit/ForceFieldShader"
{
	Properties{
		_MainTex("Texture", 2D) = "white" {}

		// edge weighting for force field
		_EdgeVal("Edge Strength", Range(0.5, 1)) = 0.6
	}
		SubShader
	{

		Tags{ "RenderType" = "Opaque" "Queue" = "Transparent" }

		CGPROGRAM
#pragma surface surf Lambert alpha

		struct Input {
		float3 worldNormal;
		float2 uv_MainTex;
		float3 viewDir;
	};

	fixed _EdgeVal;
	fixed4 _Color;
	sampler2D _MainTex;

	// surface function
	void surf(Input IN, inout SurfaceOutput o) {
		half4 k = tex2D(_MainTex, IN.uv_MainTex);

		float d = abs(dot(normalize(IN.viewDir), normalize(IN.worldNormal)));
		float mul = 1 - d;
		float con = mul * mul * 4;
		float edge = _EdgeVal * con;

		o.Albedo = _Color;
		o.Alpha = k.a * edge;
	}
	ENDCG
	}
}