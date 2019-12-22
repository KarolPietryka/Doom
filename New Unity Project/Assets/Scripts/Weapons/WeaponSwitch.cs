using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSwitch : MonoBehaviour {

    public List<Transform> weapons;
    public int initialWeapon;
    private int selectedWeapon;
    public bool autoFill;

    private void Awake()
    {
        if (autoFill)
        {
            weapons.Clear();
            foreach (Transform weapon in transform)//all subobject of object transform (object is weapons)
            {
                weapons.Add(weapon);
            }
        }
    }
    void Start ()
    {
        selectedWeapon = initialWeapon % weapons.Count;
        updateWeapon();
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            selectedWeapon = (selectedWeapon + 1) % weapons.Count;
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            if (selectedWeapon == 0)
            {
                selectedWeapon = weapons.Count;
            }
            else
            {
                selectedWeapon = (selectedWeapon - 1) % weapons.Count;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) && weapons.Count > 1)
        {
            selectedWeapon = 1;
        }

        updateWeapon();
    }

    void updateWeapon()
    {
        for (int i = 0; i < weapons.Count; i++)
        {
            if (i == selectedWeapon)
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
