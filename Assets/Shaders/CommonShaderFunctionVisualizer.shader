Shader "Unlit/CommonShaderFunctionVisualizer"
{
    Properties
    {
        [KeywordEnum(Power, RCP)]_VisualizePowerOrRCP ("Visualize Power or RCP", Float) = 0
        _Power ("Power", Range(1, 20)) = 2
        _RcpInput ("RCP Input", Range(1, 20)) = 10
        _DistanceInput ("Distance Input", Range(0, 1)) = 0.5
        _SinInput ("Sin Input", Range(1, 10)) = 10
    }
    SubShader
    {
        Tags { "RenderType" = "Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex: POSITION;
                float2 uv: TEXCOORD0;
            };

            struct v2f
            {
                float2 uv: TEXCOORD0;
                float4 vertex: SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _Power;
            half _RcpInput;
            half _DistanceInput;
            half _VisualizePowerOrRCP;
            half _SinInput;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag(v2f i): SV_Target
            {
                fixed uvXCol = i.uv.x;
                fixed powUVXCol = pow(i.uv.x, _Power);
                fixed rcpUVXCol = rcp(i.uv.x * _RcpInput);
                fixed distUVCol = distance(i.uv.x, _DistanceInput);
                fixed lengthUVCol = length(i.uv.x - _DistanceInput); //same effect as distUVCol, takes 1 vector input
                fixed lengthUVCircle = distance(i.uv.xy, _DistanceInput);
                fixed lengthUVCircleReverse = 1 - distance(i.uv.xy, _DistanceInput);
                fixed sinUVX = sin(i.uv.x * _SinInput) * 0.5 + 0.5; //shift the range from [-1, 1] to [0, 1]
                fixed2 sinUVXY = sin(i.uv.xy * _SinInput) * 0.5 + 0.5;

                return powUVXCol;
                //return distUVCol;
                //return lengthUVCol;
                //return lengthUVCircle;
                //return lengthUVCircleReverse;
                //return sinUVX;
                //return sinUVXY.x * sinUVXY.y;
            }
            ENDCG
            
        }
    }
}
