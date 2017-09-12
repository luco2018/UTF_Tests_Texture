Shader "CustomTexture/Multipass"
{
	SubShader
	{
		Blend One Zero

		Pass
	{
		Name "Pass1Name"

		CGPROGRAM
#include "UnityCustomRenderTexture.cginc"

#pragma vertex CustomRenderTextureVertexShader
#pragma fragment frag
#pragma target 3.5

		float4 frag(v2f_customrendertexture IN) : COLOR
	{
		return float4(1.0, 0.0, 0.0, 1.0);
	}
		ENDCG
	}

		Pass
	{
		Name "Pass2Name"

		CGPROGRAM
#include "UnityCustomRenderTexture.cginc"

#pragma vertex CustomRenderTextureVertexShader
#pragma fragment frag
#pragma target 3.5

		float4 frag(v2f_customrendertexture IN) : COLOR
	{
		return float4(0.0, 1.0, 0.0, 1.0);
	}
		ENDCG
	}

		Pass
	{
		//Name "Pass3Name"

		CGPROGRAM
#include "UnityCustomRenderTexture.cginc"

#pragma vertex CustomRenderTextureVertexShader
#pragma fragment frag
#pragma target 3.5

		float4 frag(v2f_customrendertexture IN) : COLOR
	{
		return float4(0.0, 0.0, 1.0, 1.0);
	}
		ENDCG
	}

		Pass
	{
		Name "Pass4Name"

		CGPROGRAM
#include "UnityCustomRenderTexture.cginc"

#pragma vertex CustomRenderTextureVertexShader
#pragma fragment frag
#pragma target 3.5

		float4 frag(v2f_customrendertexture IN) : COLOR
	{
		return float4(0.0, 0.5, 0.5, 0.5);
	}
		ENDCG
	}
	}
}
