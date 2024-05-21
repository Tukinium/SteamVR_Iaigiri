using UnityEngine;

public class ArrowMarkRotation : MonoBehaviour
{
    Vector2 slashVector = Vector2.zero; // ���̕����x�N�g��

    // Start is called before the first frame update
    void Start()
    {
        // �I�u�W�F�N�g�̏�����]�����Z�b�g
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        // �I�u�W�F�N�g�̉�]�����Z�b�g
        transform.rotation = Quaternion.identity;

        // �����x�N�g�����[���x�N�g���̏ꍇ�͉������Ȃ�
        if (slashVector == Vector2.zero) return;

        // �����x�N�g���𐳋K�����āA���̊p�x���v�Z���ăI�u�W�F�N�g����]������
        Quaternion q = Quaternion.Euler(0f, 0f, Mathf.Atan2(slashVector.x, slashVector.y) * Mathf.Rad2Deg);
        transform.rotation = q;
    }

    // ���̕����x�N�g����ݒ肷�郁�\�b�h
    public void SetVector(Vector2 _vec)
    {
        slashVector = _vec;
        slashVector.x *= -1;
    }
}
