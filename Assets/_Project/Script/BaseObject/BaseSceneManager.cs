using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

// シーン管理クラスのベース
public class BaseSceneManager : MonoBehaviour
{
    private float timer = 0; // タイマー
    private SteamVR_LoadLevel loadLevel; // SteamVRのシーンロードクラス
    [SerializeField] private string sceneAsset; // ロードするシーンの名前
    [SerializeField] private GameObject cameraRig = null; // カメラリグ

    protected virtual void Start()
    {
        // Nullチェックを追加して、SteamVR_LoadLevelコンポーネントがアタッチされていることを確認
        loadLevel = GetComponent<SteamVR_LoadLevel>();
        if (loadLevel == null)
        {
            Debug.LogError("SteamVR_LoadLevelがアタッチされていません。");
        }
    }

    protected virtual void Update()
    {
        UpdateTimer();
    }

    //タイマーの更新
    protected void UpdateTimer()
    {
        if (timer > 0)
        {
            timer -= Time.deltaTime; // タイマーを更新
        }
    }

    // タイマーの設定
    protected void SetTimer(float _time = 0)
    {
        timer = _time; // タイマーを設定
    }

    // タイマーが終了したかどうかを取得
    protected bool GetTimeUp()
    {
        return timer <= 0; // タイマーが0以下ならtrueを返す
    }

    // シーンの変更
    protected void ChangeScene()
    {
        // Nullチェックを追加して、シーンアセットが設定されていることを確認
        if (sceneAsset != null)
        {
            string sceneName = sceneAsset;
            SceneManager.LoadScene(sceneName); // 指定したシーンに遷移
        }
        else
        {
            Debug.LogError("SceneAssetが設定されていません。");
        }
    }
}
