using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class flashScreen : MonoBehaviour {

    Image flash;
    public float getToNormalScreenColorSpeed = 5;

	void Start ()
    {
        flash = GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (flash.color.a > 0)
        {
            Color tempMoreTransparetnScreenColor = new Color(flash.color.r, flash.color.g, flash.color.b, 0);
            flash.color = Color.Lerp(flash.color, tempMoreTransparetnScreenColor, getToNormalScreenColorSpeed * Time.deltaTime);
        }
		
	}

    public void tookDamage()
    {
        flash.color = new Color(1, 0, 0, 0.7f);
    }
    public void tookBonus()
    {
        flash.color = new Color(0, 1, 0, 0.7f);
    }
}
