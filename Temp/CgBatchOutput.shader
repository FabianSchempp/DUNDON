Shader "DUNDON/vertexColorGUI" {
	Properties {
	_Color ("color",Color) = (1,0,0,1)
	}
	Category {
	Tags { "Queue"="Geometry" "IgnoreProjector"="True" "RenderType"="Opaque" }
	ColorMask RGBA
	Cull Back
   	Lighting off
	BindChannels {
		Bind "Color", color
		Bind "Vertex", vertex
	}

	SubShader {
		Pass {
             color(1,1,1,1)
		}
	}
}
}

//Shader "DUNDON/vertexColorGUI" {
//	Properties {
//	_Alpha("Alpha", float) = 1
//	}
//	SubShader {
//		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
//		cull OFF
//		LOD 200
//		
//		CGPROGRAM
//		#pragma surface surf Unlit alpha
//		struct Input {
//				float4 color: COLOR;
//			};
//			
//			struct UnlitSurfaceOutput {
//				half3 Albedo;
//				half3 Emission;
//				half3 Normal;
//				half3 Gloss;
//				half Specular;
//				half Alpha;
//			};
//		float _Alpha;
//		
//			inline half4 LightingUnlit (UnlitSurfaceOutput s, half3 lightDir, half3 viewDir, half atten)
//			{
//		        half4 c ;
//		        	c.rgb = s.Albedo;
//		        	c.a = s.Alpha;
//		        return c;
//			}
//
//		void surf (Input IN, inout UnlitSurfaceOutput o) {
//			o.Albedo = IN.color;
//			o.Alpha = _Alpha;
//		}
//		ENDCG
//	} 
//	FallBack "Diffuse"
//}
