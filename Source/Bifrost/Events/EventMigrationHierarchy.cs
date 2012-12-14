using System;
using System.Collections.Generic;
using System.Linq;
#if(NETFX_CORE)
using System.Reflection;
#endif

namespace Bifrost.Events
{
    /// <summary>
    /// Represents a migration hierarchy for a logical event, containing the concrete type for each step in the chain.
    /// </summary>
    public class EventMigrationHierarchy
    {
        private readonly List<Type> _migrationLevels;

		/// <summary>
		/// Gets the logical event type
		/// </summary>
        public Type LogicalEvent { get; private set; }

		/// <summary>
		/// Gets the migration level of the hierarchy
		/// </summary>
        public int MigrationLevel
        {
            get { return _migrationLevels.Count - 1; }
        }

		/// <summary>
		/// Gets the types in the migration hierarchy
		/// </summary>
        public IEnumerable<Type> MigratedTypes { get { return _migrationLevels.ToArray(); } }

        /// <summary>
        /// Initializes an instance of <see cref="EventMigrationHierarchy"/>
        /// </summary>
        /// <param name="logicalEvent">Logical event that the hierarchy relates to.</param>
        public EventMigrationHierarchy(Type logicalEvent)
        {
            _migrationLevels = new List<Type>();
            LogicalEvent = logicalEvent;
            AddMigrationLevel(logicalEvent);
        }

        /// <summary>
        /// Adds a new concrete type as the next level in the migration hierarchy
        /// </summary>
        /// <param name="type">Concrete type of the logical event</param>
        public void AddMigrationLevel(Type type)
        {
            if (_migrationLevels.Contains(type))
                throw new DuplicateInEventMigrationHierarchyException(
                    string.Format("Type {0} already exists in the hierarchy for Event {1}.Cannot have more than one migration path for an Event ",
                    type,LogicalEvent)
                );

            if (MigrationLevel >= 0)
                ValidateMigration(type);

            _migrationLevels.Add(type);
        }

        /// <summary>
        /// Gets the concrete type of the logical event at the specified migration level
        /// </summary>
        /// <param name="level">The migration level</param>
        /// <returns>Concrete type of the logical event at the specified migration level</returns>
        public Type GetConcreteTypeForLevel(int level)
        {
            return _migrationLevels[level];
        }

        /// <summary>
        /// Gets the level which the concrete type occupies in the migration hierarchy
        /// </summary>
        /// <param name="type">Concrete type of the logical event</param>
        /// <returns>The migration level</returns>
        public int GetLevelForConcreteType(Type type)
        {
           return _migrationLevels.IndexOf(type);
        }

        void ValidateMigration(Type type)
        {
            ValidateTypeIsAMigration(type);
            ValidateTypeIsOfExpectedType(type);
        }

        void ValidateTypeIsAMigration(Type type)
        {
            if(!ImplementsMigrationInterface(type))
                throw new NotAMigratedEventTypeException(
                        "This is not a valid migrated event type.  All events that are migrations of earlier generations of events" +
                        "must implement the IAmNextGenerationOf<T> interface where T is the previous generation of the event.");
        }


        void ValidateTypeIsOfExpectedType(Type type)
        {
            var expectedTypeToMigrateFrom = _migrationLevels[MigrationLevel];
            try
            {
                var actualTypeMigratingFrom = GetMigrationFromType(type);

                if(actualTypeMigratingFrom != expectedTypeToMigrateFrom)
                    ThrowInvalidMigrationTypeException(expectedTypeToMigrateFrom, type, null);
            }
            catch (Exception ex)
            {
                ThrowInvalidMigrationTypeException(expectedTypeToMigrateFrom, type, ex);
            }

        }

        static Type GetMigrationFromType(Type migrationType)
        {
            var types = from interfaceType in migrationType
#if(NETFX_CORE)
                                    .GetTypeInfo().ImplementedInterfaces
#else
                                    .GetInterfaces()
#endif
                        where interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().IsGenericType
#else
                                    .IsGenericType
#endif
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == typeof(IAmNextGenerationOf<>)
                        select interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().GenericTypeParameters
#else
                                    .GetGenericArguments()
#endif
                            .First();

            return types.Last();
        }

        static bool ImplementsMigrationInterface(Type migrationType)
        {
            var types = from interfaceType in migrationType
#if(NETFX_CORE)
                                    .GetTypeInfo().ImplementedInterfaces
#else
                                    .GetInterfaces()
#endif
                        where interfaceType
#if(NETFX_CORE)
                                    .GetTypeInfo().IsGenericType
#else
                                    .IsGenericType
#endif
                        let baseInterface = interfaceType.GetGenericTypeDefinition()
                        where baseInterface == typeof(IAmNextGenerationOf<>)
                        select interfaceType;

            return types.FirstOrDefault() != null;
        }

        static void ThrowInvalidMigrationTypeException(Type expected, Type actual, Exception innerException)
        {
            throw new InvalidMigrationTypeException(
                    string.Format("Expected migration for type {0} but got migration for type {1} instead.", expected, actual),
                    innerException
                );
        }
    }
}