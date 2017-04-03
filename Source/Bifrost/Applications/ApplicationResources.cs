/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;
using Bifrost.Strings;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResources"/>
    /// </summary>
    public class ApplicationResources : IApplicationResources
    {
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
        IApplicationResourceTypes _applicationResourceTypes;

        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResources"/>
        /// </summary>
        /// <param name="application">The <see cref="IApplication"/> the resource belongs to</param>
        /// <param name="applicationResourceTypes"><see cref="IApplicationResourceTypes"/> for getting <see cref="IApplicationResourceType"/></param>
        public ApplicationResources(IApplication application, IApplicationResourceTypes applicationResourceTypes)
        {
            _application = application;
            _applicationResourceTypes = applicationResourceTypes;
        }

        /// <inheritdoc/>
        public IApplicationResourceIdentifier Identify(object resource)
        {
            var type = resource.GetType();
            return Identify(type);
        }

        /// <inheritdoc/>
        public IApplicationResourceIdentifier Identify(Type type)
        {
            var @namespace = type.Namespace;

            foreach (var format in _application.Structure.AllStructureFormats)
            {
                var match = format.Match(@namespace);
                if (match.HasMatches)
                {
                    var segments = GetLocationSegmentsFrom(match);
                    var identifier = new ApplicationResourceIdentifier(_application, segments, new ApplicationResource(type.Name, _applicationResourceTypes.GetFor(type)));
                    return identifier;
                }
            }

            throw new UnableToIdentifyApplicationResource(type);
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
    }
}
