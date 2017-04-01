/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an implementation of <see cref="IApplicationResourceIdentifier"/> - an identifier for <see cref="IApplicationResource">resources</see> in an <see cref="IApplication"/>
    /// </summary>
    public class ApplicationResourceIdentifier : IApplicationResourceIdentifier
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ApplicationResourceIdentifier"/>
        /// </summary>
        /// <param name="application"><see cref="IApplication"/> the resource belongs to</param>
        /// <param name="locationSegments"><see cref="IApplicationLocation">Location</see> segments for the <see cref="IApplicationResource"/></param>
        /// <param name="resource"><see cref="IApplicationResource">Resource</see> the identifier is for</param>
        public ApplicationResourceIdentifier(IApplication application, IEnumerable<IApplicationLocation> locationSegments, IApplicationResource resource)
        {
            Application = application;
            LocationSegments = locationSegments;
            Resource = resource;
        }

        /// <inheritdoc/>
        public IApplication Application { get; }

        /// <inheritdoc/>
        public IEnumerable<IApplicationLocation> LocationSegments { get; }

        /// <inheritdoc/>
        public IApplicationResource Resource { get; }

        /// <inheritdoc/>
        public static bool operator ==(ApplicationResourceIdentifier x, ApplicationResourceIdentifier y)
        {
            return x.Equals(y);
        }

        /// <inheritdoc/>
        public static bool operator !=(ApplicationResourceIdentifier x, ApplicationResourceIdentifier y)
        {
            return !x.Equals(y);
        }

        /// <inheritdoc/>
        public bool Equals(IApplicationResourceIdentifier other)
        {
            if (LocationSegments.Count() != other.LocationSegments.Count()) return false;

            if (((string) Application.Name) != ((string) other.Application.Name)) return false;
            if (((string) Resource.Name) != ((string) other.Resource.Name)) return false;

            var locationSegmentsA = LocationSegments.ToArray();
            var locationSegmentsB = other.LocationSegments.ToArray();

            for (var i = 0; i < locationSegmentsA.Length; i++)
                if (locationSegmentsA[i].Name.AsString() != locationSegmentsB[i].Name.AsString()) return false;

            return true;
        }

        /// <inheritdoc/>
        public override int GetHashCode()
        {
            var hashCode = Application.Name.GetHashCode();
            hashCode += Resource.Name.GetHashCode();
            foreach (var segment in LocationSegments) hashCode += segment.Name.GetHashCode();

            return hashCode;
        }

        /// <inheritdoc/>
        public override bool Equals(object obj)
        {
            if (!(obj is IApplicationResourceIdentifier)) return false;
            return Equals((IApplicationResourceIdentifier) obj);
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(IApplicationResourceIdentifier other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }
    }
}