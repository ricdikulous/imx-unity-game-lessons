# Lesson 2: Initializing the Immutable Passport in Unity

In this lesson, we'll guide you through the process of initializing the Immutable Passport in Unity. This is an essential step if you plan to use Immutable for user authentication in your Unity game.

## Overview

Here's an overview of what we'll cover in this lesson:

1. Create and configure the project in the Immutable Hub.
2. Write a script to initialize Immutable Passport.
3. Add the script to a game object in Unity.
4. Test the initialization process.

## Prerequisites

Before you begin, make sure you have installed the Immutable Unity SDK in your unity project, if you haven't already you can follow the [lesson to Install the Immutable Unity SDK here](../01-install-the-immutable-unity-sdk/README.md). 

## 1. Create a Project in the Immutable Hub

The first step is to set up a project for your game in the Immutable Hub. Follow these steps:

- Go to the Immutable Hub - [https://hub.immutable.com](https://hub.immutable.com/)
- If you haven't already sign up for an account and login
- Click the 'Add Project' button.
- Give your project a name (e.g., 'Trash Dash').
- Select the 'Immutable zkEVM' option.
- Click 'Create.'

### Set Up the Environment

For this tutorial, we'll be using the testnet environment. Follow these steps:

- Create an environment for your project.
- Name it 'test' and select 'testnet.'

### Configure the Client

To configure the client for your project, follow these steps:

- Click to create the default clients (web and native).
- By default, both clients are created. We will use the native client for our game, which provides default Logout and redirect URLs, along with a Client ID.
- If you wish to support Android, iOS, or macOS clients, update the redirect URL to your application's deep link scheme (e.g., 'trashdash://callback') to enable PKCE login.
- Click 'Save' to save your client configuration.

## 2. Write the Initialization Script

Now, it's time to create a script that will initialize the Immutable Passport in Unity:

- Right-click in your Unity project, go to 'Create,' and select 'C# Script.'
- Name it 'init-passport.'

```csharp
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Immutable.Passport;

public class InitPassport : MonoBehaviour
{

    private Passport passport;

    async void Start()
    {
        string clientId = "YOUR_CLIENT_ID";
        string environment = Immutable.Passport.Model.Environment.SANDBOX;

        //Can be left as null if you only want to support windows
        string? redirectUri = null;

#if UNITY_IPHONE || UNITY_ANDROID || UNITY_STANDALONE_OSX
        redirectUri = "YOUR_APPLICATIONS_DEEP_LINK_SCHEME";
#endif

        passport = await Passport.Init(clientId, environment, redirectUri);
    }

}
```

## 3. Add the Script to a Game Object

To use the initialization script in your Unity project, you need to add it to a game object:

- Drag and drop the 'init-passport' script onto a game object that is high in your hierarchy (e.g., your main camera).

## 4. Test the Initialization Process

Now, run your game, and you should see the output of the Immutable Passport initializing in the Unity console below.

That's it! You've successfully initialized the Immutable Passport in your Unity project. This is an important step for integrating Immutable into your game's user authentication system.

## Next Steps

With the Immutable Passport initialized, it's time to enable player login and take your Unity game to the next level. Dive into the following lesson to learn how to achieve this:

[Lesson 3: Using Immutable Passport for Player Login](../03-log-the-player-in/README.md).
