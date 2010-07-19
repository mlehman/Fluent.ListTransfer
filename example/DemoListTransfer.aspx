<%@ Page Language="C#" %>
<%@ Register TagPrefix="fluent" Namespace="Fluent" Assembly="Fluent.ListTransfer" %>
	
<script runat="server">

	void AddEmployees(object sender, EventArgs args){
		ListTransferEmployees.CopySelected();
	}

	void AddAllEmployees(object sender, EventArgs args){
		ListTransferEmployees.CopyAll();
	}

	void RemoveEmployees(object sender, EventArgs args){
		ListTransferEmployees.RemoveSelected();
	}

	void RemoveAllEmployees(object sender, EventArgs args){
		ListTransferEmployees.RemoveAll();
	} 
	
	void MoveUp(object sender, EventArgs args){
		ListTransferEmployees.MoveUpListControlTo();
	}

	void MoveDown(object sender, EventArgs args){
		ListTransferEmployees.MoveDownListControlTo();
	}           
	
</script>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" > 

<html>
  <head>
    <title>DemoListTransfer</title>
    <style>
		h1 {
			font-family: verdana, arial, helvetica, sans-serif;
			font-weight: bold;
			font-size: 14px;
			text-align: left;
			margin-top: 20px;
			margin-botton: 0px;
		}
		
		.exp {
			font-family: verdana, arial, helvetica, sans-serif;
			font-size: 10px;
		}
		
		.label {
			font-family: verdana, arial, helvetica, sans-serif;
			font-weight: bold;
			font-size: 12px;
			color: #666666;
			padding: 4px;
		}
		
		.title {
			font-family: verdana, arial, helvetica, sans-serif;
			font-weight: bold;
			font-size: 10px;
			color: #333333;
			text-align: left;
		}
		
		.listbox {
			width: 120px;
			height: 200px;
			border-color: Silver;
			border-width: 1px;
		}

    </style>
  </head>
  <body>
    <form id="DemoListTransfer" method="post" runat="server">
		    
		<h1>Demo 1: <i>Server Side</i></h1>
		<p class="exp">This example demonstrates the copying list items and using postbacks. <a href="#example1">see code</a></p>
		
    
		<div class="label">Select Project Members.</div>	
		<table border="0">
			<tr>
				<td class="title">All Employees</td>
				<td></td>
				<td class="title">Project Members</td>
				<td></td>
			</tr>
			<tr>
				<td valign="top">
						<asp:ListBox ID="ListBoxEmployees" Runat="server"  
							SelectionMode="Multiple" 
							CssClass="listbox">
						<asp:ListItem Value="1">Jim Robertson</asp:ListItem>
						<asp:ListItem Value="2">Steve Wilson</asp:ListItem>
						<asp:ListItem Value="3">Bob Alexander</asp:ListItem>
						<asp:ListItem Value="4">Jim Frontega</asp:ListItem>
						<asp:ListItem Value="5">Alan Meijer</asp:ListItem>
						<asp:ListItem Value="6">Stacy Hanson</asp:ListItem>
						<asp:ListItem Value="7">Theresa Davis</asp:ListItem>
						<asp:ListItem Value="8">Katy Long</asp:ListItem>
					</asp:ListBox>
				</td>
				<td valign="top">
					<fluent:ListTransfer Runat="server" ID="ListTransferEmployees" 
						ListControlTo="ListBoxProjectMembers" 
						ListControlFrom="ListBoxEmployees" 
						/>
					
					<asp:LinkButton Runat="server" OnClick="AddEmployees"><img border="0" src="images/right.gif"></asp:LinkButton><br>
					<asp:LinkButton Runat="server" OnClick="RemoveEmployees"><img border="0" src="images/left.gif"></asp:LinkButton><br>
					<asp:LinkButton Runat="server" OnClick="AddAllEmployees"><img border="0" src="images/rightAll.gif"></asp:LinkButton><br>
					<asp:LinkButton Runat="server" OnClick="RemoveAllEmployees"><img border="0" src="images/leftAll.gif"></asp:LinkButton></td>
				<td valign="top">
					<asp:ListBox ID="ListBoxProjectMembers" Runat="server" 
						SelectionMode="Multiple" 
						CssClass="listbox"  
						/>
				</td>
				<td valign="top">
					<asp:LinkButton Runat="server" OnClick="MoveUp" ><img border="0" src="images/up.gif"></asp:LinkButton><br>
					<asp:LinkButton Runat="server" OnClick="MoveDown"><img border="0" src="images/down.gif"></asp:LinkButton><br>
					<br>
				</td>
			</tr>
		</table> 
		
		
		<h1>Demo 2: <i>Client Side</i></h1>
		<p class="exp">This example demonstrates the moving of list items and is not using postbacks. <a href="#example2">see code</a><p>
    
		<div class="label">Configure your Dash Board.</div>	
		<table border="0">
			<tr>
				<td class="title">Left Column</td>
				<td></td>
				<td class="title">Middle Column</td>
				<td></td>
				<td class="title">Middle Column</td>
				<td></td>
			</tr>
			<tr>
				<td valign="top">
					<asp:ListBox ID="ListBoxLeftColumn" Runat="server"  
							SelectionMode="Multiple" 
							CssClass="listbox"
							>
						<asp:ListItem Value="1">Search</asp:ListItem>
						<asp:ListItem Value="2">News</asp:ListItem>
						<asp:ListItem Value="3">Filters</asp:ListItem>
					</asp:ListBox>
					
				</td>
				<td valign="top">
					<fluent:ListTransfer Runat="server" ID="ListTransfer1"
						EnableClientSide="True" 
						ListControlTo="ListBoxMiddleColumn" 
						ListControlFrom="ListBoxLeftColumn" 
						/>
					<a href="#" onclick="<%= ListTransfer1.ClientMoveUpListControlFrom %>" ><img border="0" src="images/up.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer1.ClientMoveDownListControlFrom %>"><img border="0" src="images/down.gif"></a><br>
					<br>
					<a href="#" onclick="<%= ListTransfer1.ClientMoveSelected %>" ><img border="0" src="images/right.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer1.ClientMoveBackSelected %>"><img border="0" src="images/left.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer1.ClientMoveAll %>"><img border="0" src="images/rightAll.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer1.ClientMoveBackAll %>"><img border="0" src="images/leftAll.gif"></a></td>
				</td>
				<td valign="top">
					<asp:ListBox ID="ListBoxMiddleColumn" Runat="server"  
							SelectionMode="Multiple" 
							CssClass="listbox"
							>
						<asp:ListItem Value="4">Shortcuts</asp:ListItem>
						<asp:ListItem Value="5">Summary</asp:ListItem>
					</asp:ListBox>
				</td>
				<td valign="top">
					<fluent:ListTransfer Runat="server" ID="ListTransfer2" 
						EnableClientSide="True" 
						ListControlTo="ListBoxRightColumn" 
						ListControlFrom="ListBoxMiddleColumn" 
						/>
					<a href="#" onclick="<%= ListTransfer2.ClientMoveUpListControlFrom %>" ><img border="0" src="images/up.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer2.ClientMoveDownListControlFrom %>"><img border="0" src="images/down.gif"></a><br>
					<br>
					<a href="#" onclick="<%= ListTransfer2.ClientMoveSelected %>"><img border="0" src="images/right.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer2.ClientMoveBackSelected %>"><img border="0" src="images/left.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer2.ClientMoveAll %>"><img border="0" src="images/rightAll.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer2.ClientMoveBackAll %>"><img border="0" src="images/leftAll.gif"></a></td>
				</td>
				<td valign="top">
					<asp:ListBox ID="ListBoxRightColumn" Runat="server"  
							SelectionMode="Multiple" 
							CssClass="listbox"
							>
						<asp:ListItem Value="6">Overdue</asp:ListItem>
						<asp:ListItem Value="7">My Tasks</asp:ListItem>
					</asp:ListBox>
				</td>
				<td valign="top">
					<a href="#" onclick="<%= ListTransfer2.ClientMoveUpListControlTo %>" ><img border="0" src="images/up.gif"></a><br>
					<a href="#" onclick="<%= ListTransfer2.ClientMoveDownListControlTo %>"><img border="0" src="images/down.gif"></a><br>
					<br>
				</td>
			</tr>
		</table> 
		
	</form>
  </body>
</html>
