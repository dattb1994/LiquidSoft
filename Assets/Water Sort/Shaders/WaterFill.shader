Shader "Smkgames/WaterFill" {
    Properties{
        _Volume("Fill",Range(-10,20)) = 0
        _Pos("Pos",Vector) = (0,0,0,0)
        _PosY("Pos",Float) = 0
        _MainColor("color near to point", Color) = (0.0, 1.0, 0.0, 1.0)
        [LM_Albedo][LM_Transparency]  _ColorFar("color far from point", Color) = (0.3, 0.3, 0.3, 0.0)
    }
        SubShader{
        Tags{ "Queue" = "Geometry" "RenderType" = "Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite off
           Pass {
              CGPROGRAM

              #pragma vertex vert  
              #pragma fragment frag 

              // uniform float4x4 unity_ObjectToWorld; 
                 // automatic definition of a Unity-specific uniform parameter

              struct vertexInput {
                 float4 vertex : POSITION;
              };
              struct vertexOutput {
                 float4 pos : SV_POSITION;
                 float4 position_in_world_space : TEXCOORD0;
              };
             float _Volume;
             float _PosY;
              vertexOutput vert(vertexInput input)
              {
                 vertexOutput output;

                 output.pos = UnityObjectToClipPos(input.vertex);
                 output.position_in_world_space =
                    mul(unity_ObjectToWorld, input.vertex) -  float4(0,_PosY,0,0);
                 // transformation of input.vertex from object 
                 // coordinates to world coordinates;
              return output;
           }

              uniform float4 _MainColor;
              uniform float4 _ColorFar;


           float4 frag(vertexOutput input) : COLOR
           {
                 float dist = distance(input.position_in_world_space,
                 float4(0.0, 0.0, 0.0, 0.0));
                 /*computes the distance between the fragment position
                and the origin (the 4th coordinate should always be
           1 for points).*/

                  if (input.position_in_world_space.y < _Volume)
                  {
                        return _MainColor;
          // color near origin
                  }
                  else
                  {
                        return _ColorFar;
                        // color far from origin
                    }
                }

            ENDCG
        }
    }
}