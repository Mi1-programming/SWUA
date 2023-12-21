Shader "Unlit/Grid"
{
    Properties
    {
        _Color ("Color", Color) = (1, 1, 1, 0)
        _Size("Size", Int) = 8
        _Gap("Gap", Float) = 0.5
        _GapVariance("Gap Variance", Float) = 0.01
        _GapVarianceVariance("Gap Variance Variance", Float) = 3.0
        _GapVarianceSpeed("Gap Variance Speed", Float) = 4.0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
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
                float2 uv : TEXCOORD0;
            };
            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float4 _Color;
            int _Size;
            float _Gap;
            float _GapVariance;
            float _GapVarianceVariance;
            float _GapVarianceSpeed;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }
            fixed4 frag(v2f i) : SV_Target
            {
                float variantGap = _Gap - sin((_Time.y + (i.uv.x + i.uv.y) * _GapVarianceVariance) * _GapVarianceSpeed) * _GapVariance;
                float modX = (i.uv.x * _Size * 4) % 1;
                float modY = (i.uv.y * _Size * 4) % 1;
                if (modX > variantGap || modX < 1 - variantGap) return _Color;
                if (modY > variantGap || modY < 1 - variantGap) return _Color;
                return fixed4(0, 0, 0, 0);
            }
            ENDCG
        }
    }
}
