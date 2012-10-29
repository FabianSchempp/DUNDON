Shader "DUNDON/vertexColor glow 5" {
	Properties {
	_Emit ("emit", float) = 0
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		cull OFF
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert
		float _Emit;
		
		struct Input {
			float4 color : COLOR;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = c.a;
			o.Emission = c.rgb*_Emit;
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha
		
		float _CycleA;
		float _AlphaA;
		
		struct Input {
			float4 color : COLOR;
		};
		
		void vert (inout appdata_full v, out Input o) {
				v.vertex.xyz += v.normal * (_CycleA);
		}
			
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = _AlphaA;
			o.Emission = c.rgb;
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha
		
		float _CycleB;
		float _AlphaB;
		
		struct Input {
			float4 color : COLOR;
		};
		
		void vert (inout appdata_full v, out Input o) {
				v.vertex.xyz += v.normal * (_CycleB);
		}
			
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = _AlphaB;
			o.Emission = c.rgb;
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha
		
		float _CycleC;
		float _AlphaC;
		
		struct Input {
			float4 color : COLOR;
		};
		
		void vert (inout appdata_full v, out Input o) {
				v.vertex.xyz += v.normal * (_CycleC);
		}
			
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = _AlphaC;
			o.Emission = c.rgb;
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha
		
		float _CycleD;
		float _AlphaD;
		
		struct Input {
			float4 color : COLOR;
		};
		
		void vert (inout appdata_full v, out Input o) {
				v.vertex.xyz += v.normal * (_CycleD);
		}
			
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = _AlphaD;
			o.Emission = c.rgb;
		}
		ENDCG
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert alpha
		
		float _CycleE;
		float _AlphaE;
		
		struct Input {
			float4 color : COLOR;
		};
		
		void vert (inout appdata_full v, out Input o) {
				v.vertex.xyz += v.normal * (_CycleE);
		}
			
		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = IN.color;
			o.Albedo = c.rgb;
			o.Alpha = _AlphaE;
			o.Emission = c.rgb;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
