# Lesson 3: Player Login with Immutable Passport

Welcome to this lesson on how to use the Immutable Passport for player login in your Unity project. In this tutorial, we will walk you through the steps to integrate the Immutable Passport into your game to allow players to log in seamlessly.

## Overview

Here's an overview of what we'll cover in this lesson:

1. Finding the login script
2. Updating the script to utilize Passport
3. Running the game to validate correct login

## Prerequisites

Before you begin, make sure you have already initialized the Immutable Passport in your project, if you haven't already you can follow the [lesson to initialize Passport here](../02-initialise-the-immutable-passport/README.md). 

## Locate or create the Login Script

To start, find the script responsible for the login functionality in your Unity project. In our Trash Dash project you can follow these steps to locate it:

- Navigate to your Unity project's `Assets` folder.
- Inside `Assets`, open the `Scripts` folder.
- Look for the `GameManager` folder and locate the `LoginState` script, which is used to provide functionality for the login screen.


## Update the Login Script

In the `LoginState` script, you will need to update it to use the Immutable Passport for player login. Follow these steps to make the necessary modifications:

### Import the Immutable library

```csharp
using Immutable.Passport;
```

### Create a field to hold your Passport instance.
```csharp
private Passport passport;
```

### In the "Tick" function, initialize the Passport field.
```csharp
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
```

This function is invoked each frame and is responsible for initializing the `passport` field.

- Initially, it checks if the `loginButton` is interactable, which is set initially set to `false` to prevent user interaction before the passport has initialized.
- It then assigns the globally available passport instance to the `passport` field.
- If the instance is null, it means the passport hasn't initialized yet, and it will retry in the next frame.
- Once the global instance is no longer null, indicating successful initialization, we make the `loginButton` interactable, and its text changes to "Login!".

### Implement the Login Function
```csharp
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
```

- This function is invoked when the player clicks the login button.
- It checks if the player has existing credentials using `passport.HasCredentialsSaved()`.
- If credentials exist, it attempts to log the player in seamlessly by calling `passport.Login(useCachedSession: true);`. If successful, it returns `true`.
- If the credentials are not valid or there are no saved credentials, then the player must log in using the browser-based flow using `passport.Login()` or `await passport.LoginPKCE()` if PKCE is needed
- We then connect them player to Immutable using `passport.ConnectImx()` or once again `passport.ConnectImxPKCE()` if PKCE is needed
- We then send the player to the main menu using `manager.SwitchState("Loadout")`


## Testing the New Flow

After implementing the Immutable Passport for player login, you can test the flow:

- Go to the same login screen as before, but now it's integrated with the Immutable Passport.
- When you press "Log In," it will trigger the proper flow.
- If you haven't logged into your game before, it will send you to the browser to complete the login request.
- New players can sign up for an Immutable account.
- If you already have an account, it will log you in after pressing "Get Started."

## Next Steps

Congratulations on successfully integrating the Immutable Passport for player login in your Unity game. In the next step, we'll dive into using the Immutable SDK to retrieve player profile information and manage player logout:

[Lesson 04: Retrieve Player Profile Data and Logout](../04-retrieve-player-data-and-logout/README.md)
