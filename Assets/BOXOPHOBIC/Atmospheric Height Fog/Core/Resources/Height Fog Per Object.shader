// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/Atmospherics/Height Fog Per Object"
{
	Properties
	{
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector]_HeightFogPerObject("_HeightFogPerObject", Float) = 1
		[HideInInspector]_IsUniversalPipeline("_IsUniversalPipeline", Float) = 0
		[HideInInspector]_IsHeightFogShader("_IsHeightFogShader", Float) = 1
		[HideInInspector]_TransparentQueue("_TransparentQueue", Int) = 3000
		[StyledBanner(Height Fog Per Object)]_TITLEE("< TITLEE >", Float) = 1
		[StyledCategory(Custom Alpha Inputs)]_CUSTOM("[ CUSTOM ]", Float) = 1
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("MainTex", 2D) = "white" {}

	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull Back
		HLSLINCLUDE
		#pragma target 3.0
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }
			
			Blend SrcAlpha OneMinusSrcAlpha , One OneMinusSrcAlpha
			ZWrite Off
			ZTest LEqual
			Offset 0,0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define _RECEIVE_SHADOWS_OFF 1
			#pragma multi_compile_instancing
			#define ASE_SRP_VERSION 70108

			#pragma prefer_hlslcc gles
			#pragma exclude_renderers d3d11_9x

			#pragma vertex vert
			#pragma fragment frag

			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/UnityInstancing.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#if ASE_SRP_VERSION <= 70108
			#define REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR
			#endif

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#define ASE_NEEDS_FRAG_WORLD_POSITION
			#pragma multi_compile AHF_DIRECTIONALMODE_OFF AHF_DIRECTIONALMODE_ON
			#pragma multi_compile AHF_NOISEMODE_OFF AHF_NOISEMODE_PROCEDURAL3D


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD0;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD1;
				#endif
				#ifdef ASE_FOG
				float fogFactor : TEXCOORD2;
				#endif
				float4 ase_texcoord3 : TEXCOORD3;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			half4 AHF_FogColor;
			half4 AHF_DirectionalColor;
			half AHF_DirectionalIntensity;
			half AHF_DirectionalModeBlend;
			half AHF_FogDistanceStart;
			half AHF_FogDistanceEnd;
			half3 AHF_FogAxisOption;
			half AHF_FogHeightEnd;
			half AHF_FogHeightStart;
			half AHF_NoiseScale;
			half3 AHF_NoiseSpeed;
			half AHF_NoiseDistanceEnd;
			half AHF_NoiseIntensity;
			half AHF_NoiseModeBlend;
			half AHF_FogIntensity;
			sampler2D _MainTex;
			CBUFFER_START( UnityPerMaterial )
			int _TransparentQueue;
			half _IsHeightFogShader;
			half _CUSTOM;
			half _HeightFogPerObject;
			half _TITLEE;
			half _IsUniversalPipeline;
			half4 _Color;
			float4 _MainTex_ST;
			CBUFFER_END


			float3 mod3D289( float3 x ) { return x - floor( x / 289.0 ) * 289.0; }
			float4 mod3D289( float4 x ) { return x - floor( x / 289.0 ) * 289.0; }
			float4 permute( float4 x ) { return mod3D289( ( x * 34.0 + 1.0 ) * x ); }
			float4 taylorInvSqrt( float4 r ) { return 1.79284291400159 - r * 0.85373472095314; }
			float snoise( float3 v )
			{
				const float2 C = float2( 1.0 / 6.0, 1.0 / 3.0 );
				float3 i = floor( v + dot( v, C.yyy ) );
				float3 x0 = v - i + dot( i, C.xxx );
				float3 g = step( x0.yzx, x0.xyz );
				float3 l = 1.0 - g;
				float3 i1 = min( g.xyz, l.zxy );
				float3 i2 = max( g.xyz, l.zxy );
				float3 x1 = x0 - i1 + C.xxx;
				float3 x2 = x0 - i2 + C.yyy;
				float3 x3 = x0 - 0.5;
				i = mod3D289( i);
				float4 p = permute( permute( permute( i.z + float4( 0.0, i1.z, i2.z, 1.0 ) ) + i.y + float4( 0.0, i1.y, i2.y, 1.0 ) ) + i.x + float4( 0.0, i1.x, i2.x, 1.0 ) );
				float4 j = p - 49.0 * floor( p / 49.0 );  // mod(p,7*7)
				float4 x_ = floor( j / 7.0 );
				float4 y_ = floor( j - 7.0 * x_ );  // mod(j,N)
				float4 x = ( x_ * 2.0 + 0.5 ) / 7.0 - 1.0;
				float4 y = ( y_ * 2.0 + 0.5 ) / 7.0 - 1.0;
				float4 h = 1.0 - abs( x ) - abs( y );
				float4 b0 = float4( x.xy, y.xy );
				float4 b1 = float4( x.zw, y.zw );
				float4 s0 = floor( b0 ) * 2.0 + 1.0;
				float4 s1 = floor( b1 ) * 2.0 + 1.0;
				float4 sh = -step( h, 0.0 );
				float4 a0 = b0.xzyw + s0.xzyw * sh.xxyy;
				float4 a1 = b1.xzyw + s1.xzyw * sh.zzww;
				float3 g0 = float3( a0.xy, h.x );
				float3 g1 = float3( a0.zw, h.y );
				float3 g2 = float3( a1.xy, h.z );
				float3 g3 = float3( a1.zw, h.w );
				float4 norm = taylorInvSqrt( float4( dot( g0, g0 ), dot( g1, g1 ), dot( g2, g2 ), dot( g3, g3 ) ) );
				g0 *= norm.x;
				g1 *= norm.y;
				g2 *= norm.z;
				g3 *= norm.w;
				float4 m = max( 0.6 - float4( dot( x0, x0 ), dot( x1, x1 ), dot( x2, x2 ), dot( x3, x3 ) ), 0.0 );
				m = m* m;
				m = m* m;
				float4 px = float4( dot( x0, g0 ), dot( x1, g1 ), dot( x2, g2 ), dot( x3, g3 ) );
				return 42.0 * dot( m, px);
			}
			

			VertexOutput vert ( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				int CustomVertexOffset918 = 0;
				float3 temp_cast_0 = (( CustomVertexOffset918 + ( _IsUniversalPipeline * 0.0 ) )).xxx;
				
				o.ase_texcoord3.xy = v.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif
				float3 vertexValue = temp_cast_0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				o.worldPos = positionWS;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				VertexPositionInputs vertexInput = (VertexPositionInputs)0;
				vertexInput.positionWS = positionWS;
				vertexInput.positionCS = positionCS;
				o.shadowCoord = GetShadowCoord( vertexInput );
				#endif
				#ifdef ASE_FOG
				o.fogFactor = ComputeFogFactor( positionCS.z );
				#endif
				o.clipPos = positionCS;
				return o;
			}

			half4 frag ( VertexOutput IN  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif
				float3 temp_output_2_0_g770 = (AHF_FogColor).rgb;
				float3 gammaToLinear3_g770 = FastSRGBToLinear( temp_output_2_0_g770 );
				#ifdef UNITY_COLORSPACE_GAMMA
				float3 staticSwitch1_g770 = temp_output_2_0_g770;
				#else
				float3 staticSwitch1_g770 = gammaToLinear3_g770;
				#endif
				float3 temp_output_34_0_g754 = staticSwitch1_g770;
				float3 temp_output_2_0_g764 = (AHF_DirectionalColor).rgb;
				float3 gammaToLinear3_g764 = FastSRGBToLinear( temp_output_2_0_g764 );
				#ifdef UNITY_COLORSPACE_GAMMA
				float3 staticSwitch1_g764 = temp_output_2_0_g764;
				#else
				float3 staticSwitch1_g764 = gammaToLinear3_g764;
				#endif
				float3 WorldPosition2_g754 = WorldPosition;
				float3 normalizeResult5_g766 = normalize( ( WorldPosition2_g754 - _WorldSpaceCameraPos ) );
				float dotResult6_g766 = dot( normalizeResult5_g766 , SafeNormalize(_MainLightPosition.xyz) );
				half DirectionalMask30_g754 = ( (dotResult6_g766*0.5 + 0.5) * AHF_DirectionalIntensity * AHF_DirectionalModeBlend );
				float3 lerpResult40_g754 = lerp( temp_output_34_0_g754 , staticSwitch1_g764 , DirectionalMask30_g754);
				#if defined(AHF_DIRECTIONALMODE_OFF)
				float3 staticSwitch45_g754 = temp_output_34_0_g754;
				#elif defined(AHF_DIRECTIONALMODE_ON)
				float3 staticSwitch45_g754 = lerpResult40_g754;
				#else
				float3 staticSwitch45_g754 = temp_output_34_0_g754;
				#endif
				float3 temp_output_937_86 = staticSwitch45_g754;
				
				float temp_output_7_0_g768 = AHF_FogDistanceStart;
				half FogDistanceMask12_g754 = saturate( ( ( distance( WorldPosition2_g754 , _WorldSpaceCameraPos ) - temp_output_7_0_g768 ) / ( AHF_FogDistanceEnd - temp_output_7_0_g768 ) ) );
				float3 break12_g772 = ( WorldPosition2_g754 * AHF_FogAxisOption );
				float temp_output_7_0_g773 = AHF_FogHeightEnd;
				half FogHeightMask16_g754 = saturate( ( ( ( break12_g772.x + break12_g772.y + break12_g772.z ) - temp_output_7_0_g773 ) / ( AHF_FogHeightStart - temp_output_7_0_g773 ) ) );
				float temp_output_29_0_g754 = ( FogDistanceMask12_g754 * FogHeightMask16_g754 );
				float simplePerlin3D15_g771 = snoise( ( ( WorldPosition2_g754 * ( 1.0 / AHF_NoiseScale ) ) + ( -AHF_NoiseSpeed * _TimeParameters.x ) ) );
				float temp_output_7_0_g777 = AHF_NoiseDistanceEnd;
				half NoiseDistanceMask7_g754 = saturate( ( ( distance( WorldPosition2_g754 , _WorldSpaceCameraPos ) - temp_output_7_0_g777 ) / ( 0.0 - temp_output_7_0_g777 ) ) );
				float lerpResult20_g771 = lerp( 1.0 , (simplePerlin3D15_g771*0.5 + 0.5) , ( NoiseDistanceMask7_g754 * AHF_NoiseIntensity * AHF_NoiseModeBlend ));
				half NoiseSimplex3D24_g754 = lerpResult20_g771;
				#if defined(AHF_NOISEMODE_OFF)
				float staticSwitch42_g754 = temp_output_29_0_g754;
				#elif defined(AHF_NOISEMODE_PROCEDURAL3D)
				float staticSwitch42_g754 = ( temp_output_29_0_g754 * NoiseSimplex3D24_g754 );
				#else
				float staticSwitch42_g754 = temp_output_29_0_g754;
				#endif
				float temp_output_43_0_g754 = ( staticSwitch42_g754 * AHF_FogIntensity );
				float2 uv0_MainTex = IN.ase_texcoord3.xy * _MainTex_ST.xy + _MainTex_ST.zw;
				float temp_output_1_0_g753 = tex2D( _MainTex, uv0_MainTex ).a;
				float temp_output_7_0_g752 = AHF_FogDistanceStart;
				float lerpResult3_g753 = lerp( temp_output_1_0_g753 , ceil( temp_output_1_0_g753 ) , saturate( ( ( distance( WorldPosition , _WorldSpaceCameraPos ) - temp_output_7_0_g752 ) / ( AHF_FogDistanceEnd - temp_output_7_0_g752 ) ) ));
				half CustomAlphaInputs897 = ( _Color.a * lerpResult3_g753 );
				float temp_output_938_0 = ( temp_output_43_0_g754 * CustomAlphaInputs897 );
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = temp_output_937_86;
				float Alpha = temp_output_938_0;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip( Alpha - AlphaClipThreshold );
				#endif

				#ifdef ASE_FOG
					Color = MixFog( Color, IN.fogFactor );
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODDitheringTransition( IN.clipPos.xyz, unity_LODFade.x );
				#endif

				return half4( Color, Alpha );
			}

			ENDHLSL
		}

	
	}
	
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=17802
1927;7;1906;1015;3629.416;5072.001;1;True;False
Node;AmplifyShaderEditor.WorldPosInputsNode;943;-3328,-3584;Float;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.TextureCoordinatesNode;895;-3328,-3840;Inherit;False;0;892;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;942;-3072,-3584;Inherit;False;Fog Distance;-1;;751;a5f090963b8f9394a984ee752ce42488;0;1;13;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;892;-3072,-3840;Inherit;True;Property;_MainTex;MainTex;10;0;Create;False;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;MipBias;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;913;-2688,-3840;Inherit;False;Handle Tex Alpha;-1;;753;92f31391e7f50294c9c2d8747c81d6b6;0;2;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;894;-3328,-4096;Half;False;Property;_Color;Color;9;0;Create;False;0;0;False;0;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.IntNode;919;-3328,-3200;Float;False;Constant;_Int0;Int 0;5;0;Create;True;0;0;False;0;0;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;896;-2304,-4096;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;897;-2144,-4096;Half;False;CustomAlphaInputs;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;918;-3136,-3200;Half;False;CustomVertexOffset;-1;True;1;0;INT;0;False;1;INT;0
Node;AmplifyShaderEditor.GetLocalVarNode;920;-2816,-4608;Inherit;False;918;CustomVertexOffset;1;0;OBJECT;0;False;1;INT;0
Node;AmplifyShaderEditor.FunctionNode;949;-2797.416,-4504.001;Inherit;False;Is Pipeline;1;;780;2b33d0c660fbdb24c98bea96428031b0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;937;-3328,-4736;Inherit;False;Base;-1;;754;7ce331de1e1cd8c4d83adad8f3b33ab6;2,116,0,99,0;0;3;FLOAT4;113;FLOAT3;86;FLOAT;87
Node;AmplifyShaderEditor.GetLocalVarNode;898;-3328,-4544;Inherit;False;897;CustomAlphaInputs;1;0;OBJECT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;384;-2816,-4736;Inherit;False;FLOAT4;4;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.IntNode;922;-2432,-5248;Float;False;Property;_TransparentQueue;_TransparentQueue;6;1;[HideInInspector];Create;False;0;0;True;0;3000;0;0;1;INT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;938;-3008,-4608;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;890;-2688,-5248;Half;False;Property;_IsHeightFogShader;_IsHeightFogShader;5;1;[HideInInspector];Create;False;0;0;True;0;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;879;-2944,-5248;Half;False;Property;_HeightFogPerObject;_HeightFogPerObject;0;1;[HideInInspector];Create;False;0;0;True;0;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;899;-3169,-5248;Half;False;Property;_CUSTOM;[ CUSTOM ];8;0;Create;True;0;0;True;1;StyledCategory(Custom Alpha Inputs);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;923;-3328,-5248;Half;False;Property;_TITLEE;< TITLEE >;7;0;Create;True;0;0;True;1;StyledBanner(Height Fog Per Object);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;936;-2512,-4592;Inherit;False;2;2;0;INT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;944;-2304,-4736;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;945;-2304,-4736;Float;False;True;-1;2;;0;3;BOXOPHOBIC/Atmospherics/Height Fog Per Object;2992e84f91cbeb14eab234972e07ea9d;True;Forward;0;1;Forward;7;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Transparent=RenderType;Queue=Transparent=Queue=0;True;2;0;True;1;5;False;-1;10;False;-1;1;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;3;False;-1;True;False;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;11;Surface;1;  Blend;0;Two Sided;1;Cast Shadows;0;Receive Shadows;0;GPU Instancing;1;LOD CrossFade;0;Built-in Fog;0;Meta Pass;0;Extra Pre Pass;0;Vertex Position,InvertActionOnDeselection;1;0;5;False;True;False;False;False;False;;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;946;-2304,-4736;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;948;-2304,-4736;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Meta;0;4;Meta;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;947;-2304,-4736;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;12;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthOnly;0;3;DepthOnly;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.CommentaryNode;891;-3328,-4224;Inherit;False;1418.51;100;Custom Alpha Inputs / Add here your custom Alpha Inputs;0;;0.684,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;939;-3328,-4864;Inherit;False;1405.154;100;Final Pass;0;;0.684,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;880;-3328,-5376;Inherit;False;1406.973;101;Drawers;0;;1,0.475862,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;916;-3328,-3328;Inherit;False;1409.549;100;Custom Vertex Offset / Add here your custom Vertex Offset;0;;0.684,1,0,1;0;0
WireConnection;942;13;943;0
WireConnection;892;1;895;0
WireConnection;913;1;892;4
WireConnection;913;2;942;0
WireConnection;896;0;894;4
WireConnection;896;1;913;0
WireConnection;897;0;896;0
WireConnection;918;0;919;0
WireConnection;384;0;937;86
WireConnection;384;3;938;0
WireConnection;938;0;937;87
WireConnection;938;1;898;0
WireConnection;936;0;920;0
WireConnection;936;1;949;0
WireConnection;945;2;937;86
WireConnection;945;3;938;0
WireConnection;945;5;936;0
ASEEND*/
//CHKSM=69544C7CC54E4D589F193E83205A4EB2C0385FE1