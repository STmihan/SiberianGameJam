Shader "Hidden/DZ"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _CirclePos ("Circle Position", Vector) = (0,0,0,0)
        _CircleRadius ("Radius", Float) = 0.5
        _CircleEdgeThickness ("Edge Thickness", Float) = 0.03
        [HDR] _CircleColor ("Circle Color", Color) = (1,1,1,1)
        _InnerColor ("Inner Color", Color) = (1,1,1,1)
        _NoiseThreshold ("Noise Threshold", Float) = 0.5
    }
    SubShader
    {
        Tags
        {
            "Queue" = "Transparent" "RenderType" = "Transparent" "RenderPipeline" = "UniversalPipeline"
        }
        Blend SrcAlpha OneMinusSrcAlpha, One OneMinusSrcAlpha
        // No culling or depth
        Cull Off ZWrite Off ZTest Always

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"
            #include "noises/perlin.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 world_pos : WORLDPOS;
            };

            v2f vert(appdata v)
            {
                v2f o;
                o.world_pos = mul(unity_ObjectToWorld, v.vertex);
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            sampler2D _MainTex;
            float2 _CirclePos;
            float _CircleRadius;
            float _CircleEdgeThickness;
            float4 _CircleColor;
            float4 _InnerColor;
            float _NoiseScale;
            float _NoiseThreshold;

            float4 permute(float4 x) { return mod(((x * 34.0) + 1.0) * x, 289.0); }
            float permute(float x) { return floor(mod(((x * 34.0) + 1.0) * x, 289.0)); }

            float snoise(float2 v)
            {
                const float4 C = float4(0.211324865405187, 0.366025403784439,
                    -0.577350269189626, 0.024390243902439);
                float2 i = floor(v + dot(v, C.yy));
                float2 x0 = v - i + dot(i, C.xx);
                float2 i1;
                i1 = (x0.x > x0.y) ? float2(1.0, 0.0) : float2(0.0, 1.0);
                float4 x12 = x0.xyxy + C.xxzz;
                x12.xy -= i1;
                i = mod(i, 289.0);
                float3 p = permute(permute(i.y + float3(0.0, i1.y, 1.0))
                    + i.x + float3(0.0, i1.x, 1.0));
                float3 m = max(0.5 - float3(dot(x0, x0), dot(x12.xy, x12.xy),
                        dot(x12.zw, x12.zw)), 0.0);
                m = m * m;
                m = m * m;
                float3 x = 2.0 * fract(p * C.www) - 1.0;
                float3 h = abs(x) - 0.5;
                float3 ox = floor(x + 0.5);
                float3 a0 = x - ox;
                m *= 1.79284291400159 - 0.85373472095314 * (a0 * a0 + h * h);
                float3 g;
                g.x = a0.x * x0.x + h.x * x0.y;
                g.yz = a0.yz * x12.xz + h.yz * x12.yw;
                return 130.0 * dot(m, g);
            }


            fixed4 frag(v2f i) : SV_Target
            {
                fixed4 col = tex2D(_MainTex, i.uv);
                // To grayscale
                col.rgb = col.r * 0.3 + col.g * 0.59 + col.b * 0.11;
                // col.rgb = col.rrr;
                float dist = distance(i.world_pos.xy, _CirclePos);
                float2 inp = normalize(col.xy / 2 + 0.5);
                float noise = perlin(inp, 2);
                noise /= 1;
                dist -= noise;
                if (dist > _CircleRadius)
                {
                    col.a = 0;
                }
                else
                {
                    if (dist > _CircleRadius - _CircleEdgeThickness)
                    {
                        col.rgb = _CircleColor.rgb;
                    }
                    // else
                    // {
                    //     col.rgb = col.rgb;
                    // }
                }

                return col;
            }
            ENDCG
        }
    }
}