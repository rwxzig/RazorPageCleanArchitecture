// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CleanArchitecture.Razor.Application.Common.Interfaces;
using Microsoft.AspNetCore.Razor.Runtime.TagHelpers;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SmartAdmin.WebUI.Extensions
{
    // You may need to install the Microsoft.AspNetCore.Razor.Runtime package into your project
    [HtmlTargetElement("select", Attributes = DICTIONARIESNAME)]
    public class DictionaryTagHelper : TagHelper
    {
        private const string DICTIONARIESNAME = "asp-dictionaries-for";

        [HtmlAttributeName(DICTIONARIESNAME)]
        public string Dictionaries { get; set; }
        private readonly IDictionaryService _dictionaryService;

        public DictionaryTagHelper(
            IDictionaryService dictionaryService
            )
        {
            _dictionaryService = dictionaryService;
        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
        
            if (!string.IsNullOrEmpty(Dictionaries))
            {
                string name = Dictionaries;
                var items = await _dictionaryService.Fetch(name);
                if (items != null)
                {
                    foreach (var key in items)
                    {
                        output.PostContent.AppendHtml($"<option value=\"{key.Key}\">{key.Value}</option>");
                    }
                }
            }
        }
    }
}