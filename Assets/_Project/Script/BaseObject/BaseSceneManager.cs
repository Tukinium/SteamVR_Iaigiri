using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

// �V�[���Ǘ��N���X�̃x�[�X
public class BaseSceneManager : MonoBehaviour
{
    private float timer = 0; // �^�C�}�[
    private SteamVR_LoadLevel loadLevel; // SteamVR�̃V�[�����[�h�N���X
    [SerializeField] private string sceneAsset; // ���[�h����V�[���̖��O
    [SerializeField] private GameObject cameraRig = null; // �J�������O

    protected virtual void Start()
    {
        // Null�`�F�b�N��ǉ����āASteamVR_LoadLevel�R���|�[�l���g���A�^�b�`����Ă��邱�Ƃ��m�F
        loadLevel = GetComponent<SteamVR_LoadLevel>();
        if (loadLevel == null)
        {
            Debug.LogError("SteamVR_LoadLevel���A�^�b�`����Ă��܂���B");
        }
    }

    protected virtual void Update()
    {
        UpdateTimer();
    }

    //�^�C�}�[�̍X�V
    protected void UpdateTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // �^�C�}�[���X�V
        }
    }

    // �^�C�}�[�̐ݒ�
    protected void SetTimer(float _time = 0)
    {
        timer = _time; // �^�C�}�[��ݒ�
    }

    // �^�C�}�[���I���������ǂ������擾
    protected bool GetTimeUp()
    {
        return timer <= 0; // �^�C�}�[��0�ȉ��Ȃ�true��Ԃ�
    }

    // �V�[���̕ύX
    protected void ChangeScene()
    {
        // Null�`�F�b�N��ǉ����āA�V�[���A�Z�b�g���ݒ肳��Ă��邱�Ƃ��m�F
        if (sceneAsset != null)
        {
            string sceneName = sceneAsset;
            SceneManager.LoadScene(sceneName); // �w�肵���V�[���ɑJ��
        }
        else
        {
            Debug.LogError("SceneAsset���ݒ肳��Ă��܂���B");
        }
    }
}
