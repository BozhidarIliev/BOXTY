#pragma checksum "C:\Users\Bozhidar\Desktop\BOXTY\Boxty\Areas\Administration\Views\Admins\EditRole.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c8619b96e82fbc86f18a23c1361b67ab4af32b1a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Areas_Administration_Views_Admins_EditRole), @"mvc.1.0.view", @"/Areas/Administration/Views/Admins/EditRole.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c8619b96e82fbc86f18a23c1361b67ab4af32b1a", @"/Areas/Administration/Views/Admins/EditRole.cshtml")]
    public class Areas_Administration_Views_Admins_EditRole : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Boxty.ViewModels.EditRoleViewModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Bozhidar\Desktop\BOXTY\Boxty\Areas\Administration\Views\Admins\EditRole.cshtml"
  
    ViewBag.Title = "Edit Role";

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
<h1>Edit Role</h1>

<form method=""post"" class=""mt-3"">
    <div class=""form-group row"">
        <label asp-for=""Id"" class=""col-sm-2 col-form-label""></label>
        <div class=""col-sm-10"">
            <input asp-for=""Id"" disabled class=""form-control"">
        </div>
    </div>
    <div class=""form-group row"">
        <label asp-for=""RoleName"" class=""col-sm-2 col-form-label""></label>
        <div class=""col-sm-10"">
            <input asp-for=""RoleName"" class=""form-control"">
            <span asp-validation-for=""RoleName"" class=""text-danger""></span>
        </div>
    </div>

    <div asp-validation-summary=""All"" class=""text-danger""></div>

    <div class=""form-group row"">
        <div class=""col-sm-10"">
            <button type=""submit"" class=""btn btn-primary"">Update</button>
            <a asp-action=""RolesList"" class=""btn btn-primary"">Cancel</a>
        </div>
    </div>
</form>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Boxty.ViewModels.EditRoleViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
