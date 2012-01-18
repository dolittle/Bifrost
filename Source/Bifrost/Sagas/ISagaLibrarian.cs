#region License
//
// Copyright (c) 2008-2012, DoLittle Studios and Komplett ASA
//
// Licensed under the Microsoft Permissive License (Ms-PL), Version 1.1 (the "License")
// With one exception :
//   Commercial libraries that is based partly or fully on Bifrost and is sold commercially, 
//   must obtain a commercial license.
//
// You may not use this file except in compliance with the License.
// You may obtain a copy of the license at 
//
//   http://bifrost.codeplex.com/license
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
//
#endregion

using System;
using System.Collections.Generic;

namespace Bifrost.Sagas
{
	/// <summary>
	/// Defines a librarian for handling sagas
	/// 
	/// The purpose of the librarian is to persist and get a <see cref="ISaga"/> or multiple
	/// sagas
	/// </summary>
    public interface ISagaLibrarian
    {
		/// <summary>
		/// Close a <see cref="ISaga"/> permanently
		/// </summary>
		/// <param name="saga"><see cref="ISaga"/> to close</param>
    	void Close(ISaga saga);

		/// <summary>
		/// Catalogue saga to the library
		/// </summary>
		/// <param name="saga"><see cref="ISaga"/> to record</param>
        void Catalogue(ISaga saga);

		/// <summary>
		/// Get a <see cref="ISaga"/> based on its id
		/// </summary>
		/// <param name="id">Id of saga to get</param>
		/// <returns>An instance of the <see cref="ISaga"/></returns>
		ISaga Get(Guid id);

		/// <summary>
		/// Get a <see cref="ISaga"/> based on the partition its in and key
		/// </summary>
		/// <param name="partition">Partition identifier</param>
		/// <param name="key">Unique partition key</param>
		/// <returns>An instance of the <see cref="ISaga"/></returns>
		ISaga Get(string partition, string key);

		/// <summary>
		/// Get all sagas within a given partition
		/// </summary>
		/// <param name="partition">Partition identifier</param>
		/// <returns>An <see cref="IEnumerable{T}"/> of <see cref="ISaga"/> with all sagas in the given partition</returns>
    	IEnumerable<ISaga> GetForPartition(string partition);
    }
}