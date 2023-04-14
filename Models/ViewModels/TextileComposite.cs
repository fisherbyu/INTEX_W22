using System;
namespace BYU_EGYPT_INTEX.Models.ViewModels
{
	public class TextileComposite
	{
		public Textile textile { get; set; }
        public List<Color> colors { get; set; }
        public List<Textilefunction> functions { get; set; }
		public List<Structure> structures { get; set; }
		public List<Analysis> analyses { get; set; }
		public List<Decoration> decorations { get; set; }
		public List<Dimension> dimensions { get; set; }
		public List<Yarnmanipulation> yarnmanipulations { get; set; }
        public List<Photodatum> photodata { get; set; }
		public string? colorString { get; set; }
        public string? functionString { get; set; }
        public string? structureString { get; set; }
        public TextileComposite(Textile textileIn, List<Color> colorsIn, List<Textilefunction> functionsIn, List<Structure> structuresIn, List<Analysis> analysesIn, List<Decoration> decorationsIn, List<Dimension> dimensionsIn, List<Yarnmanipulation> yarnmanipulationsIn, List<Photodatum> photodataIn)
		{
			textile = textileIn;
			colors = colorsIn;
			functions = functionsIn;
			structures = structuresIn;
			analyses = analysesIn;
			decorations = decorationsIn;
			dimensions = dimensionsIn;
            yarnmanipulations = yarnmanipulationsIn;
			photodata = photodataIn;


			string cString = "";
			//Get Strings:
			foreach (Color color in colors)
			{
                cString += color.Value + " ";

            }
			colorString = cString;
            foreach (Textilefunction function in functions)
            {
                this.functionString += function.Value + " ";

            }
            foreach (Structure structure in structures)
            {
                this.structureString += structure.Value + " ";

            }
        }
	}
}

