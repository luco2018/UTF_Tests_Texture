Shader "Checkerboard_5_Alpha"
{
    Properties {
        _Color1 ("Color1", Color) = (1,0,0,1)
		_Color2 ("Color2", Color) = (0,1,0,1)
		_Alpha("Alpha", float) = 0.1
		_CheckerSize("CheckerSize", float) = 5.0
    }
    SubShader
    {
		Lighting Off
		ZTest Always
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
			CGPROGRAM
			#include "UnityCustomRenderTexture.cginc"

			#pragma vertex CustomRenderTextureVertexShader
			#pragma fragment frag
			#pragma target 3.5

			float4 _Color1;
			float4 _Color2;
			float _Alpha;
			float _CheckerSize;

			float4 frag(v2f_customrendertexture IN) : COLOR
			{
				float2 c = IN.localTexcoord * _CheckerSize;
                c = floor(c) / 2;
                float checker = frac(c.x + c.y) * 2;
				float4 result = checker ? _Color1 : _Color2;
				result.a = _Alpha;
				return result;
			}
			ENDCG
        }
    }
}
