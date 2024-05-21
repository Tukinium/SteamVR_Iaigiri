using UnityEngine;

public class MenuItemAnimationControll : MonoBehaviour
{
    private Animator animator; // �A�j���[�^�[�R���|�[�l���g
    private bool isNeutral = false; // �j���[�g�����t���O

    void Start()
    {
        // �A�j���[�^�[�R���|�[�l���g��������
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animator���A�^�b�`����Ă��܂���B");
        }
    }

    void Update()
    {
        // �j���[�g�����łȂ��ꍇ�A�A�j���[�V������ύX
        animator.SetBool("isNeutral", isNeutral);
    }

    // �j���[�g�����t���O��ݒ肷��
    public void SetIsNeutral(bool _isNeutral)
    {
        isNeutral = _isNeutral;
    }
}
