using UnityEditor;
using UnityEngine;
using System.Reflection;

public class CustomWindow : EditorWindow
{
    private GameObject selectedObject;
    private string[] methodNames;
    private int selectedMethodIndex = -1;

    [UnityEditor.MenuItem("Window/Debug Window")]
    public static void ShowWindow()
    {
        EditorWindow.GetWindow(typeof(CustomWindow));
    }

    void OnGUI()
    {
        selectedObject = Selection.activeGameObject;

        if (selectedObject == null)
        {
            EditorGUILayout.LabelField("No object selected.");
            return;
        }

        EditorGUILayout.LabelField("Selected Object: " + selectedObject.name);
        EditorGUILayout.LabelField("!!!!! Attention This Editor is Private Accese Only Use Debug !!!!!");
         
        MonoBehaviour[] scripts = selectedObject.GetComponents<MonoBehaviour>();

        if (scripts.Length == 0)
        {
            EditorGUILayout.LabelField("No scripts attached to the selected object.");
            return;
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("All Methods:");

        methodNames = GetMethodNames(scripts);

        if (methodNames.Length == 0)
        {
            EditorGUILayout.LabelField("No methods found in the attached scripts.");
            return;
        }

        selectedMethodIndex = EditorGUILayout.Popup(selectedMethodIndex, methodNames);

        EditorGUILayout.Space();

        if (GUILayout.Button("Execute Selected Method"))
        {
            if (selectedMethodIndex >= 0 && selectedMethodIndex < methodNames.Length)
            {
                ExecuteMethod(methodNames[selectedMethodIndex], scripts);
            }
        }
    }

    string[] GetMethodNames(MonoBehaviour[] scripts)
    {
        System.Collections.Generic.List<string> methodNames = new System.Collections.Generic.List<string>();

        foreach (MonoBehaviour script in scripts)
        {
            System.Type type = script.GetType();
            MethodInfo[] methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

            foreach (var method in methods)
            {
                if (method.GetParameters().Length == 0) // Execute only methods without parameters
                {
                    methodNames.Add(type.Name + "/" + method.Name);
                }
            }
        }

        return methodNames.ToArray();
    }

    void ExecuteMethod(string methodName, MonoBehaviour[] scripts)
    {
        foreach (MonoBehaviour script in scripts)
        {
            if (methodName.StartsWith(script.GetType().Name + "/"))
            {
                string methodNameWithoutClassName = methodName.Substring(script.GetType().Name.Length + 1);
                MethodInfo method = script.GetType().GetMethod(methodNameWithoutClassName, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);

                if (method != null)
                {
                    method.Invoke(script, null);
                    Debug.Log("Method " + methodName + " executed.");
                }
            }
        }
    }
}
