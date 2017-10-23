Shader "TextureCheck"
{
	Properties
	{
		_MainTex ("_MainTex (RGBA)", 2D) = "white" {}
	}
	SubShader
	{
		Tags { "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent" }
		Blend SrcAlpha OneMinusSrcAlpha //Alpha Blend
		Cull Off Lighting Off ZWrite Off

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
				//fixed4 color : COLOR;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				float4 vertex : SV_POSITION;
				//fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			float4 _MainTex_ST;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv*2; //TRANSFORM_TEX(v.uv, _MainTex);
				//o.color = v.color;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float4 black = float4(0,0,0,1);
				float4 tex = tex2D(_MainTex, i.uv);

				fixed right = step(1, i.uv.x);
				fixed left = 1 - right;
				fixed top = step(1, i.uv.y);
				fixed bottom = 1 - top;

				fixed zoneR = left * top;
				fixed zoneG = right * top;
				fixed zoneB = left * bottom;
				fixed zoneA = right * bottom;

				fixed4 colR = tex;
				colR.g = 0;
				colR.b = 0;
				colR.a = 1;
				colR = lerp(black, colR, zoneR);
				colR.a = lerp(1, 0, zoneA);

				fixed4 colG = tex;
				colG.r = 0;
				colG.b = 0;
				colG.a = 1;
				colG = lerp(black, colG, zoneG);
				colG.a = lerp(1, 0, zoneA);

				fixed4 colB = tex;
				colB.r = 0;
				colB.g = 0;
				colB.a = 1;
				colB = lerp(black, colB, zoneB);
				colB.a = lerp(1, 0, zoneA);

				fixed4 colA = tex;
				colA.r = 0;
				colA.g = 0;
				colA.b = 0;
				colA = lerp(black, colA, zoneA);

				float4 result = colR + colG + colB + colA;

				return result;
			}
			ENDCG
		}
	}
}
