using System;
using System.Text;
using System.Web.Mvc;
using System.Web;
using System.Xml.Linq;
using System.Xml;

using System.Collections.Generic;
using System.Linq;
using ShoppingCart.Data.Concrete;
using ShoppingCart.Data.Entity;
using ShoppingCart.Service.ViewModel;

namespace ShoppingCart.UI.Helpers
{
    #region Htmlhelpers
    public static class HtmlHelpers
    {
       
        public static MvcHtmlString PageLinks(this HtmlHelper html, PagingInfo pagingInfo, Func<int, string> pageUrl)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 1; i <= pagingInfo.TotalPages; i++)
            {
                TagBuilder tag = new TagBuilder("a"); // Construct an <a> tag
                tag.MergeAttribute("href", pageUrl(i));
                tag.InnerHtml = i.ToString();

                if (i == pagingInfo.CurrentPage)
                    tag.AddCssClass("selected");
                result.Append(tag.ToString());
            }
            return MvcHtmlString.Create(result.ToString());
        }

        public static string GetLanguageCode(this HtmlHelper html)
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["Language"] == null || context.Session["Language"] as string == "tr")
            {
                return "tr";
            }
            else 
            {
                return "en";
            }
        }

        public static MvcHtmlString GetProductVariantAndValueFromXml(this HtmlHelper html, string xml)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            StringBuilder sb = new StringBuilder();
            HttpContext context = HttpContext.Current;
            string languageCode = "";
            if (context.Session["Language"] == null || context.Session["Language"] as string == "tr")
            {
                languageCode = "tr";
            }
            else
            {
                languageCode = "en";
            }
            ApplicationDbContext dbContext = new ApplicationDbContext();
            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.LoadXml(xml);
            XmlNodeList productVariants = xmlDocument.SelectNodes("/Variants/ProductVariant");
            foreach (XmlNode item in productVariants)
            {
                 int productVariantID = int.Parse(item.Attributes["ID"].Value);
                 int productVariantValueID = int.Parse(item.FirstChild.FirstChild.InnerText);
                 result.Add(dbContext.ProductVariantTranslations.FirstOrDefault(x => x.Language.LanguageCode == languageCode && x.ProductVariantID == productVariantID).ProductVariantName
                     , dbContext.ProductVariantValueTranslations.FirstOrDefault(x => x.Language.LanguageCode == languageCode && x.ProductVariantValueID == productVariantValueID).ProductVariantValueName);
            }
            sb.AppendLine("<span>");
            foreach (var item in result)
            {
                sb.AppendLine("("+item.Key + ":" + item.Value +")");
            }
            sb.AppendLine("</span>");
            return new MvcHtmlString(sb.ToString());
            
        }

        public static MvcHtmlString GetCategoryBreadCrumb(this HtmlHelper html, string categoryURLText, Func<string, string> categoryURL)
        { 
            HttpContext context = HttpContext.Current;
            ApplicationDbContext dbContext = new ApplicationDbContext();
            Category currentCategory = dbContext.Categories.SingleOrDefault(x => x.CategoryURLText == categoryURLText);
            if(currentCategory == null)
            {
                return null;
            }
            else
            {
                Stack<Category> parentCategoryList = new Stack<Category>();
                
                int i = 0;
                while(i<5)
                {
                    if(currentCategory == null)
                    {
                        break;
                    }
                    else
                    {
                        parentCategoryList.Push(currentCategory);
                        currentCategory = currentCategory.Parent;
                    }
                    i++;
                }
                StringBuilder sb= new StringBuilder();
                TagBuilder olTag = new TagBuilder("ol");
                olTag.AddCssClass("breadcrumb");

                foreach (var category in parentCategoryList)
	            {
		            TagBuilder liTag = new TagBuilder("li");
                    TagBuilder aTag = new TagBuilder("a");
                    aTag.MergeAttribute("href",categoryURL(category.CategoryURLText));
                    aTag.InnerHtml = category.CategoryTranslations.SingleOrDefault(x=> x.Language.LanguageCode == CodeHelpers.GetLanguageCode()).CategoryName;
                    liTag.InnerHtml = aTag.ToString();
                    sb.Append(liTag.ToString());
	            }
                olTag.InnerHtml =sb.ToString();
                return MvcHtmlString.Create(olTag.ToString());
			}
         }
     
    }

        #endregion
       
    

    public static class CodeHelpers
    {
        public static string GetLanguageCode()
        {
            HttpContext context = HttpContext.Current;
            if (context.Session["Language"] == null || context.Session["Language"] as string == "tr")
            {
                return "tr";
            }
            else
            {
                return "en";
            }
        }

        public static string CreateXmlForAttribute(int productVariantValueID)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            ProductVariantValue productVariantValue = context.ProductVariantValues.Where(x => x.ProductVariantValueID == productVariantValueID).FirstOrDefault();
            if (productVariantValue == null)
            {
                return null;
            }
            else
            {
                XDocument xml = new XDocument
                    (
                        new XElement("Variants" ,
                                new XElement("ProductVariant",
                                    new XAttribute("ID", productVariantValue.ProductVariantID),
                                    new XElement("ProductVariantValue",
                                            new XElement("Value", productVariantValue.ProductVariantValueID))
                    )));
                return xml.ToString();
            }
        }
    }

}