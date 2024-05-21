using TMPro;
using UnityEngine;

public class ResultObject : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText_RankObject; // �����N��\������e�L�X�g�I�u�W�F�N�g
    [SerializeField] TMP_Text ScoreText_SuccessObject; // ��������\������e�L�X�g�I�u�W�F�N�g
    [SerializeField] TMP_Text ScoreText_FailObject; // ���s����\������e�L�X�g�I�u�W�F�N�g
    private int successCount; // ������
    private int failCount; // ���s��
    private string scoreRank; // �����N

    // Update is called once per frame
    void Update()
    {
        // �������Ɋ�Â��ă����N��ݒ肵�A�Ή�����F��ݒ肵�܂�
        if (successCount == 10)
        {
            scoreRank = "S";
            ScoreText_RankObject.color = Color.yellow;
        }
        else if (successCount >= 7 && successCount <= 9)
        {
            scoreRank = "A";
            ScoreText_RankObject.color = Color.red;
        }
        else if (successCount >= 3 && successCount <= 6)
        {
            scoreRank = "B";
            ScoreText_RankObject.color = Color.blue;
        }
        else if (successCount <= 3)
        {
            scoreRank = "C";
            ScoreText_RankObject.color = Color.green;
        }

        // �e�L�X�g�I�u�W�F�N�g�ɐ������A���s���A�����N��\�����܂�
        ScoreText_SuccessObject.text = "���� " + successCount.ToString();
        ScoreText_FailObject.text = "���s " + failCount.ToString();
        ScoreText_RankObject.text = "Rank " + scoreRank;
    }

    // �������Ǝ��s����ݒ肷�郁�\�b�h
    public void SetScore(int _success, int _fail)
    {
        successCount = _success;
        failCount = _fail;
    }
}
