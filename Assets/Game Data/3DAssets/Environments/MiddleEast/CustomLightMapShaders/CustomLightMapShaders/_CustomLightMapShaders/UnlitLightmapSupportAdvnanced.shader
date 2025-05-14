// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "UnlitLightmapSupportAdvanced"
{
	Properties
	{
		_Albedo("Albedo", 2D) = "white" {}
		_LightMap("LightMap", 2D) = "white" {}
		_LightIntensity("LightIntensity", Range( 0.1 , 2)) = 0.1
		_BaseLight("BaseLight", Range( 0.1 , 2)) = 1
		[HideInInspector] _texcoord2( "", 2D ) = "white" {}
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" "IsEmissive" = "true"  }
		Cull Back
		CGPROGRAM
		#pragma target 2.0
		#pragma only_renderers d3d9 d3d11 glcore gles gles3 metal 
		#pragma surface surf Unlit keepalpha noshadow noambient novertexlights nolightmap  nodynlightmap nodirlightmap nofog nometa noforwardadd 
		struct Input
		{
			float2 uv_texcoord;
			float2 uv2_texcoord2;
		};

		uniform float _BaseLight;
		uniform sampler2D _Albedo;
		uniform float4 _Albedo_ST;
		uniform float _LightIntensity;
		uniform sampler2D _LightMap;
		uniform float4 _LightMap_ST;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_Albedo = i.uv_texcoord * _Albedo_ST.xy + _Albedo_ST.zw;
			float2 uv2_LightMap = i.uv2_texcoord2 * _LightMap_ST.xy + _LightMap_ST.zw;
			float4 tex2DNode7 = tex2D( _LightMap, uv2_LightMap );
			float4 temp_cast_0 = (1.0).xxxx;
			float4 ifLocalVar16 = 0;
			if( _LightIntensity >= 1.0 )
				ifLocalVar16 = tex2DNode7;
			else
				ifLocalVar16 = temp_cast_0;
			float ifLocalVar17 = 0;
			if( _LightIntensity <= 1.0 )
				ifLocalVar17 = 1.0;
			else
				ifLocalVar17 = _LightIntensity;
			o.Emission = ( ( _BaseLight * tex2D( _Albedo, uv_Albedo ) ) * ( ifLocalVar16 * ifLocalVar17 ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=16100
0;92;1088;350;2549.199;-408.8848;2.03084;True;True
Node;AmplifyShaderEditor.TexturePropertyNode;6;-2416.501,368.1394;Float;True;Property;_LightMap;LightMap;1;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.TexturePropertyNode;5;-2404.101,41.33916;Float;True;Property;_Albedo;Albedo;0;0;Create;True;0;0;False;0;None;None;False;white;Auto;Texture2D;0;1;SAMPLER2D;0
Node;AmplifyShaderEditor.RangedFloatNode;10;-1651.339,699.0862;Float;False;Property;_LightIntensity;LightIntensity;2;0;Create;True;0;0;False;0;0.1;0;0.1;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;14;-1610.739,803.6677;Float;False;Constant;_LightIntesityLimit;LightIntesityLimit;4;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;7;-1991.421,367.4646;Float;True;Property;_TextureSample0;Texture Sample 0;2;0;Create;True;0;0;False;0;None;None;True;1;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ConditionalIfNode;16;-730.0403,219.65;Float;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ConditionalIfNode;17;-743.0008,487.6101;Float;False;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;9;-1026.145,-459.3026;Float;False;Property;_BaseLight;BaseLight;3;0;Create;True;0;0;False;0;1;0;0.1;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;4;-2006.633,50.36073;Float;True;Property;_AlbedoToRGB;AlbedoToRGB;0;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-563.64,-202.7127;Float;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-417.5204,345.8323;Float;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;8;-160.2275,188.3526;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;2;87.44478,58.04381;Float;False;True;0;Float;ASEMaterialInspector;0;0;Unlit;UnlitLightmapSupportAdvanced;False;False;False;False;True;True;True;True;True;True;True;True;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Opaque;0.5;True;False;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;False;False;False;False;False;False;False;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;0;0;False;-1;0;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;7;0;6;0
WireConnection;16;0;10;0
WireConnection;16;1;14;0
WireConnection;16;2;7;0
WireConnection;16;3;7;0
WireConnection;16;4;14;0
WireConnection;17;0;10;0
WireConnection;17;1;14;0
WireConnection;17;2;10;0
WireConnection;17;3;14;0
WireConnection;17;4;14;0
WireConnection;4;0;5;0
WireConnection;12;0;9;0
WireConnection;12;1;4;0
WireConnection;11;0;16;0
WireConnection;11;1;17;0
WireConnection;8;0;12;0
WireConnection;8;1;11;0
WireConnection;2;2;8;0
ASEEND*/
//CHKSM=4FC80266FAF771D9DC53CBC31E5B3E3A7D5495F9