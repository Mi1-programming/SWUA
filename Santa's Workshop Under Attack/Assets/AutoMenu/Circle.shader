Shader "Hidden/Circle"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Prog ("Transition Progress", Float) = 0.5
        _OGTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

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

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float _Prog;
            sampler2D _OGTex;

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = lerp(
                    tex2D(_OGTex, i.uv),
                    tex2D(_MainTex, i.uv),
                    floor(min(distance(i.uv, float2(0.5, 0.5)) / _Prog, 1))
                );
                return col;
            }
            ENDCG
        }
    }
}
