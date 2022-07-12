
using System;
using AngleSharp.Html.Parser;
using Task_1.Interfaces;

namespace Task_1.BakeevoParse
{
    internal class ParserWorker<T> where T : class
    {
        IParser<T> parser;
        HtmlLoader loader;

        public IParser<T> Parser
        { 
            get { return parser; }
            set { parser = value; }
        }

        public event Action<object, T> OnCompleted;

        public ParserWorker(IParser<T> parser, string url)
        {
            loader = new HtmlLoader(url);
            this.parser = parser;
        }

        public async void Worker()
        {
            var sourse = await loader.GetSourse();
            var domParser = new HtmlParser();
            var document = await domParser.ParseDocumentAsync(sourse);
            var result = parser.Parse(document);

            OnCompleted?.Invoke(this, result);
        }
    }
}
