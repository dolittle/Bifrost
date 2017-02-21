/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bifrost.Applications
{
    /// <summary>
    /// Represents an identifier for <see cref="IApplicationResource">resources</see> in an <see cref="IApplication"/>
    /// </summary>
    public class ApplicationResourceIdentifier : IEquatable<ApplicationResourceIdentifier>, IComparable, IComparable<ApplicationResourceIdentifier>
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

        /// <summary>
        /// Gets the <see cref="IApplication"/> the resource belongs to
        /// </summary>
        public IApplication Application { get; }

        /// <summary>
        /// Gets the segments representing the full <see cref="IApplicationLocation">location</see>
        /// </summary>
        public IEnumerable<IApplicationLocation> LocationSegments { get; }

        /// <summary>
        /// Gets the <see cref="IApplicationResource">resource</see>
        /// </summary>
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
        public bool Equals(ApplicationResourceIdentifier other)
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
            return Equals((ApplicationResourceIdentifier) obj);
        }

        /// <inheritdoc/>
        public int CompareTo(object obj)
        {
            return GetHashCode().CompareTo(obj.GetHashCode());
        }

        /// <inheritdoc/>
        public int CompareTo(ApplicationResourceIdentifier other)
        {
            return GetHashCode().CompareTo(other.GetHashCode());
        }
    }
}