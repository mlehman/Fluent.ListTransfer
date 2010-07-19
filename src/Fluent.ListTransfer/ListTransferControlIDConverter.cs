using System;
using System.ComponentModel;
using System.Collections;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Fluent {

	/// <summary>
	/// Provides a type converter that can retrieve a list of Control IDs for ListControls accessible to the current component.
	/// </summary>
	public class ListTransferControlIDConverter : StringConverter {

		/// <summary>
		/// Gets the list of ListControls in the container. 
		/// </summary>
		/// <param name="container">The <see cref="IContainer"/> to search in for controls.</param>
		/// <returns>An array of <see cref="Control"/> ID <see cref="String"/>.</returns>
		protected virtual Object[] GetControls( IContainer container ) {
			ArrayList standardValues = new ArrayList();
			foreach( IComponent component in container.Components ) {
				Control control = component as Control;
				if ( control is ListControl) {
					standardValues.Add(String.Copy(control.ID));
				}
			}
			standardValues.Sort();
			return standardValues.ToArray();
		}

		/// <summary>
		/// Overrides <see cref="TypeConverter.GetStandardValues"/>.
		/// </summary>
		/// <returns>Returns valid control <see cref="Control"/> ID <see cref="String"/> array.</returns>
		public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(System.ComponentModel.ITypeDescriptorContext context) {
			if (context == null || context.Container == null) {
				return null;
			}
			object[] standardValues = this.GetControls(context.Container);
			if (standardValues != null) {
				return new StandardValuesCollection(standardValues);
			}
			return null;
		}

		/// <summary>
		/// Overrides <see cref="TypeConverter.GetStandardValuesSupported"/>.
		/// </summary>
		/// <returns>Returns true.</returns>
		public override bool GetStandardValuesSupported(System.ComponentModel.ITypeDescriptorContext context) {
			return true;
		}

		/// <summary>
		/// Overrides <see cref="TypeConverter.GetStandardValuesExclusive"/>.
		/// </summary>
		/// <returns>Returns false.</returns>
		public override bool GetStandardValuesExclusive(System.ComponentModel.ITypeDescriptorContext context) {
			return false;
		}
	}
}
