using UnityEditor;


namespace JevLogin
{
    public class MenuItems
    {
        [MenuItem("GeekBrains/Пункт меню №0 ")]
        private static void MenuOption()
        {
            EditorWindow.GetWindow(typeof(MyWindow), false, "Geekbrains");
        }

        [MenuItem("GeekBrains/Пункт меню №1 %ga")]
        private static void NewMenuOption()
        {
        }

        [MenuItem("GeekBrains/Пункт меню №2 %g")]
        private static void NewNestedOption()
        {
        }

        [MenuItem("GeekBrains/Пункт меню №3 g")]
        private static void NewOptionWithHotkey()
        {
        }

        [MenuItem("GeekBrains/Пункт меню/Пункт меню №3 g")]
        private static void NewOptionWithHot()
        {
        }

        [MenuItem("Assets/Geekbrains")]
        private static void LoadAdditiveScene()
        {
        }

        [MenuItem("Assets/Create/Geekbrains")]
        private static void AddConfig()
        {
        }

        [MenuItem("CONTEXT/Rigidbody/Geekbrains")]
        private static void NewOpenForRigidBody()
        {
        }

    }
}