using UnityEngine;

public class VRCameraFrontalDisplay : MonoBehaviour
{
    [SerializeField] private GameObject vrCamera = null; // VR �J�����̎Q��
    [SerializeField] private float distance = 3.0f; // UI �� VR �J�����̐��ʂɕ\�����鋗��

    // Update is called once per frame
    void Update()
    {
        // VR �J�������ݒ肳��Ă��Ȃ��ꍇ�͉������Ȃ�
        if (vrCamera == null) return;

        // VR �J�����̈ʒu�ƌ������擾
        Vector3 cameraPosition = vrCamera.transform.position;
        Quaternion cameraRotation = vrCamera.transform.rotation;

        // VR �J�����̐��ʕ����ɃI�t�Z�b�g�������� UI �̈ʒu���v�Z
        Vector3 uiPosition = cameraPosition + cameraRotation * Vector3.forward * distance;

        // UI �̈ʒu��ݒ�
        transform.position = uiPosition;

        // UI �̌����� VR �J�����Ɠ����ɂ���
        transform.rotation = cameraRotation;
    }
}
