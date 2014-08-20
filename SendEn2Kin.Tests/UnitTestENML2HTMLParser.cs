using Evernote.EDAM.Type;
using NUnit.Framework;
using SendEn2Kin.WebAPI;
using SendEn2Kin.WebAPI.App_Start;

namespace SendEn2Kin.Tests
{
	[TestFixture]
	public class UnitTestENML2HTMLParser
	{
		private Note Note { get; set; }

		public UnitTestENML2HTMLParser()
		{
			Note = new Note();
			Note.Content = @"<en-note>
							<h1>Hello, world</h1>
							<en-media width=""640"" height=""480"" type=""image/jpeg"" hash=""f03c1c2d96bc67eda02968c8b5af9008""/>
							<en-crypt hint=""My Cat's Name"">NKLHX5yK1MlpzemJQijAN6C4545s2EODxQ8Bg1r==</en-crypt>
							<en-todo/>An item that I haven't completed yet.   
							<br/>  
							<en-todo checked=""true""/>An completed item.
							</en-note>";
		}

		[SetUp]
		public void Init()
		{
			AutoMapperConfiguration.Configure();
		}

		[Test]
		public void TestEnNote()
		{
			var result = ENML2HTMLParser.DoWork(Note);

			var expected = @"<html><body>
							<h1>Hello, world</h1>
							<en-media width=""640"" height=""480"" type=""image/jpeg"" hash=""f03c1c2d96bc67eda02968c8b5af9008""/>
							<en-crypt hint=""My Cat's Name"">NKLHX5yK1MlpzemJQijAN6C4545s2EODxQ8Bg1r==</en-crypt>
							<en-todo/>An item that I haven't completed yet.   
							<br/>  
							<en-todo checked=""true""/>An completed item.
							</body></html>";

			Assert.AreEqual(expected, result);
		}
	}
}
