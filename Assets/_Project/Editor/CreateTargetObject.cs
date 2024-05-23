using DynamicMeshCutter;
using GluonGui.WorkspaceWindow.Views.WorkspaceExplorer;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class CreateTargetObject : EditorWindow
{
    private GameObject selectedObject;
    private AudioClip audioSource;
    string path = "Assets/_Project/Prefab/Targets/";
    [UnityEditor.MenuItem("Window/Custom Tools/Create Target Object")]
    // Start is called before the first frame update
    public static void ShowWindow()
    {
        EditorWindow.GetWindow<CreateTargetObject>("Create Target Object Window");
    }

    private void OnGUI()
    {
        GUILayout.Label("居合切りのターゲットとなるオブジェクトを作成してターゲットオブジェクトフォルダにプレハブとして保存");
        selectedObject = (GameObject)EditorGUILayout.ObjectField("元のオブジェクト", selectedObject, typeof(GameObject), true);
        audioSource = (AudioClip)EditorGUILayout.ObjectField("切られた時の音", audioSource, typeof(AudioClip), true);
        //元のオブジェクトに影響を与えないために複製
        GameObject duplicateObject = selectedObject;
        // ボタン追加
        if (GUILayout.Button("居合切りターゲットとして生成する"))
        {
            if (duplicateObject != null)
            {

                if(duplicateObject.GetComponent<MeshTarget>() == null)
                {
                    duplicateObject.AddComponent<MeshTarget>();
                    duplicateObject.GetComponent<MeshTarget>().GameobjectRoot = duplicateObject;
                }
                if(duplicateObject.GetComponent<MeshCollider>() == null)
                {
                    duplicateObject.AddComponent<MeshCollider>();
                }
                if(duplicateObject.GetComponent<Rigidbody>() == null)
                {
                    duplicateObject.AddComponent<Rigidbody>();
                }
                if(duplicateObject.GetComponent<AudioSource>() == null)
                {
                    duplicateObject.AddComponent<AudioSource>();
                    duplicateObject.GetComponent<AudioSource>().clip = audioSource;
                    duplicateObject.GetComponent<AudioSource>().playOnAwake = false;
                }

                string filePath = path + duplicateObject.name + ".prefab";

                GameObject prefab = PrefabUtility.SaveAsPrefabAsset(duplicateObject, filePath);
            }
            else
            {
                Debug.Log("No GameObject selected.");
            }
        }
    }
}
