Shader "DUNDON/vertexColor" {
	Properties {
	_Color ("color",Color) = (1,0,0,1)
//	_Emit ("emit", float) = 0
	}
	Category {
	Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Opaque" }
	ColorMask RGB
	Cull OFF
   	ColorMaterial AmbientAndDiffuse
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
	}

	SubShader {
		Pass {
			  Material {
                Diffuse (1,1,1,1)
                Ambient (1,1,1,1)
                Emission[_Color]
            }
            Lighting On
		}
	}
}
}

//Shader "DUNDON/vertexColor" {
//	Properties {
//	_Color ("color",Color) = (1,0,0,1)
//	_Emit ("emit", float) = 0
//	}
//	SubShader {
//		Tags { "RenderType"="Opaque" }
//		cull OFF
//		LOD 200
//		
//		CGPROGRAM
//		#pragma surface surf Lambert
//
//		sampler2D _MainTex;
//		float _Emit;
//		half4 _Color;
//		
//		struct Input {
//			float4 color : COLOR;
//		};
//
//		void surf (Input IN, inout SurfaceOutput o) {
//			half4 c = IN.color;
//			o.Albedo = c.rgb;
//			o.Alpha = c.a;
//			o.Emission = _Color*_Emit;
//		}
//		ENDCG
//	} 
//	FallBack "Diffuse"
//}
