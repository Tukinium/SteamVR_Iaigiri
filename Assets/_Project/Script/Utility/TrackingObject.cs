using UnityEngine;

public class TrackingObject : MonoBehaviour
{
    [SerializeField] private GameObject obj; // 追跡するオブジェクト

    // Update is called once per frame
    void Update()
    {
        // 追跡するオブジェクトが存在しない場合は処理を終了する
        if (obj == null) return;

        // 自身の位置を追跡するオブジェクトの位置に合わせる
        transform.position = obj.transform.position;
    }

    // 追跡するオブジェクトを設定する
    public void SetTrackingObject(GameObject _obj)
    {
        obj = _obj;
    }
}
