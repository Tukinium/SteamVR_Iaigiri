using UnityEngine;
using Valve.VR;

public class PlayerTranslation : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // �ړ����x
    [SerializeField] private SteamVR_Action_Vector2 leftJoystickAction; // ���W���C�X�e�B�b�N�̃A�N�V����

    private Transform vrCameraTransform; // VR �J������ Transform

    void Start()
    {
        vrCameraTransform = transform.Find("SteamVRObjects/VRCamera"); // VR �J������ Transform ���擾
        if (vrCameraTransform == null)
        {
            Debug.LogError("VRCamera��������܂���B�p�X���m�F���Ă��������B");
        }

        // leftJoystickAction �� null �łȂ����Ƃ��m�F
        if (leftJoystickAction == null)
        {
            Debug.LogError("LeftJoystick�A�N�V�������ݒ肳��Ă��܂���B");
        }
    }

    void Update()
    {
        // ���W���C�X�e�B�b�N�̒l���擾���A�ړ��x�N�g�����v�Z
        Vector2 joystickValue = leftJoystickAction.GetAxis(SteamVR_Input_Sources.LeftHand);
        Vector3 moveVec = vrCameraTransform.right * joystickValue.x + vrCameraTransform.forward * joystickValue.y;
        moveVec.y = 0; // �㉺�ړ������O
        moveVec = moveVec.normalized * (moveSpeed * Time.deltaTime);

        // �v���C���[���ړ�������
        transform.Translate(moveVec, Space.World);
    }
}
