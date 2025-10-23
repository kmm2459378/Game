Shader "Custom/InstancedShader"
{
  Properties{_Color("Color, Color") = (1,1,1,1)}
  SubShader
  {
	  Pass
	  {
		  HLSLMPROGRAM
		  #pragma vertex vertex
		  #pragma fragment frag
		  #pragma multi_compile_instancing

		  #include "UniyCG.cginc"

		  StructuredBuffer<float4x4> _Matrices;
		  float4 _Color;

		  struct appdata
		  {
			uint vertexID : SV_VertexID;
			uint instanceID : SV_InstaceID;
		  };

		  struct V2f
		  {
			  float4 pos :SV_POSITION;
		  };

		  v2f vert(appadata v)
		  {
			  v2f o;
			  float4x4 model = Materices[v.instanceID];
			  o.pos = UnityObjectToClipPos(mul(model, float(0,0,0,1)));
			  return o;
		  }

		  fixed4 frag(v2f i) : SV_Target
		  {
			  return _Color;
		  }
		  ENDHLSL
	  }
  }
}