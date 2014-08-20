using Evernote.EDAM.NoteStore;
using Evernote.EDAM.UserStore;
using System;
using System.Configuration;
using Thrift.Protocol;
using Thrift.Transport;

namespace SendEn2Kin.WebAPI
{
	/// <summary>
	/// Connector facade to Evernote API
	/// </summary>
	public sealed class EvernoteFacade
	{
		private static EvernoteFacade instance;

		private EvernoteFacade()
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
	}
}
