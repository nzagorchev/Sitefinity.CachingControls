<%@ Control Language="C#" %>

<ul>
    <asp:Repeater ID="Repeater1" runat="server" DataSource='<%# Eval("Images") %>'>
        <ItemTemplate>
            <li class="sfrelatedListItem sflistitem">
                <a href='<%# Eval("ThumbnailUrl") %>'>
                    <img src='<%# Eval("ThumbnailUrl")%>' alt='<%# Eval("AlternativeText")%>' title='<%# Eval("Title") %>' />
                </a>
            </li>
        </ItemTemplate>
    </asp:Repeater>
</ul>

<ul class="sfrelatedList sflist">
    <asp:Repeater ID="Repeater2" runat="server" DataSource='<%# Eval("Sessions") %>'>
        <ItemTemplate>
            <li class="sfrelatedListItem sflistitem"><a href='<%# Eval("DetailUrl") %>'><%# Eval("Title") %> (<%# Eval("Duration") %> mins)</a></li>
        </ItemTemplate>
    </asp:Repeater>
</ul>
