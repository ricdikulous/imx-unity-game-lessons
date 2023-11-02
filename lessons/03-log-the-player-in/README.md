# Lesson: Using Immutable Passport for Player Login

Welcome to this lesson on how to use the Immutable Passport for player login in your Unity project. In this tutorial, we will walk you through the steps to integrate the Immutable Passport into your game to allow players to log in seamlessly.

## Prerequisites

Before you begin, make sure you have already initialized the Immutable Passport in your project, if you haven't already you can follow the [lesson to initialise the Passport here](../02-initialise-the-immutable-passport/README.md). 

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
            loginButton.GetComponentInChildren<Text>().text = "Login!";
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
        bool connected = await passport.ConnectSilent();
        if (!connected)
        {
            Debug.Log("Failed to connect using saved credentials");
            await passport.Connect();
        }
    } else {
        await passport.Connect();
    }        
    manager.SwitchState("Loadout");
}
```

- This function is invoked when the player clicks the login button.
- It checks if the player has existing credentials using `passport.HasCredentialsSaved()`.
- If credentials exist, it attempts to log the player in seamlessly by calling `passport.ConnectSilent()`. If successful, it returns `true`.
- If the credentials are not valid, or there we no saved credentials, and the player must log in using the browser-based flow using `passport.Connect()`
- We then send the player to the main menu using `manager.SwitchState("Loadout")`


## Testing the New Flow

After implementing the Immutable Passport for player login, you can test the flow:

- Go to the same login screen as before, but now it's integrated with the Immutable Passport.
- When you press "Log In," it will trigger the proper flow.
- If you haven't logged into your game before, it will send you to the browser to complete the login request.
- New players can sign up for an Immutable account.
- If you already have an account, it will log you in after pressing "Get Started."

## Conclusion

Congratulations! You have successfully integrated the Immutable Passport for player login in your Unity game. In the next video, we will explore how to use the Immutable SDK to retrieve player profile information.!
