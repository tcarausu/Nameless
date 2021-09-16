using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//following CodeMonkey Utils
public class FunctionPeriodicForUI
{

    private class MonoBehaviourHook : MonoBehaviour
    {

        public Action OnUpdate;

        private void Update()
        {
            if (OnUpdate != null) OnUpdate();
        }

    }


    // Holds a reference to all active timers
    private static List<FunctionPeriodicForUI> funcList;

    // Global game object used for initializing class, is destroyed on scene change
    private static GameObject initGameObject; 

    private static void InitIfNeeded()
    {
        if (initGameObject == null)
        {
            initGameObject = new GameObject("FunctionPeriodic_Global");
            funcList = new List<FunctionPeriodicForUI>();
        }
    }

    public static FunctionPeriodicForUI Create(Action action, float timer)
    {
        return Create(action, null, timer, "", false, false, false);
    }

    public static FunctionPeriodicForUI Create(Action action, Func<bool> testDestroy, float timer, string functionName, bool useUnscaledDeltaTime, bool triggerImmediately, bool stopAllWithSameName)
    {
        InitIfNeeded();

        if (stopAllWithSameName)
        {
            StopAllFunc(functionName);
        }

        GameObject gameObject = new GameObject("FunctionPeriodic Object " + functionName, typeof(MonoBehaviourHook));
        FunctionPeriodicForUI functionPeriodic = new FunctionPeriodicForUI(gameObject, action, timer, testDestroy, functionName, useUnscaledDeltaTime);
        gameObject.GetComponent<MonoBehaviourHook>().OnUpdate = functionPeriodic.Update;

        funcList.Add(functionPeriodic);

        if (triggerImmediately) action();

        return functionPeriodic;
    }




    public static void RemoveTimer(FunctionPeriodicForUI funcTimer)
    {
        InitIfNeeded();
        funcList.Remove(funcTimer);
    }
    public static void StopAllFunc(string _name)
    {
        InitIfNeeded();
        for (int i = 0; i < funcList.Count; i++)
        {
            if (funcList[i].functionName == _name)
            {
                funcList[i].DestroySelf();
                i--;
            }
        }
    }

    private GameObject gameObject;
    private float timer;
    private float baseTimer;
    private bool useUnscaledDeltaTime;
    private string functionName;
    public Action action;
    public Func<bool> testDestroy;


    private FunctionPeriodicForUI(GameObject gameObject, Action action, float timer, Func<bool> testDestroy, string functionName, bool useUnscaledDeltaTime)
    {
        this.gameObject = gameObject;
        this.action = action;
        this.timer = timer;
        this.testDestroy = testDestroy;
        this.functionName = functionName;
        this.useUnscaledDeltaTime = useUnscaledDeltaTime;
        baseTimer = timer;
    }

    private void Update()
    {
        if (useUnscaledDeltaTime)
        {
            timer -= Time.unscaledDeltaTime;
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if (timer <= 0)
        {
            action();
            if (testDestroy != null && testDestroy())
            {
                //Destroy
                DestroySelf();
            }
            else
            {
                //Repeat
                timer += baseTimer;
            }
        }
    }

    public void DestroySelf()
    {
        RemoveTimer(this);
        if (gameObject != null)
        {
            UnityEngine.Object.Destroy(gameObject);
        }
    }
}
