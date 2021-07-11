Shader "Unlit/CommonShaderFunctionVisualizer"
{
    Properties
    {
        _Power("Power", Range(1,20)) = 2
        _RcpInput("RCP Input", Range(1,20)) = 10
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

            sampler2D _MainTex;
            float4 _MainTex_ST;
            half _Power;
            half _RcpInput;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                fixed uvXCol = i.uv.x;
                fixed powUVXCol = pow(i.uv.x, _Power);
                fixed rcpUVXCol = rcp(i.uv.x * _RcpInput); 
                return powUVXCol;
            }
            ENDCG
        }
    }
}
