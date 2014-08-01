using AutoMapper;
using Evernote.EDAM.Type;
using System.Collections.Generic;
using SendEn2Kin.WebAPI.DTO;

namespace SendEn2Kin.WebAPI.App_Start
{
	public class AutoMapperConfiguration
	{
		public static void Configure()
		{
			Mapper.CreateMap<Notebook, NotebookDTO>();
			Mapper.CreateMap<Note, NoteDTO>();
		}
	}
}