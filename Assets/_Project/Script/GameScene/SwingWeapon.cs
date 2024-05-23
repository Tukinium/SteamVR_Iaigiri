using DynamicMeshCutter;
using UnityEngine;
using Valve.VR;

public class SwingWeapon : MonoBehaviour
{
    [SerializeField] private SteamVR_Behaviour_Pose LHandPose; // ����� SteamVR �|�[�Y
    [SerializeField] private SteamVR_Behaviour_Pose RHandPose; // �E��� SteamVR �|�[�Y
    private SteamVR_Behaviour_Pose pose; // ���݂̎�� SteamVR �|�[�Y
    private Vector3 lastPosition; // �O��t���[���̈ʒu
    private Vector3 startPosition; // �U��n�߂̈ʒu
    private Vector3 currentPosition; // ���݂̈ʒu
    private Vector3 swingDirection; // �U��̕���

    private bool isCutting = false; // �ؒf�����ǂ����̃t���O

    void Update()
    {
        // ����������Ă����̃|�[�Y��ݒ肷��
        if (this.GetComponent<HoldObject>().IsLeftHandHolding())
        {
            pose = LHandPose;
        }
        else
        {
            pose = RHandPose;
        }
        currentPosition = pose.transform.position;
    }

    public Vector3 GetSwingDirection()
    {
        return swingDirection; // �U��̕�����Ԃ�
    }

    private void OnTriggerEnter(Collider other)
    {
        // �Փ˂����Ώۂ��^�[�Q�b�g�łȂ��ꍇ�͏������Ȃ�
        if (other.tag != "Target") return;

        startPosition = currentPosition; // �U��n�߂̈ʒu��ݒ�
    }

    private void OnTriggerExit(Collider other)
    {
        // �Փ˂����Ώۂ��^�[�Q�b�g�łȂ��ꍇ�A�܂��͊��ɐؒf���̏ꍇ�͏������Ȃ�
        if (other.tag != "Target" || isCutting) return;

        lastPosition = currentPosition; // �O��̃t���[���̈ʒu��ݒ�
        Vector3 direction = startPosition - lastPosition; // �U��̕������v�Z
        direction.z = 0; // Z�����̐����𖳎�����
        //direction.x *= -1; // X�����̐����𔽓]����i���E�̐U����l������j
        direction.Normalize(); // �����x�N�g���𐳋K������
        swingDirection = direction; // �U��̕�����ݒ�

        // �ؒf���ʂ̉����Đ�����
        this.GetComponent<AudioSource>().Play();

        other.GetComponent<AudioSource>().Play();

        // �^�[�Q�b�g�ɗ͂������Ĕ�΂�
        Vector3 force = new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5));
        other.GetComponent<Rigidbody>().AddForce(force);

        // PlaneBehaviour �R���|�[�l���g���g�p���Đؒf�����s����
        this.GetComponent<PlaneBehaviour>().Cut();

        // �^�[�Q�b�g�� Collider ���g���K�[�ł͂Ȃ�����i����������L���ɂ���j
        other.GetComponent<Collider>().isTrigger = false;

        isCutting = true; // �ؒf���t���O��ݒ�
    }

    public bool IsCutting
    {
        get { return isCutting; }
    }

    // �ؒf�����Z�b�g���郁�\�b�h
    public void ResetCut()
    {
        swingDirection = Vector3.zero; // �U��̕��������Z�b�g
        isCutting = false; // �ؒf���t���O�����Z�b�g
    }
}
