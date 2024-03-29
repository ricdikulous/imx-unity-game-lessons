# Lesson 4: Retrieve Player Profile Data and Logout

In this lesson, we will demonstrate how to use Immutable Passport for retrieving a player's profile information and implement the functionality to log the player out. 

## Overview

Here's an overview of what we'll cover in this lesson:

1. Update the Loadout script that is responsible for what is shown on the main menu
2. Update the Profile script which populates the data on the profile page and logs the player out
3. Run the game to test if our changes are working

## Prerequisites

Before you begin, make sure you have already created the functionality to log a player in with Immutable Passport. If you haven't already, you can achieve this in our previous lesson for [logging in the player here](../03-log-the-player-in/README.md). 

## Locate the LoadoutState Script

To start, find the LoadoutState script in your Unity project. In our Trash Dash project you can follow these steps to locate it:

- Navigate to your Unity project's `Assets` folder.
- Inside `Assets`, open the `Scripts` folder.
- Look for the `GameManager` folder and locate the `Loadout` script

### Updating the LoadoutState Script

The `LoadoutState` script is responsible for what gets displayed on the main menu of the game. We will update this script so that it shows the player's email and expose a public function to log the player out.

### Import the Immutable library

```csharp
using Immutable.Passport;
```

### Create a field for the passport
```csharp
private Passport passport;
```

### Initialize the passport field in the Enter function
```csharp
if (Passport.Instance != null)
{
    passport = Passport.Instance;
    Debug.Log("Passport Instance successfully loaded");
}
else
{
    Debug.Log("Passport Instance is null");
    emailText.text = "Could not load passport";
}
```
- This code is in the `Enter` function so that it is invoked immediately.
- Retrieve the globally available Passport instance and set it to our field.
- The globally available instance should not be null at this point because this is invoked after the player has logged in. 
- If it is null, we show an error message.

### Retrieve the email in the GetEmail function
```csharp
public async void GetEmail()
{
    if (passport != null)
    {
        string? email = await passport.GetEmail();
        emailText.text = email;
    }
}
```
- This code is in the `GetEmail` function.
- Update the code that sets the email text on the screen to dynamically retrieve the player's email using the Passport.

### Update the Logout function
```csharp
public async Task Logout()
{
    if (passport != null)
    {
        #if UNITY_ANDROID || UNITY_IPHONE || (UNITY_STANDALONE_OSX && !UNITY_EDITOR_OSX)
            await passport.LogoutPKCE();
        #else
            await passport.Logout();
        #endif
    }
    manager.SwitchState("Login");
}
```
- We will call `passport.Logout()` or `passport.LogoutPKCE()` to actually log the player out
- This code function is public so we can use it in other places of the code

## Locate the Profile Script

We now need to update the `Profile` script, which is responsible for what gets shown on the profile page.

- Navigate to your Unity project's `Assets` folder.
- Inside `Assets`, open the `Scripts` folder.
- Look for the `UI` folder and locate the `Profile` script

### Import the Immutable library

```csharp
using Immutable.Passport;
```

### Create a field for the passport
```csharp
private Passport passport;
```

### Initialize the passport field in the Open function

```csharp
if (Passport.Instance != null) {
    passport = Passport.Instance;
    Populate();
} else {
    Debug.Log("Passport Instance is null");
    emailText.text = "Could not connect to Passport";
}
```
- This is code is in the `Open` function so that it is invoked immediately.
- Retrieve the globally available Passport instance and set it to our field.
- The globally available instance should not be null at this point because this is invoked after the player has logged in. 
- If it is null, we show an error message.
- Otherwise we populate the fields.

### Update the functions that populate the address and email fields
```csharp
public async void PopulateEmail()
{
    try {
        string? email = await passport.GetEmail();
        emailText.text = email;
    } catch (Exception e){
        emailText.text = "Unable to get the email";
        Debug.Log($"Unable to get email: {e.Message}");
    }
}

public async void PopulateAddress()
{
    try {
        string? address = await passport.GetAddress(); 
        addressText.text = address;
    } catch (Exception e){
        addressText.text = "Unable to get the address";
        Debug.Log($"Unable to get address: {e.Message}");
    }
}
```


### Update the logout function
```csharp
public async void Logout() {
    LoadoutState loadoutState = GameManager.instance.topState as LoadoutState;
    await loadoutState.Logout();
    Close();
}
```
- Here we are calling the logout function that we setup in the `LoadoutState` script.

## Testing the New Functionality

1. Run the game.
2. Click the login button to seamlessly log in with valid credentials.
3. Check the loadout page to see the email displayed in the top right-hand corner.
4. Visit the profile page to find it populated with the player's actual email and Immutable address.
5. Clicking the log-out button logs out the player and navigates back to the login screen.

## Conclusion
With these updates, players can now log in with Immutable Passport, retrieve their data, and log out properly.


## Next Steps
[Lesson 05: Overview of the Minting Architecture](../05-Overview-of-the-Minting-Architecture/README.md)

