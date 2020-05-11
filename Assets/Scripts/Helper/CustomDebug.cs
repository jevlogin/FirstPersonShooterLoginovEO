using UnityEngine;


namespace JevLogin
{
    public static class CustomDebug
    {
        #region Fields

        public static bool IsDebug;

        #endregion


        #region UnityMethods

        public static void Log(object value)
        {
            if (IsDebug)
            {
                Debug.Log(value);
            }
        }

        #endregion
    }
}
