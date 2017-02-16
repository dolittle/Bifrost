/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;
using Bifrost.Strings;

namespace Bifrost.Application
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResources"/>
    /// </summary>
    public class ApplicationResources : IApplicationResources
    {
        /// <summary>
        /// The key representing a <see cref="IBoundedContext"/> as part of <see cref="IApplicationStructure"/>
        /// </summary>
        public const string BoundedContextKey = "BoundedContext";

        /// <summary>
        /// The key representing a <see cref="IModule"/> as part of <see cref="IApplicationStructure"/>
        /// </summary>
        public const string ModuleKey = "Module";

        /// <summary>
        /// The key representing a <see cref="IFeature"/> as part of <see cref="IApplicationStructure"/>
        /// </summary>
        public const string FeatureKey = "Feature";

        /// <summary>
        /// The key representing a <see cref="ISubFeature"/> as part of <see cref="IApplicationStructure"/>
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

            foreach( var format in _application.Structure.StructureFormats )
            {
                var match = format.Match(@namespace);
                if( match.HasMatches )
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

                        if( matchAsDictionary.ContainsKey(ModuleKey))
                        {
                            module = new Module(boundedContext, matchAsDictionary[ModuleKey].Single());
                            segments.Add(module);

                            if( matchAsDictionary.ContainsKey(FeatureKey))
                            {
                                feature = new Feature(module, matchAsDictionary[FeatureKey].Single());
                                segments.Add(feature);

                                if( matchAsDictionary.ContainsKey(SubFeatureKey))
                                {
                                    foreach( var subFeatureName in matchAsDictionary[SubFeatureKey] )
                                    {
                                        var subFeature = new SubFeature(feature, subFeatureName);
                                        segments.Add(subFeature);
                                    }
                                }
                            }
                        }
                    }

                    var identifier = new ApplicationResourceIdentifier(_application, segments, new ApplicationResource(type.Name));
                    return identifier;
                }
            }

            throw new UnableToIdentifyApplicationResource(type);
        }
    }
}
