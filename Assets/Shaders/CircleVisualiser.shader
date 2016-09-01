Shader "Unlit/CircleVisualiser"
{
    Properties {
        _AreaMultiples("Areas Added", float) = 2.0 // The count of areas added to circle.
        _StartRadius("Original radius", float) = 1.0
    }
    SubShader {
        Blend SrcAlpha OneMinusSrcAlpha
        Tags { 
            "RenderType"="Transparent"
            "Queue" = "Transparent"
        }

        Pass {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            struct appdata {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
                float4 values : TEXCOORD1;
            };

            float circle(float2 uv) {
                return length(uv);
            }

            float calculate_radius(float area) {
                return sqrt(area / 3.14159265358979323846);
            }

            float calculate_area(float radius) {
                return 3.14159265358979323846 * radius * radius;
            }

            float _StartRadius;
            float _AreaMultiples;
            float4 _Color;
            v2f vert (appdata v) {
                v2f o;
                o.vertex = mul(UNITY_MATRIX_MVP, v.vertex);
                o.uv = (v.uv - float2(0.5, 0.5)) * 2;
                _Color = float4(0.4, 0.1, 0.6, 1.0);
                // Calculate the scale and store it for later
                o.values.x = calculate_radius(calculate_area(_StartRadius) + _AreaMultiples);
                o.values.y = o.values.z = o.values.w = 0;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target {
                // Generate a circle based on UV
                float circle_value = circle(i.uv);

                // Create a circle mask
                float circle_mask = step(circle_value, 1);
                circle_value = calculate_area(circle_value * i.values.x);
                circle_value -= _StartRadius * 3.14159265358979323846;
                circle_value = fmod(circle_value, 1);
                /*circle_value = circle_value / calculate_area(circle_value * i.values.x);*/
                fixed4 col = fixed4(
                    circle_value, 
                    circle_value, 
                    circle_value, 
                    1) * circle_mask;
                return col;
            }
            ENDCG
        }
    }
}
