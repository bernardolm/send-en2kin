using AutoMapper;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using System.Collections.Generic;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Web.Http;
using SendEn2Kin.WebAPI.DTO;

namespace SendEn2Kin.WebAPI.Controllers
{
    public class NotesController : ApiController
    {
        private EvernoteFacade evernoteFacade;

		public NotesController()
		{
			evernoteFacade = EvernoteFacade.Instance();
		}
		
		// GET api/notes
		public List<NoteDTO> Get()
        {
			var notes = evernoteFacade.NoteStore.findNotes(evernoteFacade.AuthToken, new NoteFilter(), 0, 99);
			var notesDTO = Mapper.Map<List<Note>, List<NoteDTO>>(notes.Notes);
			return notesDTO;
        }

        // GET api/note/5
        public string Get(int id)
        {
            return "value";
        }

		// GET api/notes/5/send
		[Route("api/notes/{noteId}/send")]
		[HttpGet]
		public bool SendNote(string noteId)
		{
			var note = evernoteFacade.NoteStore.getNote(evernoteFacade.AuthToken, noteId, false, false, false, false); 
			var noteDTO = Mapper.Map<Note, NoteDTO>(note);
			var mailMessage = new MailMessage
			{
				Body = noteDTO.Title,
				Subject = noteDTO.Title
			};
			return Send(mailMessage);
		}

		// GET api/notes/5/send2
		[Route("api/notes/{noteId}/send2")]
		[HttpGet]
		public bool SendNote2(string noteId)
		{
			try
			{
				evernoteFacade.NoteStore.emailNote(evernoteFacade.AuthToken,
					new NoteEmailParameters
					{
						Guid = noteId,
						ToAddresses = new List<string>{ "bernardo.lou@kindle.com" }
					});

				return true;
			}
			catch
			{
				return false;
			}
		}

		// GET api/note/5/send3
		[Route("api/notes/{noteId}/send3")]
		[HttpGet]
		public bool SendNote3(string noteId)
		{
			var note = evernoteFacade.NoteStore.getNote(evernoteFacade.AuthToken, noteId, true, true, true, true);

			var mailMessage = new MailMessage
			{
				Body = evernoteFacade.ENMLtoHTML(note.Content),
				BodyEncoding = new UTF8Encoding(true),
				//IsBodyHtml = true,
				Subject = note.Title
			};

			var attachment = Attachment.CreateAttachmentFromString(
				evernoteFacade.ENMLtoHTML(note.Content), 
				note.Title + ".htm", 
				new UTF8Encoding(true), 
				"text/html");

			mailMessage.Attachments.Add(attachment);

			return Send(mailMessage);
		}

		// GET api/notes/send
		[Route("api/notes/send")]
		[HttpGet]
		public bool Send(MailMessage mailMessage)
		{
			try
			{
				mailMessage.To.Add("bernardo.loureiro@qx3.com.br");
				//mailMessage.CC.Add("bernardo.lou@kindle.com");
				var smtpClient = new SmtpClient();
				smtpClient.Send(mailMessage);
				return true;
			}
			catch
			{
				return false;
			}
		}

        // POST api/notes
        public void Post([FromBody]string value)
        {
        }

        // PUT api/notes/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/notes/5
        public void Delete(int id)
        {
        }
    }
}
