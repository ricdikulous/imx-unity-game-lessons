using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using Immutable.Passport;


#if UNITY_ANALYTICS
using UnityEngine.Analytics;
#endif

/// <summary>
/// State pushed on the GameManager during the Login, where the player logs in using passport
/// </summary>
public class LoginState : AState
{
    public Canvas loginCanvas;
	[Header("Other Data")]
	public Button loginButton;

	public MeshFilter skyMeshFilter;
    public MeshFilter UIGroundFilter;

	public AudioClip menuTheme;

    private Passport passport;

    public override void Enter(AState from)
    {

        loginCanvas.gameObject.SetActive(true);
        skyMeshFilter.gameObject.SetActive(true);
        UIGroundFilter.gameObject.SetActive(true);

        // Reseting the global blinking value. Can happen if the game unexpectedly exited while still blinking
        Shader.SetGlobalFloat("_BlinkingValue", 0.0f);

        if (MusicPlayer.instance.GetStem(0) != menuTheme)
		{
            MusicPlayer.instance.SetStem(0, menuTheme);
            StartCoroutine(MusicPlayer.instance.RestartAllStems());
        }

        loginButton.interactable = false;
    }

    public override void Exit(AState to)
    {
        loginCanvas.gameObject.SetActive(false);
        skyMeshFilter.gameObject.SetActive(false);
        UIGroundFilter.gameObject.SetActive(false);
    }


    public override string GetName()
    {
        return "Login";
    }

    public override void Tick()
    {
        if (!loginButton.interactable)        
        {
            passport = Passport.Instance;
            if(passport != null)
            {
                loginButton.interactable = true;
                loginButton.GetComponentInChildren<Text>().text = "Sign in with Immutable";
            }
        }

    }
    public async void LoginUser() 
    {
        loginButton.GetComponentInChildren<Text>().text = "Logging in...";
        // Check if user's logged in before
        bool hasCredsSaved = await passport.HasCredentialsSaved();
        if (hasCredsSaved)
        {
            // Use existing credentials to connect to Passport
            Debug.Log("Connecting to Passport using saved credentials...");
            bool connected = await passport.Login(useCachedSession: true);
            if (!connected)
            {
                Debug.Log("Failed to connect using saved credentials");
                // macOS editor (play scene) does not support deeplinking
                #if UNITY_ANDROID || UNITY_IPHONE || (UNITY_STANDALONE_OSX && !UNITY_EDITOR_OSX)
                    await passport.LoginPKCE();
                #else
                    await passport.Login();
                #endif
            }
        } else {
            #if UNITY_ANDROID || UNITY_IPHONE || (UNITY_STANDALONE_OSX && !UNITY_EDITOR_OSX)
                await passport.LoginPKCE();
            #else
                await passport.Login();
            #endif
        }
        #if UNITY_ANDROID || UNITY_IPHONE || (UNITY_STANDALONE_OSX && !UNITY_EDITOR_OSX)
            await passport.ConnectImxPKCE();
        #else
            await passport.ConnectImx();
        #endif
        manager.SwitchState("Loadout");
    }

}
