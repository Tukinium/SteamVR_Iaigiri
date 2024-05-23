using UnityEngine;

public class UIFadeOut : MonoBehaviour
{
    private bool isFadeStart = false; // フェード開始フラグ
    private Color newTint; // 新しい色
    private float alpha = 1.0f; // アルファ値
    private bool isReversePlayback = false; // 逆再生フラグ
    [SerializeField] private float alphaDelete = 0.01f; // アルファ値の変化量
    [SerializeField] private GameObject UIObject;

    void Start()
    {
        newTint = Color.white; // 新しい色を初期化
    }

    void Update()
    {
        // フェードが開始された場合
        if (isFadeStart)
        {
            // 逆再生の場合
            if (isReversePlayback)
            {
                if (alpha <= 1.0f)
                {
                    alpha += alphaDelete; // アルファ値を増加
                }
            }
            // 通常再生の場合
            else
            {
                if (alpha >= 0.0f)
                {
                    alpha -= alphaDelete; // アルファ値を減少
                }
            }

            Renderer renderer = UIObject.GetComponent<Renderer>(); // Rendererコンポーネントを取得
            Material material = renderer.material; // マテリアルを取得
            newTint.a = alpha; // 新しいアルファ値を設定
            material.SetColor("_Color", newTint); // マテリアルの色を更新
        }
    }

    // フェードを開始する
    public void SetFadeStart(bool _start, bool _reversePlayback = false)
    {
        isFadeStart = _start; // フェード開始フラグを設定
        isReversePlayback = _reversePlayback; // 逆再生フラグを設定
        if (isReversePlayback) alpha = 0.0f; // 逆再生の場合、アルファ値を初期化
    }

    // アルファ値を取得する
    public float GetAlpha()
    {
        return alpha;
    }

    // アルファ値を設定する
    public void SetAlpha(float _alpha)
    {
        alpha = _alpha; // アルファ値を設定
        Renderer renderer = UIObject.GetComponent<Renderer>(); // Rendererコンポーネントを取得
        Material material = renderer.material; // マテリアルを取得
        newTint.a = alpha; // 新しいアルファ値を設定
        material.SetColor("_Color", newTint); // マテリアルの色を更新
    }
}
