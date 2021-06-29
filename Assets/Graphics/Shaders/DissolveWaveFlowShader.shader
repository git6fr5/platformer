Shader "Basic/DissolveWaveFlowShader"
{
    Properties
    {
        [PerRendererData] _MainTex("Sprite Texture", 2D) = "white" {}

        _DissolveValue("Dissolve Value", Float) = 1
        _DissolveTex("Dissolve Texture", 2D) = "white" {}

        _FlowSpeed("Flow Speed", Float) = 1
        _WaveAmplitude("Wave Amplitude", Float) = 1
        _WaveDensity("Wave Density", Float) = 1
        _Horizontal("Horizontal", Float) = 1
        _Vertical("Vertical", Float) = 0
    }

    SubShader
    {
        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
            "PreviewType" = "Plane"
            "CanUseSpriteAtlas" = "True"
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

            float3 stretch(float3 vec, float x, float y)
            {
                float2x2 stretchMatrix = float2x2(x, 0, 0, y);
                return float3(mul(stretchMatrix, vec.xy), vec.z).xyz;
            };

            float modulus(float x, float y)
            {
                return x - y * floor(x / y);
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            float _DissolveValue;
            sampler2D _DissolveTex;
            float4 _DissolveTex_ST;

            float _FlowSpeed;
            float _WaveAmplitude;
            float _WaveDensity;
            float _Horizontal;
            float _Vertical;

            fixed4 frag(v2f i) : SV_Target
            {
                // parse the data
                float flowSpeed = _FlowSpeed;
                float waveAmp = _WaveAmplitude;
                float waveDens = _WaveDensity;

                // flow along x and wave along y
                float2 offsetX = float2(_Time[1] * flowSpeed, sin(i.uv.x * UNITY_PI * 2 * waveDens + _Time[1])* waveAmp);
                float2 offsetY = float2(sin(i.uv.y * UNITY_PI * 2 * waveDens + _Time[1]) * waveAmp, _Time[1] * flowSpeed);
                float2 offset = offsetX * _Horizontal + offsetY * _Vertical;
                
                float2 pos = float2(modulus(offset.x + i.uv.x, 1), modulus(offset.y + i.uv.y, 1));
                
                // output
                float4 o = tex2D(_MainTex, pos);
                float4 d = tex2D(_DissolveTex, pos);
                float dissolve = _DissolveValue * ceil(max(0, _DissolveValue - (0.95 * d.r) ));
                o = o * o.a * dissolve;
                return o;
            }
            ENDCG
        }
    }
}
