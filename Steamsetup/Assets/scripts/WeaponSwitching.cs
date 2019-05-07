using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon;
    public Transform[] weapons;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (i == 0) 
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            if (selectedWeapon == 0)
            {
                selectedWeapon = 1;
                SelectWeapon();
            }else if (selectedWeapon == 1)
            {
                selectedWeapon = 0;
                SelectWeapon();
            }
        }
        
    }

    void SelectWeapon ()
    {
        for (int i =0; i<weapons.Length; i++)
        {
            if (selectedWeapon == i)
            {
                weapons[i].gameObject.SetActive(true);
            }
            else
            {
                weapons[i].gameObject.SetActive(false);
            }
        }
    }
}
