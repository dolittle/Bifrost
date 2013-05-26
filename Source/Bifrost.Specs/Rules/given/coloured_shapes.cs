using System.Linq;
using Bifrost.Concepts;
using Machine.Specifications;

namespace Bifrost.Specs.Rules.given
{
    public class coloured_shapes
    {
        protected static IQueryable<ColouredShape> my_coloured_shapes;
        protected static string[] colours;
        protected static string[] shapes;

        protected static ColouredShape red_square = new ColouredShape(){ Colour = "Red", Shape = "Square"};
        protected static ColouredShape red_circle = new ColouredShape(){ Colour = "Red", Shape = "Circle"};
        protected static ColouredShape green_circle = new ColouredShape(){ Colour = "Green", Shape = "Circle"};
        protected static ColouredShape green_square = new ColouredShape(){ Colour = "Green", Shape = "Square"};

        Establish context = () =>
            {
                colours = new string[] {"Red","Blue","Green","Yellow"};
                shapes = new string[]{"Square","Circle","Triangle","Pentagon"};

                my_coloured_shapes = BuildColouredShapes();
            };

        static IQueryable<ColouredShape> BuildColouredShapes()
        {
           return (from colour in colours
                    from shape in shapes
                    select new ColouredShape { Colour = colour, Shape = shape }).AsQueryable();
        }
    }

    public class ColouredShape : Value<ColouredShape>
    {
        public string Shape { get; set; }
        public string Colour { get; set; }

        public override string ToString()
        {
            return string.Format("{0}:{1}", Shape, Colour);
        }
    }
}