<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StaffPreview.ascx.cs" Inherits="Mohammad.Modules.Staff.Controls.StaffPreview" %>

<asp:Repeater ID="Repeater_Staff" runat="server" DataSourceID="SqlDS_Staff">
    <HeaderTemplate>
        <ul class="staffList">
    </HeaderTemplate>
    <ItemTemplate>
        <li  class="col-md-5 col-sm-12 staffMemeber">
            <a href='<%# getFilePath(Eval("imageFileID").ToString()) %>'>
                <img class="staffMemebrPic col-md-5" src='<%# getFilePath(Eval("imageFileID").ToString()) %>' />
            </a>
            <div class="staffMemebrDesc col-md-7">
                <asp:Literal ID="Literal1" runat="server" Text='<%# decodeHTML(Eval("description").ToString())%>'> </asp:Literal>
            </div>
        </li>
    </ItemTemplate>
    <FooterTemplate>
        </ul>
    </FooterTemplate>
</asp:Repeater>

<asp:SqlDataSource ID="SqlDS_Staff" runat="server" ConnectionString='<%$ ConnectionStrings:SiteSqlServer %>'
    SelectCommand="SELECT [id],[name],[description],[imageFileID]FROM [erasmusplus].[dbo].[Staff_Memeber]where [isDeleted] <> 1 and [isActive]=1 and [ModuleID]=@ModuleID">
    <SelectParameters>
        <asp:Parameter Name="ModuleID" Type="String"></asp:Parameter>
    </SelectParameters>
</asp:SqlDataSource>

<script type="text/javascript">
    $(function () {
        setHeight($('.staffList > li'));
    });
    

    function setHeight(col) {
        console.info("Called");
        var $col = $(col);

        var $maxHeight = 0;
        $col.each(function () {
            
            var $thisHeight = $(this).outerHeight();
            if ($thisHeight > $maxHeight) {
                $maxHeight = $thisHeight;
            }
            console.info($maxHeight);
        });
        $col.height($maxHeight);
    }
</script>