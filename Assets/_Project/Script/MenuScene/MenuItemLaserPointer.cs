using UnityEngine;
using Valve.VR.Extras;

public class MenuItemLaserPointer : SteamVR_LaserPointer
{
    // ポインターがオブジェクトをクリックした時の処理
    public override void OnPointerClick(PointerEventArgs e)
    {
        MenuItem menuItem = e.target.GetComponent<MenuItem>(); // クリックしたオブジェクトがMenuItemコンポーネントを持っているかチェック
        if (menuItem != null)
        {
            menuItem.OnPointerClick(); // MenuItemのOnPointerClickメソッドを実行
        }
    }

    // ポインターがオブジェクトから出た時の処理
    public override void OnPointerOut(PointerEventArgs e)
    {
        MenuItem menuItem = e.target.GetComponent<MenuItem>(); // オブジェクトがMenuItemコンポーネントを持っているかチェック
        if (menuItem != null)
        {
            menuItem.OnPointerExit(); // MenuItemのOnPointerExitメソッドを実行
        }
    }

    // ポインターがオブジェクトに入った時の処理
    public override void OnPointerIn(PointerEventArgs e)
    {
        MenuItem menuItem = e.target.GetComponent<MenuItem>(); // オブジェクトがMenuItemコンポーネントを持っているかチェック
        if (menuItem != null)
        {
            menuItem.OnPointerEnter(); // MenuItemのOnPointerEnterメソッドを実行
        }
    }
}
