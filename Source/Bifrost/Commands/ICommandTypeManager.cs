#region License
//
// Copyright (c) 2008-2012, DoLittle Studios AS and Komplett ASA
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

namespace Bifrost.Commands
{
    /// <summary>
    /// Defines a manager for dealing with <see cref="ICommand">commands</see> and their types
    /// </summary>
	public interface ICommandTypeManager
	{
		/// <summary>
		/// Gets the type of <see cref="ICommand"/> from the name
		/// </summary>
		/// <returns>
		/// The type of <see cref="ICommand"/>
		/// </returns>
		/// <param name='name'>Name of command - must be unique</param>
		Type GetFromName(string name);
	}
}

