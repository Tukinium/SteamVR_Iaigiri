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
        GUILayout.Label("�����؂�̃^�[�Q�b�g�ƂȂ�I�u�W�F�N�g���쐬���ă^�[�Q�b�g�I�u�W�F�N�g�t�H���_�Ƀv���n�u�Ƃ��ĕۑ�");
        selectedObject = (GameObject)EditorGUILayout.ObjectField("���̃I�u�W�F�N�g", selectedObject, typeof(GameObject), true);
        audioSource = (AudioClip)EditorGUILayout.ObjectField("�؂�ꂽ���̉�", audioSource, typeof(AudioClip), true);
        //���̃I�u�W�F�N�g�ɉe����^���Ȃ����߂ɕ���
        GameObject duplicateObject = selectedObject;
        // �{�^���ǉ�
        if (GUILayout.Button("�����؂�^�[�Q�b�g�Ƃ��Đ�������"))
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
