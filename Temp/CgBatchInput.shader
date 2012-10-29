Shader "DUNDON/vertexColor Specular" {
	Properties {
	_Specular ("specular", Color) = (1,1,1,1)
	_Emit ("emit", float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		cull OFF
		LOD 200
		
		CGPROGRAM
		#pragma surface surf BlinnPhong

		sampler2D _MainTex;
		float _Emit;
		float4 _Specular;
		
		struct Input {
			float4 color : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Emission = c.rgb*_Emit;
			o.Specular = _Specular.rgb * 10;
			o.Gloss = _Specular.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
