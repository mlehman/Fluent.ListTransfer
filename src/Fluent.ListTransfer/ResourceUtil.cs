using System;
using System.Web.UI;

namespace Fluent {

	/// <summary>
	/// Utility Class for Client Script.
	/// </summary>
	internal class ResourceUtil {

		public static void RegisterScript(Control control, String scriptName) {
			if ( !control.Page.IsClientScriptBlockRegistered(scriptName) ) {
				using (System.IO.StreamReader reader = new System.IO.StreamReader(control.GetType().Assembly.GetManifestResourceStream(control.GetType(), scriptName))) { 
					String script = "<script language='javascript' type='text/javascript' >\r\n<!--\r\n" + reader.ReadToEnd() + "\r\n//-->\r\n</script>";
					control.Page.RegisterClientScriptBlock(scriptName, script);
				}
			}
		}

	}
}
