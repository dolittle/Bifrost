using System;
using Bifrost.Rules;
using Machine.Specifications;

namespace Bifrost.Specs.Rules.given
{
    public class rules_and_coloured_shapes : coloured_shapes
    {
        protected static Rule<ColouredShape> red;
        protected static Rule<ColouredShape> blue;
        protected static Rule<ColouredShape> green;
        protected static Rule<ColouredShape> yellow;
        protected static Rule<ColouredShape> circles;
        protected static Rule<ColouredShape> squares;
        protected static Rule<ColouredShape> triangles;
        protected static Rule<ColouredShape> pentagons;

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

    public class ColourRule : Rule<ColouredShape>
    {
        readonly string _colour;

        public ColourRule(string matchingColour)
        {
            _colour = matchingColour;
            Predicate = shape => shape.Colour == _colour;
        }
    }

    public class ShapeRule : Rule<ColouredShape>
    {
        readonly string _shape;

        public ShapeRule(string matchingShape)
        {
            _shape = matchingShape;
            Predicate = shape => shape.Shape == _shape;
        }
    }
}