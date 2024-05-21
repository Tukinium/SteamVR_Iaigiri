using UnityEngine;

public class CountDownObject : MonoBehaviour
{
    private bool isCountDownStart = false; // カウントダウンが開始されたかどうかのフラグ
    private float countDown = 0; // カウントダウンの残り時間
    [SerializeField] private GameObject[] countDownObject; // カウントダウンの表示オブジェクトの配列

    // Start is called before the first frame update
    void Start()
    {
        // カウントダウンの表示オブジェクトを非表示にする
        foreach (GameObject obj in countDownObject)
        {
            obj.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // カウントダウンが開始されている場合
        if (isCountDownStart)
        {
            // 残り時間を減少させる
            countDown -= Time.deltaTime;

            // カウントダウンの表示オブジェクトを非表示にする
            foreach (GameObject obj in countDownObject)
            {
                obj.SetActive(false);
            }

            // 残り時間に対応する表示オブジェクトを表示する
            countDownObject[(int)countDown].SetActive(true);

            // カウントダウンが終了した場合
            if (countDown <= -0.01)
            {
                // 全てのカウントダウンの表示オブジェクトを非表示にしてカウントダウンを終了する
                foreach (GameObject obj in countDownObject)
                {
                    obj.SetActive(false);
                }
                isCountDownStart = false;
            }
        }
    }

    // カウントダウンを開始するメソッド
    public void StartCountDown()
    {
        isCountDownStart = true; // カウントダウンが開始されたフラグを立てる
        countDown = 4; // カウントダウンの初期値を設定する
        this.GetComponent<AudioSource>().Play(); // カウントダウン開始時の効果音を再生する
    }

    // カウントダウンの残り時間を取得するメソッド
    public float GetCountDown()
    {
        return countDown;
    }
}
