using UnityEngine;

public class MenuItemAnimationControll : MonoBehaviour
{
    private Animator animator; // アニメーターコンポーネント
    private bool isNeutral = false; // ニュートラルフラグ

    void Start()
    {
        // アニメーターコンポーネントを初期化
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Animatorがアタッチされていません。");
        }
    }

    void Update()
    {
        // ニュートラルでない場合、アニメーションを変更
        animator.SetBool("isNeutral", isNeutral);
    }

    // ニュートラルフラグを設定する
    public void SetIsNeutral(bool _isNeutral)
    {
        isNeutral = _isNeutral;
    }
}
