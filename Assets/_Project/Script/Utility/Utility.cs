using UnityEngine;
using Valve.VR;

public class Utility : MonoBehaviour
{
    // �C�ӂ̎�ɂ����Ďw�肵���A�N�V�������ǂ��炩�̎�ŉ�����Ă��邩��Ԃ�
    public static bool IsAnyActionPressed(SteamVR_Action_Boolean handAction)
    {
        return handAction.GetState(SteamVR_Input_Sources.Any);
    }

    // �����_���Ȑ^�U�l��Ԃ�
    public static bool GetRandomBool()
    {
        return Random.Range(0, 2) == 0;
    }

    // �I�u�W�F�N�g�̏d�̗͂L��/������ݒ肷��
    public static void SetObjectGravity(GameObject obj, bool activeGravity)
    {
        if (obj == null || obj.GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning(obj != null ? obj.name + " has no Rigidbody component." : "Object is null.");
            return;
        }
        obj.GetComponent<Rigidbody>().useGravity = activeGravity;
    }

    // �I�u�W�F�N�g�����������ǂ�����Ԃ�
    public static bool IsFalling(GameObject obj)
    {
        if (obj == null || obj.GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning(obj != null ? obj.name + " has no Rigidbody component." : "Object is null.");
            return false;
        }
        return obj.GetComponent<Rigidbody>().velocity.magnitude > 0;
    }
}
