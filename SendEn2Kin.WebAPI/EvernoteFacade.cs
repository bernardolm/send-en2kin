using Evernote.EDAM.NoteStore;
using Evernote.EDAM.UserStore;
using System;
using System.Configuration;
using Thrift.Protocol;
using Thrift.Transport;

namespace SendEn2Kin.WebAPI
{
	public sealed class EvernoteFacade
	{
		private static EvernoteFacade instance;

		protected EvernoteFacade()
		{
			var userStoreUrl = new Uri(string.Format("https://{0}/edam/user", EvernoteHost));
			TTransport userStoreTransport = new THttpClient(userStoreUrl);
			TProtocol userStoreProtocol = new TBinaryProtocol(userStoreTransport);
			UserStore = new UserStore.Client(userStoreProtocol);

			string noteStoreUrl = UserStore.getNoteStoreUrl(AuthToken);

			TTransport noteStoreTransport = new THttpClient(new Uri(noteStoreUrl));
			TProtocol noteStoreProtocol = new TBinaryProtocol(noteStoreTransport);
			NoteStore = new NoteStore.Client(noteStoreProtocol);
		}

		public static EvernoteFacade Instance()
		{
			// Use 'Lazy initialization' 
			if (instance == null)
			{
				instance = new EvernoteFacade();
			}

			return instance;
		}

		public string AuthToken
		{
			get
			{
				try
				{
					return ConfigurationManager.AppSettings["AuthToken"];
				}
				catch
				{
					throw new Exception("AuthToken do úsuário ausente");
				}
			}
		}

		private string EvernoteHost
		{
			get
			{
				try
				{
					return ConfigurationManager.AppSettings["evernoteHost"];
				}
				catch
				{
					throw new Exception("EvernoteHost do úsuário ausente");
				}
			}
		}

		public UserStore.Client UserStore { get; set; }

		public NoteStore.Client NoteStore { get; set; }

		public string ENMLtoHTML(string enml)
		{
			/*
			 * <en-media width="640" height="480" type="image/jpeg" hash="f03c1c2d96bc67eda02968c8b5af9008"/>
			 * Html Agility Pack
			 * Regex.Replace("<div>your html in here</div>",@"<(.|\n)*?>",string.Empty);
			 * String imgUrl = "data:" + resource.getMime() + ";base64," + java.util.prefs.Base64.byteArrayToBase64(resource.getData().getBody());
			 * https://github.com/evernote/evernote-sdk-ios/blob/9c616ddd2cd7c9ea8ff034cc23e7356859f3fcde/evernote-sdk-ios/Utilities/ENMLUtility.m
			 */

			return enml.Replace("<en-note", "<html><body").Replace("</en-note>", "</body></html>");
		}
	}
}
