Shader "DUNDON/Clouds" {
Properties {
	_a ("Layer a", 2D) = "white" {}
	_Emit ("Emission factor",Range(0,1)) = 0
	_speed ("speed",float) = 1
}

Category {
	Blend SrcAlpha One
	Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
 	Lighting Off ZWrite Off Fog { Color (0,0,0,0) }
    Cull back
	LOD 300
	
	SubShader {
		Pass {
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma fragmentoption ARB_precision_hint_fastest
			#pragma multi_compile_particles

			#include "UnityCG.cginc"

			sampler2D _a;
			float _Emit;
			float _speed;
			
			struct appdata_t {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};

			struct v2f {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
			
			float4 _a_ST;

			v2f vert (appdata_t v)
			{
				v2f o;
				o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
				o.texcoord = TRANSFORM_TEX(v.texcoord,_a);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				return 2.0f * tex2D(_a, i.texcoord);
			}
			ENDCG 
		}
	} 	
	
	// ---- Dual texture cards
	SubShader {
		Pass {
			SetTexture [_a] {
				combine constant * primary
			}
			SetTexture [_a] {
				combine texture * previous DOUBLE
			}
		}
	}
	
	// ---- Single texture cards (does not do color tint)
	SubShader {
		Pass {
			SetTexture [_a] {
				combine texture * primary
			}
		}
	}

}
}