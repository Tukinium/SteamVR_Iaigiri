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
    //�o������I�u�W�F�N�g
    [SerializeField][Tooltip("�o������I�u�W�F�N�g")] GameObject[] targetObjects;
    //��������
    [SerializeField][Tooltip("��������")] private float timeLimit = 60;
    //�ҋ@����
    [SerializeField][Tooltip("�ҋ@����")] private float intervalTime = 60;
    //������
    private int SuccesePoint = 0;
    //���s��
    private int failePoint = 0;

    //�ؒf���ׂ�����
    private Vector3 QuestionSlashVector;

    //�o�������I�u�W�F�N�g
    private GameObject spawendObject = null;

    [SerializeField][Tooltip("����")] private GameObject weapon;
    [SerializeField][Tooltip("�����\���̃I�u�W�F�N�g")] GameObject SuccessMarkObject;
    [SerializeField][Tooltip("���s�\���̃I�u�W�F�N�g")] GameObject FailureMarkObject;
    [SerializeField][Tooltip("�����\���̃I�u�W�F�N�g")] GameObject ArrorMarkObject;
    [SerializeField][Tooltip("�`���[�g���A���̃I�u�W�F�N�g")] GameObject HowPlayObject;
    [SerializeField][Tooltip("��������āI�̃I�u�W�F�N�g")] GameObject GrabWeaponUIObject;
    [SerializeField][Tooltip("�J�E���g�_�E���̃I�u�W�F�N�g")] GameObject CountDownUIObject;
    [SerializeField][Tooltip("���U���g�̃I�u�W�F�N�g")] GameObject ResultObject;
    [SerializeField] GameObject objectSpawnPoint;

    int clearNum = 0;
    [SerializeField] int questionNum = 10;

    private Vector3[] directions = {
        Vector3.up, // ��
        Vector3.down, // ��
        Vector3.left, // ��
        Vector3.right, // �E
        (Vector3.up + Vector3.left), // �㍶
        (Vector3.up + Vector3.right), // ��E
        (Vector3.down + Vector3.left) ,// ����
        (Vector3.down + Vector3.right) // ���E
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

    //���݂̃X�e�[�g
    private IGameState currentState;
    //�X�e�[�g�̎�������ׂ��֐�
    public interface IGameState
    {
        //���̃X�e�[�g�ɓ��������Ɏ��s
        void Enter(GameSceneManager manager);
        //Update�֐����Ŏ��s
        void Execute(GameSceneManager manager);
        //�I�����Ɏ��s
        void Exit(GameSceneManager manager);
    }
    public void ChangeState(IGameState newState)
    {
        //���݂̃X�e�[�g�̏I������
        currentState?.Exit(this);
        //�X�e�[�g��ύX
        currentState = newState;
        //�V�����X�e�[�g�̏������s
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
