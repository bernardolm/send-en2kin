using AutoMapper;
using Evernote.EDAM.NoteStore;
using Evernote.EDAM.Type;
using System.Collections.Generic;
using System.Web.Http;
using SendEn2Kin.WebAPI.DTO;

namespace SendEn2Kin.WebAPI.Controllers
{
    public class NotebooksController : ApiController
    {
		private EvernoteFacade evernoteFacade { get; set; }

		public NotebooksController()
		{
			evernoteFacade = EvernoteFacade.Instance();
		}

        // GET api/notebooks
		public List<NotebookDTO> Get()
        {
			var notebooks = evernoteFacade.NoteStore.listNotebooks(evernoteFacade.AuthToken);
			var notebooksDTO = Mapper.Map<List<Notebook>, List<NotebookDTO>>(notebooks);
			return notebooksDTO;
        }

		// GET api/notebooks/1/notes
		[Route("api/notebooks/{notebookId}/notes")]
		[HttpGet]
		public List<NoteDTO> GetNotebookNotes(string notebookId)
		{
			var notes = evernoteFacade.NoteStore.findNotes(evernoteFacade.AuthToken,
				new NoteFilter { NotebookGuid = notebookId }, 0, 99); 
			var notesDTO = Mapper.Map<List<Note>, List<NoteDTO>>(notes.Notes);
			return notesDTO;
		}

        // GET api/notebooks/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/notebooks
        public void Post([FromBody]string value)
        {
        }

        // PUT api/notebooks/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/notebooks/5
        public void Delete(int id)
        {
        }
    }
}
