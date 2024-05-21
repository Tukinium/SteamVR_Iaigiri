using UnityEngine;

public class AllwaysBlink : MonoBehaviour
{
    private Color newTint = Color.white; // 新しい色
    private float alpha = 1.0f; // アルファ値
    private bool isReversePlayback = false; // 逆再生フラグ
    [SerializeField] private float alphaDelete = 0.01f; // アルファ値の変化量

    // Update is called once per frame
    void Update()
    {
        // 逆再生
        if (isReversePlayback)
        {
            if (alpha <= 1)
            {
                alpha += alphaDelete; // アルファ値を増加
            }
            else
            {
                isReversePlayback = false; // 逆再生フラグを無効にする
            }
        }
        else
        {
            if (alpha >= 0)
            {
                alpha -= alphaDelete; // アルファ値を減少
            }
            else
            {
                isReversePlayback = true; // 逆再生フラグを有効にする
            }
        }

        Renderer renderer = GetComponent<Renderer>(); // Rendererコンポーネントを取得
        Material material = renderer.material; // マテリアルを取得
        newTint.a = alpha; // 新しいアルファ値を設定
        material.SetColor("_Color", newTint); // マテリアルの色を更新
    }
}
