// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Hidden/BOXOPHOBIC/Atmospherics/Height Fog Global"
{
	Properties
	{
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector]_IsUniversalPipeline("_IsUniversalPipeline", Float) = 0
		[HideInInspector]_HeightFogGlobal("_HeightFogGlobal", Float) = 1
		[HideInInspector]_IsHeightFogShader("_IsHeightFogShader", Float) = 1
		[HideInInspector]_TransparentQueue("_TransparentQueue", Int) = 3000
		[StyledBanner(Height Fog Global)]_TITLE("< TITLE >", Float) = 1

	}

	SubShader
	{
		LOD 0

		
		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Overlay" "Queue"="Overlay" }
		
		Cull Front
		HLSLINCLUDE
		#pragma target 3.0
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }
			
			Blend SrcAlpha OneMinusSrcAlpha , One OneMinusSrcAlpha
			ZWrite Off
			ZTest Always
			Offset 0,0
			ColorMask RGBA
			

			HLSLPROGRAM
			#define _RECEIVE_SHADOWS_OFF 1
			#define ASE_SRP_VERSION 70108
			#define REQUIRE_DEPTH_TEXTURE 1

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
			#pragma multi_compile AHF_DIRECTIONALMODE_OFF AHF_DIRECTIONALMODE_ON
			#pragma multi_compile AHF_NOISEMODE_OFF AHF_NOISEMODE_PROCEDURAL3D


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
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
			uniform float4 _CameraDepthTexture_TexelSize;
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
			half AHF_SkyboxFogHeight;
			half AHF_SkyboxFogFill;
			half AHF_FogIntensity;
			CBUFFER_START( UnityPerMaterial )
			half _IsHeightFogShader;
			int _TransparentQueue;
			half _TITLE;
			half _HeightFogGlobal;
			half _IsUniversalPipeline;
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

				float3 temp_cast_0 = (( _IsUniversalPipeline * 0.0 )).xxx;
				
				float4 ase_clipPos = TransformObjectToHClip((v.vertex).xyz);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				o.ase_texcoord3 = screenPos;
				
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
				float3 temp_output_2_0_g826 = (AHF_FogColor).rgb;
				float3 gammaToLinear3_g826 = FastSRGBToLinear( temp_output_2_0_g826 );
				#ifdef UNITY_COLORSPACE_GAMMA
				float3 staticSwitch1_g826 = temp_output_2_0_g826;
				#else
				float3 staticSwitch1_g826 = gammaToLinear3_g826;
				#endif
				float3 temp_output_34_0_g819 = staticSwitch1_g826;
				float3 temp_output_2_0_g820 = (AHF_DirectionalColor).rgb;
				float3 gammaToLinear3_g820 = FastSRGBToLinear( temp_output_2_0_g820 );
				#ifdef UNITY_COLORSPACE_GAMMA
				float3 staticSwitch1_g820 = temp_output_2_0_g820;
				#else
				float3 staticSwitch1_g820 = gammaToLinear3_g820;
				#endif
				float4 screenPos = IN.ase_texcoord3;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 break6_g834 = (ase_screenPosNorm).xy;
				float clampDepth13_g825 = SHADERGRAPH_SAMPLE_SCENE_DEPTH( ase_screenPosNorm.xy );
				#ifdef UNITY_REVERSED_Z
				float staticSwitch14_g825 = clampDepth13_g825;
				#else
				float staticSwitch14_g825 = ( 1.0 - clampDepth13_g825 );
				#endif
				float RawDepth89_g819 = staticSwitch14_g825;
				float temp_output_41_0_g834 = RawDepth89_g819;
				#ifdef UNITY_REVERSED_Z
				float staticSwitch5_g834 = ( 1.0 - temp_output_41_0_g834 );
				#else
				float staticSwitch5_g834 = temp_output_41_0_g834;
				#endif
				float3 appendResult11_g834 = (float3(break6_g834.x , break6_g834.y , staticSwitch5_g834));
				float4 appendResult16_g834 = (float4((appendResult11_g834*2.0 + -1.0) , 1.0));
				float4 break18_g834 = mul( unity_CameraInvProjection, appendResult16_g834 );
				float3 appendResult19_g834 = (float3(break18_g834.x , break18_g834.y , break18_g834.z));
				float4 appendResult27_g834 = (float4(( ( appendResult19_g834 / break18_g834.w ) * half3(1,1,-1) ) , 1.0));
				float4 break30_g834 = mul( unity_CameraToWorld, appendResult27_g834 );
				float3 appendResult31_g834 = (float3(break30_g834.x , break30_g834.y , break30_g834.z));
				float3 WorldPosition2_g819 = appendResult31_g834;
				float3 normalizeResult5_g822 = normalize( ( WorldPosition2_g819 - _WorldSpaceCameraPos ) );
				float dotResult6_g822 = dot( normalizeResult5_g822 , SafeNormalize(_MainLightPosition.xyz) );
				half DirectionalMask30_g819 = ( (dotResult6_g822*0.5 + 0.5) * AHF_DirectionalIntensity * AHF_DirectionalModeBlend );
				float3 lerpResult40_g819 = lerp( temp_output_34_0_g819 , staticSwitch1_g820 , DirectionalMask30_g819);
				#if defined(AHF_DIRECTIONALMODE_OFF)
				float3 staticSwitch45_g819 = temp_output_34_0_g819;
				#elif defined(AHF_DIRECTIONALMODE_ON)
				float3 staticSwitch45_g819 = lerpResult40_g819;
				#else
				float3 staticSwitch45_g819 = temp_output_34_0_g819;
				#endif
				
				float temp_output_7_0_g824 = AHF_FogDistanceStart;
				half FogDistanceMask12_g819 = saturate( ( ( distance( WorldPosition2_g819 , _WorldSpaceCameraPos ) - temp_output_7_0_g824 ) / ( AHF_FogDistanceEnd - temp_output_7_0_g824 ) ) );
				float3 break12_g828 = ( WorldPosition2_g819 * AHF_FogAxisOption );
				float temp_output_7_0_g829 = AHF_FogHeightEnd;
				half FogHeightMask16_g819 = saturate( ( ( ( break12_g828.x + break12_g828.y + break12_g828.z ) - temp_output_7_0_g829 ) / ( AHF_FogHeightStart - temp_output_7_0_g829 ) ) );
				float temp_output_29_0_g819 = ( FogDistanceMask12_g819 * FogHeightMask16_g819 );
				float simplePerlin3D15_g827 = snoise( ( ( WorldPosition2_g819 * ( 1.0 / AHF_NoiseScale ) ) + ( -AHF_NoiseSpeed * _TimeParameters.x ) ) );
				float temp_output_7_0_g833 = AHF_NoiseDistanceEnd;
				half NoiseDistanceMask7_g819 = saturate( ( ( distance( WorldPosition2_g819 , _WorldSpaceCameraPos ) - temp_output_7_0_g833 ) / ( 0.0 - temp_output_7_0_g833 ) ) );
				float lerpResult20_g827 = lerp( 1.0 , (simplePerlin3D15_g827*0.5 + 0.5) , ( NoiseDistanceMask7_g819 * AHF_NoiseIntensity * AHF_NoiseModeBlend ));
				half NoiseSimplex3D24_g819 = lerpResult20_g827;
				#if defined(AHF_NOISEMODE_OFF)
				float staticSwitch42_g819 = temp_output_29_0_g819;
				#elif defined(AHF_NOISEMODE_PROCEDURAL3D)
				float staticSwitch42_g819 = ( temp_output_29_0_g819 * NoiseSimplex3D24_g819 );
				#else
				float staticSwitch42_g819 = temp_output_29_0_g819;
				#endif
				float3 normalizeResult25_g830 = normalize( WorldPosition2_g819 );
				float3 break22_g830 = ( normalizeResult25_g830 * AHF_FogAxisOption );
				float temp_output_7_0_g831 = AHF_SkyboxFogHeight;
				float lerpResult17_g830 = lerp( saturate( ( ( abs( ( break22_g830.x + break22_g830.y + break22_g830.z ) ) - temp_output_7_0_g831 ) / ( 0.0 - temp_output_7_0_g831 ) ) ) , 1.0 , AHF_SkyboxFogFill);
				half SkyboxFogHeightMask108_g819 = lerpResult17_g830;
				float temp_output_6_0_g821 = RawDepth89_g819;
				#ifdef UNITY_REVERSED_Z
				float staticSwitch11_g821 = temp_output_6_0_g821;
				#else
				float staticSwitch11_g821 = ( 1.0 - temp_output_6_0_g821 );
				#endif
				half SkyboxMask95_g819 = ( 1.0 - ceil( staticSwitch11_g821 ) );
				float lerpResult112_g819 = lerp( staticSwitch42_g819 , SkyboxFogHeightMask108_g819 , SkyboxMask95_g819);
				float temp_output_43_0_g819 = ( lerpResult112_g819 * AHF_FogIntensity );
				
				float3 BakedAlbedo = 0;
				float3 BakedEmission = 0;
				float3 Color = staticSwitch45_g819;
				float Alpha = temp_output_43_0_g819;
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
1927;7;1906;1015;4223.792;5028.447;1;True;False
Node;AmplifyShaderEditor.RangedFloatNode;885;-2912,-4864;Half;False;Property;_IsHeightFogShader;_IsHeightFogShader;5;1;[HideInInspector];Create;False;0;0;True;0;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.IntNode;891;-2656,-4864;Float;False;Property;_TransparentQueue;_TransparentQueue;6;1;[HideInInspector];Create;False;0;0;True;0;3000;0;0;1;INT;0
Node;AmplifyShaderEditor.RangedFloatNode;892;-3328,-4864;Half;False;Property;_TITLE;< TITLE >;7;0;Create;True;0;0;True;1;StyledBanner(Height Fog Global);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;879;-3136,-4864;Half;False;Property;_HeightFogGlobal;_HeightFogGlobal;4;1;[HideInInspector];Create;False;0;0;True;0;1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;948;-3328,-4416;Inherit;False;Is Pipeline;0;;802;2b33d0c660fbdb24c98bea96428031b0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;950;-3328,-4608;Inherit;False;Base;-1;;819;7ce331de1e1cd8c4d83adad8f3b33ab6;2,116,1,99,1;0;3;FLOAT4;113;FLOAT3;86;FLOAT;87
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;929;-3072,-4608;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;Meta;0;4;Meta;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;True;2;False;-1;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;925;-3072,-4544;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;True;1;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;False;False;True;0;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;1;False;-1;True;3;False;-1;True;True;0;False;-1;0;False;-1;True;0;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;927;-3072,-4608;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;False;False;True;1;False;-1;True;3;False;-1;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;928;-3072,-4608;Float;False;False;-1;2;UnityEditor.ShaderGraph.PBRMasterGUI;0;3;New Amplify Shader;2992e84f91cbeb14eab234972e07ea9d;True;DepthOnly;0;3;DepthOnly;0;False;False;False;True;0;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;0;0;False;False;False;False;True;False;False;False;False;0;False;-1;False;True;1;False;-1;False;False;True;1;LightMode=DepthOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;926;-2944,-4608;Float;False;True;-1;2;;0;3;Hidden/BOXOPHOBIC/Atmospherics/Height Fog Global;2992e84f91cbeb14eab234972e07ea9d;True;Forward;0;1;Forward;7;False;False;False;True;1;False;-1;False;False;False;False;False;True;3;RenderPipeline=UniversalPipeline;RenderType=Overlay=RenderType;Queue=Overlay=Queue=0;True;2;0;True;1;5;False;-1;10;False;-1;1;1;False;-1;10;False;-1;False;False;False;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;7;False;-1;True;False;0;False;-1;0;False;-1;True;1;LightMode=UniversalForward;False;0;Hidden/InternalErrorShader;0;0;Standard;11;Surface;1;  Blend;0;Two Sided;2;Cast Shadows;0;Receive Shadows;0;GPU Instancing;0;LOD CrossFade;0;Built-in Fog;0;Meta Pass;0;Extra Pre Pass;0;Vertex Position,InvertActionOnDeselection;1;0;5;False;True;False;False;False;False;;0
Node;AmplifyShaderEditor.CommentaryNode;880;-3328,-4992;Inherit;False;919.8825;100;Drawers;0;;1,0.475862,0,1;0;0
WireConnection;926;2;950;86
WireConnection;926;3;950;87
WireConnection;926;5;948;0
ASEEND*/
//CHKSM=A796E65637F75C12DFB2FD5266F3E5C34BC7BCA3