<%@ Control Language="C#" %>

<div class="nav-additional-data">
    <div class="img">
        <a class="nav-link" <%# (Eval("RelatedPage") != null) && (bool)Eval("RelatedPage.OpenNewWindow") ? "target=\"_blank\" " : "" %>href='<%# Eval("RelatedPage.Url") %>'>
            <img src='<%# Eval("RelatedImage.ThumbnailUrl")%>' alt='<%# Eval("RelatedImage.AlternativeText")%>' title='<%# Eval("RelatedImage.Title") %>' />
        </a>
    </div>
    <div class="content">
        <a class="nav-link" <%# (Eval("RelatedPage") != null) && (bool)Eval("RelatedPage.OpenNewWindow") ? "target=\"_blank\" " : "" %>href='<%# Eval("RelatedPage.Url") %>'>
            <h1><%# Eval("RelatedPage.Title") %></h1>
        </a>
        <div class="info">
            <div class="sfTxtContent"><%# Eval("AdditionalInfo")%></div>
        </div>
    </div>
</div>