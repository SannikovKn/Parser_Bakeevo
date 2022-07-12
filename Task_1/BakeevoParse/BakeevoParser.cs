
using System;
using System.Linq;
using System.Collections.Generic;
using AngleSharp.Html.Dom;
using Task_1.Interfaces;

namespace Task_1.BakeevoParse
{
    internal class BakeevoParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            var dataList = new List<string>();
            var items = document.QuerySelectorAll("div").Where(item => item.ClassName != null && item.ClassName.Contains("flat-item row hidden-xs hidden-sm"));

            foreach (var item in items)
            {
                string flat_id = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("data-id"), 15).Where(i => char.IsNumber(i)));
                string flat_num = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("Квартира"), 15).Where(i => char.IsNumber(i)));
                string building = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("Дом"), 15).Where(i => char.IsNumber(i)));
                string floor = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("floor"), 7).Where(i => char.IsNumber(i)));
                string area = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("S -"), 10).Where(i => char.IsNumber(i) || i == '.' ));
                string rooms = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("комнатная") - 5, 5).Where(i => char.IsNumber(i)));
                string price = string.Concat(item.OuterHtml.Substring(item.OuterHtml.IndexOf("руб.") -11, 11).Where(i => char.IsNumber(i)));
                string status = "В продаже";
                string date = DateTime.Now.ToString("yyyy-MM-dd");
                
                string result = flat_id + "|" + flat_num + "|" + building + "|" + floor + "|" + area + "|" + rooms + "|" + price + "|" + SpriteCalculation(price,area) + "|" + status + "|" + date;
                dataList.Add(result);
            }

            return dataList.ToArray();
        }

        private double SpriteCalculation(string price, string area)
        {
            double sprice = 0;

            if (price.Length > 5 && area.Length > 0)
            {
                sprice = Math.Round(Convert.ToInt32(price) / Convert.ToDouble(area.Replace(".", ",")), 2);
            }

            return sprice;
        }
    }
}
