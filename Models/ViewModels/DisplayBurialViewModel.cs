using System;
namespace BYU_EGYPT_INTEX.Models.ViewModels
{
	public class DisplayBurialViewModel
	{
        public Burialmain burial { get; set; }
        public string? BurialId { get; set; }
        public List<TextileComposite>? textiles { get; set; }
        public List<Artifactkomaushimregister> artifacts { get; set; }
        public DisplayBurialViewModel(Burialmain burialIn)
		{
            burial = burialIn;
            BurialId = burial.Squarenorthsouth + burial.Northsouth + burial.Squareeastwest + burial.Eastwest + burial.Area + burial.Burialnumber;

        }
    }
}

