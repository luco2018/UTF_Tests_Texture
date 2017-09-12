Shader "CustomTexture/Cubemap - Lightning"
{
	Properties {
		_MainTex ("Cubemap", Cube) = "white" {}
		//_Lightning("Lightning", Cube) = "white"{}
		//_Speed1("Speed 1", float) = 0
		//_Speed2("Speed 2", float) = 0
	}

		SubShader
		{
			Lighting Off
			Blend One Zero

			Pass
			{
				CGPROGRAM
				#include "UnityCustomRenderTexture.cginc"

				#pragma vertex CustomRenderTextureVertexShader
				#pragma fragment frag
				#pragma target 3.5

			//float _Speed1;
			//float _Speed2;
			samplerCUBE _MainTex;
			//samplerCUBE _Lightning;
			//samplerCUBE _SelfTextureCube;

			float4 frag(v2f_customrendertexture IN) : COLOR
			{
				float4 col = texCUBE(_MainTex, IN.direction);
				//float4 lightning = texCUBE(_Lightning, IN.direction);
				//lightning = lerp(float4(0, 0, 0, 1), lightning, max(0, sin(_Time.z * 5)));
				

				//float4 doubleBuffered = texCUBE(_SelfTextureCube, IN.direction);


				return col; //+ lightning;
			}
			ENDCG
        }
    }
}
