using System;
using Bifrost.Specifications;
using Machine.Specifications;

namespace Bifrost.Specs.Specifications.given
{
    public class rules_and_coloured_shapes : coloured_shapes
    {
        protected static Specification<ColouredShape> red;
        protected static Specification<ColouredShape> blue;
        protected static Specification<ColouredShape> green;
        protected static Specification<ColouredShape> yellow;
        protected static Specification<ColouredShape> circles;
        protected static Specification<ColouredShape> squares;
        protected static Specification<ColouredShape> triangles;
        protected static Specification<ColouredShape> pentagons;

        Establish context = () =>
            {
                red = new ColourRule("Red");
                blue = new ColourRule("Blue");
                green = new ColourRule("Green");
                yellow = new ColourRule("Yellow");
                circles = new ShapeRule("Circle");
                squares = new ShapeRule("Square");
                triangles = new ShapeRule("Triangle");
                pentagons = new ShapeRule("Pentagon");
            };
    }

    public class ColourRule : Specification<ColouredShape>
    {
        readonly string _colour;

        public ColourRule(string matchingColour)
        {
            _colour = matchingColour;
            Predicate = shape => shape.Colour == _colour;
        }
    }

    public class ShapeRule : Specification<ColouredShape>
    {
        readonly string _shape;

        public ShapeRule(string matchingShape)
        {
            _shape = matchingShape;
            Predicate = shape => shape.Shape == _shape;
        }
    }
}