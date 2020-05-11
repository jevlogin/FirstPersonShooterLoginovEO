using UnityEngine;


namespace JevLogin
{
    public static class CustomDebug
    {
        public static bool IsDebug;
        public static void Log(object value)
        {
            if (IsDebug)
            {
                Debug.Log(value); 
            }
        }
    }
}
