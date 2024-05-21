using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;

// タイトルシーンの管理クラス
public class TitleSceneManager : BaseSceneManager
{
    [SerializeField] private SteamVR_Action_Boolean handMainTrigger; // メイントリガー
    [SerializeField] private GameObject attentionPlane = null; // 警告パネル
    [SerializeField] private GameObject titleLogoPlane = null; // タイトルロゴパネル
    [SerializeField] private GameObject audioObj = null; // 音の格納コンテナ
    private ITitleState currentState; // 現在のステート

    protected override void Start()
    {

        base.Start(); // BaseSceneManagerのStartメソッドを実行
        ChangeState(new ENTRY_State()); // 初期状態をENTRY_Stateに変更
        attentionPlane.SetActive(false);
        titleLogoPlane.SetActive(false);
        Debug.Log("Start");
    }

    protected override void Update()
    {
        currentState?.Execute(this); // 現在のステートを実行
    }

    void PlayAudio(string _str)
    {
        AudioSource audio = audioObj.transform.Find(_str).GetComponent<AudioSource>(); // 音を再生
        audio?.Play(); // Nullチェックを追加して、オーディオが存在する場合のみ再生
    }

    // ステート変更関数
    public void ChangeState(ITitleState newState)
    {
        // 現在のステートの終了処理
        currentState?.Exit(this);
        // ステートを変更
        currentState = newState;
        // 新しいステートの初期実行
        currentState.Enter(this);
    }

    // タイトルステートのインターフェース
    public interface ITitleState
    {
        void Enter(TitleSceneManager manager); // ステートに入った時の処理
        void Execute(TitleSceneManager manager); // 毎フレーム実行する処理
        void Exit(TitleSceneManager manager); // ステートを終了する時の処理
    }

    // タイトルの各状態
    public class ENTRY_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            manager.attentionPlane.SetActive(false); // 警告パネルを非表示にする
            manager.titleLogoPlane.SetActive(false); // タイトルロゴパネルを非表示にする
        }

        public void Execute(TitleSceneManager manager)
        {
            manager.ChangeState(new ATTENTION_State()); // ATTENTION_Stateに遷移
        }

        public void Exit(TitleSceneManager manager)
        {
            // 何もしない
        }
    }

    public class ATTENTION_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            manager.attentionPlane.SetActive(true); // 警告パネルを表示する
        }

        public void Execute(TitleSceneManager manager)
        {
            if (Utility.IsAnyActionPressed(manager.handMainTrigger))
            {
                // メイントリガーが押されたら
                manager.attentionPlane.GetComponent<UIFadeOut>().SetFadeStart(true); // フェードアウト開始
                manager.PlayAudio("決定ボタンを押す2"); // 音を再生
            }
            if (manager.attentionPlane.GetComponent<UIFadeOut>().GetAlpha() <= 0)
            {
                manager.ChangeState(new TITLELOGO_State()); // タイトルロゴステートに遷移
            }
        }

        public void Exit(TitleSceneManager manager)
        {
            manager.attentionPlane.SetActive(false); // 警告パネルを非表示にする
        }
    }

    public class TITLELOGO_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            manager.titleLogoPlane.SetActive(true); // タイトルロゴパネルを表示する
            manager.titleLogoPlane.GetComponent<UIFadeOut>().SetFadeStart(true, true); // フェードイン開始
            manager.PlayAudio("nc350109_WindowsImit"); // 音を再生
        }

        public void Execute(TitleSceneManager manager)
        {
            if (manager.titleLogoPlane.GetComponent<UIFadeOut>().GetAlpha() >= 1)
            {
                // メイントリガーが押されたら
                if (Utility.IsAnyActionPressed(manager.handMainTrigger))
                {
                    manager.titleLogoPlane.GetComponent<UIFadeOut>().SetFadeStart(true); // フェードアウト開始
                    manager.PlayAudio("決定ボタンを押す2"); // 音を再生
                    manager.ChangeState(new EXIT_State()); // EXIT_Stateに遷移
                }
            }
        }

        public void Exit(TitleSceneManager manager)
        {
            // 何もしない
        }
    }

    public class EXIT_State : ITitleState
    {
        public void Enter(TitleSceneManager manager)
        {
            // 何もしない
        }

        public void Execute(TitleSceneManager manager)
        {
            if (manager.titleLogoPlane.GetComponent<UIFadeOut>().GetAlpha() <= 0)
            {
                manager.ChangeScene(); // シーンを変更する
            }
        }

        public void Exit(TitleSceneManager manager)
        {
            // 何もしない
        }
    }
}
