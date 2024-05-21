using UnityEngine;
using Valve.VR.Extras;

public class MenuItemLaserPointer : SteamVR_LaserPointer
{
    // �|�C���^�[���I�u�W�F�N�g���N���b�N�������̏���
    public override void OnPointerClick(PointerEventArgs e)
    {
        MenuItem menuItem = e.target.GetComponent<MenuItem>(); // �N���b�N�����I�u�W�F�N�g��MenuItem�R���|�[�l���g�������Ă��邩�`�F�b�N
        if (menuItem != null)
        {
            menuItem.OnPointerClick(); // MenuItem��OnPointerClick���\�b�h�����s
        }
    }

    // �|�C���^�[���I�u�W�F�N�g����o�����̏���
    public override void OnPointerOut(PointerEventArgs e)
    {
        MenuItem menuItem = e.target.GetComponent<MenuItem>(); // �I�u�W�F�N�g��MenuItem�R���|�[�l���g�������Ă��邩�`�F�b�N
        if (menuItem != null)
        {
            menuItem.OnPointerExit(); // MenuItem��OnPointerExit���\�b�h�����s
        }
    }

    // �|�C���^�[���I�u�W�F�N�g�ɓ��������̏���
    public override void OnPointerIn(PointerEventArgs e)
    {
        MenuItem menuItem = e.target.GetComponent<MenuItem>(); // �I�u�W�F�N�g��MenuItem�R���|�[�l���g�������Ă��邩�`�F�b�N
        if (menuItem != null)
        {
            menuItem.OnPointerEnter(); // MenuItem��OnPointerEnter���\�b�h�����s
        }
    }
}
