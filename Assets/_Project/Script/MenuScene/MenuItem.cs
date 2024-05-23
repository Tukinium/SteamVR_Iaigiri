using UnityEditor;
using UnityEngine;
using Valve.VR;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private MenuItemAnimationControll AnimationObject; // �A�j���[�V�����I�u�W�F�N�g
    [SerializeField] private string sceneAsset; // ���[�h����V�[���̃A�Z�b�g

    // �I�u�W�F�N�g���}�E�X�|�C���^�[�ɏd�Ȃ������̏���
    public void OnPointerEnter()
    {
        if (AnimationObject != null)
        {
            AnimationObject.SetIsNeutral(false); // �A�j���[�V�������j���[�g�����ɐݒ�
        }
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // �X�P�[�����g��
    }

    // �I�u�W�F�N�g���N���b�N���ꂽ���̏���
    public void OnPointerClick()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play(); // �I�[�f�B�I���Đ�
        }

        if (sceneAsset != null)
        {
            if(sceneAsset ==  "Exit")
            {
                Application.Quit();
            }
            SteamVR_LoadLevel.Begin(sceneAsset); // �w�肵���V�[���ɑJ��
        }
        else
        {
            Debug.LogError("SceneAsset���ݒ肳��Ă��܂���B");
        }
    }

    // �I�u�W�F�N�g����}�E�X�|�C���^�[�����ꂽ���̏���
    public void OnPointerExit()
    {
        if (AnimationObject != null)
        {
            AnimationObject.SetIsNeutral(true); // �A�j���[�V�������j���[�g�����ɐݒ�
        }
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); // �X�P�[�������ɖ߂�
    }
}
