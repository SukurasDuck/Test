#pragma checksum "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\Image\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "e7ee2524a363638935311a8dc09b0653b2a8fc40"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Image_Index), @"mvc.1.0.view", @"/Views/Image/Index.cshtml")]
[assembly:global::Microsoft.AspNetCore.Mvc.Razor.Compilation.RazorViewAttribute(@"/Views/Image/Index.cshtml", typeof(AspNetCore.Views_Image_Index))]
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
#line 1 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\_ViewImports.cshtml"
using ImageManagement;

#line default
#line hidden
#line 2 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\_ViewImports.cshtml"
using ImageManagement.Models;

#line default
#line hidden
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"e7ee2524a363638935311a8dc09b0653b2a8fc40", @"/Views/Image/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"fe1970a9a2145a3e96ef8b89fc625d05e958c7ef", @"/Views/_ViewImports.cshtml")]
    public class Views_Image_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<ImageManagement.Models.ApiImgResultModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#line 2 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\Image\Index.cshtml"
  
    ViewData["Title"] = "Index";

#line default
#line hidden
            BeginContext(90, 23, true);
            WriteLiteral("\r\n<h2>上传图片调用接口测试</h2>\r\n");
            EndContext();
#line 7 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\Image\Index.cshtml"
 using (Html.BeginForm(FormMethod.Post,new{ enctype = "multipart/form-data" }))
{

#line default
#line hidden
            BeginContext(197, 101, true);
            WriteLiteral("    <input type=\"file\" name=\"img\" multiple  value=\"点击上传识别\"/>\r\n    <input type=\"submit\" value=\"识别\"/>\r\n");
            EndContext();
#line 11 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\Image\Index.cshtml"
}

#line default
#line hidden
            BeginContext(301, 7, true);
            WriteLiteral("<div>\r\n");
            EndContext();
#line 13 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\Image\Index.cshtml"
     foreach (var item in this.Model.words_result)
    {

#line default
#line hidden
            BeginContext(367, 15, true);
            WriteLiteral("        <label>");
            EndContext();
            BeginContext(383, 4, false);
#line 15 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\Image\Index.cshtml"
          Write(item);

#line default
#line hidden
            EndContext();
            BeginContext(387, 10, true);
            WriteLiteral("</label>\r\n");
            EndContext();
#line 16 "C:\Users\PC012\Desktop\NewTest\ImageManagement\ImageManagement\Views\Image\Index.cshtml"
    }

#line default
#line hidden
            BeginContext(404, 10, true);
            WriteLiteral("</div>\r\n\r\n");
            EndContext();
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<ImageManagement.Models.ApiImgResultModel> Html { get; private set; }
    }
}
#pragma warning restore 1591
