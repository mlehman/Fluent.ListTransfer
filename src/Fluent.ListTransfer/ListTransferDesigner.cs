using System;
using System.Web.UI.Design;

namespace Fluent {

	/// <summary>
	/// Extends design-time behavior for the ListTransfer.
	/// </summary>
	public class ListTransferDesigner : System.Web.UI.Design.ControlDesigner {
		
		/// <summary>
		/// Creates a placeholder.
		/// </summary>
		/// <returns></returns>
		public override string GetDesignTimeHtml() {
			string textValue = "Switch to HTML view to edit the control's content.";
			return CreatePlaceHolderDesignTimeHtml(textValue);
		}

		/// <summary>
		/// Disabled resizing.
		/// </summary>
		public override bool AllowResize {
			get {
				return false;
			}
		}

	}
}