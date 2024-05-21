using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

// �^�C�g���V�[���̊Ǘ��N���X
public class TitleSceneManager : BaseSceneManager
{
    [SerializeField] private SteamVR_Action_Boolean handMainTrigger; // ���C���g���K�[
    [SerializeField] private GameObject attentionPlane = null; // �x���p�l��
    [SerializeField] private GameObject titleLogoPlane = null; // �^�C�g�����S�p�l��
    [SerializeField] private GameObject audioObj = null; // ���̊i�[�R���e�i
    private ITitleState currentState; // ���݂̃X�e�[�g

    protected override void Start()
    {

        base.Start(); // BaseSceneManager��Start���\�b�h�����s
        ChangeState(new ENTRY_State()); // ������Ԃ�ENTRY_State�ɕύX
        attentionPlane.SetActive(false);
        titleLogoPlane.SetActive(false);
        Debug.Log("Start");
    }

    protected override void Update()
    {
        currentState?.Execute(this); // ���݂̃X�e�[�g�����s
    }

    void PlayAudio(string _str)
    {
        AudioSource audio = audioObj.transform.Find(_str).GetComponent<AudioSource>(); // �����Đ�
        audio?.Play(); // Null�`�F�b�N��ǉ����āA�I�[�f�B�I�����݂���ꍇ�̂ݍĐ�
    }

    // �X�e�[�g�ύX�֐�
    public void ChangeState(ITitleState newState)
    {
        // ���݂̃X�e�[�g�̏I������
        currentState?.Exit(this);
        // �X�e�[�g��ύX
        currentState = newState;
        // �V�����X�e�[�g�̏������s
        currentState.Enter(this);
    }

    // �^�C�g���X�e�[�g�̃C���^�[�t�F�[�X
    public interface ITitleState
    {
        void Enter(TitleSceneManager manager); // �X�e�[�g�ɓ��������̏���
        void Execute(TitleSceneManager manager); // ���t���[�����s���鏈��
        void Exit(TitleSceneManager manager); // �X�e�[�g���I�����鎞�̏���
    }

    // �^�C�g���̊e���
    public class ENTRY_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            manager.attentionPlane.SetActive(false); // �x���p�l�����\���ɂ���
            manager.titleLogoPlane.SetActive(false); // �^�C�g�����S�p�l�����\���ɂ���
        }

        public void Execute(TitleSceneManager manager)
        {
            manager.ChangeState(new ATTENTION_State()); // ATTENTION_State�ɑJ��
        }

        public void Exit(TitleSceneManager manager)
        {
            // �������Ȃ�
        }
    }

    public class ATTENTION_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            manager.attentionPlane.SetActive(true); // �x���p�l����\������
        }

        public void Execute(TitleSceneManager manager)
        {
            if (Utility.IsAnyActionPressed(manager.handMainTrigger))
            {
                // ���C���g���K�[�������ꂽ��
                manager.attentionPlane.GetComponent<UIFadeOut>().SetFadeStart(true); // �t�F�[�h�A�E�g�J�n
                manager.PlayAudio("����{�^��������2"); // �����Đ�
            }
            if (manager.attentionPlane.GetComponent<UIFadeOut>().GetAlpha() <= 0)
            {
                manager.ChangeState(new TITLELOGO_State()); // �^�C�g�����S�X�e�[�g�ɑJ��
            }
        }

        public void Exit(TitleSceneManager manager)
        {
            manager.attentionPlane.SetActive(false); // �x���p�l�����\���ɂ���
        }
    }

    public class TITLELOGO_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            manager.titleLogoPlane.SetActive(true); // �^�C�g�����S�p�l����\������
            manager.titleLogoPlane.GetComponent<UIFadeOut>().SetFadeStart(true, true); // �t�F�[�h�C���J�n
            manager.PlayAudio("nc350109_WindowsImit"); // �����Đ�
        }

        public void Execute(TitleSceneManager manager)
        {
            if (manager.titleLogoPlane.GetComponent<UIFadeOut>().GetAlpha() >= 1)
            {
                // ���C���g���K�[�������ꂽ��
                if (Utility.IsAnyActionPressed(manager.handMainTrigger))
                {
                    manager.titleLogoPlane.GetComponent<UIFadeOut>().SetFadeStart(true); // �t�F�[�h�A�E�g�J�n
                    manager.PlayAudio("����{�^��������2"); // �����Đ�
                    manager.ChangeState(new EXIT_State()); // EXIT_State�ɑJ��
                }
            }
        }

        public void Exit(TitleSceneManager manager)
        {
            // �������Ȃ�
        }
    }

    public class EXIT_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            // �������Ȃ�
        }

        public void Execute(TitleSceneManager manager)
        {
            if (manager.titleLogoPlane.GetComponent<UIFadeOut>().GetAlpha() <= 0)
            {
                manager.ChangeScene(); // �V�[����ύX����
            }
        }

        public void Exit(TitleSceneManager manager)
        {
            // �������Ȃ�
        }
    }
}
