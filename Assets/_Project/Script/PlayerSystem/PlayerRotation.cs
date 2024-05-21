using UnityEngine;
using Valve.VR;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90.0f; // ��]���x
    [SerializeField] private SteamVR_Action_Boolean turnLeftInput; // ����]�A�N�V����
    [SerializeField] private SteamVR_Action_Boolean turnRightInput; // �E��]�A�N�V����

    void Update()
    {
        // ����]�A�N�V�������A�N�e�B�u�Ȃ獶�ɉ�]�A�E��]�A�N�V�������A�N�e�B�u�Ȃ�E�ɉ�]
        if (turnLeftInput != null && turnLeftInput.GetState(SteamVR_Input_Sources.Any))
        {
            Rotate(-rotationSpeed * Time.deltaTime);
        }
        else if (turnRightInput != null && turnRightInput.GetState(SteamVR_Input_Sources.Any))
        {
            Rotate(rotationSpeed * Time.deltaTime);
        }
    }

    private void Rotate(float angle)
    {
        // �v���C���[�i����GameObject�j���̂���]������
        transform.Rotate(Vector3.up, angle);
    }
}
