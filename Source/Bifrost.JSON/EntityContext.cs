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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Bifrost.Entities;
using Newtonsoft.Json;

namespace Bifrost.JSON
{
	/// <summary>
	/// Represents a simple JSON file based implementation of <see cref="IEntityContext{T}">IEntityContext</see>
	/// </summary>
	/// <typeparam name="T">Type of entity for the context</typeparam>
	public class EntityContext<T> : IEntityContext<T>
	{
		private const string CollectionFileName = "Collection.json";

		private readonly string _entityDirectory;
		private readonly string _entityCollectionFile;

		private List<T> _entities;

		/// <summary>
		/// Initializes a new instance of <see cref="EntityContext{T}">EntityContext</see>
		/// </summary>
		/// <param name="connection">Connection configuration to use</param>
		public EntityContext(EntityContextConnection connection)
		{
			_entities = new List<T>();
			_entityDirectory = string.Format("{0}/{1}", connection.Directory, typeof (T).Name);
			_entityCollectionFile = string.Format("{0}/{1}", _entityDirectory, CollectionFileName);
			MakeSureEntityDirectoryExists();
			MakeSureEntityCollectionExists();
			ReadFile();
		}

		private void MakeSureEntityDirectoryExists()
		{
			if( !Directory.Exists(_entityDirectory))
			{
				Directory.CreateDirectory(_entityDirectory);
			}
		}

		private void MakeSureEntityCollectionExists()
		{
			if( !File.Exists(_entityCollectionFile))
			{
				File.WriteAllText(_entityCollectionFile,string.Empty);
			}
		}

		private void ReadFile()
		{
			var serializer = new JsonSerializer();
			using( var reader = File.OpenText(_entityCollectionFile) )
			{
				using( var jsonReader = new JsonTextReader(reader))
				{
					var entities = serializer.Deserialize<List<T>>(jsonReader);
					if (null != entities)
					{
						_entities = entities;
					}
				}
			}
		}

		private void WriteFile()
		{
			var serializer = new JsonSerializer();
			using( var writer = File.CreateText(_entityCollectionFile))
			{
				using( var jsonWriter = new JsonTextWriter(writer) )
				{
					serializer.Serialize(jsonWriter, Entities);
				}
			}

		}

#pragma warning disable 1591 // Xml Comments
		public IQueryable<T> Entities { get { return _entities.AsQueryable(); } }

		public void Attach(T entity)
		{
			if( _entities.Contains(entity))
			{
				_entities.Remove(entity);
				_entities.Add(entity);
			} else
			{
				Insert(entity);
			}
		}

		public void Insert(T entity)
		{
			_entities.Add(entity);
		}

		public void Update(T entity)
		{
			Attach(entity);
		}

		public void Delete(T entity)
		{
			_entities.Remove(entity);
		}

	    public void Save(T entity)
	    {
	        throw new NotImplementedException();
	    }

	    public void Commit()
		{
			WriteFile();
		}

		public void Dispose()
		{
			WriteFile();
		}

        public T GetById<TProperty>(TProperty id)
        {
            throw new NotImplementedException();
        }


        public void DeleteById<TProperty>(TProperty id)
        {
            throw new NotImplementedException();
        }
#pragma warning restore 1591  // Xml Comments
    }
}
