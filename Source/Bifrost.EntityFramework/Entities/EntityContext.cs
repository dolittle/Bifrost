/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2008-2017 Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Reflection;
using Bifrost.Concepts;
using Bifrost.Configuration;
using Bifrost.Entities;
using Bifrost.Execution;
using Bifrost.Extensions;

namespace Bifrost.EntityFramework.Entities
{
    /// <summary>
    /// Represents an implementation of <see cref="IEntityContext{T}"/> that is specific to EntityFramework
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EntityContext<T> : DbContext, IEntityContext<T>
        where T:class
    {
        EntityContextConnection _connection;
        IEntityTypeConfigurations _entityTypeConfigurations;
        
        /// <summary>
        /// Initializes a new instance of <see cref="EntityContext{T}"/>
        /// </summary>
        /// <param name="connection"><see cref="EntityContextConnection"/> to use</param>
        /// <param name="entityTypeConfigurations"><see cref="IEntityTypeConfigurations"/> to use for getting the correct configuration</param>
        public EntityContext(EntityContextConnection connection, IEntityTypeConfigurations entityTypeConfigurations) : base(connection.Configuration.ConnectionString)
        {
            _connection = connection;
            _entityTypeConfigurations = entityTypeConfigurations;
        }

        /// <summary>
        /// Gets or sets the <see cref="DbSet"/> for the EntityContext
        /// </summary>
        public DbSet<T> DbSet { get; set; }

#pragma warning disable 1591 // Xml Comments
        public IQueryable<T> Entities
        {
            get 
            {
                var orderable = this.Ordered(DbSet);
                return orderable;
            }
        }

        public void Attach(T entity)
        {
            DbSet.Attach(entity);
        }

        public void Insert(T entity)
        {
            DbSet.Add(entity);
        }

        public void Update(T entity)
        {
            DbSet.Attach(entity);
        }

        public void Delete(T entity)
        {
            DbSet.Remove(entity);
        }

        public void Save(T entity)
        {
            SaveChanges();
        }

        public void Commit()
        {
            SaveChanges();
        }

        public T GetById<TProperty>(TProperty id)
        {
            return DbSet.Find(id);
        }

        public void DeleteById<TProperty>(TProperty id)
        {
            var entity = DbSet.Find(id);
            if (entity != null)
                DbSet.Remove(entity);

            SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
            var typeDiscoverer = Configure.Instance.Container.Get<ITypeDiscoverer>();
            var concepts = typeDiscoverer.FindMultiple(typeof(IAmConceptAs<>));

            var complexTypeMethod = typeof(DbModelBuilder).GetMethod("ComplexType");
            concepts.Where(t=>!t.ContainsGenericParameters).ForEach(t=> {
                var genericComplexTypeMethod = complexTypeMethod.MakeGenericMethod(t);
                var complexTypeConfiguration = genericComplexTypeMethod.Invoke(modelBuilder, null);
                var propertyMethods = complexTypeConfiguration.GetType().GetMethods(BindingFlags.Public|BindingFlags.Instance).Where(m => m.Name == "Property");

                var i = 0;
                i++;
            });*/

            //ComplexTypeConfiguration<object> 
            //modelBuilder.ComplexType<object>().Property()
            //modelBuilder.ComplexType<Something>().Property(s=>s.Number).
            
            var configuration = _entityTypeConfigurations.GetFor<T>();
            modelBuilder.Configurations.Add(configuration);
        }
#pragma warning restore 1591 // Xml Comments
    }
}
