// Copyright (c) Valve Corporation, All rights reserved. ======================================================================================================



using UnityEngine;
using System.Collections;

namespace Valve.VR.InteractionSystem
{
    public class SmoothTurn : MonoBehaviour
    {
        public float rotationSpeed = 30.0f; // 秒速度での回転速度

        public SteamVR_Action_Boolean turnLeftAction = SteamVR_Input.GetBooleanAction("TurnLeft");
        public SteamVR_Action_Boolean turnRightAction = SteamVR_Input.GetBooleanAction("TurnRight");

        private void Update()
        {
            Player player = Player.instance;

            if (turnLeftAction != null && turnRightAction != null)
            {
                if (turnLeftAction.GetState(SteamVR_Input_Sources.Any))
                {
                    // 左に回転
                    RotatePlayer(-rotationSpeed * Time.deltaTime);
                }
                else if (turnRightAction.GetState(SteamVR_Input_Sources.Any))
                {
                    // 右に回転
                    RotatePlayer(rotationSpeed * Time.deltaTime);
                }
            }
        }

        public void RotatePlayer(float angle)
        {
            Player player = Player.instance;
            player.transform.Rotate(Vector3.up, angle);
        }
    }
}
