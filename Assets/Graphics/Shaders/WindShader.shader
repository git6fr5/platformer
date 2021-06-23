﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Basic/WindShader"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0

        _AddColor("Add Color", Color) = (1,0,0,1)
        _MultiplyColor("Multiply Color", Color) = (0,1,0,1)

        _LeafTex("Leaf Map", 2D) = "white" {}
        _WindForce("Wind Force", Float) = 0.05
    }

    SubShader
    {
        Tags
		{ 
			"Queue"="Transparent" 
			"IgnoreProjector"="True" 
			"RenderType"="Transparent" 
			"PreviewType"="Plane"
			"CanUseSpriteAtlas"="True"
		}

		Cull Off
		Lighting Off
		ZWrite Off
		Blend One OneMinusSrcAlpha

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
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float round(float x) {
                float y = floor(x + 0.5);
                return y;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _LeafTex;
            float4 _LeafTex_ST;
            float _WindForce;

            float4 _MultiplyColor;
            float4 _AddColor;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 lCol = tex2D(_LeafTex, i.uv);
                float2 offset = lCol.a * float2(_WindForce * sin(_Time[1]), 0);
                fixed4 col = tex2D(_MainTex, i.uv + offset);

                float4 m = _MultiplyColor;
                float4 mCol = col.a * m.a * float4(m.r * col.r, m.g * col.g, m.b * col.b, 1);

                float4 a = _AddColor;
                float4 aCol = col.a * _AddColor;

                float4 o = (col + mCol + aCol) * col.a;
                return o;
            }
            ENDCG
        }
    }
}
