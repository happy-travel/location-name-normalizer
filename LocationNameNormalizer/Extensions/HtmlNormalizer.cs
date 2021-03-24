using System.Collections.Generic;
using System.Linq;
using HtmlAgilityPack;

namespace HappyTravel.LocationNameNormalizer.Extensions
{
    public static class HtmlNormalizer
    {
        /*
        1. Allowed tags
        p, b, i, ul, li, h1, br
        2. Other tags are prohibited
        Prohibited tag should be replaced with a space
        3. Replacing tags:
        strong -> b
        em -> i
        ol -> ul
        h2, h3, h4, h5, h6 -> h1
        </div>  -> <br>
        4. h1 should not be inside other tags. If so it should be replaces with <b>text</b><br>. h1 tag should not contain other tags
        5. <br> tag
        a. Multiple <br> tags are prohibited/
        After <h1> <ul> <p> <br> cannot go
        b. <br/> <br></br> tags should have one standard writing*/
        public static string NormalizeInlineHtml(this string target)
        {
            HtmlDocument.DisableBehaviorTagP = false;
            var htmlDocument = new HtmlDocument();
            htmlDocument.OptionAutoCloseOnEnd = true;
            htmlDocument.LoadHtml(target);

            htmlDocument.RemoveAttributes()
                .RemoveEmptyTags()
                .ReplaceDivTagsWithBr()
                .RemoveNotNeededTags()
                .ReplaceTags()
                .ProcessH1Tags()
                .RemoveNotNeededBrTags();

            return htmlDocument.DocumentNode.OuterHtml;
        }


        private static HtmlDocument RemoveAttributes(this HtmlDocument htmlDocument)
        {
            foreach (var node in htmlDocument.DocumentNode.Descendants())
                node.Attributes.Remove();

            return htmlDocument;
        }


        private static HtmlDocument ReplaceTags(this HtmlDocument htmlDocument)
        {
            var nodesToReplace = new List<HtmlNode>();

            foreach (var node in htmlDocument.DocumentNode.Descendants())
                if (TagsToReplace.Keys.Contains(node.Name))
                    nodesToReplace.Add(node);

            foreach (var node in nodesToReplace)
            {
                var nodeToReplace = htmlDocument.CreateElement(TagsToReplace[node.Name]);
                nodeToReplace.InnerHtml = node.InnerHtml;
                node.ParentNode.ReplaceChild(nodeToReplace, node);
            }

            return htmlDocument;
        }


        private static HtmlDocument RemoveEmptyTags(this HtmlDocument htmlDocument)
        {
            var nodesToRemove = htmlDocument.DocumentNode.Descendants()
                .Where(n => !HtmlNode.IsClosedElement(n.Name)
                    && n.NodeType == HtmlNodeType.Element
                    && string.IsNullOrWhiteSpace(n.InnerHtml)).ToList();

            foreach (var node in nodesToRemove)
                node.ParentNode.RemoveChild(node, false);

            return htmlDocument;
        }


        private static HtmlDocument ReplaceDivTagsWithBr(this HtmlDocument htmlDocument)
        {
            var divNodes = htmlDocument.DocumentNode.SelectNodes("//div");
            if (divNodes == null)
                return htmlDocument;

            foreach (var node in divNodes)
            {
                var brNode = htmlDocument.CreateElement("br");
                node.PrependChild(brNode);
                node.AppendChild(brNode);
                node.ParentNode.RemoveChild(node, true);
            }

            return htmlDocument;
        }


        private static HtmlDocument RemoveNotNeededTags(this HtmlDocument htmlDocument)
        {
            var nodesToRemove = new List<HtmlNode>();

            foreach (var node in htmlDocument.DocumentNode.Descendants())
                if (!AcceptableTags.Contains(node.Name) && !TagsToReplace.Keys.Contains(node.Name))
                    nodesToRemove.Add(node);

            foreach (var node in nodesToRemove)
                node.ParentNode.RemoveChild(node, true);

            return htmlDocument;
        }


        private static HtmlDocument ProcessH1Tags(this HtmlDocument htmlDocument)
        {
            var rootH1Nodes = htmlDocument.DocumentNode.ChildNodes.Where(ch => ch.Name == "h1").ToList();

            if (rootH1Nodes.Any())
            {
                var nodesToRemove = new List<HtmlNode>();
                foreach (var node in rootH1Nodes)
                    nodesToRemove.AddRange(node
                        .Descendants()
                        .Where(n => n.NodeType != HtmlNodeType.Text));

                foreach (var node in nodesToRemove)
                    node.ParentNode.RemoveChild(node, true);
            }

            var nodesToReplace = htmlDocument.DocumentNode.Descendants()
                .Where(d => d.Name == "h1" && d.ParentNode.NodeType != HtmlNodeType.Document)
                .ToList();

            foreach (var node in nodesToReplace)
            {
                var brNode = htmlDocument.CreateElement("br");
                node.ParentNode.InsertAfter(brNode, node);
                var nodeToReplace = htmlDocument.CreateElement("b");
                nodeToReplace.InnerHtml = node.InnerHtml;
                node.ParentNode.ReplaceChild(nodeToReplace, node);
            }

            return htmlDocument;
        }


        private static HtmlDocument RemoveNotNeededBrTags(this HtmlDocument htmlDocument)
        {
            var siblingBrNodes = htmlDocument.DocumentNode.Descendants()
                .Where(n => n.Name == "br" && n.NextSibling?.Name == "br")
                .ToList();

            foreach (var node in siblingBrNodes)
                node.Remove();

            var nodesToRemove = new List<HtmlNode>();
            foreach (var node in htmlDocument.DocumentNode.Descendants().Where(d => TagsNotCompatibleWithBr.Contains(d.Name)))
            {
                if (node.PreviousSibling?.Name == "br")
                    nodesToRemove.Add(node.PreviousSibling);

                if (node.NextSibling?.Name == "br")
                    nodesToRemove.Add(node.NextSibling);

                if (node.FirstChild?.Name == "br")
                    nodesToRemove.Add(node.FirstChild);

                if (node.LastChild?.Name == "br")
                    nodesToRemove.Add(node.LastChild);
            }

            foreach (var node in nodesToRemove)
                node.Remove();

            return htmlDocument;
        }


        private static readonly string[] AcceptableTags = {"p", "b", "i", "ul", "li", "h1", "br", "#text"};
        private static readonly string[] TagsNotCompatibleWithBr = {"ul", "h1", "p"};
        private static readonly Dictionary<string, string> TagsToReplace = new Dictionary<string, string>
        {
            {"strong", "b"}, {"em", "i"}, {"ol", "ul"}, {"h2", "h1"}, {"h3", "h1"}, {"h4", "h1"}, {"h5", "h1"}, {"h6", "h1"}
        };
    }
}