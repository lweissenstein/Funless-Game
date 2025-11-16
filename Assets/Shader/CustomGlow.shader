Shader "Custom/Glow"
{
    Properties
    {
        _BaseColor("Base Color", Color) = (1, 1, 1, 1)
        _BaseMap("Base Map", 2D) = "white" {}
        _GlowColor("Glow Color", Color) = (0, 1, 0, 1)
        _GlowStrength("Glow Strength", Range(0,2)) = 1
        _GlowSpeed("Glow Speed", Range(0,10)) = 3
    }

    SubShader
    {
        // Transparent so alpha is respected
        Tags { "RenderType"="Transparent" "Queue"="Transparent" "RenderPipeline"="UniversalPipeline" }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            HLSLPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            TEXTURE2D(_BaseMap);
            SAMPLER(sampler_BaseMap);

            CBUFFER_START(UnityPerMaterial)
                half4 _BaseColor;
                half4 _GlowColor;
                float _GlowStrength;
                float _GlowSpeed;
            CBUFFER_END

            Varyings vert(Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.uv = IN.uv;
                return OUT;
            }

            half4 frag(Varyings IN) : SV_Target
            {
                // Sample base texture
                half4 texColor = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, IN.uv) * _BaseColor;

                // Pulsing glow factor
                float glow = abs(sin(_Time.y * _GlowSpeed)) * _GlowStrength;

                // Add glow only where sprite is visible (respect alpha)
                texColor.rgb += _GlowColor.rgb * glow * texColor.a;

                return texColor;
            }
            ENDHLSL
        }
    }
}
