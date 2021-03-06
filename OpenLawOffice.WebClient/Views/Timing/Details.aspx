﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<OpenLawOffice.WebClient.ViewModels.Timing.TimeViewModel>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Time Entry Details
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div id="roadmap">
        <div class="zero">Matter: [<%: Html.ActionLink((string)ViewData["Matter"], "Details", "Matters", new { id = ViewData["MatterId"] }, null) %>]</div>
        <div class="one">Task: [<%: Html.ActionLink((string)ViewData["Task"], "Details", "Tasks", new { id = ViewData["TaskId"] }, null) %>]</div>
        <div id="current" class="two">Time Entry Details<a id="pageInfo" class="btn-question" style="padding-left: 15px;">Help</a></div>
    </div>
    
    <table class="detail_table">
        <tr>
            <td class="display-label">
                Id
            </td>
            <td class="display-field">
                <%: Model.Id %>
            </td>
        </tr>
        <tr>
            <td class="display-label">
                Duration
            </td>
            <td class="display-field">
                <% if (Model.Stop.HasValue)
                   { %>
                    <%: Math.Round(((TimeSpan)(Model.Stop - Model.Start)).TotalMinutes, 0)%> min.
                <% } %>
            </td>
        </tr>
        <tr>
            <td class="display-label">
                Start
            </td>
            <td class="display-field">
                <%: String.Format("{0:g}", Model.Start)%>
            </td>
        </tr>
        <tr>
            <td class="display-label">
                Stop
            </td>
            <td class="display-field">
                <% if (Model.Stop.HasValue)
                   { %>
                    <%: String.Format("{0:g}", Model.Stop.Value)%>
                <% } %>
            </td>
        </tr>
        <tr>
            <td class="display-label" style="width: 125px;">
                Billable
            </td>
            <td class="display-field">
                <%: Html.CheckBoxFor(model => model.Billable, new { disabled = true })%>
            </td>
        </tr>
        <tr>
            <td class="display-label">
                Worker
            </td>
            <td class="display-field">
                <%: Html.ActionLink(Model.Worker.DisplayName, "Details", "Contacts", new { id = Model.Worker.Id }, null) %>
            </td>
        </tr>
        <tr>
            <td class="display-label">
                Details
            </td>
            <td class="display-field">
                <%: Model.Details %>
            </td>
        </tr>
    </table>
<% Html.RenderPartial("CoreDetailsView"); %>

    <div id="pageInfoDialog" title="Help">
        <p>
        <span style="font-weight: bold; text-decoration: underline;">Info:</span>
        Displays detailed information about the time entry.
        </p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MenuContent" runat="server">
    <li>Actions</li>
    <ul style="list-style: none outside none; padding-left: 1em;">
        <li>
            <%: Html.ActionLink("Edit", "Edit", new { id = Model.Id })%></li>
    </ul>
    <% if (ViewData["TaskId"] != null)
        { %>
    <li>
        <%: Html.ActionLink("Task", "Details", "Tasks", new { id = ViewData["TaskId"] }, null)%></li>
    <li>
        <%: Html.ActionLink("Times for Task", "Time", "Tasks", new { id = ViewData["TaskId"] }, null)%></li>
    <% }
        if ((bool)ViewData["IsFastTime"])
        { %>
    <li>
        <%: Html.ActionLink("Relate Task", "RelateTask", "TaskTime", new { id = ViewData["TaskId"] }, null)%></li>
    <% } %>
</asp:Content>