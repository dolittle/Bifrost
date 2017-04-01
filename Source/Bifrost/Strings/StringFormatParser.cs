/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Text.RegularExpressions;

namespace Bifrost.Strings
{
    /// <summary>
    /// Represents an implementation of <see cref="IStringFormatParser"/>
    /// </summary>
    public class StringFormatParser : IStringFormatParser
    {
        static Regex _separatorsExpression = new Regex(@"\[([\S]+)\]");
        static Regex _prefixesExpression = new Regex(@"([\-])*([\^])*");

        /// <inheritdoc/>
        public IStringFormat Parse(string format)
        {
            var separatorsMatch = _separatorsExpression.Match(format);
            ThrowIfMissingSeparators(format, separatorsMatch);
            format = format.Substring(separatorsMatch.Groups[0].Value.Length);
            var separators = separatorsMatch.Groups[1].Captures[0].Value.ToCharArray();
            IStringFormatBuilder builder = new StringFormatBuilder(separators);

            var stringSegments = format.Split(separators);
            foreach( var stringSegment in stringSegments )
            {
                var actualStringSegment = stringSegment;
                var optional = false;
                var dependingOnPrevious = false;
                var recurring = false;
                HandlePrefixes(ref actualStringSegment, out optional, out dependingOnPrevious);

                if (IsStringRecurring(actualStringSegment))
                {
                    actualStringSegment = actualStringSegment.Substring(0, actualStringSegment.Length - 1);
                    recurring = true;
                }

                if (IsVariableSegment(actualStringSegment))
                {
                    actualStringSegment = actualStringSegment.Substring(1, actualStringSegment.Length - 2);
                    builder = BuildVariableStringSegment(builder, actualStringSegment, optional, dependingOnPrevious, recurring);
                }
                else
                    builder = BuildFixedStringSegment(builder, actualStringSegment, optional, dependingOnPrevious, recurring);

            }

            return builder.Build();
        }

        bool IsVariableSegment(string actualStringSegment)
        {
            return actualStringSegment.StartsWith("{") && actualStringSegment.EndsWith("}");
        }

        private void HandlePrefixes(ref string stringSegment, out bool optional, out bool dependingOnPrevious)
        {
            optional = false;
            dependingOnPrevious = false;

            var prefixesMatch = _prefixesExpression.Match(stringSegment);
            for (var prefixGroupIndex = 1; prefixGroupIndex < prefixesMatch.Groups.Count; prefixGroupIndex++)
            {
                var prefixGroup = prefixesMatch.Groups[prefixGroupIndex];
                stringSegment = stringSegment.Substring(prefixGroup.Value.Length);
                switch (prefixGroup.Value)
                {
                    case "-": optional = true; break;
                    case "^": dependingOnPrevious = true; break;
                }
            }
        }


        bool IsStringRecurring(string segment)
        {
            return segment.EndsWith("*");
        }

        IStringFormatBuilder BuildFixedStringSegment(IStringFormatBuilder builder, string actualStringSegment, bool optional, bool dependingOnPrevious, bool recurring)
        {
            builder = builder.FixedString(actualStringSegment, (IFixedStringSegmentBuilder fixedStringBuilder) =>
            {
                if (optional) fixedStringBuilder = fixedStringBuilder.Optional();
                if (dependingOnPrevious) fixedStringBuilder = fixedStringBuilder.DependingOnPrevious();
                if (recurring) fixedStringBuilder = fixedStringBuilder.Recurring();
                return fixedStringBuilder;
            });
            return builder;
        }

        IStringFormatBuilder BuildVariableStringSegment(IStringFormatBuilder builder, string actualStringSegment, bool optional, bool dependingOnPrevious, bool recurring)
        {
            return builder.VariableString(actualStringSegment, (IVariableStringSegmentBuilder variableStringBuilder) =>
            {
                if (optional) variableStringBuilder = variableStringBuilder.Optional();
                if (dependingOnPrevious) variableStringBuilder = variableStringBuilder.DependingOnPrevious();
                if (recurring) variableStringBuilder = variableStringBuilder.Recurring();
                return variableStringBuilder;
            });
        }

        void ThrowIfMissingSeparators(string format, Match separatorsMatch)
        {
            if (!separatorsMatch.Success) throw new MissingSeparatorsInFormatString(format);
        }
    }
}
