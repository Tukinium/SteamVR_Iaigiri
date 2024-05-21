using UnityEngine;

public class AllwaysBlink : MonoBehaviour
{
    private Color newTint = Color.white; // �V�����F
    private float alpha = 1.0f; // �A���t�@�l
    private bool isReversePlayback = false; // �t�Đ��t���O
    [SerializeField] private float alphaDelete = 0.01f; // �A���t�@�l�̕ω���

    // Update is called once per frame
    void Update()
    {
        // �t�Đ�
        if (isReversePlayback)
        {
            if (alpha <= 1)
            {
                alpha += alphaDelete; // �A���t�@�l�𑝉�
            }
            else
            {
                isReversePlayback = false; // �t�Đ��t���O�𖳌��ɂ���
            }
        }
        else
        {
            if (alpha >= 0)
            {
                alpha -= alphaDelete; // �A���t�@�l������
            }
            else
            {
                isReversePlayback = true; // �t�Đ��t���O��L���ɂ���
            }
        }

        Renderer renderer = GetComponent<Renderer>(); // Renderer�R���|�[�l���g���擾
        Material material = renderer.material; // �}�e���A�����擾
        newTint.a = alpha; // �V�����A���t�@�l��ݒ�
        material.SetColor("_Color", newTint); // �}�e���A���̐F���X�V
    }
}
