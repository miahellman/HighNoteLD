Shader "examples/week 5/ufoCircle"
{
    Properties
    {
        _displacement ("displacement", Range(0, 0.1)) = 0.05
        _timeScale ("time scale", Float) = 1
    }

    SubShader
    {
        Tags { "RenderType"="Opaque" }

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            float _scale;
            float _displacement;
            float _timeScale;

            float rand (float2 uv) {
                return frac(sin(dot(uv.xy, float2(12.9898, 78.233))) * 43758.5453123);
            }

            float3 rand_vec (float3 pos)
            {
                //normalize vectors - makes vectors 1 unit long
                //scale so -1,1 values, cancels out frac in return function
                return normalize(float3(rand(pos.xy), rand(pos.yz), rand(pos.zx)) * 2 - 1);
            }

            struct MeshData
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct Interpolators
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float disp : TEXCOORD1;
            };

            Interpolators vert (MeshData v)
            {
                Interpolators o;

                //random vector var
                float rv = rand_vec(v.vertex.xyz + round(_Time.y * _timeScale));

                v.vertex.xyz += rv * _displacement;
                v.vertex.xyz *= rv;

                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            float4 frag (Interpolators i) : SV_Target
            {
                float2 uv = (i.uv) * 2 - 1;

                float3 c = float3(0, 0.8, 0.2);

                float3 output = c;

                output += float3(uv.y, uv.x, uv.x);
                output *= output;

                return float4(output, 1.0);
            }
            ENDCG
        }
    }
}
