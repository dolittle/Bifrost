using System;
using Bifrost.Commands;
using Bifrost.Execution;

namespace Bifrost.Sagas
{
    /// <summary>
    /// Locates pre-populators
    /// </summary>
    public interface IPrePopulatorLocator {
        /// <summary>
        /// Get the pre-populator for a command in the context of a saga
        /// </summary>
        /// <param name="saga"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        IPrePopulate<ISaga, ICommand> GetPrePopulatorFor(ISaga saga, ICommand command);
    }

    class PrePopulatorLocator : IPrePopulatorLocator {
        readonly ITypeImporter _typeImporter;
        IPrePopulate<ISaga, ICommand>[] _prePopulators;

        public PrePopulatorLocator(ITypeImporter typeImporter)
        {
            _typeImporter = typeImporter;
            Initialize();
        }

        void Initialize()
        {
            _prePopulators = _typeImporter.ImportMany<IPrePopulate<ISaga, ICommand>>();
        }

        public IPrePopulate<ISaga, ICommand> GetPrePopulatorFor(ISaga saga, ICommand command)
        {
            foreach(var prep in _prePopulators)
            {
                try
                {
                    prep.PrePopulate(saga, command);
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }

            var t = typeof (IPrePopulate<ISaga, ICommand>);
            var m = t.GetMethod("PrePopulate");
            var hurr = m.MakeGenericMethod(saga.GetType(), command.GetType());
            

            var typeOfSaga = saga.GetType();
            var typeOfCommand = command.GetType();

            var sagaMethod = typeOfSaga.GetMethod("Begin");
            var commandProp = typeOfCommand.GetProperty("Id");

            var sagaHr = sagaMethod.MakeGenericMethod();

        }
    }
}