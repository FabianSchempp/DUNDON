Shader "DUNDON/vertexColorWithFog" {
	Properties {
	      _FogColor ("Fog Color", Color) = (0.3, 0.4, 0.7, 1.0)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		cull OFF
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert finalcolor:fog vertex:vert

		float4 _FogColor;
		
		struct Input {
			float4 color : COLOR;
			float3 worldPos;
		};
		void vert(inout appdata_full v, out Input data){
		}
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		void fog (Input IN, SurfaceOutput o, inout fixed4 color)
     	 {
          fixed3 fogColor = _FogColor.rgb;
          color.rgb = lerp (color.rgb, fogColor,clamp(IN.worldPos.y * 0.1f,0,1));
      	}
		ENDCG
	} 
	FallBack "Diffuse"
}
