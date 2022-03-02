Shader "Unlit/visualizeLengthSmoothstep"
{
    Properties
    {
        _SmoothstepMin("Smoothstep Min", float) = 0.1
        _SmoothstepMax("Smoothstep Max", float) = 1    
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
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            float _SmoothstepMin;
            float _SmoothstepMax;
            
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 fade = length(i.uv.xy - 0.5);
                fixed smoothStepCol = smoothstep(_SmoothstepMin, _SmoothstepMax, fade);
                return smoothStepCol;
            }
            ENDCG
        }
    }
}
