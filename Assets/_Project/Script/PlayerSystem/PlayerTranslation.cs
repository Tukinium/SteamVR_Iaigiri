using UnityEngine;
using Valve.VR;

public class PlayerTranslation : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 0.5f; // 移動速度
    [SerializeField] private SteamVR_Action_Vector2 leftJoystickAction; // 左ジョイスティックのアクション

    private Transform vrCameraTransform; // VR カメラの Transform

    void Start()
    {
        vrCameraTransform = transform.Find("SteamVRObjects/VRCamera"); // VR カメラの Transform を取得
        if (vrCameraTransform == null)
        {
            Debug.LogError("VRCameraが見つかりません。パスを確認してください。");
        }

        // leftJoystickAction が null でないことを確認
        if (leftJoystickAction == null)
        {
            Debug.LogError("LeftJoystickアクションが設定されていません。");
        }
    }

    void Update()
    {
        // 左ジョイスティックの値を取得し、移動ベクトルを計算
        Vector2 joystickValue = leftJoystickAction.GetAxis(SteamVR_Input_Sources.LeftHand);
        Vector3 moveVec = vrCameraTransform.right * joystickValue.x + vrCameraTransform.forward * joystickValue.y;
        moveVec.y = 0; // 上下移動を除外
        moveVec = moveVec.normalized * (moveSpeed * Time.deltaTime);

        // プレイヤーを移動させる
        transform.Translate(moveVec, Space.World);
    }
}
