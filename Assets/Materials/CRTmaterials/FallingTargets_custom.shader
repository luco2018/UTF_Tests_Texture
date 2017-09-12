Shader "CustomTexture/FallingTarget"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Tex("InputTex", 2D) = "white" {}
		_Speed("Pulse Speed", range(0,10)) = 1
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
#pragma target 3.0

	float4		_Color;
	sampler2D	_Tex;
	float _Speed;

	float4 frag(v2f_customrendertexture IN) : COLOR
	{
		float4 color = _Color;// lerp(_Color, float4(1,1,1,1), sin(_Time.z*_Speed));
		return tex2D(_Tex, IN.globalTexcoord.xy)*color;
	}
		ENDCG
	}
	}
}
