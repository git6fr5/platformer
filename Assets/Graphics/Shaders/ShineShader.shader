﻿// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Basic/ShineShader"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}
        [MaterialToggle] PixelSnap("Pixel snap", Float) = 0

        _AddColor("Add Color", Color) = (1,0,0,1)
        _MultiplyColor("Multiply Color", Color) = (0,1,0,1)
        _Opacity("Opacity", Float) = 1

        [PerRendererData] _ShineTex("Shine Texture", 2D) = "white" {}
        _ShineColor("Shine Color", Color) = (1,1,1,1)
        _ShineOpacity("Shine Opacity", Float) = 1
        _ShineRadius("Shine Radius", Float) = 0.1
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

        Pass // Outline
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

            float3 stretch(float3 vec, float x, float y)
            {
                float2x2 stretchMatrix = float2x2(x, 0, 0, y);
                return float3(mul(stretchMatrix, vec.xy), vec.z).xyz;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            sampler2D _ShineTex;
            float4 _ShineTex_ST;

            float _ShineRadius;
            float4 _ShineColor;
            float _ShineOpacity;

            v2f vert(appdata v)
            {

                float3 vertex = stretch(v.vertex, _ShineRadius, _ShineRadius);

                v2f o;
                o.vertex = UnityObjectToClipPos(vertex);
                o.uv = TRANSFORM_TEX(v.uv, _ShineTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_ShineTex, i.uv).a;                                
                

                return col;
            }
            ENDCG
        }
    
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

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _MultiplyColor;
            float4 _AddColor;

            float _Opacity;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);

                float4 m = _MultiplyColor;
                float4 mCol = col.a * m.a * float4(m.r * col.r, m.g * col.g, m.b * col.b, 1);

                float4 a = _AddColor;
                float4 aCol = col.a * _AddColor;

                float4 o = (col + mCol + aCol) * col.a * _Opacity;
                return o;
            }
            ENDCG
        }
    }
}
