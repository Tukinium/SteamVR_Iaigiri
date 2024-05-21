using UnityEngine;

public class CountDownObject : MonoBehaviour
{
    private bool isCountDownStart = false; // �J�E���g�_�E�����J�n���ꂽ���ǂ����̃t���O
    private float countDown = 0; // �J�E���g�_�E���̎c�莞��
    [SerializeField] private GameObject[] countDownObject; // �J�E���g�_�E���̕\���I�u�W�F�N�g�̔z��

    // Start is called before the first frame update
    void Start()
    {
        // �J�E���g�_�E���̕\���I�u�W�F�N�g���\���ɂ���
        foreach (GameObject obj in countDownObject)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �J�E���g�_�E�����J�n����Ă���ꍇ
        if (isCountDownStart)
        {
            // �c�莞�Ԃ�����������
            countDown -= Time.deltaTime;

            // �J�E���g�_�E���̕\���I�u�W�F�N�g���\���ɂ���
            foreach (GameObject obj in countDownObject)
            {
                obj.SetActive(false);
            }

            // �c�莞�ԂɑΉ�����\���I�u�W�F�N�g��\������
            countDownObject[(int)countDown].SetActive(true);

            // �J�E���g�_�E�����I�������ꍇ
            if (countDown <= -0.01)
            {
                // �S�ẴJ�E���g�_�E���̕\���I�u�W�F�N�g���\���ɂ��ăJ�E���g�_�E�����I������
                foreach (GameObject obj in countDownObject)
                {
                    obj.SetActive(false);
                }
                isCountDownStart = false;
            }
        }
    }

    // �J�E���g�_�E�����J�n���郁�\�b�h
    public void StartCountDown()
    {
        isCountDownStart = true; // �J�E���g�_�E�����J�n���ꂽ�t���O�𗧂Ă�
        countDown = 4; // �J�E���g�_�E���̏����l��ݒ肷��
        this.GetComponent<AudioSource>().Play(); // �J�E���g�_�E���J�n���̌��ʉ����Đ�����
    }

    // �J�E���g�_�E���̎c�莞�Ԃ��擾���郁�\�b�h
    public float GetCountDown()
    {
        return countDown;
    }
}
