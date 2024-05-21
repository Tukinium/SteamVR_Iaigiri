using TMPro;
using UnityEngine;

public class ResultObject : MonoBehaviour
{
    [SerializeField] TMP_Text ScoreText_RankObject; // ランクを表示するテキストオブジェクト
    [SerializeField] TMP_Text ScoreText_SuccessObject; // 成功数を表示するテキストオブジェクト
    [SerializeField] TMP_Text ScoreText_FailObject; // 失敗数を表示するテキストオブジェクト
    private int successCount; // 成功数
    private int failCount; // 失敗数
    private string scoreRank; // ランク

    // Update is called once per frame
    void Update()
    {
        // 成功数に基づいてランクを設定し、対応する色を設定します
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

        // テキストオブジェクトに成功数、失敗数、ランクを表示します
        ScoreText_SuccessObject.text = "成功 " + successCount.ToString();
        ScoreText_FailObject.text = "失敗 " + failCount.ToString();
        ScoreText_RankObject.text = "Rank " + scoreRank;
    }

    // 成功数と失敗数を設定するメソッド
    public void SetScore(int _success, int _fail)
    {
        successCount = _success;
        failCount = _fail;
    }
}
