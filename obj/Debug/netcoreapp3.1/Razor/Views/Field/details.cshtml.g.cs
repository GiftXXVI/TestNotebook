#pragma checksum "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "cf48af5be65afd50f743e1c74650d794c9419946"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Field_details), @"mvc.1.0.view", @"/Views/Field/details.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"cf48af5be65afd50f743e1c74650d794c9419946", @"/Views/Field/details.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"2070f3b410310d0cebca47d3a3db12e0cca18c53", @"/Views/_ViewImports.cshtml")]
    public class Views_Field_details : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<TestNotebook.Models.Field>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml"
  
    ViewBag.Title = "Details";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container\">\r\n    <h1>Field</h1>\r\n    <h4>Details</h4>\r\n    <div class=\"card\" style=\"width: 18rem;\">\r\n        <div class=\"card-body\">\r\n            <h5 class=\"card-title\">");
#nullable restore
#line 12 "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml"
                              Write(Html.DisplayNameFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h6 class=\"card-subtitle mb-2 text-muted\">");
#nullable restore
#line 13 "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml"
                                                 Write(Html.DisplayFor(model => model.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n            <h5 class=\"card-title\">");
#nullable restore
#line 14 "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml"
                              Write(Html.DisplayNameFor(model => model.Screen));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n            <h6 class=\"card-subtitle mb-2 text-muted\">");
#nullable restore
#line 15 "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml"
                                                 Write(Html.DisplayFor(model => model.Screen.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h6>\r\n            <p class=\"card-text\">*</p>\r\n            <div class=\"btn-group\">\r\n                    ");
#nullable restore
#line 18 "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml"
               Write(Html.ActionLink("Edit", "Edit", new { @id = Model.Id,@screenid=Model.ScreenId }, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ");
#nullable restore
#line 19 "C:\Users\chimphonda\TestNotebook\Views\Field\details.cshtml"
               Write(Html.ActionLink("Back to List","Details", "Screen",new {@id=Model.ScreenId}, new {@class="btn btn-primary"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<TestNotebook.Models.Field> Html { get; private set; }
    }
}
#pragma warning restore 1591
