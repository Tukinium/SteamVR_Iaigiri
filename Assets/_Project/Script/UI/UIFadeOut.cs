using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
    private bool isFadeStart = false; // �t�F�[�h�J�n�t���O
    private Color newTint; // �V�����F
    private float alpha = 1.0f; // �A���t�@�l
    private bool isReversePlayback = false; // �t�Đ��t���O
    [SerializeField] private float alphaDelete = 0.01f; // �A���t�@�l�̕ω���
    [SerializeField] private GameObject UIObject;

    void Start()
    {
        newTint = Color.white; // �V�����F��������
    }

    void Update()
    {
        // �t�F�[�h���J�n���ꂽ�ꍇ
        if (isFadeStart)
        {
            // �t�Đ��̏ꍇ
            if (isReversePlayback)
            {
                if (alpha <= 1.0f)
                {
                    alpha += alphaDelete; // �A���t�@�l�𑝉�
                }
            }
            // �ʏ�Đ��̏ꍇ
            else
            {
                if (alpha >= 0.0f)
                {
                    alpha -= alphaDelete; // �A���t�@�l������
                }
            }

            Renderer renderer = UIObject.GetComponent<Renderer>(); // Renderer�R���|�[�l���g���擾
            Material material = renderer.material; // �}�e���A�����擾
            newTint.a = alpha; // �V�����A���t�@�l��ݒ�
            material.SetColor("_Color", newTint); // �}�e���A���̐F���X�V
        }
    }

    // �t�F�[�h���J�n����
    public void SetFadeStart(bool _start, bool _reversePlayback = false)
    {
        isFadeStart = _start; // �t�F�[�h�J�n�t���O��ݒ�
        isReversePlayback = _reversePlayback; // �t�Đ��t���O��ݒ�
        if (isReversePlayback) alpha = 0.0f; // �t�Đ��̏ꍇ�A�A���t�@�l��������
    }

    // �A���t�@�l���擾����
    public float GetAlpha()
    {
        return alpha;
    }

    // �A���t�@�l��ݒ肷��
    public void SetAlpha(float _alpha)
    {
        alpha = _alpha; // �A���t�@�l��ݒ�
        Renderer renderer = UIObject.GetComponent<Renderer>(); // Renderer�R���|�[�l���g���擾
        Material material = renderer.material; // �}�e���A�����擾
        newTint.a = alpha; // �V�����A���t�@�l��ݒ�
        material.SetColor("_Color", newTint); // �}�e���A���̐F���X�V
    }
}
