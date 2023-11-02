using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Immutable.Passport;

public class InitPassport : MonoBehaviour
{
    private Passport passport;


    async void Start()
    {
        string clientId = "ZJL7JvetcDFBNDlgRs5oJoxuAUUl6uQj";
        string environment = Immutable.Passport.Model.Environment.SANDBOX;
        string? redirectUri = null;

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_STANDALONE_OSX
        redirectUri = "trashdash://callback";
#endif

        passport = await Passport.Init(clientId, environment, redirectUri);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
