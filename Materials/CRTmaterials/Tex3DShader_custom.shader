Shader "CustomTexture/Tex3DShader custom"
{
	Properties {
		_Color0 ("Color0", Color) = (1,0,0,1)
		_Color1 ("Color1", Color) = (0,1,0,1)
		_Color2 ("Color2", Color) = (0,0,1,1)
		_Color3 ("Color3", Color) = (1,1,0,1)
		_Color4 ("Color4", Color) = (0,1,1,1)
		_Color5 ("Color5", Color) = (1,0,1,1)

		_Factor("Factor", Range(0,1)) = 0
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

			float4		_Color0;
			float4		_Color1;
			float4		_Color2;
			float4		_Color3;
			float4		_Color4;
			float4		_Color5;
			float _Factor;

			float4 frag(v2f_customrendertexture IN) : COLOR
			{
				float4 col = float4(0.0, 0.0, 0.0, 0.0);

				uint index = ((_CustomRenderTexture3DSlice / 32) + frac(_Factor)) * 6 % 6;

				//uint index = ((_CustomRenderTexture3DSlice / 32) + frac(_Time.y)) * 6 % 6; 

				if(index == 0)
				{
					col = _Color0;
				}
				else if(index == 1)
				{
					col = _Color1;
				}
				else if(index == 2)
				{
					col = _Color2;
				}
				else if(index == 3)
				{
					col = _Color3;
				}
				else if(index == 4)
				{
					col = _Color4;
				}
				else if(index == 5)
				{
					col = _Color5;
				}

				return col * IN.globalTexcoord.z * IN.globalTexcoord.y;
			}
			ENDCG
        }
    }
}
