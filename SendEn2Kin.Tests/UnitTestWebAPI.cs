using NUnit.Framework;
using SendEn2Kin.WebAPI;
using SendEn2Kin.WebAPI.App_Start;
using SendEn2Kin.WebAPI.Controllers;
using SendEn2Kin.WebAPI.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;

namespace SendEn2Kin.Tests
{
	[TestFixture]
	public class UnitTestWebAPI
	{
		private EvernoteFacade evernoteFacade { get; set; }

		public UnitTestWebAPI()
		{
			evernoteFacade = EvernoteFacade.Instance();
		}

		[SetUp]
		public void Init()
		{
			AutoMapperConfiguration.Configure();
		}

		// GET api/notebooks
		[Test]
		public void TestCountNotebooks()
		{
			var controller = new NotebooksController();

			var result = controller.Get() as List<NotebookDTO>;

			Assert.AreEqual(2, result.Count());
		}

		// GET api/notebooks
		[Test]
		public void TestFirstNotebook()
		{
			var testNotebooks = new List<NotebookDTO>
			{
				new NotebookDTO
				{
					Guid = "a28c4083-5a13-468a-b7af-54861db836fc"
				}
			};

			var controller = new NotebooksController();

			var result = controller.Get() as List<NotebookDTO>;

			Assert.AreEqual(testNotebooks.FirstOrDefault().Guid, result.FirstOrDefault().Guid);
		}

		// GET api/notes
		[Test]
		public void TestCountNotes()
		{
			var controller = new NotesController();

			var result = controller.Get() as List<NoteDTO>;

			Assert.AreEqual(1, result.Count());
		}

		// GET api/notes
		[Test]
		public void TestFirstNote()
		{
			var controller = new NotesController();

			var result = controller.Get() as List<NoteDTO>;

			Assert.AreEqual("b664a99b-805c-441f-ae38-6bacff4adcd7", result.FirstOrDefault().Guid);
		}

		// GET api/notes/send
		//[Test]
		public void TestSend()
		{
			var controller = new NotesController();

			var result = controller.Send(new MailMessage { Subject = "teste" }) as bool?;

			Assert.AreEqual(true, result.Value);
		}

		// GET api/notes/5/send
		//[Test]
		public void TestSendNote()
		{
			var controller = new NotesController();

			var noteId = (controller.Get() as List<NoteDTO>).FirstOrDefault().Guid.ToString();

			var result = controller.SendNote(noteId) as bool?;

			Assert.AreEqual(true, result.Value);
		}

		// GET api/notes/5/send2
		//[Test]
		public void TestSendNote2()
		{
			var controller = new NotesController();

			var noteId = (controller.Get() as List<NoteDTO>).FirstOrDefault().Guid.ToString();

			var result = controller.SendNote2(noteId) as bool?;

			Assert.AreEqual(true, result.Value);
		}

		// GET api/notes/5/send3
		//[Test]
		public void TestSendNote3()
		{
			var controller = new NotesController();

			var noteId = (controller.Get() as List<NoteDTO>).FirstOrDefault().Guid.ToString();

			var result = controller.SendNote3(noteId) as bool?;

			Assert.AreEqual(true, result.Value);
		}
	}
}
