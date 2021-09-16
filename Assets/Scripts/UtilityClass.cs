using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UtilityClass : MonoBehaviour
{


    public enum UsedStaminaForAttack { daggerAttack = 5, arrowAttack = 5, swordAttack = 10, heavyAttack = 20 };

    //can't use enum of type String, what a joke...
    public const string bowType = "bow", swordType = "sword", axeType = "axe", daggerType = "dagger", potionType = "potion";

    public static readonly string FirstPlay = "FirstPlay";
    public static readonly string BackgroundPrefs = "BackgroundPrefs";
    public static readonly string MusicPrefs = "MusicPrefs";
    public static readonly string SFXPrefs = "SFXPrefs";

    public static Vector3 getMouseWorldPosition()
    {
        Vector3 vec = getMouseWorldPositonZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }

    private static Vector3 getMouseWorldPositonZ()
    {
        return getMouseWorldPositonZ(Input.mousePosition, Camera.main);

    }

    private static Vector3 getMouseWorldPositonZ(Camera worldCamera)
    {
        return getMouseWorldPositonZ(Input.mousePosition, worldCamera);
    }

    private static Vector3 getMouseWorldPositonZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosion = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosion;
    }


    public static Vector2 getMouseDirection(Transform transform)
    {
        var mouse = Input.mousePosition;
        var screenPoint = Camera.main.WorldToScreenPoint(transform.localPosition);

        var mouseDirection = new Vector2(mouse.x - screenPoint.x, mouse.y - screenPoint.y);
        return mouseDirection;
    }

    public static float getAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        if (n < 0) n += 360;

        return n;
    }


    public static void useStamina(StaminaScript staminaScript, double staminaUsed)
    {
        double staminaValue;
        String staminaTextCurrent = staminaScript.textMesh.text;

        staminaValue = Double.Parse(staminaTextCurrent);

        if (staminaValue > staminaUsed)
        {
            staminaValue -= staminaUsed;

            staminaScript.textMesh.text = staminaValue.ToString();
            staminaScript.setStamina((float)staminaValue);
            staminaScript.hasAttacked = false;
        }
    }

    public static bool ContainsWithLowercase(string source, string toCheck)
    {
        StringComparison defaultComp = StringComparison.OrdinalIgnoreCase; //maybe changed if needed
        return source?.IndexOf(toCheck, defaultComp) >= 0;
    }
}
