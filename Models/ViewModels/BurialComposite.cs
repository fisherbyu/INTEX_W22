﻿using System;
using BYU_EGYPT_INTEX.Models;

namespace BYU_EGYPT_INTEX.Models.ViewModels
{
	public class BurialComposite
	{
		public Burialmain burial { get; set; }
        public List<Textile> textiles { get; set; }
        public List<Color> colors { get; set; }
        public string? BurialId { get; set; }

        public BurialComposite(Burialmain burialIn, List<Textile> textileList, List<Color> colorList)
		{
			burial = burialIn;
			textiles = textileList;
			colors = colorList;
            BurialId = burial.Squarenorthsouth + burial.Northsouth + burial.Squareeastwest + burial.Eastwest + burial.Area + burial.Burialnumber;
        }
    }
}
