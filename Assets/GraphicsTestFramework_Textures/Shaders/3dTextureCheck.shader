// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "Unlit/3dTextureCheck"
{
	Properties
	{
		_texture ("Texture", 3D) = "white" {}
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float3 uvVol : TEXCOORD0;
			};

			struct v2f
			{
				float3 uvVol : TEXCOORD0;
				float4 vertex : SV_POSITION;
			};

			sampler3D _texture;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uvVol = mul(unity_ObjectToWorld, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				fixed4 col = tex3D(_texture, (i.uvVol+fixed3(0, 0.5, 0.5))*3);
				return col;
			}
			ENDCG
		}
	}
}
