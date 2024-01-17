# Lesson 4: Retrieve Player Profile Data and Logout

In this lesson, we will demonstrate how to integrate the Immutable Passport for retrieving a player's profile information and implement the functionality to log the player out. 

In the lesson, we successfully logged the player in, but we still have placeholder data for the player's information and lack the mechanism to properly log them out. In this lesson, we'll address these issues.

## Prerequisite

Before you begin, make sure you have already logged a play in using the Immutable Passport, if you haven't already you can follow the [lesson log in the player here](../03-log-the-player-in/README.md). 

## Updating the LoadoutState Script

The `LoadoutState` script is responsible for what gets displayed on the main menu of the game. We will update this script so that it shows the player's email and expose a public function to log the player out.

### Import the Immutable library

```csharp
using Immutable.Passport;
```

### Create a field for the passport
```csharp
private Passport passport;
```

### Initialize the passport field
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
- This is code is in the `Enter` function so that it is invoked immediately.
- Retrieve the globally available Passport instance and set it to our field.
- The globally available instance should not be null at this point because this is invoked after the player has logged in. 
- If it is null, we show an error message.

### Retrieve the email 
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
- Update the code that sets the email text on the screen to dynamically retrieve the player's email using the Passport.

### Update the logout function
```csharp
public async Task Logout()
{
    if (passport != null)
    {
        await passport.Logout();
    }
    manager.SwitchState("Login");
}
```
- We will call passport.Logout() to actually log the player out
- This code function is public so we can use it in other places of the code

## Updating the Profile Script

The `Profile` script is responsible for what gets shown on the profile page.

### Import the Immutable library

```csharp
using Immutable.Passport;
```

### Create a field for the passport
```csharp
private Passport passport;
```

### Initialize the passport field.

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

