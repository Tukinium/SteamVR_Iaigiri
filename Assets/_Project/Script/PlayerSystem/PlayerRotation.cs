using UnityEngine;
using Valve.VR;

public class PlayerRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90.0f; // 回転速度
    [SerializeField] private SteamVR_Action_Boolean turnLeftInput; // 左回転アクション
    [SerializeField] private SteamVR_Action_Boolean turnRightInput; // 右回転アクション

    void Update()
    {
        // 左回転アクションがアクティブなら左に回転、右回転アクションがアクティブなら右に回転
        if (turnLeftInput != null && turnLeftInput.GetState(SteamVR_Input_Sources.Any))
        {
            Rotate(-rotationSpeed * Time.deltaTime);
        }
        else if (turnRightInput != null && turnRightInput.GetState(SteamVR_Input_Sources.Any))
        {
            Rotate(rotationSpeed * Time.deltaTime);
        }
    }

    private void Rotate(float angle)
    {
        // プレイヤー（このGameObject）自体を回転させる
        transform.Rotate(Vector3.up, angle);
    }
}
