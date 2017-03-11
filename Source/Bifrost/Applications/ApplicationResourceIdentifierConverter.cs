/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Extensions;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResourceIdentifierConverter"/>
    /// </summary>
    public class ApplicationResourceIdentifierConverter : IApplicationResourceIdentifierConverter
    {
        /// <summary>
        /// The expected format when parsing resources as strings
        /// </summary>
        public static string ExpectedFormat = $"Application{ApplicationSeparator} LocationSegments separated with {ApplicationLocationSeparator} then {ApplicationResourceSeparator} and resource identifier then {ApplicationResourceTypeSeparator} and the type. e.g. 'Application{ApplicationSeparator}BoundedContext{ApplicationLocationSeparator}Module{ApplicationLocationSeparator}Feature{ApplicationLocationSeparator}SubFeature{ApplicationResourceSeparator}Resource{ApplicationResourceTypeSeparator}Type'";

        /// <summary>
        /// The separator character used for separating the identification for the <see cref="IApplication"/>
        /// </summary>
        public const char ApplicationSeparator = '#';

        /// <summary>
        /// The separator character used for separating the identification for every <see cref="IApplicationLocation"/> segment
        /// </summary>
        public const char ApplicationLocationSeparator = '.';

        /// <summary>
        /// The separator character used for separating the <see cref="IApplicationResource"/> from the rest in a string
        /// </summary>
        public const char ApplicationResourceSeparator = '-';

        /// <summary>
        /// The separator character used for separating the <see cref="IApplicationResourceType"/> from the rest in a string
        /// </summary>
        public const char ApplicationResourceTypeSeparator = '+';

        IApplication _application;
        IApplicationResourceTypes _applicationResourceTypes;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResourceIdentifierConverter"/>
        /// </summary>
        /// <param name="application">The <see cref="IApplication">application context</see></param>
        /// <param name="applicationResourceTypes"><see cref="IApplicationResourceTypes"/> available</param>
        public ApplicationResourceIdentifierConverter(IApplication application, IApplicationResourceTypes applicationResourceTypes)
        {
            _application = application;
            _applicationResourceTypes = applicationResourceTypes;
        }

        /// <inheritdoc/>
        public string AsString(IApplicationResourceIdentifier identifier)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append($"{identifier.Application.Name}{ApplicationSeparator}");
            var first = true;
            identifier.LocationSegments.ForEach(l =>
            {
                if (!first) stringBuilder.Append(ApplicationLocationSeparator);
                first = false;
                stringBuilder.Append($"{l.Name}");
            });
            stringBuilder.Append($"{ApplicationResourceSeparator}{identifier.Resource.Name}");
            stringBuilder.Append($"{ApplicationResourceTypeSeparator}{identifier.Resource.Type.Identifier}");
            return stringBuilder.ToString();
        }

        /// <inheritdoc/>
        public IApplicationResourceIdentifier FromString(string identifierAsString)
        {
            var applicationSeparatorIndex = identifierAsString.IndexOf('#');
            ThrowIfApplicationSeparatorMissing(applicationSeparatorIndex, identifierAsString);

            var applicationResourceSeparatorIndex = identifierAsString.IndexOf('-');
            ThrowIfApplicationResourceMissing(applicationResourceSeparatorIndex, identifierAsString);

            var applicationIdentifier = identifierAsString.Substring(0, applicationSeparatorIndex);
            ThrowIfApplicationMismatches(applicationIdentifier, identifierAsString);

            var applicationResourceTypeSeparatorIndex = identifierAsString.IndexOf('+');
            ThrowIfApplicationResourceTypeMissing(applicationResourceTypeSeparatorIndex, identifierAsString);

            var resourceIdentifier = identifierAsString.Substring(applicationResourceSeparatorIndex + 1, applicationResourceTypeSeparatorIndex - (applicationResourceSeparatorIndex + 1));
            var locationIdentifiers = identifierAsString.Substring(applicationSeparatorIndex + 1, applicationResourceSeparatorIndex - (applicationSeparatorIndex + 1));
            var resourceTypeIdentifier = identifierAsString.Substring(applicationResourceTypeSeparatorIndex + 1);

            ThrowIfApplicationLocationsMissing(locationIdentifiers, identifierAsString);

            var locationStrings = locationIdentifiers.Split(ApplicationLocationSeparator).Where(s => s.Length > 0).ToArray();
            var locations = new List<IApplicationLocation>();

            var boundedContext = new BoundedContext(locationStrings[0]);
            locations.Add(boundedContext);

            if (locationStrings.Length >= 2)
            {
                var module = new Module(boundedContext, locationStrings[1]);
                locations.Add(module);

                if (locationStrings.Length >= 3)
                {
                    var feature = new Feature(module, locationStrings[2]);
                    locations.Add(feature);

                    if (locationStrings.Length >= 4)
                    {
                        var parentFeature = feature;
                        var locationStringIndex = 3;
                        do
                        {
                            var subFeature = new SubFeature(parentFeature, locationStrings[locationStringIndex]);
                            locations.Add(subFeature);

                            locationStringIndex++;
                        } while (locationStringIndex < locationStrings.Length);
                    }
                }
            }

            var resource = new ApplicationResource(resourceIdentifier, _applicationResourceTypes.GetFor(resourceTypeIdentifier));
            return new ApplicationResourceIdentifier(
                    _application,
                    locations,
                    resource
                );
        }

        void ThrowIfApplicationSeparatorMissing(int applicationSeparatorIndex, string identifierAsString)
        {
            if (applicationSeparatorIndex <= 0) throw new UnableToIdentifyApplication(identifierAsString);
        }

        void ThrowIfApplicationResourceMissing(int applicationResourceSeparatorIndex, string identifierAsString)
        {
            if (applicationResourceSeparatorIndex <= 0) throw new MissingApplicationResource(identifierAsString);
        }

        void ThrowIfApplicationLocationsMissing(string locationIdentifiers, string identifierAsString)
        {
            if (locationIdentifiers.Length == 0) throw new MissingApplicationLocations(identifierAsString);
        }

        void ThrowIfApplicationMismatches(string applicationIdentifier, string identifierAsString)
        {
            if (_application.Name != applicationIdentifier) throw new ApplicationMismatch(_application.Name, identifierAsString);
        }

        void ThrowIfApplicationResourceTypeMissing(int applicationResourceTypeSeparatorIndex, string identifierAsString)
        {
            if (applicationResourceTypeSeparatorIndex <= 0) throw new MissingApplicationResourceType(identifierAsString);
        }
    }
}
