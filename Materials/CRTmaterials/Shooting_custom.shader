Shader "CustomTexture/Shooting"
{
	Properties
	{
		_Color("Color", Color) = (1,1,1,1)
		_Tex("InputTex", 2D) = "white" {}
	_Bullet("Bullet Mark", 2D) = "white" {}
	_Size("Size", float) = 3
	}

		SubShader{
		Lighting Off
		Blend One Zero

		Pass{
		CGPROGRAM
#include "UnityCustomRenderTexture.cginc"
#pragma vertex CustomRenderTextureVertexShader
#pragma fragment frag
#pragma target 3.0

		float4		_Color;
	sampler2D	_Tex;
	sampler2D	_Bullet;
	float3		_BulletUV;
	float		_Size;

	float4 frag(v2f_customrendertexture IN) : COLOR
	{
		float4 col = _Color * tex2D(_Tex, IN.globalTexcoord.xy);

		//float3 bulletUVs = IN.globalTexcoord * _Size - _BulletUV *_Size + _BulletUV / 2;
		float3 bulletUVs = IN.globalTexcoord * _Size - _BulletUV *_Size + _BulletUV / 2;
		float4 bulletMark = tex2D(_Bullet, bulletUVs.xy);
		//float4 oldTex = tex2D(_SelfTexture2D, IN.globalTexcoord.xy);
		return lerp(col, bulletMark, bulletMark.a);
	}
		ENDCG
	}
	}
}
