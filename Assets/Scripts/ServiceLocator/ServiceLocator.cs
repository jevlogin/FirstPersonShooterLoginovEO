using System;
using System.Collections.Generic;


namespace JevLogin
{
    public static class ServiceLocator
    {
        #region PrivedData

        private static readonly Dictionary<Type, object> _serviceContainer = new Dictionary<Type, object>();

        #endregion


        #region Methods

        public static void SetService<T>(T value) where T : class
        {
            var typeValue = value.GetType();
            if (!_serviceContainer.ContainsKey(typeValue))
            {
                _serviceContainer[typeValue] = value;
            }
        }

        public static T Resolve<T>()
        {
            return (T)_serviceContainer[typeof(T)];
        }

        #endregion
    }
}
