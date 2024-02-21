Shader "Zololgo-PaperLands/Standard Color URP" {

	Properties{
		[HDR] _Color("Main Color", Color) = (1,1,1,1)
		[HDR] _RimColor("Rim Color", Color) = (0.5,0.5,0.5,0.0)
		_RimPower("Rim Falloff", Range(0.5,8.0)) = 5.0
	}

		SubShader{

			Tags { "RenderType" = "Opaque" "RenderPipeline" = "UniversalPipeline" }

			HLSLPROGRAM
			#pragma surface surf StandardSpecular fullforwardshadows

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

			struct Input {
				float3 viewDir;
			};

			half4 _Color;
			float4 _RimColor;
			float _RimPower;

			void surf(Input IN, inout SurfaceOutputStandardSpecular o) {
				half rim = 1.0 - saturate(dot(normalize(IN.viewDir), o.Normal));
				o.Emission = _RimColor.rgb * pow(rim, _RimPower);
				o.Albedo = _Color.rgb;
				o.Alpha = _Color.a;
			}

			ENDHLSL

	}

		Fallback "UniversalRenderPipeline"

}