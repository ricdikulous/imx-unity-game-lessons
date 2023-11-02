## Cloning note

**This repository use git Large File Support.
To clone it successfully, you will need to install git lfs** :

- Download git lfs here : https://git-lfs.github.com/
- run `git lfs install` in a command line

Now you git clone should get the LFS files properly. For support of LFS in Git
GUI client, please refer to their respective documentation

## Description

This project is an "endless runner" type mobile game created in Unity, enhanced with the Unity Immutable SDK for secure player login and data retrieval using zkEVM technology.

You can find the [original project on the Unity Asset Store](https://assetstore.unity.com/packages/essentials/tutorial-projects/endless-runner-sample-game-87901). Please note that this is the old version and does not use the Lightweight Rendering Pipeline or Addressable. See the note at the end of this file. The Asset Store version will be updated when Addressable is out of preview.

Inside the Asset folder, you'll find an "INSTRUCTION.txt" file that highlights various essential aspects of the project, including the integration of the Unity Immutable SDK.

Additionally, an article is available on the [Unity Learn website](https://unity3d.com/learn/tutorials/topics/mobile-touch/trash-dash-code-walkthrough), which provides insights into specific parts of the code, including the implementation of the Unity Immutable SDK for player authentication and data retrieval using zkEVM.

For more in-depth information about the project, instructions on how to build and customize it, and details on how the Unity Immutable SDK is integrated, please visit the [project's wiki](https://github.com/Unity-Technologies/EndlessRunnerSampleGame/wiki).

Now, with the Unity Immutable SDK, players can enjoy a secure and seamless login experience and retrieve their data using zkEVM technology within this exciting endless runner game.

## Lessons

In the lessons directory you will see the immutable sdk be integrated in a step by step fashion.