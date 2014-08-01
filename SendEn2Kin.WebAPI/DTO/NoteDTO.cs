using System;

namespace SendEn2Kin.WebAPI.DTO
{
	public class NoteDTO
	{
		public string Guid { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public byte[] ContentHash { get; set; }
		public int ContentLength { get; set; }
	}
}