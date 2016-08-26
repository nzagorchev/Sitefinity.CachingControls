<%@ Control Language="C#" %>

<div class="main-submenu-three">
    <div class="nav-blurb">
        <div class="nb-img">
            <a class="customNavAnchor" <%# (Eval("RedirectPage1") != null) && (bool)Eval("RedirectPage1.OpenNewWindow") ? "target=\"_blank\" " : "" %>href='<%# Eval("RedirectPage1.Url") %>'>
                <img src='<%# Eval("ThumbnailImage1.ThumbnailUrl")%>' alt='<%# Eval("ThumbnailImage1.AlternativeText")%>' title='<%# Eval("ThumbnailImage1.Title") %>' /></a>
        </div>
        <div class="nb-content">
            <a class="customNavAnchor" <%# (Eval("RedirectPage1") != null) && (bool)Eval("RedirectPage1.OpenNewWindow") ? "target=\"_blank\" " : "" %>href='<%# Eval("RedirectPage1.Url") %>'>
                <h1><%# Eval("RedirectPage1.Title") %></h1>
            </a>

            <div class="grid info">
                <a <%# (Eval("RedirectPage1") != null) && (bool)Eval("RedirectPage1.OpenNewWindow") ? "target=\"_blank\" " : "" %>href='<%# Eval("RedirectPage1.Url") %>'>
                    <sitefinity:TextField ID="TextField1" runat="server" DisplayMode="Read" Value='<%# Eval("AdditionalText1")%>' />
                </a>
                <a class="customNavAnchor" <%# (Eval("RedirectPage1") != null) && (bool)Eval("RedirectPage1.OpenNewWindow") ? "target=\"_blank\" " : "" %>href='<%# Eval("RedirectPage1.Url") %>'>
                    <sitefinity:TextField ID="TextField2" runat="server" DisplayMode="Read" Value='<%# Eval("RedirectButton1")%>' />
                </a>
            </div>
        </div>
    </div>
</div>