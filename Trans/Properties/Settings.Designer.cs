using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Runtime.CompilerServices;

namespace Trans.Properties
{
	// Token: 0x0200000A RID: 10
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class Settings : ApplicationSettingsBase
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00005268 File Offset: 0x00003468
		public static Settings Default
		{
			get
			{
				return Settings.defaultInstance;
			}
		}

		// Token: 0x04000063 RID: 99
		private static Settings defaultInstance = (Settings)SettingsBase.Synchronized(new Settings());
	}
}
