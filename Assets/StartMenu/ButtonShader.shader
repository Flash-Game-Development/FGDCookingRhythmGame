Shader "Unlit/ButtonShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Tint ("Tint Color", Color) = (0,1,0,1)
        _BorderColor("Border Color", Color) = (0,0,0,1)
        _BorderThickness ("Border thickness", float) = .1
        _BSize ("Button Size (W, H)", Vector) = (2, 1, 0, 0)

        _HoverColor ("Hover Color", Color) = (0.8, 0.8, 0.8, 1)
        _PressColor ("Press Color", Color) = (0.5, 0.5, 0.5, 1)
        _State ("State (0=Norm, 1=Hov, 2=Pres)", Float) = 0
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
            float4 _Tint;
            float4 _BorderColor;
            float _BorderThickness;
            float4 _BSize; 
            float4 _HoverColor;
            float4 _PressColor;
            float _State;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {

                fixed4 col = tex2D(_MainTex, i.uv);

                fixed4 activeColor = _Tint;
                if (_State >= 1.5) activeColor = _PressColor;
                else if (_State >= 0.5) activeColor = _HoverColor;

                float2 thickness = _BorderThickness / _BSize.xy;
                float2 borderCheck = step(thickness, i.uv) * step(i.uv, 1.0 - thickness);
                float isInside = borderCheck.x * borderCheck.y;

                return lerp(_BorderColor, lerp(col, activeColor, activeColor.a), isInside);
            }
            ENDCG
        }
    }
}
