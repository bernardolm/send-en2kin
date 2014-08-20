using Evernote.EDAM.Type;
using HtmlAgilityPack;
using System;
using System.Text.RegularExpressions;

namespace SendEn2Kin.WebAPI
{
	public static class ENML2HTMLParser
	{
		public static string DoWork(Note note)
		{
			/*
			 * <en-media width="640" height="480" type="image/jpeg" hash="f03c1c2d96bc67eda02968c8b5af9008"/>
			 * Html Agility Pack
			 * Regex.Replace("<div>your html in here</div>",@"<(.|\n)*?>",string.Empty);
			 * String imgUrl = "data:" + resource.getMime() + ";base64," + java.util.prefs.Base64.byteArrayToBase64(resource.getData().getBody());
			 * https://github.com/evernote/evernote-sdk-ios/blob/9c616ddd2cd7c9ea8ff034cc23e7356859f3fcde/evernote-sdk-ios/Utilities/ENMLUtility.m
			 * https://dev.evernote.com/doc/articles/enml.php
			 */

			string result = note.Content;


			var document = new HtmlDocument();
			document.LoadHtml(result);

			var enNote = document.DocumentNode.SelectSingleNode("//en-note");

			var htmlBody = HtmlNode.CreateNode("<html><body>");

			var b = document.DocumentNode.ReplaceChild(htmlBody, enNote);







			var groups = Regex.Matches(result, "<(.|\n)*?>");

			foreach (var item in groups)
			{
				switch (item.ToString().Substring(0, Math.Min(10, item.ToString().Length)))
				{
					case "<en-note>":
						result = result.Replace(item.ToString(), "<html><body>");
						break;
					case "</en-note>":
						result = result.Replace(item.ToString(), "</body></html>");
						break;
					case "<en-crypt":
						result = result.Replace(item.ToString(), "<pre");
						break;
					default:
						break;
				}
			}

			//var result = note.Content.Replace("<en-note", "<html><body").Replace("</en-note>", "</body></html>");

			//result = result.Replace(@"<en-media(.|\n)*?>", "<img />");

			return result;
		}
	}
}