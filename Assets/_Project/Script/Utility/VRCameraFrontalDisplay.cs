using UnityEngine;

public class VRCameraFrontalDisplay : MonoBehaviour
{
    [SerializeField] private GameObject vrCamera = null; // VR カメラの参照
    [SerializeField] private float distance = 3.0f; // UI を VR カメラの正面に表示する距離

    // Update is called once per frame
    void Update()
    {
        // VR カメラが設定されていない場合は何もしない
        if (vrCamera == null) return;

        // VR カメラの位置と向きを取得
        Vector3 cameraPosition = vrCamera.transform.position;
        Quaternion cameraRotation = vrCamera.transform.rotation;

        // VR カメラの正面方向にオフセットを加えて UI の位置を計算
        Vector3 uiPosition = cameraPosition + cameraRotation * Vector3.forward * distance;

        // UI の位置を設定
        transform.position = uiPosition;

        // UI の向きを VR カメラと同じにする
        transform.rotation = cameraRotation;
    }
}
