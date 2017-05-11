#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;
using System.Diagnostics;
using System.IO;
using System.Collections.Generic;
using System;


public class AkWwiseMenu_iOS : MonoBehaviour {
#if !UNITY_5
	private static AkUnityPluginInstaller_iOS m_installer = new AkUnityPluginInstaller_iOS();

	// private static AkUnityIntegrationBuilder_iOS m_builder = new AkUnityIntegrationBuilder_iOS();

	[MenuItem("Assets/Wwise/Install Plugins/iOS/Debug", false, (int)AkWwiseMenuOrder.IosDebug)]
	public static void InstallPlugin_Debug () {
		m_installer.InstallPluginByConfig("Debug");
	}

	[MenuItem("Assets/Wwise/Install Plugins/iOS/Profile", false, (int)AkWwiseMenuOrder.IosProfile)]
	public static void InstallPlugin_Profile () {
		m_installer.InstallPluginByConfig("Profile");
	}

	[MenuItem("Assets/Wwise/Install Plugins/iOS/Release", false, (int)AkWwiseMenuOrder.IosRelease)]
	public static void InstallPlugin_Release () {
		m_installer.InstallPluginByConfig("Release");
	}
#endif

//	[MenuItem("Assets/Wwise/Rebuild Integration/iOS/Debug")]
//	public static void RebuildIntegration_Debug () {
//		m_builder.BuildByConfig("Debug", null);
//	}
//
//	[MenuItem("Assets/Wwise/Rebuild Integration/iOS/Profile")]
//	public static void RebuildIntegration_Profile () {
//		m_builder.BuildByConfig("Profile", null);
//	}
//
//	[MenuItem("Assets/Wwise/Rebuild Integration/iOS/Release")]
//	public static void RebuildIntegration_Release () {
//		m_builder.BuildByConfig("Release", null);
//	}
}


public class AkUnityPluginInstaller_iOS : AkUnityPluginInstallerBase
{
	public AkUnityPluginInstaller_iOS()
	{
		m_platform = "iOS";
	}

	protected override string GetPluginDestPath(string arch)
	{
		return Path.Combine(m_pluginDir, m_platform);
	}	
}


public class AkUnityIntegrationBuilder_iOS : AkUnityIntegrationBuilderBase
{
	public AkUnityIntegrationBuilder_iOS()
	{
		m_platform = "iOS";
	}
}
#endif // #if UNITY_EDITOR