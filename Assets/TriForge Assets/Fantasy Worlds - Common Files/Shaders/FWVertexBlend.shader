// Made with Amplify Shader Editor v1.9.2.1
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "TriForge/Fantasy Worlds/FWVertexBlend"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		_Layer1Color("Layer 1 Color", Color) = (1,1,1,0)
		_Layer1BaseColor("Layer 1 Base Color", 2D) = "white" {}
		_Layer1Saturation("Layer 1 Saturation", Range( 0 , 1)) = 1
		_Layer1Mask("Layer 1 Mask", 2D) = "white" {}
		_Layer1Normal("Layer 1 Normal", 2D) = "bump" {}
		_Layer1NormalStrength("Layer 1 Normal Strength", Float) = 1
		[Toggle(_LAYER1USEAOASHEIGHT_ON)] _Layer1UseAOasHeight("Layer 1 Use AO as Height", Float) = 0
		_Layer1AO("Layer 1 AO", Range( 0 , 1)) = 1
		_Layer1Smoothness("Layer 1 Smoothness", Range( 0 , 1)) = 1
		_Layer2Color("Layer 2 Color", Color) = (1,1,1,0)
		_Layer2BaseColor("Layer 2 Base Color", 2D) = "white" {}
		_Layer2Mask("Layer 2 Mask", 2D) = "white" {}
		_Layer2Normal("Layer 2 Normal", 2D) = "bump" {}
		_Layer2NormalStrength("Layer 2 Normal Strength", Float) = 1
		[Toggle(_LAYER2USEAOASHEIGHT_ON)] _Layer2UseAOasHeight("Layer 2 Use AO as Height", Float) = 0
		_Layer2AO("Layer 2 AO", Range( 0 , 1)) = 1
		_Layer2Smoothness("Layer 2 Smoothness", Range( 0 , 1)) = 1
		_Layer2BlendSharpness("Layer 2 Blend Sharpness", Float) = 5
		_Layer3Color("Layer 3 Color", Color) = (1,1,1,0)
		_Layer3BaseColor("Layer 3 Base Color", 2D) = "white" {}
		_Layer3Mask("Layer 3 Mask", 2D) = "white" {}
		_Layer3Normal("Layer 3 Normal", 2D) = "bump" {}
		_Layer3NormalStrength("Layer 3 Normal Strength", Float) = 0
		[Toggle(_LAYER3USEAOASHEIGHT_ON)] _Layer3UseAOasHeight("Layer 3 Use AO as Height", Float) = 0
		_Layer3AO("Layer 3 AO", Range( 0 , 1)) = 1
		_Layer3Smoothness("Layer 3 Smoothness", Range( 0 , 1)) = 1
		_Layer3BlendSharpness("Layer 3 Blend Sharpness", Range( 0 , 50)) = 5
		_Layer4Color("Layer 4 Color", Color) = (1,1,1,0)
		_Layer4BaseColor("Layer 4 Base Color", 2D) = "white" {}
		_Layer4Normal("Layer 4 Normal", 2D) = "bump" {}
		_Layer4Mask("Layer 4 Mask", 2D) = "white" {}
		_Layer4NormalStrength("Layer 4 Normal Strength", Float) = 0
		_Layer4AO("Layer 4 AO", Range( 0 , 1)) = 1
		_Layer4Smoothness("Layer 4 Smoothness", Range( 0 , 1)) = 1
		_Layer4BlendSharpness("Layer 4 Blend Sharpness", Range( 0 , 30)) = 3
		[Toggle(_ENABLELAYER3_ON)] _EnableLayer3("Enable Layer 3", Float) = 1
		[Toggle(_ENABLELAYER2_ON)] _EnableLayer2("Enable Layer 2", Float) = 1
		[Toggle(_ENABLELAYER4_ON)] _EnableLayer4("Enable Layer 4", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}


		//_TransmissionShadow( "Transmission Shadow", Range( 0, 1 ) ) = 0.5
		//_TransStrength( "Trans Strength", Range( 0, 50 ) ) = 1
		//_TransNormal( "Trans Normal Distortion", Range( 0, 1 ) ) = 0.5
		//_TransScattering( "Trans Scattering", Range( 1, 50 ) ) = 2
		//_TransDirect( "Trans Direct", Range( 0, 1 ) ) = 0.9
		//_TransAmbient( "Trans Ambient", Range( 0, 1 ) ) = 0.1
		//_TransShadow( "Trans Shadow", Range( 0, 1 ) ) = 0.5
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25

		[HideInInspector][ToggleOff] _SpecularHighlights("Specular Highlights", Float) = 1.0
		[HideInInspector][ToggleOff] _EnvironmentReflections("Environment Reflections", Float) = 1.0
		[HideInInspector][ToggleOff] _ReceiveShadows("Receive Shadows", Float) = 1.0

		[HideInInspector] _QueueOffset("_QueueOffset", Float) = 0
        [HideInInspector] _QueueControl("_QueueControl", Float) = -1

        [HideInInspector][NoScaleOffset] unity_Lightmaps("unity_Lightmaps", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_LightmapsInd("unity_LightmapsInd", 2DArray) = "" {}
        [HideInInspector][NoScaleOffset] unity_ShadowMasks("unity_ShadowMasks", 2DArray) = "" {}
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="UniversalPipeline" "RenderType"="Opaque" "Queue"="Geometry" "UniversalMaterialType"="Lit" }

		Cull Back
		ZWrite On
		ZTest LEqual
		Offset 0 , 0
		AlphaToMask Off

		

		HLSLINCLUDE
		#pragma target 4.5
		#pragma prefer_hlslcc gles
		// ensure rendering platforms toggle list is visible

		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
		#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Filtering.hlsl"

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}

		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlane (float3 pos, float4 plane)
		{
			float d = dot (float4(pos,1.0f), plane);
			return d;
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlane(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlane(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlane(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlane(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
							(( DistanceFromPlane(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS
		ENDHLSL

		
		Pass
		{
			
			Name "Forward"
			Tags { "LightMode"="UniversalForward" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma instancing_options renderinglayer
			#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma shader_feature_local _RECEIVE_SHADOWS_OFF
			#pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF

			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile _ _ADDITIONAL_LIGHTS_VERTEX _ADDITIONAL_LIGHTS
			#pragma multi_compile_fragment _ _ADDITIONAL_LIGHT_SHADOWS
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
			#pragma multi_compile_fragment _ _SHADOWS_SOFT
			#pragma multi_compile_fragment _ _SCREEN_SPACE_OCCLUSION
			#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
			#pragma multi_compile_fragment _ _LIGHT_LAYERS
			#pragma multi_compile_fragment _ _LIGHT_COOKIES
			#pragma multi_compile _ _FORWARD_PLUS

			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DYNAMICLIGHTMAP_ON
			#pragma multi_compile_fragment _ DEBUG_DISPLAY
			#pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_FORWARD

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
				#define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _ENABLELAYER4_ON
			#pragma shader_feature_local _ENABLELAYER3_ON
			#pragma shader_feature_local _ENABLELAYER2_ON
			#pragma shader_feature_local _LAYER1USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER2USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER3USEAOASHEIGHT_ON


			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				ASE_SV_POSITION_QUALIFIERS float4 clipPos : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float4 lightmapUVOrVertexSH : TEXCOORD1;
				half4 fogFactorAndVertexLight : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					float4 shadowCoord : TEXCOORD6;
				#endif
				#if defined(DYNAMICLIGHTMAP_ON)
					float2 dynamicLightmapUV : TEXCOORD7;
				#endif
				float4 ase_texcoord8 : TEXCOORD8;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			TEXTURE2D(_Layer1BaseColor);
			SAMPLER(sampler_Trilinear_Repeat_Aniso16);
			TEXTURE2D(_Layer2BaseColor);
			TEXTURE2D(_Layer1Mask);
			TEXTURE2D(_Layer3BaseColor);
			TEXTURE2D(_Layer2Mask);
			TEXTURE2D(_Layer4BaseColor);
			TEXTURE2D(_Layer4Mask);
			TEXTURE2D(_Layer3Mask);
			TEXTURE2D(_Layer1Normal);
			TEXTURE2D(_Layer2Normal);
			TEXTURE2D(_Layer3Normal);
			TEXTURE2D(_Layer4Normal);


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord8.xy = v.texcoord.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord8.zw = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif
				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 positionVS = TransformWorldToView( positionWS );
				float4 positionCS = TransformWorldToHClip( positionWS );

				VertexNormalInputs normalInput = GetVertexNormalInputs( v.ase_normal, v.ase_tangent );

				o.tSpace0 = float4( normalInput.normalWS, positionWS.x);
				o.tSpace1 = float4( normalInput.tangentWS, positionWS.y);
				o.tSpace2 = float4( normalInput.bitangentWS, positionWS.z);

				#if defined(LIGHTMAP_ON)
					OUTPUT_LIGHTMAP_UV( v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy );
				#endif

				#if !defined(LIGHTMAP_ON)
					OUTPUT_SH( normalInput.normalWS.xyz, o.lightmapUVOrVertexSH.xyz );
				#endif

				#if defined(DYNAMICLIGHTMAP_ON)
					o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					o.lightmapUVOrVertexSH.zw = v.texcoord.xy;
					o.lightmapUVOrVertexSH.xy = v.texcoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				half3 vertexLight = VertexLighting( positionWS, normalInput.normalWS );

				#ifdef ASE_FOG
					half fogFactor = ComputeFogFactor( positionCS.z );
				#else
					half fogFactor = 0;
				#endif

				o.fogFactorAndVertexLight = half4(fogFactor, vertexLight);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;
				o.clipPosV = positionCS;
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_tangent = v.ase_tangent;
				o.texcoord = v.texcoord;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_tangent = patch[0].ase_tangent * bary.x + patch[1].ase_tangent * bary.y + patch[2].ase_tangent * bary.z;
				o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag ( VertexOutput IN
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						#ifdef _WRITE_RENDERING_LAYERS
						, out float4 outRenderingLayers : SV_Target1
						#endif
						 ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (IN.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( IN.tSpace0.xyz );
					float3 WorldTangent = IN.tSpace1.xyz;
					float3 WorldBiTangent = IN.tSpace2.xyz;
				#endif

				float3 WorldPosition = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				float4 ClipPos = IN.clipPosV;
				float4 ScreenPos = ComputeScreenPos( IN.clipPosV );

				float2 NormalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(IN.clipPos);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = IN.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#endif

				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float2 uv_Layer1BaseColor = IN.ase_texcoord8.xy * _Layer1BaseColor_ST.xy + _Layer1BaseColor_ST.zw;
				float3 desaturateInitialColor235 = ( _Layer1Color * SAMPLE_TEXTURE2D( _Layer1BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer1BaseColor ) ).rgb;
				float desaturateDot235 = dot( desaturateInitialColor235, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar235 = lerp( desaturateInitialColor235, desaturateDot235.xxx, ( 1.0 - _Layer1Saturation ) );
				float3 Layer1Color27 = desaturateVar235;
				float2 uv_Layer2BaseColor = IN.ase_texcoord8.xy * _Layer2BaseColor_ST.xy + _Layer2BaseColor_ST.zw;
				float4 Layer2Color58 = ( _Layer2Color * SAMPLE_TEXTURE2D( _Layer2BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer2BaseColor ) );
				float2 uv_Layer1Mask = IN.ase_texcoord8.xy * _Layer1Mask_ST.xy + _Layer1Mask_ST.zw;
				float4 tex2DNode2 = SAMPLE_TEXTURE2D( _Layer1Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Mask );
				float Layer1Height31 = tex2DNode2.b;
				float Layer1RawAO201 = tex2DNode2.g;
				#ifdef _LAYER1USEAOASHEIGHT_ON
				float staticSwitch63 = ( 1.0 - Layer1RawAO201 );
				#else
				float staticSwitch63 = ( 1.0 - Layer1Height31 );
				#endif
				float VCRed18 = IN.ase_color.r;
				float HeightMask10 = saturate(pow(max( (((staticSwitch63*VCRed18)*4)+(VCRed18*2)), 0 ),_Layer2BlendSharpness));
				float Layer2Mask143 = saturate( HeightMask10 );
				float4 lerpResult14 = lerp( float4( Layer1Color27 , 0.0 ) , Layer2Color58 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float4 staticSwitch210 = lerpResult14;
				#else
				float4 staticSwitch210 = float4( Layer1Color27 , 0.0 );
				#endif
				float2 uv_Layer3BaseColor = IN.ase_texcoord8.xy * _Layer3BaseColor_ST.xy + _Layer3BaseColor_ST.zw;
				float4 Layer3Color98 = ( _Layer3Color * SAMPLE_TEXTURE2D( _Layer3BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer3BaseColor ) );
				float2 uv_Layer2Mask = IN.ase_texcoord8.xy * _Layer2Mask_ST.xy + _Layer2Mask_ST.zw;
				float4 tex2DNode46 = SAMPLE_TEXTURE2D( _Layer2Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Mask );
				float Layer2Height54 = tex2DNode46.b;
				float lerpResult186 = lerp( staticSwitch63 , ( 1.0 - Layer2Height54 ) , Layer2Mask143);
				float Layer2RawAO202 = tex2DNode46.g;
				float lerpResult200 = lerp( staticSwitch63 , ( 1.0 - Layer2RawAO202 ) , Layer2Mask143);
				#ifdef _LAYER2USEAOASHEIGHT_ON
				float staticSwitch130 = lerpResult200;
				#else
				float staticSwitch130 = lerpResult186;
				#endif
				float VCGreen19 = IN.ase_color.g;
				float HeightMask126 = saturate(pow(max( (((staticSwitch130*VCGreen19)*4)+(VCGreen19*2)), 0 ),_Layer3BlendSharpness));
				float Layer3Mask144 = saturate( HeightMask126 );
				float4 lerpResult67 = lerp( staticSwitch210 , Layer3Color98 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float4 staticSwitch204 = lerpResult67;
				#else
				float4 staticSwitch204 = staticSwitch210;
				#endif
				float2 uv_Layer4BaseColor = IN.ase_texcoord8.xy * _Layer4BaseColor_ST.xy + _Layer4BaseColor_ST.zw;
				float4 Layer4Color117 = ( _Layer4Color * SAMPLE_TEXTURE2D( _Layer4BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer4BaseColor ) );
				float2 uv_Layer4Mask = IN.ase_texcoord8.xy * _Layer4Mask_ST.xy + _Layer4Mask_ST.zw;
				float4 tex2DNode115 = SAMPLE_TEXTURE2D( _Layer4Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Mask );
				float Layer4Height121 = tex2DNode115.b;
				float lerpResult189 = lerp( staticSwitch130 , ( 1.0 - Layer4Height121 ) , Layer3Mask144);
				float2 uv_Layer3Mask = IN.ase_texcoord8.xy * _Layer3Mask_ST.xy + _Layer3Mask_ST.zw;
				float4 tex2DNode99 = SAMPLE_TEXTURE2D( _Layer3Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Mask );
				float Layer3AO103 = saturate( ( tex2DNode99.g + ( 1.0 - _Layer3AO ) ) );
				float lerpResult214 = lerp( staticSwitch130 , ( 1.0 - Layer3AO103 ) , Layer3Mask144);
				#ifdef _LAYER3USEAOASHEIGHT_ON
				float staticSwitch140 = lerpResult214;
				#else
				float staticSwitch140 = lerpResult189;
				#endif
				float VCBlue20 = IN.ase_color.b;
				float HeightMask134 = saturate(pow(max( (((staticSwitch140*VCBlue20)*4)+(VCBlue20*2)), 0 ),_Layer4BlendSharpness));
				float Layer4Mask145 = saturate( HeightMask134 );
				float4 lerpResult139 = lerp( staticSwitch204 , Layer4Color117 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float4 staticSwitch205 = lerpResult139;
				#else
				float4 staticSwitch205 = staticSwitch204;
				#endif
				float4 FinalColorBlend141 = staticSwitch205;
				
				float2 uv_Layer1Normal = IN.ase_texcoord8.xy * _Layer1Normal_ST.xy + _Layer1Normal_ST.zw;
				float3 unpack1 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer1Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Normal ), _Layer1NormalStrength );
				unpack1.z = lerp( 1, unpack1.z, saturate(_Layer1NormalStrength) );
				float3 Layer1Normal28 = unpack1;
				float2 uv_Layer2Normal = IN.ase_texcoord8.xy * _Layer2Normal_ST.xy + _Layer2Normal_ST.zw;
				float3 unpack45 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer2Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Normal ), _Layer2NormalStrength );
				unpack45.z = lerp( 1, unpack45.z, saturate(_Layer2NormalStrength) );
				float3 Layer2Normal56 = unpack45;
				float3 lerpResult163 = lerp( Layer1Normal28 , Layer2Normal56 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float3 staticSwitch211 = lerpResult163;
				#else
				float3 staticSwitch211 = Layer1Normal28;
				#endif
				float2 uv_Layer3Normal = IN.ase_texcoord8.xy * _Layer3Normal_ST.xy + _Layer3Normal_ST.zw;
				float3 unpack96 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer3Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Normal ), _Layer3NormalStrength );
				unpack96.z = lerp( 1, unpack96.z, saturate(_Layer3NormalStrength) );
				float3 Layer3Normal105 = unpack96;
				float3 lerpResult156 = lerp( staticSwitch211 , Layer3Normal105 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float3 staticSwitch206 = lerpResult156;
				#else
				float3 staticSwitch206 = staticSwitch211;
				#endif
				float2 uv_Layer4Normal = IN.ase_texcoord8.xy * _Layer4Normal_ST.xy + _Layer4Normal_ST.zw;
				float3 unpack114 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer4Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Normal ), _Layer4NormalStrength );
				unpack114.z = lerp( 1, unpack114.z, saturate(_Layer4NormalStrength) );
				float3 Layer4Normal118 = unpack114;
				float3 lerpResult157 = lerp( lerpResult156 , Layer4Normal118 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float3 staticSwitch207 = lerpResult157;
				#else
				float3 staticSwitch207 = staticSwitch206;
				#endif
				float3 FinalNormalBlend161 = staticSwitch207;
				
				float Layer1Smoothness32 = ( tex2DNode2.a * _Layer1Smoothness );
				float Layer2Smoothness53 = ( tex2DNode46.a * _Layer2Smoothness );
				float lerpResult223 = lerp( Layer1Smoothness32 , Layer2Smoothness53 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float staticSwitch232 = lerpResult223;
				#else
				float staticSwitch232 = Layer1Smoothness32;
				#endif
				float Layer3Smoothness100 = ( tex2DNode99.a * _Layer3Smoothness );
				float lerpResult220 = lerp( staticSwitch232 , Layer3Smoothness100 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float staticSwitch231 = lerpResult220;
				#else
				float staticSwitch231 = staticSwitch232;
				#endif
				float Layer4Smoothness122 = ( tex2DNode115.a * _Layer4Smoothness );
				float lerpResult221 = lerp( staticSwitch231 , Layer4Smoothness122 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float staticSwitch230 = lerpResult221;
				#else
				float staticSwitch230 = staticSwitch231;
				#endif
				float FinalSmoothnessBlend229 = staticSwitch230;
				
				float Layer1AO30 = saturate( ( tex2DNode2.g + ( 1.0 - _Layer1AO ) ) );
				float Layer2AO49 = saturate( ( tex2DNode46.g + ( 1.0 - _Layer2AO ) ) );
				float lerpResult171 = lerp( Layer1AO30 , Layer2AO49 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float staticSwitch212 = lerpResult171;
				#else
				float staticSwitch212 = Layer1AO30;
				#endif
				float lerpResult168 = lerp( staticSwitch212 , Layer3AO103 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float staticSwitch208 = lerpResult168;
				#else
				float staticSwitch208 = staticSwitch212;
				#endif
				float Layer4AO120 = saturate( ( tex2DNode115.g + ( 1.0 - _Layer4AO ) ) );
				float lerpResult169 = lerp( staticSwitch208 , Layer4AO120 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float staticSwitch209 = lerpResult169;
				#else
				float staticSwitch209 = staticSwitch208;
				#endif
				float FinalAOBlend177 = staticSwitch209;
				

				float3 BaseColor = FinalColorBlend141.rgb;
				float3 Normal = FinalNormalBlend161;
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = 0;
				float Smoothness = FinalSmoothnessBlend229;
				float Occlusion = FinalAOBlend177;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = IN.clipPos.z;
				#endif

				#ifdef _CLEARCOAT
					float CoatMask = 0;
					float CoatSmoothness = 0;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				InputData inputData = (InputData)0;
				inputData.positionWS = WorldPosition;
				inputData.viewDirectionWS = WorldViewDirection;

				#ifdef _NORMALMAP
						#if _NORMAL_DROPOFF_TS
							inputData.normalWS = TransformTangentToWorld(Normal, half3x3(WorldTangent, WorldBiTangent, WorldNormal));
						#elif _NORMAL_DROPOFF_OS
							inputData.normalWS = TransformObjectToWorldNormal(Normal);
						#elif _NORMAL_DROPOFF_WS
							inputData.normalWS = Normal;
						#endif
					inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				#else
					inputData.normalWS = WorldNormal;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					inputData.shadowCoord = ShadowCoords;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					inputData.shadowCoord = TransformWorldToShadowCoord(inputData.positionWS);
				#else
					inputData.shadowCoord = float4(0, 0, 0, 0);
				#endif

				#ifdef ASE_FOG
					inputData.fogCoord = IN.fogFactorAndVertexLight.x;
				#endif
					inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = IN.lightmapUVOrVertexSH.xyz;
				#endif

				#if defined(DYNAMICLIGHTMAP_ON)
					inputData.bakedGI = SAMPLE_GI(IN.lightmapUVOrVertexSH.xy, IN.dynamicLightmapUV.xy, SH, inputData.normalWS);
				#else
					inputData.bakedGI = SAMPLE_GI(IN.lightmapUVOrVertexSH.xy, SH, inputData.normalWS);
				#endif

				#ifdef ASE_BAKEDGI
					inputData.bakedGI = BakedGI;
				#endif

				inputData.normalizedScreenSpaceUV = NormalizedScreenSpaceUV;
				inputData.shadowMask = SAMPLE_SHADOWMASK(IN.lightmapUVOrVertexSH.xy);

				#if defined(DEBUG_DISPLAY)
					#if defined(DYNAMICLIGHTMAP_ON)
						inputData.dynamicLightmapUV = IN.dynamicLightmapUV.xy;
					#endif
					#if defined(LIGHTMAP_ON)
						inputData.staticLightmapUV = IN.lightmapUVOrVertexSH.xy;
					#else
						inputData.vertexSH = SH;
					#endif
				#endif

				SurfaceData surfaceData;
				surfaceData.albedo              = BaseColor;
				surfaceData.metallic            = saturate(Metallic);
				surfaceData.specular            = Specular;
				surfaceData.smoothness          = saturate(Smoothness),
				surfaceData.occlusion           = Occlusion,
				surfaceData.emission            = Emission,
				surfaceData.alpha               = saturate(Alpha);
				surfaceData.normalTS            = Normal;
				surfaceData.clearCoatMask       = 0;
				surfaceData.clearCoatSmoothness = 1;

				#ifdef _CLEARCOAT
					surfaceData.clearCoatMask       = saturate(CoatMask);
					surfaceData.clearCoatSmoothness = saturate(CoatSmoothness);
				#endif

				#ifdef _DBUFFER
					ApplyDecalToSurfaceData(IN.clipPos, surfaceData, inputData);
				#endif

				half4 color = UniversalFragmentPBR( inputData, surfaceData);

				#ifdef ASE_TRANSMISSION
				{
					float shadow = _TransmissionShadow;

					#define SUM_LIGHT_TRANSMISSION(Light)\
						float3 atten = Light.color * Light.distanceAttenuation;\
						atten = lerp( atten, atten * Light.shadowAttenuation, shadow );\
						half3 transmission = max( 0, -dot( inputData.normalWS, Light.direction ) ) * atten * Transmission;\
						color.rgb += BaseColor * transmission;

					SUM_LIGHT_TRANSMISSION( GetMainLight( inputData.shadowCoord ) );

					#if defined(_ADDITIONAL_LIGHTS)
						uint meshRenderingLayers = GetMeshRenderingLayer();
						uint pixelLightCount = GetAdditionalLightsCount();
						#if USE_FORWARD_PLUS
							for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
							{
								FORWARD_PLUS_SUBTRACTIVE_LIGHT_CHECK

								Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
								#ifdef _LIGHT_LAYERS
								if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								#endif
								{
									SUM_LIGHT_TRANSMISSION( light );
								}
							}
						#endif
						LIGHT_LOOP_BEGIN( pixelLightCount )
							Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
							#ifdef _LIGHT_LAYERS
							if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
							#endif
							{
								SUM_LIGHT_TRANSMISSION( light );
							}
						LIGHT_LOOP_END
					#endif
				}
				#endif

				#ifdef ASE_TRANSLUCENCY
				{
					float shadow = _TransShadow;
					float normal = _TransNormal;
					float scattering = _TransScattering;
					float direct = _TransDirect;
					float ambient = _TransAmbient;
					float strength = _TransStrength;

					#define SUM_LIGHT_TRANSLUCENCY(Light)\
						float3 atten = Light.color * Light.distanceAttenuation;\
						atten = lerp( atten, atten * Light.shadowAttenuation, shadow );\
						half3 lightDir = Light.direction + inputData.normalWS * normal;\
						half VdotL = pow( saturate( dot( inputData.viewDirectionWS, -lightDir ) ), scattering );\
						half3 translucency = atten * ( VdotL * direct + inputData.bakedGI * ambient ) * Translucency;\
						color.rgb += BaseColor * translucency * strength;

					SUM_LIGHT_TRANSLUCENCY( GetMainLight( inputData.shadowCoord ) );

					#if defined(_ADDITIONAL_LIGHTS)
						uint meshRenderingLayers = GetMeshRenderingLayer();
						uint pixelLightCount = GetAdditionalLightsCount();
						#if USE_FORWARD_PLUS
							for (uint lightIndex = 0; lightIndex < min(URP_FP_DIRECTIONAL_LIGHTS_COUNT, MAX_VISIBLE_LIGHTS); lightIndex++)
							{
								FORWARD_PLUS_SUBTRACTIVE_LIGHT_CHECK

								Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
								#ifdef _LIGHT_LAYERS
								if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
								#endif
								{
									SUM_LIGHT_TRANSLUCENCY( light );
								}
							}
						#endif
						LIGHT_LOOP_BEGIN( pixelLightCount )
							Light light = GetAdditionalLight(lightIndex, inputData.positionWS);
							#ifdef _LIGHT_LAYERS
							if (IsMatchingLightLayer(light.layerMask, meshRenderingLayers))
							#endif
							{
								SUM_LIGHT_TRANSLUCENCY( light );
							}
						LIGHT_LOOP_END
					#endif
				}
				#endif

				#ifdef ASE_REFRACTION
					float4 projScreenPos = ScreenPos / ScreenPos.w;
					float3 refractionOffset = ( RefractionIndex - 1.0 ) * mul( UNITY_MATRIX_V, float4( WorldNormal,0 ) ).xyz * ( 1.0 - dot( WorldNormal, WorldViewDirection ) );
					projScreenPos.xy += refractionOffset.xy;
					float3 refraction = SHADERGRAPH_SAMPLE_SCENE_COLOR( projScreenPos.xy ) * RefractionColor;
					color.rgb = lerp( refraction, color.rgb, color.a );
					color.a = 1;
				#endif

				#ifdef ASE_FINAL_COLOR_ALPHA_MULTIPLY
					color.rgb *= color.a;
				#endif

				#ifdef ASE_FOG
					#ifdef TERRAIN_SPLAT_ADDPASS
						color.rgb = MixFogColor(color.rgb, half3( 0, 0, 0 ), IN.fogFactorAndVertexLight.x );
					#else
						color.rgb = MixFog(color.rgb, IN.fogFactorAndVertexLight.x);
					#endif
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				#ifdef _WRITE_RENDERING_LAYERS
					uint renderingLayers = GetMeshRenderingLayer();
					outRenderingLayers = float4( EncodeMeshRenderingLayer( renderingLayers ), 0, 0, 0 );
				#endif

				return color;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }

			ZWrite On
			ZTest LEqual
			AlphaToMask Off
			ColorMask 0

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_vertex _ _CASTING_PUNCTUAL_LIGHT_SHADOW

			#define SHADERPASS SHADERPASS_SHADOWCASTER

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				ASE_SV_POSITION_QUALIFIERS float4 clipPos : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 worldPos : TEXCOORD1;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD2;
				#endif				
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			float3 _LightDirection;
			float3 _LightPosition;

			VertexOutput VertexFunction( VertexInput v )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				float3 normalWS = TransformObjectToWorldDir(v.ase_normal);

				#if _CASTING_PUNCTUAL_LIGHT_SHADOW
					float3 lightDirectionWS = normalize(_LightPosition - positionWS);
				#else
					float3 lightDirectionWS = _LightDirection;
				#endif

				float4 clipPos = TransformWorldToHClip(ApplyShadowBias(positionWS, normalWS, lightDirectionWS));

				#if UNITY_REVERSED_Z
					clipPos.z = min(clipPos.z, UNITY_NEAR_CLIP_VALUE);
				#else
					clipPos.z = max(clipPos.z, UNITY_NEAR_CLIP_VALUE);
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = clipPos;
				o.clipPosV = clipPos;
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(	VertexOutput IN
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				float4 ClipPos = IN.clipPosV;
				float4 ScreenPos = ComputeScreenPos( IN.clipPosV );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = IN.clipPos.z;
				#endif

				#ifdef _ALPHATEST_ON
					#ifdef _ALPHATEST_SHADOW_ON
						clip(Alpha - AlphaClipThresholdShadow);
					#else
						clip(Alpha - AlphaClipThreshold);
					#endif
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthOnly"
			Tags { "LightMode"="DepthOnly" }

			ZWrite On
			ColorMask R
			AlphaToMask Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"
			
			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			

			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				ASE_SV_POSITION_QUALIFIERS float4 clipPos : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 worldPos : TEXCOORD1;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
				float4 shadowCoord : TEXCOORD2;
				#endif
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

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

				o.clipPos = positionCS;
				o.clipPosV = positionCS;
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(	VertexOutput IN
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						 ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
				float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				float4 ClipPos = IN.clipPosV;
				float4 ScreenPos = ComputeScreenPos( IN.clipPosV );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				

				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = IN.clipPos.z;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return 0;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Meta"
			Tags { "LightMode"="Meta" }

			Cull Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma vertex vert
			#pragma fragment frag

			#pragma shader_feature EDITOR_VISUALIZATION

			#define SHADERPASS SHADERPASS_META

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/MetaInput.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _ENABLELAYER4_ON
			#pragma shader_feature_local _ENABLELAYER3_ON
			#pragma shader_feature_local _ENABLELAYER2_ON
			#pragma shader_feature_local _LAYER1USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER2USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER3USEAOASHEIGHT_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
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
				#ifdef EDITOR_VISUALIZATION
					float4 VizUV : TEXCOORD2;
					float4 LightCoord : TEXCOORD3;
				#endif
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			TEXTURE2D(_Layer1BaseColor);
			SAMPLER(sampler_Trilinear_Repeat_Aniso16);
			TEXTURE2D(_Layer2BaseColor);
			TEXTURE2D(_Layer1Mask);
			TEXTURE2D(_Layer3BaseColor);
			TEXTURE2D(_Layer2Mask);
			TEXTURE2D(_Layer4BaseColor);
			TEXTURE2D(_Layer4Mask);
			TEXTURE2D(_Layer3Mask);


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord4.xy = v.texcoord0.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord4.zw = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				o.clipPos = MetaVertexPosition( v.vertex, v.texcoord1.xy, v.texcoord1.xy, unity_LightmapST, unity_DynamicLightmapST );

				#ifdef EDITOR_VISUALIZATION
					float2 VizUV = 0;
					float4 LightCoord = 0;
					UnityEditorVizData(v.vertex.xyz, v.texcoord0.xy, v.texcoord1.xy, v.texcoord2.xy, VizUV, LightCoord);
					o.VizUV = float4(VizUV, 0, 0);
					o.LightCoord = LightCoord;
				#endif

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = o.clipPos;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 texcoord0 : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.texcoord0 = v.texcoord0;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.texcoord0 = patch[0].texcoord0 * bary.x + patch[1].texcoord0 * bary.y + patch[2].texcoord0 * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
			{
				UNITY_SETUP_INSTANCE_ID(IN);
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

				float2 uv_Layer1BaseColor = IN.ase_texcoord4.xy * _Layer1BaseColor_ST.xy + _Layer1BaseColor_ST.zw;
				float3 desaturateInitialColor235 = ( _Layer1Color * SAMPLE_TEXTURE2D( _Layer1BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer1BaseColor ) ).rgb;
				float desaturateDot235 = dot( desaturateInitialColor235, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar235 = lerp( desaturateInitialColor235, desaturateDot235.xxx, ( 1.0 - _Layer1Saturation ) );
				float3 Layer1Color27 = desaturateVar235;
				float2 uv_Layer2BaseColor = IN.ase_texcoord4.xy * _Layer2BaseColor_ST.xy + _Layer2BaseColor_ST.zw;
				float4 Layer2Color58 = ( _Layer2Color * SAMPLE_TEXTURE2D( _Layer2BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer2BaseColor ) );
				float2 uv_Layer1Mask = IN.ase_texcoord4.xy * _Layer1Mask_ST.xy + _Layer1Mask_ST.zw;
				float4 tex2DNode2 = SAMPLE_TEXTURE2D( _Layer1Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Mask );
				float Layer1Height31 = tex2DNode2.b;
				float Layer1RawAO201 = tex2DNode2.g;
				#ifdef _LAYER1USEAOASHEIGHT_ON
				float staticSwitch63 = ( 1.0 - Layer1RawAO201 );
				#else
				float staticSwitch63 = ( 1.0 - Layer1Height31 );
				#endif
				float VCRed18 = IN.ase_color.r;
				float HeightMask10 = saturate(pow(max( (((staticSwitch63*VCRed18)*4)+(VCRed18*2)), 0 ),_Layer2BlendSharpness));
				float Layer2Mask143 = saturate( HeightMask10 );
				float4 lerpResult14 = lerp( float4( Layer1Color27 , 0.0 ) , Layer2Color58 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float4 staticSwitch210 = lerpResult14;
				#else
				float4 staticSwitch210 = float4( Layer1Color27 , 0.0 );
				#endif
				float2 uv_Layer3BaseColor = IN.ase_texcoord4.xy * _Layer3BaseColor_ST.xy + _Layer3BaseColor_ST.zw;
				float4 Layer3Color98 = ( _Layer3Color * SAMPLE_TEXTURE2D( _Layer3BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer3BaseColor ) );
				float2 uv_Layer2Mask = IN.ase_texcoord4.xy * _Layer2Mask_ST.xy + _Layer2Mask_ST.zw;
				float4 tex2DNode46 = SAMPLE_TEXTURE2D( _Layer2Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Mask );
				float Layer2Height54 = tex2DNode46.b;
				float lerpResult186 = lerp( staticSwitch63 , ( 1.0 - Layer2Height54 ) , Layer2Mask143);
				float Layer2RawAO202 = tex2DNode46.g;
				float lerpResult200 = lerp( staticSwitch63 , ( 1.0 - Layer2RawAO202 ) , Layer2Mask143);
				#ifdef _LAYER2USEAOASHEIGHT_ON
				float staticSwitch130 = lerpResult200;
				#else
				float staticSwitch130 = lerpResult186;
				#endif
				float VCGreen19 = IN.ase_color.g;
				float HeightMask126 = saturate(pow(max( (((staticSwitch130*VCGreen19)*4)+(VCGreen19*2)), 0 ),_Layer3BlendSharpness));
				float Layer3Mask144 = saturate( HeightMask126 );
				float4 lerpResult67 = lerp( staticSwitch210 , Layer3Color98 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float4 staticSwitch204 = lerpResult67;
				#else
				float4 staticSwitch204 = staticSwitch210;
				#endif
				float2 uv_Layer4BaseColor = IN.ase_texcoord4.xy * _Layer4BaseColor_ST.xy + _Layer4BaseColor_ST.zw;
				float4 Layer4Color117 = ( _Layer4Color * SAMPLE_TEXTURE2D( _Layer4BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer4BaseColor ) );
				float2 uv_Layer4Mask = IN.ase_texcoord4.xy * _Layer4Mask_ST.xy + _Layer4Mask_ST.zw;
				float4 tex2DNode115 = SAMPLE_TEXTURE2D( _Layer4Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Mask );
				float Layer4Height121 = tex2DNode115.b;
				float lerpResult189 = lerp( staticSwitch130 , ( 1.0 - Layer4Height121 ) , Layer3Mask144);
				float2 uv_Layer3Mask = IN.ase_texcoord4.xy * _Layer3Mask_ST.xy + _Layer3Mask_ST.zw;
				float4 tex2DNode99 = SAMPLE_TEXTURE2D( _Layer3Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Mask );
				float Layer3AO103 = saturate( ( tex2DNode99.g + ( 1.0 - _Layer3AO ) ) );
				float lerpResult214 = lerp( staticSwitch130 , ( 1.0 - Layer3AO103 ) , Layer3Mask144);
				#ifdef _LAYER3USEAOASHEIGHT_ON
				float staticSwitch140 = lerpResult214;
				#else
				float staticSwitch140 = lerpResult189;
				#endif
				float VCBlue20 = IN.ase_color.b;
				float HeightMask134 = saturate(pow(max( (((staticSwitch140*VCBlue20)*4)+(VCBlue20*2)), 0 ),_Layer4BlendSharpness));
				float Layer4Mask145 = saturate( HeightMask134 );
				float4 lerpResult139 = lerp( staticSwitch204 , Layer4Color117 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float4 staticSwitch205 = lerpResult139;
				#else
				float4 staticSwitch205 = staticSwitch204;
				#endif
				float4 FinalColorBlend141 = staticSwitch205;
				

				float3 BaseColor = FinalColorBlend141.rgb;
				float3 Emission = 0;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				MetaInput metaInput = (MetaInput)0;
				metaInput.Albedo = BaseColor;
				metaInput.Emission = Emission;
				#ifdef EDITOR_VISUALIZATION
					metaInput.VizUV = IN.VizUV.xy;
					metaInput.LightCoord = IN.LightCoord;
				#endif

				return UnityMetaFragment(metaInput);
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "Universal2D"
			Tags { "LightMode"="Universal2D" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_2D

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _ENABLELAYER4_ON
			#pragma shader_feature_local _ENABLELAYER3_ON
			#pragma shader_feature_local _ENABLELAYER2_ON
			#pragma shader_feature_local _LAYER1USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER2USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER3USEAOASHEIGHT_ON


			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
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
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			TEXTURE2D(_Layer1BaseColor);
			SAMPLER(sampler_Trilinear_Repeat_Aniso16);
			TEXTURE2D(_Layer2BaseColor);
			TEXTURE2D(_Layer1Mask);
			TEXTURE2D(_Layer3BaseColor);
			TEXTURE2D(_Layer2Mask);
			TEXTURE2D(_Layer4BaseColor);
			TEXTURE2D(_Layer4Mask);
			TEXTURE2D(_Layer3Mask);


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				o.ase_texcoord2.xy = v.ase_texcoord.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord2.zw = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

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

				o.clipPos = positionCS;

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN  ) : SV_TARGET
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

				float2 uv_Layer1BaseColor = IN.ase_texcoord2.xy * _Layer1BaseColor_ST.xy + _Layer1BaseColor_ST.zw;
				float3 desaturateInitialColor235 = ( _Layer1Color * SAMPLE_TEXTURE2D( _Layer1BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer1BaseColor ) ).rgb;
				float desaturateDot235 = dot( desaturateInitialColor235, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar235 = lerp( desaturateInitialColor235, desaturateDot235.xxx, ( 1.0 - _Layer1Saturation ) );
				float3 Layer1Color27 = desaturateVar235;
				float2 uv_Layer2BaseColor = IN.ase_texcoord2.xy * _Layer2BaseColor_ST.xy + _Layer2BaseColor_ST.zw;
				float4 Layer2Color58 = ( _Layer2Color * SAMPLE_TEXTURE2D( _Layer2BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer2BaseColor ) );
				float2 uv_Layer1Mask = IN.ase_texcoord2.xy * _Layer1Mask_ST.xy + _Layer1Mask_ST.zw;
				float4 tex2DNode2 = SAMPLE_TEXTURE2D( _Layer1Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Mask );
				float Layer1Height31 = tex2DNode2.b;
				float Layer1RawAO201 = tex2DNode2.g;
				#ifdef _LAYER1USEAOASHEIGHT_ON
				float staticSwitch63 = ( 1.0 - Layer1RawAO201 );
				#else
				float staticSwitch63 = ( 1.0 - Layer1Height31 );
				#endif
				float VCRed18 = IN.ase_color.r;
				float HeightMask10 = saturate(pow(max( (((staticSwitch63*VCRed18)*4)+(VCRed18*2)), 0 ),_Layer2BlendSharpness));
				float Layer2Mask143 = saturate( HeightMask10 );
				float4 lerpResult14 = lerp( float4( Layer1Color27 , 0.0 ) , Layer2Color58 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float4 staticSwitch210 = lerpResult14;
				#else
				float4 staticSwitch210 = float4( Layer1Color27 , 0.0 );
				#endif
				float2 uv_Layer3BaseColor = IN.ase_texcoord2.xy * _Layer3BaseColor_ST.xy + _Layer3BaseColor_ST.zw;
				float4 Layer3Color98 = ( _Layer3Color * SAMPLE_TEXTURE2D( _Layer3BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer3BaseColor ) );
				float2 uv_Layer2Mask = IN.ase_texcoord2.xy * _Layer2Mask_ST.xy + _Layer2Mask_ST.zw;
				float4 tex2DNode46 = SAMPLE_TEXTURE2D( _Layer2Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Mask );
				float Layer2Height54 = tex2DNode46.b;
				float lerpResult186 = lerp( staticSwitch63 , ( 1.0 - Layer2Height54 ) , Layer2Mask143);
				float Layer2RawAO202 = tex2DNode46.g;
				float lerpResult200 = lerp( staticSwitch63 , ( 1.0 - Layer2RawAO202 ) , Layer2Mask143);
				#ifdef _LAYER2USEAOASHEIGHT_ON
				float staticSwitch130 = lerpResult200;
				#else
				float staticSwitch130 = lerpResult186;
				#endif
				float VCGreen19 = IN.ase_color.g;
				float HeightMask126 = saturate(pow(max( (((staticSwitch130*VCGreen19)*4)+(VCGreen19*2)), 0 ),_Layer3BlendSharpness));
				float Layer3Mask144 = saturate( HeightMask126 );
				float4 lerpResult67 = lerp( staticSwitch210 , Layer3Color98 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float4 staticSwitch204 = lerpResult67;
				#else
				float4 staticSwitch204 = staticSwitch210;
				#endif
				float2 uv_Layer4BaseColor = IN.ase_texcoord2.xy * _Layer4BaseColor_ST.xy + _Layer4BaseColor_ST.zw;
				float4 Layer4Color117 = ( _Layer4Color * SAMPLE_TEXTURE2D( _Layer4BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer4BaseColor ) );
				float2 uv_Layer4Mask = IN.ase_texcoord2.xy * _Layer4Mask_ST.xy + _Layer4Mask_ST.zw;
				float4 tex2DNode115 = SAMPLE_TEXTURE2D( _Layer4Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Mask );
				float Layer4Height121 = tex2DNode115.b;
				float lerpResult189 = lerp( staticSwitch130 , ( 1.0 - Layer4Height121 ) , Layer3Mask144);
				float2 uv_Layer3Mask = IN.ase_texcoord2.xy * _Layer3Mask_ST.xy + _Layer3Mask_ST.zw;
				float4 tex2DNode99 = SAMPLE_TEXTURE2D( _Layer3Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Mask );
				float Layer3AO103 = saturate( ( tex2DNode99.g + ( 1.0 - _Layer3AO ) ) );
				float lerpResult214 = lerp( staticSwitch130 , ( 1.0 - Layer3AO103 ) , Layer3Mask144);
				#ifdef _LAYER3USEAOASHEIGHT_ON
				float staticSwitch140 = lerpResult214;
				#else
				float staticSwitch140 = lerpResult189;
				#endif
				float VCBlue20 = IN.ase_color.b;
				float HeightMask134 = saturate(pow(max( (((staticSwitch140*VCBlue20)*4)+(VCBlue20*2)), 0 ),_Layer4BlendSharpness));
				float Layer4Mask145 = saturate( HeightMask134 );
				float4 lerpResult139 = lerp( staticSwitch204 , Layer4Color117 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float4 staticSwitch205 = lerpResult139;
				#else
				float4 staticSwitch205 = staticSwitch204;
				#endif
				float4 FinalColorBlend141 = staticSwitch205;
				

				float3 BaseColor = FinalColorBlend141.rgb;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;

				half4 color = half4(BaseColor, Alpha );

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				return color;
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthNormals"
			Tags { "LightMode"="DepthNormals" }

			ZWrite On
			Blend One Zero
			ZTest LEqual
			ZWrite On

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma vertex vert
			#pragma fragment frag

			#pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS

			#define SHADERPASS SHADERPASS_DEPTHNORMALSONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _ENABLELAYER4_ON
			#pragma shader_feature_local _ENABLELAYER3_ON
			#pragma shader_feature_local _ENABLELAYER2_ON
			#pragma shader_feature_local _LAYER1USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER2USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER3USEAOASHEIGHT_ON


			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				ASE_SV_POSITION_QUALIFIERS float4 clipPos : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float3 worldNormal : TEXCOORD1;
				float4 worldTangent : TEXCOORD2;
				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 worldPos : TEXCOORD3;
				#endif
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					float4 shadowCoord : TEXCOORD4;
				#endif
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			TEXTURE2D(_Layer1Normal);
			SAMPLER(sampler_Trilinear_Repeat_Aniso16);
			TEXTURE2D(_Layer2Normal);
			TEXTURE2D(_Layer1Mask);
			TEXTURE2D(_Layer3Normal);
			TEXTURE2D(_Layer2Mask);
			TEXTURE2D(_Layer4Normal);
			TEXTURE2D(_Layer4Mask);
			TEXTURE2D(_Layer3Mask);


			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord5.xy = v.ase_texcoord.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord5.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;
				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 normalWS = TransformObjectToWorldNormal( v.ase_normal );
				float4 tangentWS = float4(TransformObjectToWorldDir( v.ase_tangent.xyz), v.ase_tangent.w);
				float4 positionCS = TransformWorldToHClip( positionWS );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					o.worldPos = positionWS;
				#endif

				o.worldNormal = normalWS;
				o.worldTangent = tangentWS;

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR) && defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;
				o.clipPosV = positionCS;
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_tangent = v.ase_tangent;
				o.ase_texcoord = v.ase_texcoord;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_tangent = patch[0].ase_tangent * bary.x + patch[1].ase_tangent * bary.y + patch[2].ase_tangent * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void frag(	VertexOutput IN
						, out half4 outNormalWS : SV_Target0
						#ifdef ASE_DEPTH_WRITE_ON
						,out float outputDepth : ASE_SV_DEPTH
						#endif
						#ifdef _WRITE_RENDERING_LAYERS
						, out float4 outRenderingLayers : SV_Target1
						#endif
						 )
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( IN );

				#if defined(ASE_NEEDS_FRAG_WORLD_POSITION)
					float3 WorldPosition = IN.worldPos;
				#endif

				float4 ShadowCoords = float4( 0, 0, 0, 0 );
				float3 WorldNormal = IN.worldNormal;
				float4 WorldTangent = IN.worldTangent;

				float4 ClipPos = IN.clipPosV;
				float4 ScreenPos = ComputeScreenPos( IN.clipPosV );

				#if defined(ASE_NEEDS_FRAG_SHADOWCOORDS)
					#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
						ShadowCoords = IN.shadowCoord;
					#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
						ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
					#endif
				#endif

				float2 uv_Layer1Normal = IN.ase_texcoord5.xy * _Layer1Normal_ST.xy + _Layer1Normal_ST.zw;
				float3 unpack1 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer1Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Normal ), _Layer1NormalStrength );
				unpack1.z = lerp( 1, unpack1.z, saturate(_Layer1NormalStrength) );
				float3 Layer1Normal28 = unpack1;
				float2 uv_Layer2Normal = IN.ase_texcoord5.xy * _Layer2Normal_ST.xy + _Layer2Normal_ST.zw;
				float3 unpack45 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer2Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Normal ), _Layer2NormalStrength );
				unpack45.z = lerp( 1, unpack45.z, saturate(_Layer2NormalStrength) );
				float3 Layer2Normal56 = unpack45;
				float2 uv_Layer1Mask = IN.ase_texcoord5.xy * _Layer1Mask_ST.xy + _Layer1Mask_ST.zw;
				float4 tex2DNode2 = SAMPLE_TEXTURE2D( _Layer1Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Mask );
				float Layer1Height31 = tex2DNode2.b;
				float Layer1RawAO201 = tex2DNode2.g;
				#ifdef _LAYER1USEAOASHEIGHT_ON
				float staticSwitch63 = ( 1.0 - Layer1RawAO201 );
				#else
				float staticSwitch63 = ( 1.0 - Layer1Height31 );
				#endif
				float VCRed18 = IN.ase_color.r;
				float HeightMask10 = saturate(pow(max( (((staticSwitch63*VCRed18)*4)+(VCRed18*2)), 0 ),_Layer2BlendSharpness));
				float Layer2Mask143 = saturate( HeightMask10 );
				float3 lerpResult163 = lerp( Layer1Normal28 , Layer2Normal56 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float3 staticSwitch211 = lerpResult163;
				#else
				float3 staticSwitch211 = Layer1Normal28;
				#endif
				float2 uv_Layer3Normal = IN.ase_texcoord5.xy * _Layer3Normal_ST.xy + _Layer3Normal_ST.zw;
				float3 unpack96 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer3Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Normal ), _Layer3NormalStrength );
				unpack96.z = lerp( 1, unpack96.z, saturate(_Layer3NormalStrength) );
				float3 Layer3Normal105 = unpack96;
				float2 uv_Layer2Mask = IN.ase_texcoord5.xy * _Layer2Mask_ST.xy + _Layer2Mask_ST.zw;
				float4 tex2DNode46 = SAMPLE_TEXTURE2D( _Layer2Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Mask );
				float Layer2Height54 = tex2DNode46.b;
				float lerpResult186 = lerp( staticSwitch63 , ( 1.0 - Layer2Height54 ) , Layer2Mask143);
				float Layer2RawAO202 = tex2DNode46.g;
				float lerpResult200 = lerp( staticSwitch63 , ( 1.0 - Layer2RawAO202 ) , Layer2Mask143);
				#ifdef _LAYER2USEAOASHEIGHT_ON
				float staticSwitch130 = lerpResult200;
				#else
				float staticSwitch130 = lerpResult186;
				#endif
				float VCGreen19 = IN.ase_color.g;
				float HeightMask126 = saturate(pow(max( (((staticSwitch130*VCGreen19)*4)+(VCGreen19*2)), 0 ),_Layer3BlendSharpness));
				float Layer3Mask144 = saturate( HeightMask126 );
				float3 lerpResult156 = lerp( staticSwitch211 , Layer3Normal105 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float3 staticSwitch206 = lerpResult156;
				#else
				float3 staticSwitch206 = staticSwitch211;
				#endif
				float2 uv_Layer4Normal = IN.ase_texcoord5.xy * _Layer4Normal_ST.xy + _Layer4Normal_ST.zw;
				float3 unpack114 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer4Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Normal ), _Layer4NormalStrength );
				unpack114.z = lerp( 1, unpack114.z, saturate(_Layer4NormalStrength) );
				float3 Layer4Normal118 = unpack114;
				float2 uv_Layer4Mask = IN.ase_texcoord5.xy * _Layer4Mask_ST.xy + _Layer4Mask_ST.zw;
				float4 tex2DNode115 = SAMPLE_TEXTURE2D( _Layer4Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Mask );
				float Layer4Height121 = tex2DNode115.b;
				float lerpResult189 = lerp( staticSwitch130 , ( 1.0 - Layer4Height121 ) , Layer3Mask144);
				float2 uv_Layer3Mask = IN.ase_texcoord5.xy * _Layer3Mask_ST.xy + _Layer3Mask_ST.zw;
				float4 tex2DNode99 = SAMPLE_TEXTURE2D( _Layer3Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Mask );
				float Layer3AO103 = saturate( ( tex2DNode99.g + ( 1.0 - _Layer3AO ) ) );
				float lerpResult214 = lerp( staticSwitch130 , ( 1.0 - Layer3AO103 ) , Layer3Mask144);
				#ifdef _LAYER3USEAOASHEIGHT_ON
				float staticSwitch140 = lerpResult214;
				#else
				float staticSwitch140 = lerpResult189;
				#endif
				float VCBlue20 = IN.ase_color.b;
				float HeightMask134 = saturate(pow(max( (((staticSwitch140*VCBlue20)*4)+(VCBlue20*2)), 0 ),_Layer4BlendSharpness));
				float Layer4Mask145 = saturate( HeightMask134 );
				float3 lerpResult157 = lerp( lerpResult156 , Layer4Normal118 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float3 staticSwitch207 = lerpResult157;
				#else
				float3 staticSwitch207 = staticSwitch206;
				#endif
				float3 FinalNormalBlend161 = staticSwitch207;
				

				float3 Normal = FinalNormalBlend161;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = IN.clipPos.z;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				#if defined(_GBUFFER_NORMALS_OCT)
					float2 octNormalWS = PackNormalOctQuadEncode(WorldNormal);
					float2 remappedOctNormalWS = saturate(octNormalWS * 0.5 + 0.5);
					half3 packedNormalWS = PackFloat2To888(remappedOctNormalWS);
					outNormalWS = half4(packedNormalWS, 0.0);
				#else
					#if defined(_NORMALMAP)
						#if _NORMAL_DROPOFF_TS
							float crossSign = (WorldTangent.w > 0.0 ? 1.0 : -1.0) * GetOddNegativeScale();
							float3 bitangent = crossSign * cross(WorldNormal.xyz, WorldTangent.xyz);
							float3 normalWS = TransformTangentToWorld(Normal, half3x3(WorldTangent.xyz, bitangent, WorldNormal.xyz));
						#elif _NORMAL_DROPOFF_OS
							float3 normalWS = TransformObjectToWorldNormal(Normal);
						#elif _NORMAL_DROPOFF_WS
							float3 normalWS = Normal;
						#endif
					#else
						float3 normalWS = WorldNormal;
					#endif
					outNormalWS = half4(NormalizeNormalPerPixel(normalWS), 0.0);
				#endif

				#ifdef _WRITE_RENDERING_LAYERS
					uint renderingLayers = GetMeshRenderingLayer();
					outRenderingLayers = float4( EncodeMeshRenderingLayer( renderingLayers ), 0, 0, 0 );
				#endif
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "GBuffer"
			Tags { "LightMode"="UniversalGBuffer" }

			Blend One Zero, One Zero
			ZWrite On
			ZTest LEqual
			Offset 0 , 0
			ColorMask RGBA
			

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#pragma multi_compile_instancing
			#pragma instancing_options renderinglayer
			#pragma multi_compile_fragment _ LOD_FADE_CROSSFADE
			#pragma multi_compile_fog
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma shader_feature_local _RECEIVE_SHADOWS_OFF
			#pragma shader_feature_local_fragment _SPECULARHIGHLIGHTS_OFF
			#pragma shader_feature_local_fragment _ENVIRONMENTREFLECTIONS_OFF

			#pragma multi_compile _ _MAIN_LIGHT_SHADOWS _MAIN_LIGHT_SHADOWS_CASCADE _MAIN_LIGHT_SHADOWS_SCREEN
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BLENDING
			#pragma multi_compile_fragment _ _REFLECTION_PROBE_BOX_PROJECTION
			#pragma multi_compile_fragment _ _SHADOWS_SOFT
			#pragma multi_compile_fragment _ _DBUFFER_MRT1 _DBUFFER_MRT2 _DBUFFER_MRT3
			#pragma multi_compile_fragment _ _RENDER_PASS_ENABLED

			#pragma multi_compile _ LIGHTMAP_SHADOW_MIXING
			#pragma multi_compile _ SHADOWS_SHADOWMASK
			#pragma multi_compile _ DIRLIGHTMAP_COMBINED
			#pragma multi_compile _ LIGHTMAP_ON
			#pragma multi_compile _ DYNAMICLIGHTMAP_ON
			#pragma multi_compile_fragment _ _GBUFFER_NORMALS_OCT
			#pragma multi_compile_fragment _ _WRITE_RENDERING_LAYERS

			#pragma vertex vert
			#pragma fragment frag

			#define SHADERPASS SHADERPASS_GBUFFER

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Shadows.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/DBuffer.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			#if defined(LOD_FADE_CROSSFADE)
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/LODCrossFade.hlsl"
            #endif
			
			#if defined(UNITY_INSTANCING_ENABLED) && defined(_TERRAIN_INSTANCED_PERPIXEL_NORMAL)
				#define ENABLE_TERRAIN_PERPIXEL_NORMAL
			#endif

			#define ASE_NEEDS_FRAG_COLOR
			#pragma shader_feature_local _ENABLELAYER4_ON
			#pragma shader_feature_local _ENABLELAYER3_ON
			#pragma shader_feature_local _ENABLELAYER2_ON
			#pragma shader_feature_local _LAYER1USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER2USEAOASHEIGHT_ON
			#pragma shader_feature_local _LAYER3USEAOASHEIGHT_ON


			#if defined(ASE_EARLY_Z_DEPTH_OPTIMIZE) && (SHADER_TARGET >= 45)
				#define ASE_SV_DEPTH SV_DepthLessEqual
				#define ASE_SV_POSITION_QUALIFIERS linear noperspective centroid
			#else
				#define ASE_SV_DEPTH SV_Depth
				#define ASE_SV_POSITION_QUALIFIERS
			#endif

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				ASE_SV_POSITION_QUALIFIERS float4 clipPos : SV_POSITION;
				float4 clipPosV : TEXCOORD0;
				float4 lightmapUVOrVertexSH : TEXCOORD1;
				half4 fogFactorAndVertexLight : TEXCOORD2;
				float4 tSpace0 : TEXCOORD3;
				float4 tSpace1 : TEXCOORD4;
				float4 tSpace2 : TEXCOORD5;
				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
				float4 shadowCoord : TEXCOORD6;
				#endif
				#if defined(DYNAMICLIGHTMAP_ON)
				float2 dynamicLightmapUV : TEXCOORD7;
				#endif
				float4 ase_texcoord8 : TEXCOORD8;
				float4 ase_color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			TEXTURE2D(_Layer1BaseColor);
			SAMPLER(sampler_Trilinear_Repeat_Aniso16);
			TEXTURE2D(_Layer2BaseColor);
			TEXTURE2D(_Layer1Mask);
			TEXTURE2D(_Layer3BaseColor);
			TEXTURE2D(_Layer2Mask);
			TEXTURE2D(_Layer4BaseColor);
			TEXTURE2D(_Layer4Mask);
			TEXTURE2D(_Layer3Mask);
			TEXTURE2D(_Layer1Normal);
			TEXTURE2D(_Layer2Normal);
			TEXTURE2D(_Layer3Normal);
			TEXTURE2D(_Layer4Normal);


			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/UnityGBuffer.hlsl"

			
			VertexOutput VertexFunction( VertexInput v  )
			{
				VertexOutput o = (VertexOutput)0;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				o.ase_texcoord8.xy = v.texcoord.xy;
				o.ase_color = v.ase_color;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord8.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				float3 positionVS = TransformWorldToView( positionWS );
				float4 positionCS = TransformWorldToHClip( positionWS );

				VertexNormalInputs normalInput = GetVertexNormalInputs( v.ase_normal, v.ase_tangent );

				o.tSpace0 = float4( normalInput.normalWS, positionWS.x);
				o.tSpace1 = float4( normalInput.tangentWS, positionWS.y);
				o.tSpace2 = float4( normalInput.bitangentWS, positionWS.z);

				#if defined(LIGHTMAP_ON)
					OUTPUT_LIGHTMAP_UV(v.texcoord1, unity_LightmapST, o.lightmapUVOrVertexSH.xy);
				#endif

				#if defined(DYNAMICLIGHTMAP_ON)
					o.dynamicLightmapUV.xy = v.texcoord2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				#endif

				#if !defined(LIGHTMAP_ON)
					OUTPUT_SH(normalInput.normalWS.xyz, o.lightmapUVOrVertexSH.xyz);
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					o.lightmapUVOrVertexSH.zw = v.texcoord.xy;
					o.lightmapUVOrVertexSH.xy = v.texcoord.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				#endif

				half3 vertexLight = VertexLighting( positionWS, normalInput.normalWS );

				o.fogFactorAndVertexLight = half4(0, vertexLight);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					VertexPositionInputs vertexInput = (VertexPositionInputs)0;
					vertexInput.positionWS = positionWS;
					vertexInput.positionCS = positionCS;
					o.shadowCoord = GetShadowCoord( vertexInput );
				#endif

				o.clipPos = positionCS;
				o.clipPosV = positionCS;
				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				float4 ase_tangent : TANGENT;
				float4 texcoord : TEXCOORD0;
				float4 texcoord1 : TEXCOORD1;
				float4 texcoord2 : TEXCOORD2;
				float4 ase_color : COLOR;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				o.ase_tangent = v.ase_tangent;
				o.texcoord = v.texcoord;
				o.texcoord1 = v.texcoord1;
				o.texcoord2 = v.texcoord2;
				o.ase_color = v.ase_color;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				o.ase_tangent = patch[0].ase_tangent * bary.x + patch[1].ase_tangent * bary.y + patch[2].ase_tangent * bary.z;
				o.texcoord = patch[0].texcoord * bary.x + patch[1].texcoord * bary.y + patch[2].texcoord * bary.z;
				o.texcoord1 = patch[0].texcoord1 * bary.x + patch[1].texcoord1 * bary.y + patch[2].texcoord1 * bary.z;
				o.texcoord2 = patch[0].texcoord2 * bary.x + patch[1].texcoord2 * bary.y + patch[2].texcoord2 * bary.z;
				o.ase_color = patch[0].ase_color * bary.x + patch[1].ase_color * bary.y + patch[2].ase_color * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			FragmentOutput frag ( VertexOutput IN
								#ifdef ASE_DEPTH_WRITE_ON
								,out float outputDepth : ASE_SV_DEPTH
								#endif
								 )
			{
				UNITY_SETUP_INSTANCE_ID(IN);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(IN);

				#ifdef LOD_FADE_CROSSFADE
					LODFadeCrossFade( IN.clipPos );
				#endif

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float2 sampleCoords = (IN.lightmapUVOrVertexSH.zw / _TerrainHeightmapRecipSize.zw + 0.5f) * _TerrainHeightmapRecipSize.xy;
					float3 WorldNormal = TransformObjectToWorldNormal(normalize(SAMPLE_TEXTURE2D(_TerrainNormalmapTexture, sampler_TerrainNormalmapTexture, sampleCoords).rgb * 2 - 1));
					float3 WorldTangent = -cross(GetObjectToWorldMatrix()._13_23_33, WorldNormal);
					float3 WorldBiTangent = cross(WorldNormal, -WorldTangent);
				#else
					float3 WorldNormal = normalize( IN.tSpace0.xyz );
					float3 WorldTangent = IN.tSpace1.xyz;
					float3 WorldBiTangent = IN.tSpace2.xyz;
				#endif

				float3 WorldPosition = float3(IN.tSpace0.w,IN.tSpace1.w,IN.tSpace2.w);
				float3 WorldViewDirection = _WorldSpaceCameraPos.xyz  - WorldPosition;
				float4 ShadowCoords = float4( 0, 0, 0, 0 );

				float4 ClipPos = IN.clipPosV;
				float4 ScreenPos = ComputeScreenPos( IN.clipPosV );

				float2 NormalizedScreenSpaceUV = GetNormalizedScreenSpaceUV(IN.clipPos);

				#if defined(REQUIRES_VERTEX_SHADOW_COORD_INTERPOLATOR)
					ShadowCoords = IN.shadowCoord;
				#elif defined(MAIN_LIGHT_CALCULATE_SHADOWS)
					ShadowCoords = TransformWorldToShadowCoord( WorldPosition );
				#else
					ShadowCoords = float4(0, 0, 0, 0);
				#endif

				WorldViewDirection = SafeNormalize( WorldViewDirection );

				float2 uv_Layer1BaseColor = IN.ase_texcoord8.xy * _Layer1BaseColor_ST.xy + _Layer1BaseColor_ST.zw;
				float3 desaturateInitialColor235 = ( _Layer1Color * SAMPLE_TEXTURE2D( _Layer1BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer1BaseColor ) ).rgb;
				float desaturateDot235 = dot( desaturateInitialColor235, float3( 0.299, 0.587, 0.114 ));
				float3 desaturateVar235 = lerp( desaturateInitialColor235, desaturateDot235.xxx, ( 1.0 - _Layer1Saturation ) );
				float3 Layer1Color27 = desaturateVar235;
				float2 uv_Layer2BaseColor = IN.ase_texcoord8.xy * _Layer2BaseColor_ST.xy + _Layer2BaseColor_ST.zw;
				float4 Layer2Color58 = ( _Layer2Color * SAMPLE_TEXTURE2D( _Layer2BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer2BaseColor ) );
				float2 uv_Layer1Mask = IN.ase_texcoord8.xy * _Layer1Mask_ST.xy + _Layer1Mask_ST.zw;
				float4 tex2DNode2 = SAMPLE_TEXTURE2D( _Layer1Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Mask );
				float Layer1Height31 = tex2DNode2.b;
				float Layer1RawAO201 = tex2DNode2.g;
				#ifdef _LAYER1USEAOASHEIGHT_ON
				float staticSwitch63 = ( 1.0 - Layer1RawAO201 );
				#else
				float staticSwitch63 = ( 1.0 - Layer1Height31 );
				#endif
				float VCRed18 = IN.ase_color.r;
				float HeightMask10 = saturate(pow(max( (((staticSwitch63*VCRed18)*4)+(VCRed18*2)), 0 ),_Layer2BlendSharpness));
				float Layer2Mask143 = saturate( HeightMask10 );
				float4 lerpResult14 = lerp( float4( Layer1Color27 , 0.0 ) , Layer2Color58 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float4 staticSwitch210 = lerpResult14;
				#else
				float4 staticSwitch210 = float4( Layer1Color27 , 0.0 );
				#endif
				float2 uv_Layer3BaseColor = IN.ase_texcoord8.xy * _Layer3BaseColor_ST.xy + _Layer3BaseColor_ST.zw;
				float4 Layer3Color98 = ( _Layer3Color * SAMPLE_TEXTURE2D( _Layer3BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer3BaseColor ) );
				float2 uv_Layer2Mask = IN.ase_texcoord8.xy * _Layer2Mask_ST.xy + _Layer2Mask_ST.zw;
				float4 tex2DNode46 = SAMPLE_TEXTURE2D( _Layer2Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Mask );
				float Layer2Height54 = tex2DNode46.b;
				float lerpResult186 = lerp( staticSwitch63 , ( 1.0 - Layer2Height54 ) , Layer2Mask143);
				float Layer2RawAO202 = tex2DNode46.g;
				float lerpResult200 = lerp( staticSwitch63 , ( 1.0 - Layer2RawAO202 ) , Layer2Mask143);
				#ifdef _LAYER2USEAOASHEIGHT_ON
				float staticSwitch130 = lerpResult200;
				#else
				float staticSwitch130 = lerpResult186;
				#endif
				float VCGreen19 = IN.ase_color.g;
				float HeightMask126 = saturate(pow(max( (((staticSwitch130*VCGreen19)*4)+(VCGreen19*2)), 0 ),_Layer3BlendSharpness));
				float Layer3Mask144 = saturate( HeightMask126 );
				float4 lerpResult67 = lerp( staticSwitch210 , Layer3Color98 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float4 staticSwitch204 = lerpResult67;
				#else
				float4 staticSwitch204 = staticSwitch210;
				#endif
				float2 uv_Layer4BaseColor = IN.ase_texcoord8.xy * _Layer4BaseColor_ST.xy + _Layer4BaseColor_ST.zw;
				float4 Layer4Color117 = ( _Layer4Color * SAMPLE_TEXTURE2D( _Layer4BaseColor, sampler_Trilinear_Repeat_Aniso16, uv_Layer4BaseColor ) );
				float2 uv_Layer4Mask = IN.ase_texcoord8.xy * _Layer4Mask_ST.xy + _Layer4Mask_ST.zw;
				float4 tex2DNode115 = SAMPLE_TEXTURE2D( _Layer4Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Mask );
				float Layer4Height121 = tex2DNode115.b;
				float lerpResult189 = lerp( staticSwitch130 , ( 1.0 - Layer4Height121 ) , Layer3Mask144);
				float2 uv_Layer3Mask = IN.ase_texcoord8.xy * _Layer3Mask_ST.xy + _Layer3Mask_ST.zw;
				float4 tex2DNode99 = SAMPLE_TEXTURE2D( _Layer3Mask, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Mask );
				float Layer3AO103 = saturate( ( tex2DNode99.g + ( 1.0 - _Layer3AO ) ) );
				float lerpResult214 = lerp( staticSwitch130 , ( 1.0 - Layer3AO103 ) , Layer3Mask144);
				#ifdef _LAYER3USEAOASHEIGHT_ON
				float staticSwitch140 = lerpResult214;
				#else
				float staticSwitch140 = lerpResult189;
				#endif
				float VCBlue20 = IN.ase_color.b;
				float HeightMask134 = saturate(pow(max( (((staticSwitch140*VCBlue20)*4)+(VCBlue20*2)), 0 ),_Layer4BlendSharpness));
				float Layer4Mask145 = saturate( HeightMask134 );
				float4 lerpResult139 = lerp( staticSwitch204 , Layer4Color117 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float4 staticSwitch205 = lerpResult139;
				#else
				float4 staticSwitch205 = staticSwitch204;
				#endif
				float4 FinalColorBlend141 = staticSwitch205;
				
				float2 uv_Layer1Normal = IN.ase_texcoord8.xy * _Layer1Normal_ST.xy + _Layer1Normal_ST.zw;
				float3 unpack1 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer1Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer1Normal ), _Layer1NormalStrength );
				unpack1.z = lerp( 1, unpack1.z, saturate(_Layer1NormalStrength) );
				float3 Layer1Normal28 = unpack1;
				float2 uv_Layer2Normal = IN.ase_texcoord8.xy * _Layer2Normal_ST.xy + _Layer2Normal_ST.zw;
				float3 unpack45 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer2Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer2Normal ), _Layer2NormalStrength );
				unpack45.z = lerp( 1, unpack45.z, saturate(_Layer2NormalStrength) );
				float3 Layer2Normal56 = unpack45;
				float3 lerpResult163 = lerp( Layer1Normal28 , Layer2Normal56 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float3 staticSwitch211 = lerpResult163;
				#else
				float3 staticSwitch211 = Layer1Normal28;
				#endif
				float2 uv_Layer3Normal = IN.ase_texcoord8.xy * _Layer3Normal_ST.xy + _Layer3Normal_ST.zw;
				float3 unpack96 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer3Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer3Normal ), _Layer3NormalStrength );
				unpack96.z = lerp( 1, unpack96.z, saturate(_Layer3NormalStrength) );
				float3 Layer3Normal105 = unpack96;
				float3 lerpResult156 = lerp( staticSwitch211 , Layer3Normal105 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float3 staticSwitch206 = lerpResult156;
				#else
				float3 staticSwitch206 = staticSwitch211;
				#endif
				float2 uv_Layer4Normal = IN.ase_texcoord8.xy * _Layer4Normal_ST.xy + _Layer4Normal_ST.zw;
				float3 unpack114 = UnpackNormalScale( SAMPLE_TEXTURE2D( _Layer4Normal, sampler_Trilinear_Repeat_Aniso16, uv_Layer4Normal ), _Layer4NormalStrength );
				unpack114.z = lerp( 1, unpack114.z, saturate(_Layer4NormalStrength) );
				float3 Layer4Normal118 = unpack114;
				float3 lerpResult157 = lerp( lerpResult156 , Layer4Normal118 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float3 staticSwitch207 = lerpResult157;
				#else
				float3 staticSwitch207 = staticSwitch206;
				#endif
				float3 FinalNormalBlend161 = staticSwitch207;
				
				float Layer1Smoothness32 = ( tex2DNode2.a * _Layer1Smoothness );
				float Layer2Smoothness53 = ( tex2DNode46.a * _Layer2Smoothness );
				float lerpResult223 = lerp( Layer1Smoothness32 , Layer2Smoothness53 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float staticSwitch232 = lerpResult223;
				#else
				float staticSwitch232 = Layer1Smoothness32;
				#endif
				float Layer3Smoothness100 = ( tex2DNode99.a * _Layer3Smoothness );
				float lerpResult220 = lerp( staticSwitch232 , Layer3Smoothness100 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float staticSwitch231 = lerpResult220;
				#else
				float staticSwitch231 = staticSwitch232;
				#endif
				float Layer4Smoothness122 = ( tex2DNode115.a * _Layer4Smoothness );
				float lerpResult221 = lerp( staticSwitch231 , Layer4Smoothness122 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float staticSwitch230 = lerpResult221;
				#else
				float staticSwitch230 = staticSwitch231;
				#endif
				float FinalSmoothnessBlend229 = staticSwitch230;
				
				float Layer1AO30 = saturate( ( tex2DNode2.g + ( 1.0 - _Layer1AO ) ) );
				float Layer2AO49 = saturate( ( tex2DNode46.g + ( 1.0 - _Layer2AO ) ) );
				float lerpResult171 = lerp( Layer1AO30 , Layer2AO49 , Layer2Mask143);
				#ifdef _ENABLELAYER2_ON
				float staticSwitch212 = lerpResult171;
				#else
				float staticSwitch212 = Layer1AO30;
				#endif
				float lerpResult168 = lerp( staticSwitch212 , Layer3AO103 , Layer3Mask144);
				#ifdef _ENABLELAYER3_ON
				float staticSwitch208 = lerpResult168;
				#else
				float staticSwitch208 = staticSwitch212;
				#endif
				float Layer4AO120 = saturate( ( tex2DNode115.g + ( 1.0 - _Layer4AO ) ) );
				float lerpResult169 = lerp( staticSwitch208 , Layer4AO120 , Layer4Mask145);
				#ifdef _ENABLELAYER4_ON
				float staticSwitch209 = lerpResult169;
				#else
				float staticSwitch209 = staticSwitch208;
				#endif
				float FinalAOBlend177 = staticSwitch209;
				

				float3 BaseColor = FinalColorBlend141.rgb;
				float3 Normal = FinalNormalBlend161;
				float3 Emission = 0;
				float3 Specular = 0.5;
				float Metallic = 0;
				float Smoothness = FinalSmoothnessBlend229;
				float Occlusion = FinalAOBlend177;
				float Alpha = 1;
				float AlphaClipThreshold = 0.5;
				float AlphaClipThresholdShadow = 0.5;
				float3 BakedGI = 0;
				float3 RefractionColor = 1;
				float RefractionIndex = 1;
				float3 Transmission = 1;
				float3 Translucency = 1;

				#ifdef ASE_DEPTH_WRITE_ON
					float DepthValue = IN.clipPos.z;
				#endif

				#ifdef _ALPHATEST_ON
					clip(Alpha - AlphaClipThreshold);
				#endif

				InputData inputData = (InputData)0;
				inputData.positionWS = WorldPosition;
				inputData.positionCS = IN.clipPos;
				inputData.shadowCoord = ShadowCoords;

				#ifdef _NORMALMAP
					#if _NORMAL_DROPOFF_TS
						inputData.normalWS = TransformTangentToWorld(Normal, half3x3( WorldTangent, WorldBiTangent, WorldNormal ));
					#elif _NORMAL_DROPOFF_OS
						inputData.normalWS = TransformObjectToWorldNormal(Normal);
					#elif _NORMAL_DROPOFF_WS
						inputData.normalWS = Normal;
					#endif
				#else
					inputData.normalWS = WorldNormal;
				#endif

				inputData.normalWS = NormalizeNormalPerPixel(inputData.normalWS);
				inputData.viewDirectionWS = SafeNormalize( WorldViewDirection );

				inputData.vertexLighting = IN.fogFactorAndVertexLight.yzw;

				#if defined(ENABLE_TERRAIN_PERPIXEL_NORMAL)
					float3 SH = SampleSH(inputData.normalWS.xyz);
				#else
					float3 SH = IN.lightmapUVOrVertexSH.xyz;
				#endif

				#ifdef ASE_BAKEDGI
					inputData.bakedGI = BakedGI;
				#else
					#if defined(DYNAMICLIGHTMAP_ON)
						inputData.bakedGI = SAMPLE_GI( IN.lightmapUVOrVertexSH.xy, IN.dynamicLightmapUV.xy, SH, inputData.normalWS);
					#else
						inputData.bakedGI = SAMPLE_GI( IN.lightmapUVOrVertexSH.xy, SH, inputData.normalWS );
					#endif
				#endif

				inputData.normalizedScreenSpaceUV = NormalizedScreenSpaceUV;
				inputData.shadowMask = SAMPLE_SHADOWMASK(IN.lightmapUVOrVertexSH.xy);

				#if defined(DEBUG_DISPLAY)
					#if defined(DYNAMICLIGHTMAP_ON)
						inputData.dynamicLightmapUV = IN.dynamicLightmapUV.xy;
						#endif
					#if defined(LIGHTMAP_ON)
						inputData.staticLightmapUV = IN.lightmapUVOrVertexSH.xy;
					#else
						inputData.vertexSH = SH;
					#endif
				#endif

				#ifdef _DBUFFER
					ApplyDecal(IN.clipPos,
						BaseColor,
						Specular,
						inputData.normalWS,
						Metallic,
						Occlusion,
						Smoothness);
				#endif

				BRDFData brdfData;
				InitializeBRDFData
				(BaseColor, Metallic, Specular, Smoothness, Alpha, brdfData);

				Light mainLight = GetMainLight(inputData.shadowCoord, inputData.positionWS, inputData.shadowMask);
				half4 color;
				MixRealtimeAndBakedGI(mainLight, inputData.normalWS, inputData.bakedGI, inputData.shadowMask);
				color.rgb = GlobalIllumination(brdfData, inputData.bakedGI, Occlusion, inputData.positionWS, inputData.normalWS, inputData.viewDirectionWS);
				color.a = Alpha;

				#ifdef ASE_FINAL_COLOR_ALPHA_MULTIPLY
					color.rgb *= color.a;
				#endif

				#ifdef ASE_DEPTH_WRITE_ON
					outputDepth = DepthValue;
				#endif

				return BRDFDataToGbuffer(brdfData, inputData, Smoothness, Emission + color.rgb, Occlusion);
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "SceneSelectionPass"
			Tags { "LightMode"="SceneSelectionPass" }

			Cull Off
			AlphaToMask Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma vertex vert
			#pragma fragment frag

			#define SCENESELECTIONPASS 1

			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			VertexOutput VertexFunction(VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );

				o.clipPos = TransformWorldToHClip(positionWS);

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;

				

				surfaceDescription.Alpha = 1;
				surfaceDescription.AlphaClipThreshold = 0.5;

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
					clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = 0;

				#ifdef SCENESELECTIONPASS
					outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				#elif defined(SCENEPICKINGPASS)
					outColor = _SelectionID;
				#endif

				return outColor;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ScenePickingPass"
			Tags { "LightMode"="Picking" }

			AlphaToMask Off

			HLSLPROGRAM

			#define _NORMAL_DROPOFF_TS 1
			#define ASE_FOG 1
			#define _NORMALMAP 1
			#define ASE_SRP_VERSION 140009
			#define ASE_USING_SAMPLING_MACROS 1


			#pragma vertex vert
			#pragma fragment frag

		    #define SCENEPICKINGPASS 1

			#define ATTRIBUTES_NEED_NORMAL
			#define ATTRIBUTES_NEED_TANGENT
			#define SHADERPASS SHADERPASS_DEPTHONLY

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Color.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Texture.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Lighting.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Input.hlsl"
			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/TextureStack.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/ShaderGraphFunctions.hlsl"
			#include "Packages/com.unity.render-pipelines.universal/Editor/ShaderGraph/Includes/ShaderPass.hlsl"

			

			struct VertexInput
			{
				float4 vertex : POSITION;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 clipPos : SV_POSITION;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START(UnityPerMaterial)
			float4 _Layer1Color;
			float4 _Layer2Normal_ST;
			float4 _Layer1Normal_ST;
			float4 _Layer3Mask_ST;
			float4 _Layer4Mask_ST;
			float4 _Layer4BaseColor_ST;
			float4 _Layer4Color;
			float4 _Layer4Normal_ST;
			float4 _Layer2Mask_ST;
			float4 _Layer3Normal_ST;
			float4 _Layer3Color;
			float4 _Layer1Mask_ST;
			float4 _Layer2BaseColor_ST;
			float4 _Layer2Color;
			float4 _Layer1BaseColor_ST;
			float4 _Layer3BaseColor_ST;
			float _Layer4Smoothness;
			float _Layer2Smoothness;
			float _Layer1Smoothness;
			float _Layer1AO;
			float _Layer4NormalStrength;
			float _Layer3Smoothness;
			float _Layer3AO;
			float _Layer2NormalStrength;
			float _Layer1NormalStrength;
			float _Layer4BlendSharpness;
			float _Layer2AO;
			float _Layer3BlendSharpness;
			float _Layer2BlendSharpness;
			float _Layer1Saturation;
			float _Layer3NormalStrength;
			float _Layer4AO;
			#ifdef ASE_TRANSMISSION
				float _TransmissionShadow;
			#endif
			#ifdef ASE_TRANSLUCENCY
				float _TransStrength;
				float _TransNormal;
				float _TransScattering;
				float _TransDirect;
				float _TransAmbient;
				float _TransShadow;
			#endif
			#ifdef ASE_TESSELLATION
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			#ifdef SCENEPICKINGPASS
				float4 _SelectionID;
			#endif

			#ifdef SCENESELECTIONPASS
				int _ObjectId;
				int _PassValue;
			#endif

			

			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			VertexOutput VertexFunction(VertexInput v  )
			{
				VertexOutput o;
				ZERO_INITIALIZE(VertexOutput, o);

				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);

				

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					float3 defaultVertexValue = v.vertex.xyz;
				#else
					float3 defaultVertexValue = float3(0, 0, 0);
				#endif

				float3 vertexValue = defaultVertexValue;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
					v.vertex.xyz = vertexValue;
				#else
					v.vertex.xyz += vertexValue;
				#endif

				v.ase_normal = v.ase_normal;

				float3 positionWS = TransformObjectToWorld( v.vertex.xyz );
				o.clipPos = TransformWorldToHClip(positionWS);

				return o;
			}

			#if defined(ASE_TESSELLATION)
			struct VertexControl
			{
				float4 vertex : INTERNALTESSPOS;
				float3 ase_normal : NORMAL;
				
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.vertex = v.vertex;
				o.ase_normal = v.ase_normal;
				
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), _WorldSpaceCameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(v[0].vertex, v[1].vertex, v[2].vertex, edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), _WorldSpaceCameraPos, _ScreenParams, unity_CameraWorldClipPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
				return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.vertex = patch[0].vertex * bary.x + patch[1].vertex * bary.y + patch[2].vertex * bary.z;
				o.ase_normal = patch[0].ase_normal * bary.x + patch[1].ase_normal * bary.y + patch[2].ase_normal * bary.z;
				
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.vertex.xyz - patch[i].ase_normal * (dot(o.vertex.xyz, patch[i].ase_normal) - dot(patch[i].vertex.xyz, patch[i].ase_normal));
				float phongStrength = _TessPhongStrength;
				o.vertex.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.vertex.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			half4 frag(VertexOutput IN ) : SV_TARGET
			{
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;

				

				surfaceDescription.Alpha = 1;
				surfaceDescription.AlphaClipThreshold = 0.5;

				#if _ALPHATEST_ON
					float alphaClipThreshold = 0.01f;
					#if ALPHA_CLIP_THRESHOLD
						alphaClipThreshold = surfaceDescription.AlphaClipThreshold;
					#endif
						clip(surfaceDescription.Alpha - alphaClipThreshold);
				#endif

				half4 outColor = 0;

				#ifdef SCENESELECTIONPASS
					outColor = half4(_ObjectId, _PassValue, 1.0, 1.0);
				#elif defined(SCENEPICKINGPASS)
					outColor = _SelectionID;
				#endif

				return outColor;
			}

			ENDHLSL
		}
		
	}
	
	CustomEditor "UnityEditor.ShaderGraphLitGUI"
	FallBack "Hidden/Shader Graph/FallbackError"
	
	Fallback Off
}
/*ASEBEGIN
Version=19201
Node;AmplifyShaderEditor.CommentaryNode;216;-2667.201,-3532.647;Inherit;False;538.6532;411.3999;Comment;5;18;19;20;21;13;Vertex Colors;1,1,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;142;-2429.699,-2745.198;Inherit;False;757.3807;960.0593;Comment;14;204;205;141;14;65;66;149;148;133;139;67;132;147;210;Color Blend;0.135849,0.5675491,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;40;-3965.457,-3547.22;Inherit;False;1120.403;1083.635;Comment;21;4;35;3;1;2;36;39;30;37;34;33;32;31;38;28;16;27;29;235;236;251;Layer 1;1,1,1,1;0;0
Node;AmplifyShaderEditor.SamplerNode;1;-3667.766,-3129.261;Inherit;True;Property;_Layer1Normal;Layer 1 Normal;4;0;Create;True;0;0;0;False;0;False;-1;None;f9580cb900df774488c101af2651e0a9;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;2;-3667.192,-2931.989;Inherit;True;Property;_Layer1Mask;Layer 1 Mask;3;0;Create;True;0;0;0;False;0;False;-1;None;31866ced73513b24ab30b7ba25a7e94b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;36;-3346.481,-2879.476;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;39;-3213.568,-2879.456;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;37;-3345.032,-2776.834;Inherit;False;Property;_Layer1AO;Layer 1 AO;7;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;33;-3250.961,-2597.818;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-3095.961,-2602.818;Inherit;False;Layer1Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;31;-3069.853,-2689.925;Inherit;False;Layer1Height;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;28;-3075.017,-3127.563;Inherit;False;Layer1Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;16;-3230.228,-3418.224;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;29;-3071.014,-2977.327;Inherit;False;Layer1Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;41;-3965.577,-2348.821;Inherit;False;1120.403;1083.635;Comment;19;59;58;57;56;55;54;53;52;50;49;48;47;46;45;44;43;42;87;252;Layer 2;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;34;-3577.439,-2578.984;Inherit;False;Property;_Layer1Smoothness;Layer 1 Smoothness;8;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;47;-3346.6,-1681.076;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;48;-3213.688,-1681.056;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;52;-3251.081,-1399.418;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;57;-3230.347,-2219.825;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;42;-3579.423,-2298.821;Inherit;False;Property;_Layer2Color;Layer 2 Color;9;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.6588235,0.6352941,0.5960784,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;44;-3667.063,-2124.961;Inherit;True;Property;_Layer2BaseColor;Layer 2 Base Color;10;0;Create;True;0;0;0;False;0;False;-1;None;6d44492ec54c7f24281cad44a8281390;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;45;-3667.885,-1930.861;Inherit;True;Property;_Layer2Normal;Layer 2 Normal;12;0;Create;True;0;0;0;False;0;False;-1;None;4f977251a30e81c489909a172c15a7e7;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;58;-3077.244,-2225.003;Inherit;False;Layer2Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;46;-3667.311,-1733.589;Inherit;True;Property;_Layer2Mask;Layer 2 Mask;11;0;Create;True;0;0;0;False;0;False;-1;None;4bd8eb7bc0143b94e9ac97e6ca2b5668;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;53;-3096.081,-1405.418;Inherit;False;Layer2Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;54;-3069.972,-1491.525;Inherit;False;Layer2Height;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;59;-3071.134,-1778.927;Inherit;False;Layer2Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;56;-3075.136,-1929.163;Inherit;False;Layer2Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;87;-3581.174,-1378.792;Inherit;False;Property;_Layer2Smoothness;Layer 2 Smoothness;16;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;88;-3961.026,-1166.691;Inherit;False;1120.403;1083.635;Comment;19;106;105;104;103;102;101;100;99;98;97;96;95;94;93;92;91;90;89;253;Layer 3;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;89;-3342.049,-498.9451;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;90;-3209.137,-498.9251;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;91;-3246.53,-217.2871;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;92;-3043.601,-391.303;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;93;-3225.796,-1037.694;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SamplerNode;95;-3662.512,-942.8303;Inherit;True;Property;_Layer3BaseColor;Layer 3 Base Color;19;0;Create;True;0;0;0;False;0;False;-1;None;6333d14926bab2348877d4548d64687b;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;99;-3662.76,-551.4583;Inherit;True;Property;_Layer3Mask;Layer 3 Mask;20;0;Create;True;0;0;0;False;0;False;-1;None;7e357888480d1d447a198bf0029fca03;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;97;-3911.026,-701.8223;Inherit;False;Property;_Layer3NormalStrength;Layer 3 Normal Strength;22;0;Create;True;0;0;0;False;0;False;0;1.8;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;98;-3072.693,-1042.872;Inherit;False;Layer3Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;105;-3070.585,-747.0323;Inherit;False;Layer3Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;104;-3066.583,-597.7963;Inherit;False;Layer3Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;103;-3065.576,-504.6041;Inherit;False;Layer3AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;102;-3065.421,-309.3941;Inherit;False;Layer3Height;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;100;-3091.53,-223.2871;Inherit;False;Layer3Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;106;-3576.623,-196.6611;Inherit;False;Property;_Layer3Smoothness;Layer 3 Smoothness;25;0;Create;True;0;0;0;False;0;False;1;0.608;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;101;-3340.601,-396.303;Inherit;False;Property;_Layer3AO;Layer 3 AO;24;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;94;-3574.872,-1116.691;Inherit;False;Property;_Layer3Color;Layer 3 Color;18;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.5660378,0.5532218,0.5147738,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.CommentaryNode;107;-3952.39,7.992369;Inherit;False;1120.403;1083.635;Comment;19;125;124;123;122;121;120;119;118;117;116;115;114;113;112;111;110;109;108;254;Layer 4;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleAddOpNode;108;-3333.412,675.7385;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;109;-3200.5,675.7584;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;110;-3237.894,957.3964;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;111;-3034.965,783.3806;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;112;-3217.159,136.9894;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;125;-3566.236,57.99236;Inherit;False;Property;_Layer4Color;Layer 4 Color;27;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.7924528,0.7924528,0.7924528,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;113;-3653.875,231.8531;Inherit;True;Property;_Layer4BaseColor;Layer 4 Base Color;28;0;Create;True;0;0;0;False;0;False;-1;None;302add096a8f9454db3f75ed7fba6b7a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;115;-3654.123,623.2252;Inherit;True;Property;_Layer4Mask;Layer 4 Mask;30;0;Create;True;0;0;0;False;0;False;-1;None;7e357888480d1d447a198bf0029fca03;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;123;-3567.987,978.0225;Inherit;False;Property;_Layer4Smoothness;Layer 4 Smoothness;33;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;124;-3331.965,778.3806;Inherit;False;Property;_Layer4AO;Layer 4 AO;32;0;Create;True;0;0;0;False;0;False;1;0.425;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;121;-3056.784,865.2894;Inherit;False;Layer4Height;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;122;-3082.894,951.3964;Inherit;False;Layer4Smoothness;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;118;-3061.948,427.651;Inherit;False;Layer4Normal;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;119;-3057.947,576.8872;Inherit;False;Layer4Metallic;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;120;-3056.94,670.0794;Inherit;False;Layer4AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;117;-3064.057,131.8114;Inherit;False;Layer4Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.CommentaryNode;152;-1636.18,-2746.533;Inherit;False;770.518;956.7303;Comment;14;161;207;206;158;159;155;153;162;163;160;157;156;154;211;Normal Blend;0.4980392,0.5764706,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;43;-3915.577,-1883.953;Inherit;False;Property;_Layer2NormalStrength;Layer 2 Normal Strength;13;0;Create;True;0;0;0;False;0;False;1;1.6;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;35;-3915.457,-3082.353;Inherit;False;Property;_Layer1NormalStrength;Layer 1 Normal Strength;5;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;166;-826.322,-2755.184;Inherit;False;806.0124;982.106;Comment;14;209;208;176;174;173;172;177;175;171;170;169;168;167;212;AO Blend;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;50;-3345.152,-1578.434;Inherit;False;Property;_Layer2AO;Layer 2 AO;15;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;55;-3051.152,-1572.434;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;30;-3067.007,-2897.135;Inherit;False;Layer1AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;38;-3038.032,-2765.834;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;201;-3067.743,-2823.781;Inherit;False;Layer1RawAO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;49;-3071.127,-1700.735;Inherit;False;Layer2AO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;202;-3068.743,-1626.781;Inherit;False;Layer2RawAO;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;114;-3654.698,425.953;Inherit;True;Property;_Layer4Normal;Layer 4 Normal;29;0;Create;True;0;0;0;False;0;False;-1;None;ea99fda29b136ba42916112afa3d6e5f;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;116;-3915.389,473.861;Inherit;False;Property;_Layer4NormalStrength;Layer 4 Normal Strength;31;0;Create;True;0;0;0;False;0;False;0;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;96;-3663.334,-748.7303;Inherit;True;Property;_Layer3Normal;Layer 3 Normal;21;0;Create;True;0;0;0;False;0;False;-1;None;cabe32a8037063647a36cc30d810a874;True;0;True;bump;Auto;True;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;61;-2325.199,-1034.751;Inherit;False;31;Layer1Height;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;194;-2093.018,-1029.18;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;63;-1882.349,-1034.419;Inherit;False;Property;_Layer1UseAOasHeight;Layer 1 Use AO as Height;6;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;64;-2326.113,-946.8351;Inherit;False;201;Layer1RawAO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;198;-2093.064,-941.2176;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;62;-1771.131,-926.825;Inherit;False;18;VCRed;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.HeightMapBlendNode;10;-1536.074,-943.4969;Inherit;False;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;179;-1280.948,-943.0359;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;15;-1843.158,-845.4601;Inherit;False;Property;_Layer2BlendSharpness;Layer 2 Blend Sharpness;17;0;Create;True;0;0;0;False;0;False;5;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;130;-1882.064,-615.8504;Inherit;False;Property;_Layer2UseAOasHeight;Layer 2 Use AO as Height;14;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;127;-1768.182,-510.1414;Inherit;False;19;VCGreen;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;128;-1872.23,-433.1395;Inherit;False;Property;_Layer3BlendSharpness;Layer 3 Blend Sharpness;26;0;Create;True;0;0;0;False;0;False;5;25;0;50;0;1;FLOAT;0
Node;AmplifyShaderEditor.HeightMapBlendNode;126;-1544.219,-528.8305;Inherit;False;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;180;-1285.63,-529.0889;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;144;-1143.883,-534.2274;Inherit;False;Layer3Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;184;-2458.786,-615.6242;Inherit;False;54;Layer2Height;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;195;-2244.686,-610.0256;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;186;-2072.802,-609.6241;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;200;-2069.982,-481.8663;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;138;-1737.083,-18.0881;Inherit;False;20;VCBlue;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;135;-1840.636,62.07582;Inherit;False;Property;_Layer4BlendSharpness;Layer 4 Blend Sharpness;34;0;Create;True;0;0;0;False;0;False;3;19.3;0;30;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;140;-1853.083,-125.0884;Inherit;False;Property;_Layer3UseAOasHeight;Layer 3 Use AO as Height;23;0;Create;True;0;0;0;False;0;False;0;0;0;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.HeightMapBlendNode;134;-1550.265,-69.74512;Inherit;False;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;188;-2472.865,-113.234;Inherit;False;121;Layer4Height;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;131;-2468.021,-23.27321;Inherit;False;144;Layer3Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;137;-2454.291,67.05689;Inherit;False;103;Layer3AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;215;-2268.487,70.74299;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;213;-2265.787,-108.2026;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;189;-2077.922,-131.6978;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;214;-2075.598,16.50164;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;181;-1294.354,-70.18582;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;145;-1131.249,-73.4953;Inherit;False;Layer4Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;18;-2357.349,-3482.647;Inherit;False;VCRed;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;19;-2356.347,-3403.647;Inherit;False;VCGreen;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;20;-2354.347,-3321.647;Inherit;False;VCBlue;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;21;-2353.348,-3236.647;Inherit;False;VCAlpha;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexColorNode;13;-2617.201,-3415.516;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;147;-2357.506,-2493.942;Inherit;False;143;Layer2Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;132;-2353.589,-2361.017;Inherit;False;98;Layer3Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;67;-2106.267,-2355.908;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;139;-2109.703,-2125.041;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;133;-2348.104,-2130.867;Inherit;False;117;Layer4Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;148;-2353.553,-2276.433;Inherit;False;144;Layer3Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;149;-2346.309,-2044.715;Inherit;False;145;Layer4Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;154;-1563.987,-2495.277;Inherit;False;143;Layer2Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;156;-1312.747,-2357.243;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;157;-1316.183,-2126.376;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;160;-1552.79,-2046.049;Inherit;False;145;Layer4Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;163;-1310.114,-2646.871;Inherit;False;3;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;162;-1567.559,-2678.232;Inherit;False;28;Layer1Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;153;-1566.559,-2588.232;Inherit;False;56;Layer2Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;155;-1560.07,-2362.352;Inherit;False;105;Layer3Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;159;-1560.034,-2277.768;Inherit;False;144;Layer3Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;158;-1554.585,-2132.202;Inherit;False;118;Layer4Normal;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;66;-2361.078,-2586.897;Inherit;False;58;Layer2Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;65;-2361.078,-2676.897;Inherit;False;27;Layer1Color;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;141;-1902.76,-1891.653;Inherit;False;FinalColorBlend;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;205;-1925.267,-2153.895;Inherit;False;Property;_EnableLayer4;Enable Layer 4;37;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;161;-1100.241,-1958.987;Inherit;False;FinalNormalBlend;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;210;-1927.833,-2680.358;Inherit;False;Property;_EnableLayer2;Enable Layer 2;36;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;192;-2451.161,-529.9256;Inherit;False;143;Layer2Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;129;-2457.34,-440.3071;Inherit;False;202;Layer2RawAO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;199;-2245.784,-439.7654;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;143;-1138.831,-947.1251;Inherit;False;Layer2Mask;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;204;-1927.925,-2386.895;Inherit;False;Property;_EnableLayer3;Enable Layer 3;35;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Create;True;True;All;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;207;-1118.648,-2209.138;Inherit;False;Property;_EnableLayer6;Enable Layer 4;37;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;205;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;206;-1121.163,-2416.056;Inherit;False;Property;_EnableLayer5;Enable Layer 3;35;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;204;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StaticSwitch;211;-1121.133,-2683.951;Inherit;False;Property;_EnableLayer9;Enable Layer 2;36;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;210;True;True;All;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.LerpOp;14;-2104.634,-2612.536;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;167;-754.129,-2503.928;Inherit;False;143;Layer2Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;168;-502.8888,-2365.894;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;169;-506.3248,-2135.027;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;170;-742.9319,-2054.702;Inherit;False;145;Layer4Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;171;-500.2559,-2655.522;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;175;-750.176,-2286.419;Inherit;False;144;Layer3Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;172;-757.701,-2686.883;Inherit;False;30;Layer1AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;173;-756.701,-2596.883;Inherit;False;49;Layer2AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;174;-750.212,-2371.003;Inherit;False;103;Layer3AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;176;-744.727,-2140.853;Inherit;False;120;Layer4AO;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;177;-284.1827,-1932.24;Inherit;False;FinalAOBlend;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;209;-311.269,-2221.021;Inherit;False;Property;_EnableLayer8;Enable Layer 4;37;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;205;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;208;-316.6855,-2496.75;Inherit;False;Property;_EnableLayer7;Enable Layer 3;35;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;204;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;212;-308.4397,-2699.086;Inherit;False;Property;_EnableLayer10;Enable Layer 2;36;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;210;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;218;18.81791,-2754.438;Inherit;False;806.0124;982.106;Comment;14;232;231;230;229;228;227;226;225;224;223;222;221;220;219;Smoothness Blend;1,1,1,1;0;0
Node;AmplifyShaderEditor.GetLocalVarNode;219;91.01092,-2503.182;Inherit;False;143;Layer2Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;220;342.2511,-2365.148;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;221;338.8151,-2134.281;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;222;102.208,-2053.956;Inherit;False;145;Layer4Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;223;344.884,-2654.776;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;224;94.96392,-2285.673;Inherit;False;144;Layer3Mask;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;230;533.8708,-2220.275;Inherit;False;Property;_EnableLayer11;Enable Layer 4;37;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;205;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;231;528.4543,-2496.004;Inherit;False;Property;_EnableLayer12;Enable Layer 3;35;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;204;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;232;536.7001,-2698.34;Inherit;False;Property;_EnableLayer13;Enable Layer 2;36;0;Create;True;0;0;0;False;0;False;0;1;1;True;;Toggle;2;Key0;Key1;Reference;210;True;True;All;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;225;87.43896,-2686.137;Inherit;False;32;Layer1Smoothness;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;226;88.43896,-2596.137;Inherit;False;53;Layer2Smoothness;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;227;94.92797,-2370.257;Inherit;False;100;Layer3Smoothness;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;228;100.4129,-2140.107;Inherit;False;122;Layer4Smoothness;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;229;560.9571,-1931.494;Inherit;False;FinalSmoothnessBlend;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;164;-249.5994,-16.1487;Inherit;False;161;FinalNormalBlend;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;233;-281.4984,62.55371;Inherit;False;229;FinalSmoothnessBlend;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;4;-3596.604,-3504.22;Inherit;False;Property;_Layer1Color;Layer 1 Color;0;0;Create;True;0;0;0;False;0;False;1,1,1,0;0.6603774,0.6358006,0.5943396,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DesaturateOpNode;235;-3268.239,-3306.885;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-3077.124,-3417.402;Inherit;False;Layer1Color;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.OneMinusNode;237;-3245.239,-3212.885;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;236;-3364.239,-3142.885;Inherit;False;Property;_Layer1Saturation;Layer 1 Saturation;2;0;Create;True;0;0;0;False;0;False;1;1;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;238;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ExtraPrePass;0;0;ExtraPrePass;5;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;0;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;240;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ShadowCaster;0;2;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;False;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=ShadowCaster;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;241;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthOnly;0;3;DepthOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;True;True;False;False;False;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;False;False;True;1;LightMode=DepthOnly;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;242;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Meta;0;4;Meta;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;243;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;Universal2D;0;5;Universal2D;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=Universal2D;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;244;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;DepthNormals;0;6;DepthNormals;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;0;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;False;;True;3;False;;False;True;1;LightMode=DepthNormals;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;245;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;GBuffer;0;7;GBuffer;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalGBuffer;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;246;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;SceneSelectionPass;0;8;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;2;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=SceneSelectionPass;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;247;175.1501,-14.42413;Float;False;False;-1;2;UnityEditor.ShaderGraphLitGUI;0;1;New Amplify Shader;94348b07e5e8bab40bd6c8a1e3df54cd;True;ScenePickingPass;0;9;ScenePickingPass;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Picking;False;False;0;;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.GetLocalVarNode;178;-247.2133,147.1809;Inherit;False;177;FinalAOBlend;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;239;175.1501,-14.42413;Float;False;True;-1;2;UnityEditor.ShaderGraphLitGUI;0;12;TriForge/Fantasy Worlds/FWVertexBlend;94348b07e5e8bab40bd6c8a1e3df54cd;True;Forward;0;1;Forward;20;False;False;False;False;False;False;False;False;False;False;False;False;True;0;False;;False;True;0;False;;False;False;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;4;RenderPipeline=UniversalPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;UniversalMaterialType=Lit;True;5;True;12;all;0;False;True;1;1;False;;0;False;;1;1;False;;0;False;;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;True;True;True;True;0;False;;False;False;False;False;False;False;False;True;False;0;False;;255;False;;255;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;0;False;;False;True;1;False;;True;3;False;;True;True;0;False;;0;False;;True;1;LightMode=UniversalForward;False;False;0;;0;0;Standard;41;Workflow;1;0;Surface;0;0;  Refraction Model;0;0;  Blend;0;0;Two Sided;1;0;Fragment Normal Space,InvertActionOnDeselection;0;0;Forward Only;0;0;Transmission;0;0;  Transmission Shadow;0.5,False,;0;Translucency;0;0;  Translucency Strength;1,False,;0;  Normal Distortion;0.5,False,;0;  Scattering;2,False,;0;  Direct;0.9,False,;0;  Ambient;0.1,False,;0;  Shadow;0.5,False,;0;Cast Shadows;1;0;  Use Shadow Threshold;0;0;Receive Shadows;1;0;GPU Instancing;1;0;LOD CrossFade;1;0;Built-in Fog;1;0;_FinalColorxAlpha;0;0;Meta Pass;1;0;Override Baked GI;0;0;Extra Pre Pass;0;0;DOTS Instancing;0;0;Tessellation;0;0;  Phong;0;0;  Strength;0.5,False,;0;  Type;0;0;  Tess;16,False,;0;  Min;10,False,;0;  Max;25,False,;0;  Edge Length;16,False,;0;  Max Displacement;25,False,;0;Write Depth;0;0;  Early Z;0;0;Vertex Position,InvertActionOnDeselection;1;0;Debug Display;0;0;Clear Coat;0;0;0;10;False;True;True;True;True;True;True;True;True;True;False;;True;0
Node;AmplifyShaderEditor.SamplerNode;3;-3666.944,-3323.36;Inherit;True;Property;_Layer1BaseColor;Layer 1 Base Color;1;0;Create;True;0;0;0;False;0;False;-1;None;467bdec3003379145979220bae4cd383;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;251;-3915.494,-2996.763;Inherit;False;250;SamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.GetLocalVarNode;252;-3921.532,-1797.691;Inherit;False;250;SamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.GetLocalVarNode;150;-250.1315,-100.5504;Inherit;False;141;FinalColorBlend;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;253;-3911.085,-615.0313;Inherit;False;250;SamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.GetLocalVarNode;254;-3917.145,562.0007;Inherit;False;250;SamplerState;1;0;OBJECT;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;250;-3752.625,-3751.741;Inherit;False;SamplerState;-1;True;1;0;SAMPLERSTATE;;False;1;SAMPLERSTATE;0
Node;AmplifyShaderEditor.SamplerStateNode;248;-3952.834,-3752.66;Inherit;False;0;0;0;2;-1;X16;1;0;SAMPLER2D;;False;1;SAMPLERSTATE;0
WireConnection;1;5;35;0
WireConnection;1;7;251;0
WireConnection;2;7;251;0
WireConnection;36;0;2;2
WireConnection;36;1;38;0
WireConnection;39;0;36;0
WireConnection;33;0;2;4
WireConnection;33;1;34;0
WireConnection;32;0;33;0
WireConnection;31;0;2;3
WireConnection;28;0;1;0
WireConnection;16;0;4;0
WireConnection;16;1;3;0
WireConnection;29;0;2;1
WireConnection;47;0;46;2
WireConnection;47;1;55;0
WireConnection;48;0;47;0
WireConnection;52;0;46;4
WireConnection;52;1;87;0
WireConnection;57;0;42;0
WireConnection;57;1;44;0
WireConnection;44;7;252;0
WireConnection;45;5;43;0
WireConnection;45;7;252;0
WireConnection;58;0;57;0
WireConnection;46;7;252;0
WireConnection;53;0;52;0
WireConnection;54;0;46;3
WireConnection;59;0;46;1
WireConnection;56;0;45;0
WireConnection;89;0;99;2
WireConnection;89;1;92;0
WireConnection;90;0;89;0
WireConnection;91;0;99;4
WireConnection;91;1;106;0
WireConnection;92;0;101;0
WireConnection;93;0;94;0
WireConnection;93;1;95;0
WireConnection;95;7;253;0
WireConnection;99;7;253;0
WireConnection;98;0;93;0
WireConnection;105;0;96;0
WireConnection;104;0;99;1
WireConnection;103;0;90;0
WireConnection;102;0;99;3
WireConnection;100;0;91;0
WireConnection;108;0;115;2
WireConnection;108;1;111;0
WireConnection;109;0;108;0
WireConnection;110;0;115;4
WireConnection;110;1;123;0
WireConnection;111;0;124;0
WireConnection;112;0;125;0
WireConnection;112;1;113;0
WireConnection;113;7;254;0
WireConnection;115;7;254;0
WireConnection;121;0;115;3
WireConnection;122;0;110;0
WireConnection;118;0;114;0
WireConnection;119;0;115;1
WireConnection;120;0;109;0
WireConnection;117;0;112;0
WireConnection;55;0;50;0
WireConnection;30;0;39;0
WireConnection;38;0;37;0
WireConnection;201;0;2;2
WireConnection;49;0;48;0
WireConnection;202;0;46;2
WireConnection;114;5;116;0
WireConnection;114;7;254;0
WireConnection;96;5;97;0
WireConnection;96;7;253;0
WireConnection;194;0;61;0
WireConnection;63;1;194;0
WireConnection;63;0;198;0
WireConnection;198;0;64;0
WireConnection;10;0;63;0
WireConnection;10;1;62;0
WireConnection;10;2;15;0
WireConnection;179;0;10;0
WireConnection;130;1;186;0
WireConnection;130;0;200;0
WireConnection;126;0;130;0
WireConnection;126;1;127;0
WireConnection;126;2;128;0
WireConnection;180;0;126;0
WireConnection;144;0;180;0
WireConnection;195;0;184;0
WireConnection;186;0;63;0
WireConnection;186;1;195;0
WireConnection;186;2;192;0
WireConnection;200;0;63;0
WireConnection;200;1;199;0
WireConnection;200;2;192;0
WireConnection;140;1;189;0
WireConnection;140;0;214;0
WireConnection;134;0;140;0
WireConnection;134;1;138;0
WireConnection;134;2;135;0
WireConnection;215;0;137;0
WireConnection;213;0;188;0
WireConnection;189;0;130;0
WireConnection;189;1;213;0
WireConnection;189;2;131;0
WireConnection;214;0;130;0
WireConnection;214;1;215;0
WireConnection;214;2;131;0
WireConnection;181;0;134;0
WireConnection;145;0;181;0
WireConnection;18;0;13;1
WireConnection;19;0;13;2
WireConnection;20;0;13;3
WireConnection;21;0;13;4
WireConnection;67;0;210;0
WireConnection;67;1;132;0
WireConnection;67;2;148;0
WireConnection;139;0;204;0
WireConnection;139;1;133;0
WireConnection;139;2;149;0
WireConnection;156;0;211;0
WireConnection;156;1;155;0
WireConnection;156;2;159;0
WireConnection;157;0;156;0
WireConnection;157;1;158;0
WireConnection;157;2;160;0
WireConnection;163;0;162;0
WireConnection;163;1;153;0
WireConnection;163;2;154;0
WireConnection;141;0;205;0
WireConnection;205;1;204;0
WireConnection;205;0;139;0
WireConnection;161;0;207;0
WireConnection;210;1;65;0
WireConnection;210;0;14;0
WireConnection;199;0;129;0
WireConnection;143;0;179;0
WireConnection;204;1;210;0
WireConnection;204;0;67;0
WireConnection;207;1;206;0
WireConnection;207;0;157;0
WireConnection;206;1;211;0
WireConnection;206;0;156;0
WireConnection;211;1;162;0
WireConnection;211;0;163;0
WireConnection;14;0;65;0
WireConnection;14;1;66;0
WireConnection;14;2;147;0
WireConnection;168;0;212;0
WireConnection;168;1;174;0
WireConnection;168;2;175;0
WireConnection;169;0;208;0
WireConnection;169;1;176;0
WireConnection;169;2;170;0
WireConnection;171;0;172;0
WireConnection;171;1;173;0
WireConnection;171;2;167;0
WireConnection;177;0;209;0
WireConnection;209;1;208;0
WireConnection;209;0;169;0
WireConnection;208;1;212;0
WireConnection;208;0;168;0
WireConnection;212;1;172;0
WireConnection;212;0;171;0
WireConnection;220;0;232;0
WireConnection;220;1;227;0
WireConnection;220;2;224;0
WireConnection;221;0;231;0
WireConnection;221;1;228;0
WireConnection;221;2;222;0
WireConnection;223;0;225;0
WireConnection;223;1;226;0
WireConnection;223;2;219;0
WireConnection;230;1;231;0
WireConnection;230;0;221;0
WireConnection;231;1;232;0
WireConnection;231;0;220;0
WireConnection;232;1;225;0
WireConnection;232;0;223;0
WireConnection;229;0;230;0
WireConnection;235;0;16;0
WireConnection;235;1;237;0
WireConnection;27;0;235;0
WireConnection;237;0;236;0
WireConnection;239;0;150;0
WireConnection;239;1;164;0
WireConnection;239;4;233;0
WireConnection;239;5;178;0
WireConnection;3;7;251;0
WireConnection;250;0;248;0
ASEEND*/
//CHKSM=63AE64FF520E4A32FAC28EA4D0B76043A60B2D5C