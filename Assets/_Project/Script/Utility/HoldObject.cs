using UnityEngine;
using Valve.VR;

public class HoldObject : MonoBehaviour
{
    [SerializeField] private SteamVR_Action_Boolean handGripTriggerAction; // 左グリップトリガーアクション
    [SerializeField] private float holdRange = 1.0f; // ホールド範囲
    [SerializeField] private GameObject leftHand, rightHand; // 左手と右手のゲームオブジェクト

    private bool isLeftHandHolding = false; // 左手がホールドしているかどうかのフラグ

    void Update()
    {
        CheckHold(); // ホールドのチェック
    }

    // ホールドをチェックする
    private void CheckHold()
    {
        if(Vector3.Distance(leftHand.transform.position,transform.position) < Vector3.Distance(rightHand.transform.position,transform.position))
        {
            if (TryHold(leftHand, handGripTriggerAction))
            {
                isLeftHandHolding = true; // 左手がホールドしている場合はフラグをtrueにする
            }
        }
        else
        {
            if (TryHold(rightHand, handGripTriggerAction))
            {
                isLeftHandHolding = false; // 右手がホールドしている場合はフラグをfalseにする
            }
        }
    }

    // オブジェクトをホールドするかどうかを試行する
    private bool TryHold(GameObject hand, SteamVR_Action_Boolean gripAction)
    {
        float distance = Vector3.Distance(hand.transform.position, transform.position); // オブジェクトと手の距離を計算
        if (distance <= holdRange && gripAction.GetState(SteamVR_Input_Sources.Any)) // 手がホールド範囲内にあり、グリップトリガーが押されている場合
        {
            transform.SetParent(hand.transform); // オブジェクトを手の子オブジェクトに設定する
            return true;
        }
        else
        {
            transform.SetParent(null); // オブジェクトの親を解除する
            return false;
        }
    }

    // 左手がホールドしているかどうかを返す
    public bool IsLeftHandHolding()
    {
        return isLeftHandHolding;
    }

    // どちらかの手がオブジェクトをホールドしているかどうかを返す
    public bool IsHolding()
    {
        return transform.parent != null;
    }
}
