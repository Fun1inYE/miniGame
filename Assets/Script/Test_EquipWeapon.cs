using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Test_EquipWeapon : MonoBehaviour
{
    public Button swordButton;
    public Button axeButton;

    private void Start()
    {
        swordButton.onClick.AddListener(() =>
        {
            MessageManager.Instance.Send(MessageDefine.EQUIPMENT_WEAPON, WeaponFactory.CreateAWoodSword());
        });

        axeButton.onClick.AddListener(() =>
        {
            MessageManager.Instance.Send(MessageDefine.EQUIPMENT_WEAPON, WeaponFactory.CreateAFlyingKnifeEmitter());
        });
    }
}
