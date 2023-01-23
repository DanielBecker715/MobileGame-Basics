 using UnityEngine;
using UnityEngine.Events;

public class WaitForAnimationTrigger : MonoBehaviour
{/*
    private string OutputMessage;
    //Trigger will be checked during parameter
    public bool checkAnimationTriggerDropped(string message)
    {   //ADD HERE NEW TRIGGER
        //Event triggert by "FadeOut"
        if (message != null)
        {
            Debug.Log("Animation " + message + " finished");
            return true;
        } else
        {
            return false;
        }


    }*/
    private string OutputMessage;

    //Trigger will be checked during parameter
    public void FadeAnimationEvent(string message)
    {   //ADD HERE NEW TRIGGER
        //Event triggert by "FadeOut"
        if (message == "FadeOut")
        {
            OutputMessage = message;
        }
        //Event triggert by "FadeIn"
        if (message == "FadeIn")
        {
            OutputMessage = message;
        }
    }

    //Check if a custom string match with a trigger
    public bool checkAnimTriggerDropped(string message)
    {
        if (message == OutputMessage)
        {
            OutputMessage = null;
            return true;
        }
        else
        {
            OutputMessage = null;
            return false;
        }
    }
}
