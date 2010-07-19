using System;
using System.Text;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Security;

[assembly:AllowPartiallyTrustedCallers]
namespace Fluent {

	/// <summary>
	/// The ListTransfer control simplifies the transfer of ListItems between two ListControls.
	/// </summary>
	/// <remarks>
	/// <list type="table">
	///		<listheader>
	///			<term>CommandName</term>
	///			<description>Description</description>
	///		</listheader>
	///		<item>
	///			<term>CopySelected</term>
	///			<description>Copy the selected ListItems in the source ListControlFrom to the destination ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>CopyAll</term>
	///			<description> Copy all the ListItems in the source ListControlFrom to the destination ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>RemoveSelected</term>
	///			<description>Remove the selected ListItems in the destination ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>RemoveAll</term>
	///			<description>Remove all the ListItems in the destination ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>MoveSelected</term>
	///			<description>Move the selected ListItems in the source ListControlFrom to the destination ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>MovelAll</term>
	///			<description>Move all the ListItems in the source ListControlFrom to the destination ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>MoveBackSelected</term>
	///			<description>Move the selected ListItems in the destination ListControlTo to the source ListControlFrom.</description>
	///		</item>
	///		<item>
	///			<term>MoveBackAll</term>
	///			<description>Move all the ListItems in the destination ListControlTo to the source ListControlFrom.</description>
	///		</item>
	///		<item>
	///			<term>MoveUpListControlTo</term>
	///			<description>Moves up all the selected ListItems in ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>MoveDownListControlTo</term>
	///			<description>Moves down all the selected ListItems in ListControlTo.</description>
	///		</item>
	///		<item>
	///			<term>MoveUpListControlFrom</term>
	///			<description>Moves up all the selected ListItems in ListControlFrom.</description>
	///		</item>
	///		<item>
	///			<term>MoveDownListControlFrom</term>
	///			<description>Moves down all the selected ListItems in ListControlFrom.</description>
	///		</item>
	/// </list>
	/// </remarks>
	/// <example>
	/// The following example demonstrates how to create a ListTransfer control that copies 
	/// ListItems to the destination ListControl.
	///	<code><![CDATA[
	///	<%@ Register TagPrefix="fluent" Namespace="Fluent"  Assembly="Fluent.ListTransfer" %>
	///	<html>
	///	  <body>
	///   <form runat="server">
	///   
	///       <table>
	///         <tr>
	///           <td>
	///             <asp:ListBox ID="ListBoxEmployees" SelectionMode="Multiple"  Runat="server"/>
	///	          </td>
	///           <td>
	///             <fluent:ListTransfer 
	///               Runat="server" 
	///               ID="ListTransferEmployees"
	///               EnableClientSide="True"
	///               ListControlFrom="ListBoxEmployees" 
	///               ListControlTo="ListBoxProjectMembers" 
	///               >
	///               <asp:Button Runat="server" Text=">" CommandName="MoveSelected" /><br/>
	///               <asp:Button Runat="server" Text="<" CommandName="MoveBackSelected" /><br/>
	///               <asp:Button Runat="server" Text=">>" CommandName="MoveAll" /><br/>
	///               <asp:Button Runat="server" Text="<<" CommandName="MoveBackAll" /><br/>
	///             </fluent:ListTransfer>
	///           </td>
	///           <td>
	///             <asp:ListBox ID="ListBoxProjectMembers" SelectionMode="Multiple"  Runat="server"/>
	///           </td>
	///         </tr>
	///       </table>
	///       
	///     </form>
	///   </body>
	///	</html>
	/// ]]></code>
	/// </example>
	[ToolboxData("<{0}:ListTransfer runat=server></{0}:ListTransfer>")]
	[Designer(typeof(ListTransferDesigner))]
	public class ListTransfer : Control, IPostBackDataHandler {

		private ListControl sourceControl;
		private ListControl destinationControl;
		

		/// <summary>
		/// The ListControl that is the destination for transfer operations.
		/// </summary>
		/// <remarks>
		/// Use the ListControlTo property to specify the ListControl that is the destination for trasfer operations. 
		/// This property must be set to the ID of a ListControl. If you do not specify 
		/// a valid ListControl, an exception will be thrown when the page is rendered. The ID must 
		/// refer to a ListControl within the same container as the ListTransfer control. It must be in the 
		/// same page or user control, or it must be in the same template of a templated control.
		/// </remarks>
		[
		Description("The ListControl that is the destination for transfer operations."),
		Category("Behavior"),
		Bindable(true),
		DefaultValue(""),
		TypeConverter(typeof(ListTransferControlIDConverter))
		]
		public string ListControlTo {
			get { return GetViewStateString("ListControlTo"); }
			set { ViewState["ListControlTo"] = value; }
		}

		/// <summary>
		/// The ListControl that is the source for transfer operations.
		/// </summary>
		/// <remarks>
		/// Use the ListControlTo property to specify the ListControl that is the source for trasfer operations. 
		/// This property must be set to the ID of a ListControl. If you do not specify 
		/// a valid ListControl, an exception will be thrown when the page is rendered. The ID must 
		/// refer to a ListControl within the same container as the ListTransfer control. It must be in the 
		/// same page or user control, or it must be in the same template of a templated control.
		/// </remarks>
		[
		Description("The ListControl that is the source for transfer operations."),
		Category("Behavior"),
		Bindable(true),
		DefaultValue(""),
		TypeConverter(typeof(ListTransferControlIDConverter))
		]
		public string ListControlFrom {
			get { return GetViewStateString("ListControlFrom"); }
			set { ViewState["ListControlFrom"] = value; }
		}

		/// <summary>
		/// Whether the ListTransfer registers script for client side transfers.
		/// </summary>
		/// <remarks>
		/// Enabling the ListControl for client side transfer allows javascript calls to be made to all
		/// transfer functions.
		/// 
		/// </remarks>
		/// /// <example>
		/// The following example demonstrates how to use a ListTransfer control with Client Side Script.
		///	<code><![CDATA[
		///	
		///	...
		///	
		/// <fluent:ListTransfer 
		/// Runat="server" 
		/// ID="ListTransferEmployees"
		/// ListControlFrom="ListBoxEmployees" 
		/// ListControlTo="ListBoxProjectMembers" 
		/// />
		/// 
		/// ...
		/// 
		///	<Button OnClick="<%= ListTransferEmployees.ClientCopySelected %>" >></Button>
		///	<Button OnClick="<%= ListTransferEmployees.ClientRemoveSelected %>" ><</Button>
		///	<Button OnClick="<%= ListTransferEmployees.ClientCopyAll %>" >>></Button>
		///	<Button OnClick="<%= ListTransferEmployees.ClientRemoveAll %>" ><<</Button>
		///
		/// ...
		/// ]]></code>
		/// </example>
		[
		Description("Whether the ListTransfer registers script for client side transfers."),
		Category("Behavior"),
		Bindable(true),
		DefaultValue("False"),
		]
		public bool EnableClientSide {
			get {
				object o = ViewState["EnableClientSide"];
				if(o == null){
					return false;
				} else {
					return (bool)o;
				}
			}
			set { ViewState["EnableClientSide"] = value; }
		}


		/// <summary>
		/// Whether duplicate ListItems will be transfered.
		/// </summary>
		[
		Description("Gets or sets whether duplicate ListItems will be copied."),
		Category("Behavior"), 
		Bindable(true),
		DefaultValue("False")
		]
		public bool AllowDuplicates {
			get {
				object o = ViewState["AllowDuplicates"];
				if(o == null){
					return false;
				} else {
					return (bool)o;
				}
			}
			set { ViewState["AllowDuplicates"] = value; }
		}

		private string GetViewStateString(string key){
			object o = ViewState[key];
			if(o == null){
				return string.Empty;
			} else {
				return (string)o;
			}
		}

		/// <summary>
		/// Move the selected ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		public void MoveSelected(){
			Transfer(GetSourceListControl(), GetDestinationListControl(), true, true, true);
		}

		/// <summary>
		/// Move all the ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		public void MoveAll(){
			Transfer(GetSourceListControl(), GetDestinationListControl(), false, true, true);
		}

		/// <summary>
		/// Move the selected ListItems in the destination ListControlTo to the source ListControlFrom.
		/// </summary>
		public void MoveBackSelected(){
			Transfer(GetDestinationListControl(), GetSourceListControl(), true, true, true);
		}

		/// <summary>
		/// Move all the ListItems in the destination ListControlTo to the source ListControlFrom.
		/// </summary>
		public void MoveBackAll(){
			Transfer(GetDestinationListControl(), GetSourceListControl(), false, true, true);
		}

		/// <summary>
		/// Copy the selected ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		public void CopySelected(){
			Transfer(GetSourceListControl(), GetDestinationListControl(), true, true, false);
		}

		/// <summary>
		/// Copy all the ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		public void CopyAll(){
			Transfer(GetSourceListControl(), GetDestinationListControl(), false, true, false);
		}

		/// <summary>
		/// Remove the selected ListItems in the destination ListControlTo.
		/// </summary>
		public void RemoveSelected(){
			Transfer(GetDestinationListControl(), GetSourceListControl(), true, false, true);
		}

		/// <summary>
		/// Remove all the ListItems in the destination ListControlTo.
		/// </summary>
		public void RemoveAll(){
			Transfer(GetDestinationListControl(), GetSourceListControl(), false, false, true);
		}

		/// <summary>
		/// Moves up all the selected ListItems in ListControlTo.
		/// </summary>
		public void MoveUpListControlTo(){
			Move(GetDestinationListControl(), true);
		}

		/// <summary>
		/// Moves down all the selected ListItems in ListControlTo.
		/// </summary>
		public void MoveDownListControlTo(){
			Move(GetDestinationListControl(), false);
		}

		/// <summary>
		/// Moves up all the selected ListItems in ListControlFrom.
		/// </summary>
		public void MoveUpListControlFrom(){
			Move(GetSourceListControl(), true);
		}

		/// <summary>
		/// Moves down all the selected ListItems in ListControlFrom.
		/// </summary>
		public void MoveDownListControlFrom(){
			Move(GetSourceListControl(), false);
		}


		/// <summary>
		/// Transfers ListItems between two ListControls.
		/// </summary>
		/// <param name="source">The source ListControl.</param>
		/// <param name="destination">The destination ListControl.</param>
		/// <param name="selectedOnly">Whether to transfer only selected ListItems.</param>
		/// <param name="add">Whether to add the transfered ListItems to the destination ListControl.</param>
		/// <param name="remove">Whether to remove the transfered ListItems to the source ListControl.</param>
		public void Transfer(ListControl source, ListControl destination, bool selectedOnly, bool add, bool remove){

			bool multipleSelection = false;
			if(destination is ListBox){
				multipleSelection = ((ListBox)destination).SelectionMode == ListSelectionMode.Multiple;
			}

			//2 steps to Avoid 'Collection was modified; enumeration operation may not execute. '
			ArrayList itemsForTransfer = new ArrayList();

			foreach(ListItem item in source.Items){
				if(item.Selected || !selectedOnly){
					itemsForTransfer.Add(item);
				}
			}

			foreach(ListItem item in itemsForTransfer){
				if(remove){
					source.Items.Remove(item);
				} 
				if(add && ( AllowDuplicates || !destination.Items.Contains(item))){
					item.Selected = false;
					destination.Items.Add(item);
				}
				
			}
		}

		/// <summary>
		/// Moves selected ListItems in a ListControl.
		/// </summary>
		/// <param name="listControl">The ListControl in which to move ListItems</param>
		/// <param name="moveUp">Whether to move items up or down.</param>
		public void Move(ListControl listControl, bool moveUp){

			ListItem tmpListItem;
			
			if(moveUp){
				for(int i = 1; i < listControl.Items.Count; i++){
					if(listControl.Items[i].Selected && !listControl.Items[i-1].Selected){
						tmpListItem = listControl.Items[i];
						listControl.Items.RemoveAt(i);
						listControl.Items.Insert(i-1,tmpListItem);
					}
				}
			} else {
				for(int i = listControl.Items.Count - 2; i >= 0; i--){
					if(listControl.Items[i].Selected && !listControl.Items[i+1].Selected){
						tmpListItem = listControl.Items[i];
						listControl.Items.RemoveAt(i);
						listControl.Items.Insert(i+1,tmpListItem);
					}
				}
			}
		}



		private ListControl GetDestinationListControl(){
			if(ListControlTo == null || ListControlTo == string.Empty){
				throw new ArgumentException("Property 'ListControlTo' must be a valid ListControl ID.");
			}
			return CachedFindListControl(ref destinationControl, ListControlTo);
		}

		private ListControl GetSourceListControl(){
			if(ListControlFrom == null || ListControlFrom == string.Empty){
				throw new ArgumentException("Property 'ListControlFrom' must be a valid ListControl ID.");
			}
			return CachedFindListControl(ref sourceControl, ListControlFrom);
		}

		private ListControl CachedFindListControl(ref ListControl control, String id){
			
			try{
				if(control == null){
					control = (ListControl)this.FindControl(id);
					if(control == null){
						throw new ArgumentException("ListControl not found: " + id);
					}
				}
				return control;
			} catch (Exception e){
				throw new ArgumentException("ListControl not found: " + id, e);
			}
		}

		/// <summary>
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output) {
			base.Render(output);
		}


		//Client Side Support
		protected override void OnLoad(System.EventArgs e) {

			//Sanity checks
			if(GetSourceListControl() == GetDestinationListControl()){
				throw new ArgumentException("The ListControlTo can not be the same ListControl as ListControlFrom");
			}

			//TODO: Should move to pre render?
			if(EnableClientSide){
				ResourceUtil.RegisterScript(this,"Fluent.ListTransfer.js");
			}
			
		}

		protected override void OnInit(System.EventArgs e) {
			if(EnableClientSide && EnableViewState){
				GetSourceListControl().EnableViewState = false;
				GetDestinationListControl().EnableViewState = false;
			}		
		}

		protected override void OnPreRender(System.EventArgs e) {
			if(EnableClientSide){
				SaveListControlState();
				InjectJavaScript(this.Controls);
			}		
		}

		private void InjectJavaScript(ControlCollection controls){
			
			if(controls.Count == 0) return;
			
			foreach(Control c in controls){
				if(c is LinkButton){
					LinkButton lb = (LinkButton)c;
					string js;
					if((js = GetJavaScript(lb.CommandName)) != null){
						lb.Attributes.Add("onclick",js);
					}
				} else if(c is Button){
					Button b = (Button)c;
					string js;
					if((js = GetJavaScript(b.CommandName)) != null){
						b.Attributes.Add("onclick",js);
					}
				} else if(c is ImageButton){
					ImageButton ib = (ImageButton)c;
					string js;
					if((js = GetJavaScript(ib.CommandName)) != null){
						ib.Attributes.Add("onclick",js);
					}
				} else {
					InjectJavaScript(c.Controls);
				}
			}
		}

		private string GetJavaScript(string commandName){
			switch(commandName.ToLower()){
				case "copyselected":
					return this.ClientCopySelected;
				case "removeselected":
					return this.ClientRemoveSelected;
				case "copyall":
					return this.ClientCopyAll;
				case "removeall":
					return this.ClientRemoveAll;
				case "moveselected":
					return this.ClientMoveSelected;
				case "movebackselected":
					return this.ClientMoveBackSelected;
				case "moveall":
					return this.ClientMoveAll;
				case "movebackall":
					return this.ClientMoveBackAll;
				case "moveuplistcontrolfrom":
					return this.ClientMoveUpListControlFrom;
				case "moveuplistcontrolto":
					return this.ClientMoveUpListControlTo;
				case "movedownlistcontrolfrom":
					return this.ClientMoveDownListControlFrom;
				case "movedownlistcontrolto":
					return this.ClientMoveDownListControlTo;
				default:
					return null;
			}
		}

		protected override bool OnBubbleEvent(object source, System.EventArgs args) {
			if(args is CommandEventArgs){ 
				CommandEventArgs ce = (CommandEventArgs)args;
				switch(ce.CommandName.ToLower()){
					case "copyselected":
						this.CopySelected();
						return true;
					case "removeselected":
						this.RemoveSelected();
						return true;
					case "copyall":
						this.CopyAll();
						return true;
					case "removeall":
						this.RemoveAll();
						return true;
					case "moveselected":
						this.MoveSelected();
						return true;
					case "movebackselected":
						this.MoveBackSelected();
						return true;
					case "moveall":
						this.MoveAll();
						return true;
					case "movebackall":
						this.MoveBackAll();
						return true;
					case "moveuplistcontrolfrom":
						this.MoveUpListControlFrom();
						return true;
					case "moveuplistcontrolto":
						this.MoveUpListControlTo();
						return true;
					case "movedownlistcontrolfrom":
						this.MoveDownListControlFrom();
						return true;
					case "movedownlistcontrolto":
						this.MoveDownListControlTo();
						return true;
					default:
						return false;
				}
			} else {
				return false;
			}
		}
	
		private string GetClientID(Control control){
			//			string clientID = control.ID;
			//			while(control.Parent != null){
			//				control = control.Parent;
			//				if(control is INamingContainer && control.ID != null){
			//					clientID = control.ID + "_" + clientID;
			//				}
			//			}
			return control.ClientID;
		}

		
		#region Implementation of IPostBackDataHandler
		public void RaisePostDataChangedEvent() {
			
		}

		public bool LoadPostData(string postDataKey, System.Collections.Specialized.NameValueCollection postCollection) {
			if(EnableClientSide && EnableViewState){
				RestoreListControlState();
			}	
			return false;
		}
		#endregion

		private void RestoreListControlState(){

			if(Page.IsPostBack){
				string listFromHidden = (string)ViewState["listFromHidden"];
				string listToHidden = (string)ViewState["listToHidden"];

#if (TRACE)
				this.Page.Trace.Write("Loading State: " + listFromHidden);
				this.Page.Trace.Write("Loading State: " + listToHidden);
#endif


				RestoreListControl(GetSourceListControl(), Page.Request[listFromHidden]);
				RestoreListControl(GetDestinationListControl(), Page.Request[listToHidden]);
			}
		}

		private void SaveListControlState(){

			string listFromHidden = GetClientID(GetSourceListControl()) + "_State";
			string listToHidden = GetClientID(GetDestinationListControl()) + "_State";

			ViewState["listFromHidden"] = listFromHidden;
			ViewState["listToHidden"] = listToHidden;

#if (TRACE)
			this.Page.Trace.Write("Saving State: " + listFromHidden);
			this.Page.Trace.Write("Saving State: " + listToHidden);
#endif
			this.Page.RegisterHiddenField(this.UniqueID,"");
			this.Page.RegisterHiddenField(listFromHidden,StoreListState(GetSourceListControl()));
			this.Page.RegisterHiddenField(listToHidden,StoreListState(GetDestinationListControl()));
		}

		private void RestoreListControl(ListControl list, string state){


			if(state != string.Empty && state != null){

				if(!state.StartsWith("1")){
					state = Encoding.Unicode.GetString( Convert.FromBase64String(state) );
				} 

				string[] items = state.Split(new char[]{'|'});
			
				if(items[0] == "1"){
					list.Items.Clear();
					for(int i = 1; i < (items.Length); ){
						ListItem item = new ListItem(items[i++],items[i++]);
						list.Items.Add(item);
					}
				}
			}
		}

		private string StoreListState(ListControl list){
			
			StringBuilder state = new StringBuilder();
			state.Append("1");
			foreach(ListItem item in list.Items){
				state.Append("|" + item.Text + "|" + item.Value);
			}
			
			return Convert.ToBase64String(Encoding.Unicode.GetBytes(state.ToString()));
		}


		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// moves the selected ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveSelected{
			get { 
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', true, true, true, {2}); return false;",
					GetClientID(GetSourceListControl()), GetClientID(GetDestinationListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// moves all the ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveAll{
			get { 
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', false, true, true, {2}); return false;"
					,GetClientID(GetSourceListControl()), GetClientID(GetDestinationListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// moves the selected ListItems in the destination ListControlTo to the source ListControlFrom.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveBackSelected{
			get { 
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', true, true, true, {2}); return false;",
					GetClientID(GetDestinationListControl()), GetClientID(GetSourceListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// copies the selected ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveBackAll{
			get {
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', false, true, true, {2}); return false;",
					GetClientID(GetDestinationListControl()), GetClientID(GetSourceListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// copies the selected ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		[Browsable(false)]
		public string ClientCopySelected{
			get { 
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', true, true, false, {2}); return false;",
					GetClientID(GetSourceListControl()), GetClientID(GetDestinationListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// copies all the ListItems in the source ListControlFrom to the destination ListControlTo.
		/// </summary>
		[Browsable(false)]
		public string ClientCopyAll{
			get {
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', false, true, false, {2}); return false;",
					GetClientID(GetSourceListControl()), GetClientID(GetDestinationListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// removes the selected ListItems in the destination ListControlTo.
		/// </summary>
		[Browsable(false)]
		public string ClientRemoveSelected{
			get {
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', true, false, true, {2}); return false;",
					GetClientID(GetDestinationListControl()), GetClientID(GetSourceListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// removes all the ListItems in the destination ListControlTo.
		/// </summary>
		[Browsable(false)]
		public string ClientRemoveAll{
			get {
				EnableClientSide = true;
				return string.Format("javascript:LT_Transfer('{0}','{1}', false, false, true, {2}); return false;",
					GetClientID(GetDestinationListControl()), GetClientID(GetSourceListControl()), this.AllowDuplicates.ToString().ToLower()); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// moves the selected ListItems in the ListControlTo up.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveUpListControlTo {
			get {
				EnableClientSide = true;
				return string.Format("javascript:LT_Move('{0}', true); return false;",GetClientID(GetDestinationListControl())); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// moves the selected ListItems in the ListControlTo down.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveDownListControlTo {
			get { 
				EnableClientSide = true;
				return string.Format("javascript:LT_Move('{0}', false); return false;",GetClientID(GetDestinationListControl())); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// moves the selected ListItems in the ListControlFrom up.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveUpListControlFrom {
			get {
				EnableClientSide = true;
				return string.Format("javascript:LT_Move('{0}', true); return false;",GetClientID(GetSourceListControl())); 
			}
		}

		/// <summary>
		/// Obtains a reference to a client-side script function that causes, when invoked, 
		/// moves the selected ListItems in the ListControlFrom down.
		/// </summary>
		[Browsable(false)]
		public string ClientMoveDownListControlFrom {
			get {
				EnableClientSide = true;
				return string.Format("javascript:LT_Move('{0}', false); return false;",GetClientID(GetSourceListControl())); 
			}
		}

	}
}
