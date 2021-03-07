using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace Trans.Properties
{
	// Token: 0x02000009 RID: 9
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000051F3 File Offset: 0x000033F3
		internal Resources()
		{
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000046 RID: 70 RVA: 0x00005200 File Offset: 0x00003400
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				bool flag = Resources.resourceMan == null;
				if (flag)
				{
					ResourceManager resourceManager = new ResourceManager("Trans.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000047 RID: 71 RVA: 0x00005248 File Offset: 0x00003448
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000525F File Offset: 0x0000345F
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x04000061 RID: 97
		private static ResourceManager resourceMan;

		// Token: 0x04000062 RID: 98
		private static CultureInfo resourceCulture;
	}
}
