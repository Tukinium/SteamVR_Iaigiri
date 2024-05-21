using UnityEngine;

public class ArrowMarkRotation : MonoBehaviour
{
    Vector2 slashVector = Vector2.zero; // 矢印の方向ベクトル

    // Start is called before the first frame update
    void Start()
    {
        // オブジェクトの初期回転をリセット
        transform.rotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトの回転をリセット
        transform.rotation = Quaternion.identity;

        // 方向ベクトルがゼロベクトルの場合は何もしない
        if (slashVector == Vector2.zero) return;

        // 方向ベクトルを正規化して、その角度を計算してオブジェクトを回転させる
        Quaternion q = Quaternion.Euler(0f, 0f, Mathf.Atan2(slashVector.x, slashVector.y) * Mathf.Rad2Deg);
        transform.rotation = q;
    }

    // 矢印の方向ベクトルを設定するメソッド
    public void SetVector(Vector2 _vec)
    {
        slashVector = _vec;
        slashVector.x *= -1;
    }
}
