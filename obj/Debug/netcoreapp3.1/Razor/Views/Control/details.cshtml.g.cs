#pragma checksum "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "95dad3a081d06d047d09c6e78dde75d051ebb708"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Control_details), @"mvc.1.0.view", @"/Views/Control/details.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\chimphonda\TestNotebook\Views\_ViewImports.cshtml"
using TestNotebook;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\chimphonda\TestNotebook\Views\_ViewImports.cshtml"
using TestNotebook.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"95dad3a081d06d047d09c6e78dde75d051ebb708", @"/Views/Control/details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2070f3b410310d0cebca47d3a3db12e0cca18c53", @"/Views/_ViewImports.cshtml")]
    public class Views_Control_details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TestNotebook.Models.Control>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
  
    ViewBag.Title = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <h1>Control</h1>\r\n    <h5>");
#nullable restore
#line 9 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
   Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n    <h6 class=\"text-muted\">");
#nullable restore
#line 10 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
                      Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n    <h4>Details</h4>\r\n    <div class=\"card\">\r\n        <div class=\"card-body\">\r\n            <p>\r\n                ");
#nullable restore
#line 15 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
           Write(Html.ActionLink("Create Question", "Create","Question", new {@controlid=Model.Id}, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
            </p>
            <p class=""card-text"">
                <table class=""table table-striped table-bordered"">
                <tr>
                    <th>
                        Description
                    </th>
                    <th>Action</th>
                </tr>
");
#nullable restore
#line 25 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
                 foreach (var item in Model.Questions)
                {

#line default
#line hidden
#nullable disable
            WriteLiteral("                    <tr>\r\n                        <td>\r\n                            ");
#nullable restore
#line 29 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
                       Write(Html.DisplayFor(modelItem => item.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </td>\r\n                        <td>\r\n                            <div class=\"btn-group\">\r\n                                ");
#nullable restore
#line 33 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
                           Write(Html.ActionLink("Edit", "Edit","Question", new { id=item.Id, @controlid=item.ControlId }, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                ");
#nullable restore
#line 34 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
                           Write(Html.ActionLink("Details", "Details","Question", new { id=item.Id, @controlid=item.ControlId }, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                ");
#nullable restore
#line 35 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
                           Write(Html.ActionLink("Delete", "Delete","Question", new { id=item.Id,  @controlid=item.ControlId }, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                            </div>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 39 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                </table>\r\n            </p>            \r\n        </div>\r\n    </div>\r\n    <br/>\r\n    <div class=\"btn-group\">\r\n        ");
#nullable restore
#line 46 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
   Write(Html.ActionLink("Edit", "Edit", new { id = Model.Id }, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        ");
#nullable restore
#line 47 "C:\Users\chimphonda\TestNotebook\Views\Control\details.cshtml"
   Write(Html.ActionLink("Back to List", "Index",new {}, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n</div>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TestNotebook.Models.Control> Html { get; private set; }
    }
}
#pragma warning restore 1591