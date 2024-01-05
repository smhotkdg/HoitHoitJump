using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirebaseNotificationContorller : MonoBehaviour
{
    // Start is called before the first frame update

    private static FirebaseNotificationContorller _instance = null;

    public static FirebaseNotificationContorller Instance
    {
        get
        {
            if (_instance == null)
                Debug.LogError("cSingleton FirebaseNotificationContorller == null");
            return _instance;
        }
    }

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    void Start()
    {
        Firebase.Messaging.FirebaseMessaging.TokenReceived += FirebaseMessaging_TokenReceived;
        Firebase.Messaging.FirebaseMessaging.MessageReceived += FirebaseMessaging_MessageReceived;
    }

    private void FirebaseMessaging_TokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
    {
        Debug.Log("Recevied Registration Token : " + token.Token);
    }

    private void FirebaseMessaging_MessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
    {
        Debug.Log("Received a new message from : " + e.Message.From);
    }

    public void DIgDepth()
    {
      
    }
    public void ShowAds()
    {

    }
}
