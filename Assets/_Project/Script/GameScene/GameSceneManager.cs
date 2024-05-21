using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using Valve.VR;
using static TitleSceneManager;

public class GameSceneManager : BaseSceneManager
{
    [SerializeField] private AudioManager BGM1;
    [SerializeField] private AudioManager BGM2;
    [SerializeField] private SteamVR_Action_Boolean handMainTrigger;
    //出現するオブジェクト
    [SerializeField][Tooltip("出現するオブジェクト")] GameObject[] targetObjects;
    //制限時間
    [SerializeField][Tooltip("制限時間")] private float timeLimit = 60;
    //待機時間
    [SerializeField][Tooltip("待機時間")] private float intervalTime = 60;
    //成功回数
    private int SuccesePoint = 0;
    //失敗回数
    private int failePoint = 0;

    //切断すべき方向
    private Vector3 QuestionSlashVector;

    //出現したオブジェクト
    private GameObject spawendObject = null;

    [SerializeField][Tooltip("武器")] private GameObject weapon;
    [SerializeField][Tooltip("成功表示のオブジェクト")] GameObject SuccessMarkObject;
    [SerializeField][Tooltip("失敗表示のオブジェクト")] GameObject FailureMarkObject;
    [SerializeField][Tooltip("方向表示のオブジェクト")] GameObject ArrorMarkObject;
    [SerializeField][Tooltip("チュートリアルのオブジェクト")] GameObject HowPlayObject;
    [SerializeField][Tooltip("武器を持て！のオブジェクト")] GameObject GrabWeaponUIObject;
    [SerializeField][Tooltip("カウントダウンのオブジェクト")] GameObject CountDownUIObject;
    [SerializeField][Tooltip("リザルトのオブジェクト")] GameObject ResultObject;
    [SerializeField] GameObject objectSpawnPoint;

    int clearNum = 0;
    [SerializeField] int questionNum = 10;

    private Vector3[] directions = {
        Vector3.up, // 上
        Vector3.down, // 下
        Vector3.left, // 左
        Vector3.right, // 右
        (Vector3.up + Vector3.left), // 上左
        (Vector3.up + Vector3.right), // 上右
        (Vector3.down + Vector3.left) ,// 下左
        (Vector3.down + Vector3.right) // 下右
    };

    protected override void Start()
    {
        base.Start();

        GrabWeaponUIObject.SetActive(false);
        HowPlayObject.SetActive(false);
        CountDownUIObject.SetActive(false);
        ResultObject.SetActive(false);

        ResetObject();

        ChangeState(new Entry_State());
    }
    protected override void Update()
    {
        currentState?.Execute(this);
        UpdateTimer();
    }

    //現在のステート
    private IGameState currentState;
    //ステートの実装するべき関数
    public interface IGameState
    {
        //そのステートに入った時に実行
        void Enter(GameSceneManager manager);
        //Update関数内で実行
        void Execute(GameSceneManager manager);
        //終了時に実行
        void Exit(GameSceneManager manager);
    }
    public void ChangeState(IGameState newState)
    {
        //現在のステートの終了処理
        currentState?.Exit(this);
        //ステートを変更
        currentState = newState;
        //新しいステートの初期実行
        currentState.Enter(this);
    }

    public class Entry_State : IGameState
    {
        public void Enter(GameSceneManager manager)
        {

        }

        public void Execute(GameSceneManager manager)
        {
            manager.ChangeState(new HowPlay_State());
        }

        public void Exit(GameSceneManager manager)
        {

        }
    }
    public class HowPlay_State : IGameState
    {
        public void Enter(GameSceneManager manager)
        {
            manager.BGM1.NormalIn();
            manager.HowPlayObject.SetActive(true);
        }

        public void Execute(GameSceneManager manager)
        {
            if (Utility.IsAnyActionPressed(manager.handMainTrigger))
            {
                manager.ChangeState(new GrabWeapon_State());
            }
        }

        public void Exit(GameSceneManager manager)
        {
            manager.HowPlayObject.SetActive(false);
        }
    }
    public class GrabWeapon_State : IGameState
    {
        public void Enter(GameSceneManager manager)
        {
            manager.GrabWeaponUIObject.SetActive(true);
        }

        public void Execute(GameSceneManager manager)
        {
            if (manager.weapon.GetComponent<HoldObject>().IsHolding())
            {
                manager.ChangeState(new CountDown_State());
            }

        }

        public void Exit(GameSceneManager manager)
        {
            manager.BGM1.FadeOut();
            manager.GrabWeaponUIObject.SetActive(false);
        }
    }
    public class CountDown_State : IGameState
    {
        public void Enter(GameSceneManager manager)
        {
            manager.CountDownUIObject.SetActive(true);
            manager.CountDownUIObject.GetComponent<CountDownObject>().StartCountDown();
        }

        public void Execute(GameSceneManager manager)
        {
            if (manager.CountDownUIObject.GetComponent<CountDownObject>().GetCountDown() <= 0)
            {
                manager.ChangeState(new Question_State());
            }
        }

        public void Exit(GameSceneManager manager)
        {
            manager.CountDownUIObject?.SetActive(false);
            manager.BGM2.NormalIn();
        }
    }

    public class Question_State : IGameState
    {
        private float requiredAccuracy = 0.9f;

        public void Enter(GameSceneManager manager)
        {
            manager.ResetObject();
            manager.GenerateQuestion();
        }

        public void Execute(GameSceneManager manager)
        {
            if (manager.GetTimeUp())
            {
                manager.FailedCut();
                manager.ChangeState(new IntervalTime_State());
            }
            else
            {
                if (manager.weapon.GetComponent<SwingWeapon>().IsCutting)
                {
                    if (Vector3.Dot(manager.weapon.GetComponent<SwingWeapon>().GetSwingDirection(), manager.QuestionSlashVector) > requiredAccuracy)
                    {
                        manager.SucceseCut();
                    }
                    else
                    {
                        manager.FailedCut();
                    }
                    Utility.SetObjectGravity(manager.spawendObject, true);
                    manager.ChangeState(new IntervalTime_State());
                }
                else
                {
                    if (manager.spawendObject.transform.position.y <= 1.2)
                    {
                        manager.spawendObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        manager.spawendObject.GetComponent<Rigidbody>().useGravity = false;
                        manager.ArrorMarkObject.GetComponent<TrackingObject>().SetTrackingObject(null);
                    }
                }
            }
        }

        public void Exit(GameSceneManager manager)
        {
            manager.ArrorMarkObject.SetActive(false);
        }
    }

    public class IntervalTime_State : IGameState
    {
        public void Enter(GameSceneManager manager)
        {
            manager.SetTimer(manager.intervalTime);
        }

        public void Execute(GameSceneManager manager)
        {
            if (manager.GetTimeUp())
            {
                manager.ChangeState(new QuestionEnd_State());
            }
        }

        public void Exit(GameSceneManager manager)
        {
            manager.ResetObject();
        }
    }

    public class QuestionEnd_State : IGameState
    {
        public void Enter(GameSceneManager manager)
        {
            manager.clearNum++;
        }

        public void Execute(GameSceneManager manager)
        {
            if (manager.clearNum == manager.questionNum)
            {
                manager.BGM2.NormalOut();
                manager.ChangeState(new Result_State());
            }
            else
            {
                manager.ChangeState(new Question_State());
            }
        }

        public void Exit(GameSceneManager manager)
        {

        }
    }

    public class Result_State : IGameState
    {
        public void Enter(GameSceneManager manager)
        {
            manager.ResultObject.GetComponent<ResultObject>().SetScore(manager.SuccesePoint, manager.failePoint);
            manager.ResultObject.SetActive(true);
        }

        public void Execute(GameSceneManager manager)
        {
            if (Utility.IsAnyActionPressed(manager.handMainTrigger))
            {
                manager.ChangeScene();
            }
        }

        public void Exit(GameSceneManager manager)
        {

        }
    }

    void GenerateQuestion()
    {
        QuestionSlashVector = directions[Random.Range(0, directions.Length)];
        QuestionSlashVector.z = 0;
        QuestionSlashVector.Normalize();

        SetTimer(timeLimit);

        ArrorMarkObject.SetActive(true);
        ArrorMarkObject.GetComponent<ArrowMarkRotation>().SetVector(QuestionSlashVector);

        spawendObject = Instantiate(targetObjects[Random.Range(0, targetObjects.Length)], objectSpawnPoint.transform.position, Quaternion.identity, this.transform);

        ArrorMarkObject.GetComponent<TrackingObject>().SetTrackingObject(spawendObject);

        objectSpawnPoint.GetComponent<AudioSource>().Play();
    }

    void SucceseCut()
    {
        SuccessMarkObject.SetActive(true);
        SuccessMarkObject.GetComponents<AudioSource>()[0].Play();
        SuccessMarkObject.GetComponents<AudioSource>()[1].Play();
        SuccesePoint++;
    }

    void FailedCut()
    {
        FailureMarkObject.SetActive(true);
        FailureMarkObject.GetComponent<AudioSource>().Play();
        failePoint++;
    }

    void ResetObject()
    {
        weapon.GetComponent<SwingWeapon>().ResetCut();
        QuestionSlashVector = Vector2.zero;
        ArrorMarkObject.SetActive(false);
        SuccessMarkObject.SetActive(false);
        FailureMarkObject.SetActive(false);
        DestoryTargetObject();
    }

    void DestoryTargetObject()
    {
        spawendObject = null;
        GameObject[] allObjects = GameObject.FindObjectsOfType<GameObject>();
        foreach (GameObject obj in allObjects)
        {
            if (obj.tag == "Target")
            {
                Destroy(obj);
                allObjects = null;
            }
        }
    }
}
