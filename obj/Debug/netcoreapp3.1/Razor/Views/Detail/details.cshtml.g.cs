#pragma checksum "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "27bcb1570cce55a3c1acdedd3703d479b016d09b"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Detail_details), @"mvc.1.0.view", @"/Views/Detail/details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27bcb1570cce55a3c1acdedd3703d479b016d09b", @"/Views/Detail/details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2070f3b410310d0cebca47d3a3db12e0cca18c53", @"/Views/_ViewImports.cshtml")]
    public class Views_Detail_details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TestNotebook.Models.Detail>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
  
        ViewBag.Title = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n        <h1>Detail</h1>\r\n        <h4>Details</h4>\r\n        <div class=\"card\" style=\"width: 18rem;\">\r\n                <div class=\"card-body\">\r\n                        <h5 class=\"card-title\">");
#nullable restore
#line 12 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                          Write(Html.DisplayFor(model=>model.Header.Title));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                        <h6 class=\"card-subtitle mb-2 text-muted\">");
#nullable restore
#line 13 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                                             Write(Html.DisplayFor(model => model.Field.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n                        <p class=\"card-text\">\r\n                        <dl class=\"dl-horizontal\">\r\n                                <dt>\r\n                                                ");
#nullable restore
#line 17 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayNameFor(model => model.Field.Screen));

#line default
#line hidden
#nullable disable
            WriteLiteral(":\r\n                                </dt>\r\n                                <dl>\r\n                                                ");
#nullable restore
#line 20 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayFor(model => model.Field.Screen.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                                </dl>\r\n                                <dt>\r\n                                                ");
#nullable restore
#line 23 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayNameFor(model=>model.Question.Control));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </dt>\r\n                                <dl>\r\n                                                ");
#nullable restore
#line 26 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayFor(model=>model.Question.Control.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                                </dl>\r\n                                <dt>\r\n                                                ");
#nullable restore
#line 29 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayNameFor(model=>model.Question));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </dt>\r\n                                <dl>\r\n                                                ");
#nullable restore
#line 32 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayFor(model=>model.Question.Description));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                                </dl>\r\n                                <dt>\r\n                                                ");
#nullable restore
#line 35 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayNameFor(model=>model.AnswerYN));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </dt>\r\n                                <dl>\r\n                                                ");
#nullable restore
#line 38 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayFor(model=>model.AnswerYN));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                                </dl>\r\n                                <dt>\r\n                                                ");
#nullable restore
#line 41 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayNameFor(model=>model.AnswerText));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </dt>\r\n                                <dl>\r\n                                                ");
#nullable restore
#line 44 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayFor(model=>model.AnswerText));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                                </dl>\r\n                                <dt>\r\n                                                ");
#nullable restore
#line 47 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayNameFor(model=>model.Result));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                </dt>\r\n                                <dl>\r\n                                                ");
#nullable restore
#line 50 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                                           Write(Html.DisplayFor(model=>model.Result.description));

#line default
#line hidden
#nullable disable
            WriteLiteral("<br />\r\n                                </dl>\r\n                        </dl>\r\n                        </p>\r\n                        <div class=\"btn-group\">\r\n                                ");
#nullable restore
#line 55 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                           Write(Html.ActionLink("Edit", "Edit", new { @id = Model.Id,@HeaderId=Model.HeaderId }, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                                ");
#nullable restore
#line 56 "C:\Users\chimphonda\TestNotebook\Views\Detail\details.cshtml"
                           Write(Html.ActionLink("Back to List", "Details", "Header",new {@id=Model.HeaderId}, new
                                {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                </div>\r\n        </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TestNotebook.Models.Detail> Html { get; private set; }
    }
}
#pragma warning restore 1591
