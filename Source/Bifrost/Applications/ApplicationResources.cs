/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bifrost.Extensions;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResources"/>
    /// </summary>
    public class ApplicationResources : IApplicationResources
    {
        /// <summary>
        /// The expected format when parsing resources as strings
        /// </summary>
        public static string ExpectedFormat = $"Application{ApplicationSeparator} LocationSegments separated with {ApplicationLocationSeparator} then {ApplicationResourceSeparator} And resource identifier. e.g. 'Application{ApplicationSeparator}BoundedContext{ApplicationLocationSeparator}Module{ApplicationLocationSeparator}Feature{ApplicationLocationSeparator}SubFeature{ApplicationResourceSeparator}Resource'";

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
        /// The key representing a <see cref="IBoundedContext"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string BoundedContextKey = "BoundedContext";

        /// <summary>
        /// The key representing a <see cref="IModule"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string ModuleKey = "Module";

        /// <summary>
        /// The key representing a <see cref="IFeature"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string FeatureKey = "Feature";

        /// <summary>
        /// The key representing a <see cref="ISubFeature"/> as part of <see cref="IApplicationStructureConfigurationBuilder"/>
        /// </summary>
        public const string SubFeatureKey = "SubFeature";


        IApplication _application;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResources"/>
        /// </summary>
        /// <param name="application">The <see cref="IApplication"/> the resource belongs to</param>
        public ApplicationResources(IApplication application)
        {
            _application = application;
        }

        /// <inheritdoc/>
        public ApplicationResourceIdentifier Identify(object resource)
        {
            var type = resource.GetType();
            var @namespace = type.Namespace;

            foreach (var format in _application.Structure.AllStructureFormats)
            {
                var match = format.Match(@namespace);
                if (match.HasMatches)
                {
                    var segments = GetLocationSegmentsFrom(match);
                    var identifier = new ApplicationResourceIdentifier(_application, segments, new ApplicationResource(type.Name));
                    return identifier;
                }
            }

            throw new UnableToIdentifyApplicationResource(type);
        }

        /// <inheritdoc/>
        public string AsString(ApplicationResourceIdentifier identifier)
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
            return stringBuilder.ToString();
        }

        /// <inheritdoc/>
        public ApplicationResourceIdentifier FromString(string identifierAsString)
        {
            var applicationSeparatorIndex = identifierAsString.IndexOf('#');
            ThrowIfApplicationSeparatorMissing(applicationSeparatorIndex, identifierAsString);

            var applicationResourceSeparatorIndex = identifierAsString.IndexOf('-');
            ThrowIfApplicationResourceMissing(applicationResourceSeparatorIndex, identifierAsString);

            var applicationIdentifier = identifierAsString.Substring(0, applicationSeparatorIndex);
            ThrowIfApplicationMismatches(applicationIdentifier, identifierAsString);

            var resourceIdentifier = identifierAsString.Substring(applicationResourceSeparatorIndex + 1);
            var locationIdentifiers = identifierAsString.Substring(applicationSeparatorIndex+1, applicationResourceSeparatorIndex - (applicationSeparatorIndex+1));
            ThrowIfApplicationLocationsMissing(locationIdentifiers, identifierAsString);

            var locationStrings = locationIdentifiers.Split(ApplicationLocationSeparator).Where(s => s.Length > 0).ToArray();
            var locations = new List<IApplicationLocation>();

            var boundedContext = new BoundedContext(locationStrings[0]);
            locations.Add(boundedContext);

            if( locationStrings.Length >= 2 )
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

            var resource = new ApplicationResource(resourceIdentifier);
            return new ApplicationResourceIdentifier(
                    _application,
                    locations,
                    resource
                );
        }


        IEnumerable<IApplicationLocation> GetLocationSegmentsFrom(ISegmentMatches match)
        {
            var matchAsDictionary = match.AsDictionary();

            var segments = new List<IApplicationLocation>();
            BoundedContext boundedContext = null;
            Module module = null;
            Feature feature = null;
            List<SubFeature> subFeatures = new List<SubFeature>();

            if (matchAsDictionary.ContainsKey(BoundedContextKey))
            {
                boundedContext = new BoundedContext(matchAsDictionary[BoundedContextKey].Single());
                segments.Add(boundedContext);

                if (matchAsDictionary.ContainsKey(ModuleKey))
                {
                    module = new Module(boundedContext, matchAsDictionary[ModuleKey].Single());
                    segments.Add(module);

                    if (matchAsDictionary.ContainsKey(FeatureKey))
                    {
                        feature = new Feature(module, matchAsDictionary[FeatureKey].Single());
                        segments.Add(feature);

                        if (matchAsDictionary.ContainsKey(SubFeatureKey))
                        {
                            foreach (var subFeatureName in matchAsDictionary[SubFeatureKey])
                            {
                                var subFeature = new SubFeature(feature, subFeatureName);
                                segments.Add(subFeature);
                            }
                        }
                    }
                }
            }

            return segments;
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
    }
}
