using UnityEngine;
using Valve.VR;

public class HoldObject : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean handGripTriggerAction; // ���O���b�v�g���K�[�A�N�V����
    [SerializeField] private float holdRange = 1.0f; // �z�[���h�͈�
    [SerializeField] private GameObject leftHand, rightHand; // ����ƉE��̃Q�[���I�u�W�F�N�g

    private bool isLeftHandHolding = false; // ���肪�z�[���h���Ă��邩�ǂ����̃t���O

    void Update()
    {
        CheckHold(); // �z�[���h�̃`�F�b�N
    }

    // �z�[���h���`�F�b�N����
    private void CheckHold()
    {
        if(Vector3.Distance(leftHand.transform.position,transform.position) < Vector3.Distance(rightHand.transform.position,transform.position))
        {
            if (TryHold(leftHand, handGripTriggerAction))
            {
                isLeftHandHolding = true; // ���肪�z�[���h���Ă���ꍇ�̓t���O��true�ɂ���
            }
        }
        else
        {
            if (TryHold(rightHand, handGripTriggerAction))
            {
                isLeftHandHolding = false; // �E�肪�z�[���h���Ă���ꍇ�̓t���O��false�ɂ���
            }
        }
    }

    // �I�u�W�F�N�g���z�[���h���邩�ǂ��������s����
    private bool TryHold(GameObject hand, SteamVR_Action_Boolean gripAction)
    {
        float distance = Vector3.Distance(hand.transform.position, transform.position); // �I�u�W�F�N�g�Ǝ�̋������v�Z
        if (distance <= holdRange && gripAction.GetState(SteamVR_Input_Sources.Any)) // �肪�z�[���h�͈͓��ɂ���A�O���b�v�g���K�[��������Ă���ꍇ
        {
            transform.SetParent(hand.transform); // �I�u�W�F�N�g����̎q�I�u�W�F�N�g�ɐݒ肷��
            return true;
        }
        else
        {
            transform.SetParent(null); // �I�u�W�F�N�g�̐e����������
            return false;
        }
    }

    // ���肪�z�[���h���Ă��邩�ǂ�����Ԃ�
    public bool IsLeftHandHolding()
    {
        return isLeftHandHolding;
    }

    // �ǂ��炩�̎肪�I�u�W�F�N�g���z�[���h���Ă��邩�ǂ�����Ԃ�
    public bool IsHolding()
    {
        return transform.parent != null;
    }
}
