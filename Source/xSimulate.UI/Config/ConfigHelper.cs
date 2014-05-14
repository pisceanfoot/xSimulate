using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace xSimulate.UI.Config
{
    public class ConfigHelper
    {
        private static string Read(string file, params string[] arg)
        {
            string content = File.ReadAllText(Path.Combine("Setting", file));

            if (arg.Length > 0)
                return string.Format(content, arg);
            else
                return content;
        }

        private static void HomePage_Browser(StringBuilder builder, FileGenerator fileGenerator)
        {
            string file = string.Empty;
            if (fileGenerator.Browser == "Tmall")
            {
                file = ConfigHelper.Read("HomePage_Browser.txt", "http://www.tmall.com/");
            }
            else if (fileGenerator.Browser == "Taobao")
            {
                file = ConfigHelper.Read("HomePage_Browser.txt", "http://www.taobao.com/");
            }

            builder.AppendLine(file);
        }

        private static void HomePage_Attribute(StringBuilder builder, FileGenerator fileGenerator)
        {
            string file = string.Empty;
            if (string.IsNullOrEmpty(fileGenerator.Keyword))
            {
                throw new ConfigValidationException("keyword");
            }

            file = Read("HomePage_Attribute.txt", fileGenerator.Keyword);

            builder.AppendLine(file);
        }

        private static void SearchPage_Sort(StringBuilder builder, FileGenerator fileGenerator)
        {
            if (!string.IsNullOrEmpty(fileGenerator.SearchPageBrowserType))
            {
                if (fileGenerator.SearchPageBrowserType != "默认")
                {
                    builder.AppendLine(Read("HomePage_Browser.txt", fileGenerator.SearchPageBrowserType));
                }
            }
        }

        private static void SearchPage_PriceFilter(StringBuilder builder, FileGenerator fileGenerator)
        {
            if (string.IsNullOrEmpty(fileGenerator.PriceFrom) && string.IsNullOrEmpty(fileGenerator.PriceTo))
            {
                return;
            }

            string from = string.Empty;
            string to = string.Empty;

            if (!string.IsNullOrEmpty(fileGenerator.PriceFrom))
            {
                from = Read("SearchPage_PriceFrom.txt", fileGenerator.PriceFrom);
            }
            if (!string.IsNullOrEmpty(fileGenerator.PriceTo))
            {
                to = Read("SearchPage_PriceTO.txt", fileGenerator.PriceTo);
            }

            builder.AppendLine(Read("SearchPage_PriceFilter.txt", from, to));
        }

        private static void SearchPage_PageFind(StringBuilder builder, FileGenerator fileGenerator)
        {
            if (string.IsNullOrEmpty(fileGenerator.FindItemID) && string.IsNullOrEmpty(fileGenerator.FindWangWang))
            {
                throw new ConfigValidationException("FindItemID/FindWangWang");
            }

            string find = string.Empty;
            if (!string.IsNullOrEmpty(fileGenerator.FindItemID))
            {
                find = Read("SearchPage_FindItemID.txt", fileGenerator.FindItemID);
            }
            if (!string.IsNullOrEmpty(fileGenerator.FindWangWang))
            {
                find = Read("SearchPage_FindWangWang.txt", fileGenerator.FindWangWang);
            }

            int maxPage;
            if (!int.TryParse(fileGenerator.MaxPage, out maxPage))
            {
                maxPage = 10;
            }

            builder.AppendLine(Read("SearchPage_PageFind.txt", maxPage.ToString(), find));
        }

        private static void ProductDetailPage_Browser(StringBuilder builder, FileGenerator fileGenerator)
        {
            int times;
            if (int.TryParse(fileGenerator.ItemBrowserTime, out times))
            {
                times = 0;
            }

            builder.AppendLine(Read("Detail_Product.txt"));
        }

        public static string Create(FileGenerator fileGenerator)
        {
            return Tmall(fileGenerator);
        }

        private static string Tmall(FileGenerator fileGenerator)
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Read("Header.txt"));

            builder.AppendLine(Read("Step_Begin.txt", "HomePage"));
            HomePage_Browser(builder, fileGenerator);
            HomePage_Attribute(builder, fileGenerator);
            builder.AppendLine(Read("Step_End.txt"));

            builder.AppendLine(Read("Step_Begin.txt", "SearchPage"));
            SearchPage_Sort(builder, fileGenerator);
            SearchPage_PriceFilter(builder, fileGenerator);
            SearchPage_PageFind(builder, fileGenerator);
            builder.AppendLine(Read("Step_End.txt"));


            builder.AppendLine(Read("Step_Begin.txt", "DetailPage"));
            ProductDetailPage_Browser(builder, fileGenerator);
            builder.AppendLine(Read("Step_End.txt"));

            builder.AppendLine(Read("Footer.txt"));
            return builder.ToString();
        }
    }
}
 