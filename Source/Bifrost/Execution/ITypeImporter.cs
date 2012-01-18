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
namespace Bifrost.Execution
{
	/// <summary>
	/// Defines a container for importing types.
	/// </summary>
	public interface ITypeImporter
	{
		/// <summary>
		/// Import many instances of type
		/// </summary>
		/// <typeparam name="T">Basetype to import - any inheritors will be found and created</typeparam>
		/// <returns>An array of instances that implements or inherits from the given base type</returns>
		T[] ImportMany<T>();

		/// <summary>
		/// Import a single instance of a type
		/// </summary>
		/// <typeparam name="T">Basetype to import</typeparam>
		/// <returns>An instance of a type that implements the given base type</returns>
		T Import<T>();
	}
}
