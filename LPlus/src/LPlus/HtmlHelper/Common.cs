using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Model.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Encodings.Web;

namespace LPlus.HtmlHelper
{
    public static class Common
    {
        public static HtmlString ContactList(this IHtmlHelper htmlHelper, List<UserModel> users)
        {
            TagBuilder divTag = new TagBuilder("div");
            TagBuilder ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("media-list media-list-contacts");
            if(users!=null&& users.Count>0)
            {
                foreach (var user in users)
                {
                    ulTag.InnerHtml.AppendHtml(GetTagLI(user));
                }
            }
            else
            {
                ulTag.InnerHtml.SetContent("单身狗");
            }
            var writer = new StringWriter();
            divTag.InnerHtml.AppendHtml(ulTag);
            divTag.InnerHtml.WriteTo(writer, HtmlEncoder.Default);
            return new HtmlString(writer.ToString());
        }
        private static TagBuilder GetTagLI(UserModel user)//, int companyId
        {
            TagBuilder LiTag = new TagBuilder("li");
            LiTag.AddCssClass("media");
            TagBuilder aTag = new TagBuilder("a");
            TagBuilder divLeftTag = new TagBuilder("div");
            divLeftTag.AddCssClass("media-left");
            TagBuilder imageTag = new TagBuilder("img");
            imageTag.MergeAttribute("src",user.Pictrue);
            imageTag.MergeAttribute("style", "width: 40px;");
            imageTag.AddCssClass("media-object img-circle");
            //imageTag.Attributes.
            divLeftTag.InnerHtml.AppendHtml(imageTag);
            TagBuilder divBodyTag = new TagBuilder("div");
            divBodyTag.AddCssClass("media-body");
            TagBuilder hTag = new TagBuilder("h4");
            hTag.AddCssClass("media-heading");
            hTag.InnerHtml.SetContent(user.Name);
            TagBuilder spanTag = new TagBuilder("span");
            spanTag.InnerHtml.SetContent(user.Phone);
            TagBuilder iTag = new TagBuilder("i");
            iTag.AddCssClass("fa fa-phone");
            spanTag.InnerHtml.AppendHtml(iTag);
            divBodyTag.InnerHtml.AppendHtml(hTag);
            divBodyTag.InnerHtml.AppendHtml(spanTag);
            aTag.InnerHtml.AppendHtml(divLeftTag);
            aTag.InnerHtml.AppendHtml(divBodyTag);
            LiTag.InnerHtml.AppendHtml(aTag);
            return LiTag;
        }
    }
}
