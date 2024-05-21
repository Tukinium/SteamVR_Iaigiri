using UnityEngine;

public class TrackingObject : MonoBehaviour
{
    [SerializeField] private GameObject obj; // �ǐՂ���I�u�W�F�N�g

    // Update is called once per frame
    void Update()
    {
        // �ǐՂ���I�u�W�F�N�g�����݂��Ȃ��ꍇ�͏������I������
        if (obj == null) return;

        // ���g�̈ʒu��ǐՂ���I�u�W�F�N�g�̈ʒu�ɍ��킹��
        transform.position = obj.transform.position;
    }

    // �ǐՂ���I�u�W�F�N�g��ݒ肷��
    public void SetTrackingObject(GameObject _obj)
    {
        obj = _obj;
    }
}
