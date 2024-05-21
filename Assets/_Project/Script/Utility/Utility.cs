using UnityEngine;
using Valve.VR;

public class Utility : MonoBehaviour
{
    // 任意の手において指定したアクションがどちらかの手で押されているかを返す
    public static bool IsAnyActionPressed(SteamVR_Action_Boolean handAction)
    {
        return handAction.GetState(SteamVR_Input_Sources.Any);
    }

    // ランダムな真偽値を返す
    public static bool GetRandomBool()
    {
        return Random.Range(0, 2) == 0;
    }

    // オブジェクトの重力の有効/無効を設定する
    public static void SetObjectGravity(GameObject obj, bool activeGravity)
    {
        if (obj == null || obj.GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning(obj != null ? obj.name + " has no Rigidbody component." : "Object is null.");
            return;
        }
        obj.GetComponent<Rigidbody>().useGravity = activeGravity;
    }

    // オブジェクトが落下中かどうかを返す
    public static bool IsFalling(GameObject obj)
    {
        if (obj == null || obj.GetComponent<Rigidbody>() == null)
        {
            Debug.LogWarning(obj != null ? obj.name + " has no Rigidbody component." : "Object is null.");
            return false;
        }
        return obj.GetComponent<Rigidbody>().velocity.magnitude > 0;
    }
}
