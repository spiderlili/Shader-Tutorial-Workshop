﻿Shader "Unlit/Visualize UV X"
{
    Properties
    {
        //todo: add dropdown menu for selecting different visualizations
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
                fixed powUVXCol = pow(i.uv.x, 2);
                fixed rcpUVXCol = rcp(i.uv.x * 10); 
                return uvXCol;
            }
            ENDCG
        }
    }
}
