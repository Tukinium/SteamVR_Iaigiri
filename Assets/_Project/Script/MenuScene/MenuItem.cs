using UnityEditor;
using UnityEngine;
using Valve.VR;

public class MenuItem : MonoBehaviour
{
    [SerializeField] private MenuItemAnimationControll AnimationObject; // アニメーションオブジェクト
    [SerializeField] private string sceneAsset; // ロードするシーンのアセット

    // オブジェクトがマウスポインターに重なった時の処理
    public void OnPointerEnter()
    {
        if (AnimationObject != null)
        {
            AnimationObject.SetIsNeutral(false); // アニメーションを非ニュートラルに設定
        }
        transform.localScale = new Vector3(1.2f, 1.2f, 1.2f); // スケールを拡大
    }

    // オブジェクトがクリックされた時の処理
    public void OnPointerClick()
    {
        if (GetComponent<AudioSource>() != null)
        {
            GetComponent<AudioSource>().Play(); // オーディオを再生
        }

        if (sceneAsset != null)
        {
            if(sceneAsset ==  "Exit")
            {
                Application.Quit();
            }
            SteamVR_LoadLevel.Begin(sceneAsset); // 指定したシーンに遷移
        }
        else
        {
            Debug.LogError("SceneAssetが設定されていません。");
        }
    }

    // オブジェクトからマウスポインターが離れた時の処理
    public void OnPointerExit()
    {
        if (AnimationObject != null)
        {
            AnimationObject.SetIsNeutral(true); // アニメーションをニュートラルに設定
        }
        transform.localScale = new Vector3(1.0f, 1.0f, 1.0f); // スケールを元に戻す
    }
}
