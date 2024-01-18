# Integrating NFTs into a Unity Game with Immutable

## Course Context
This course focuses on updating an existing Unity runner game called "Trash Dash" to incorporate Non-Fungible Tokens (NFTs) using technology provided by Immutable and their Unity SDK. The course covers various aspects of NFT integration, including logging players in using Immutable Passport to access their wallets, making API calls for minting and displaying NFTs, uploading NFT metadata, and setting up a server for NFT minting. The primary goal is to transform character accessories in "Trash Dash" into NFTs, enhancing player experience and game dynamics. 

**Note**: This course is designed for game developers and focuses specifically on integrating NFTs using Immutable's technology. It is not intended to teach basic Unity development or server setup.

## Repository Structure
This repository is organized into four main folders, each integral to the course:

### 1. Infrastructure
Contains the AWS CDK project for deploying the S3 bucket to store NFT metadata.

### 2. MintingServer
A TypeScript Express.js app that runs a local server for minting NFTs, uploading metadata, and retrieving player NFT data.

### 3. Lessons
A series of lessons stepping through each step of integrating NFTs into a Unity game.

### 4. TrashDashUnity
The Unity game codebase for "Trash Dash".

## Prerequisites
- Ensure Git Large File Storage (Git LFS) is installed to properly use this repository.

## Course Structure
1. [**Install the Immutable SDK into Unity**](./lessons/01-install-the-immutable-unity-sdk/README.md)
2. [**Initialize the Immutable Passport in Unity**](./lessons/02-initialise-the-immutable-passport/)
3. [**Log the Player in with the Immutable Passport**](./lessons/03-log-the-player-in/README.md)
4. [**Retrieve Player Data with the Immutable Passport**](./lessons/04-retrieve-player-data-and-logout/README.md)
5. [**Overview of the Minting Architecture**](./lessons/05-Overview-of-the-Minting-Architecture/README.md)
6. [**Deploy S3 Bucket for NFT Metadata with AWS CDK**](./lessons/06-Creating-an-S3-Bucket-for-NFT-Metadata/README.md)
7. [**Upload NFT Collection Metadata & Create the Smart Contract**](./lessons/07-Upload-Initial-Metadata-&-Create-Smart-Contract/README.md)
8. [**Dynamically Upload Metadata**](./lessons/08-Dynamically-Upload-Metadata/README.md)
9. [**Setting Up Smart Contract Permissions: Granting the Minter Role**](./lessons/09-Setting-Up-Smart-Contract%20Permissions:Granting-the-Minter-Role/README.md)
10. [**Minting a NFT**](./lessons/10-Minting-Endpoint/README.md)
11. [**Retrieve a Player's NFTs**](./lessons/11-Retrieve-a-Players-NFTs/README.md)
12. [**Show NFT Inventory in Game**](./lessons/12-Display-the-Players-NFTs/README.md)
13. [**Update Game to Use the NFTs**](./lessons/13-Equipping-the-NFT-Accessories/README.md)
14. [**Mint NFTs in Game**](./lessons/14-Mint-NFTs-In-Game/README.md)

---

*For the best learning experience, it is recommended to follow the course structure sequentially, as each lesson builds upon the knowledge of the previous ones.*
