# Lesson 07: Upload Initial Metadata & Create Smart Contract

## Prerequisites
Before starting this lesson, make sure you have:
- Basic knowledge of TypeScript and AWS services.
- Access to the Immutable Hub.
- An AWS account with S3 bucket set up.
- The bootstrap TypeScript project with the necessary metadata for the NFT collection.

## Overview
Welcome to Lesson 07 of our course! In this tutorial, we'll establish the foundation of our NFT collection for the "Trash Dash" game. This involves two critical steps:
1. **Uploading the initial collection metadata** to an AWS S3 bucket.
2. **Creating and linking a smart contract** for the collection using Immutable Hub.

### What We're Not Doing
We won't be uploading metadata for individual NFTs in this lesson. Instead, our focus is on setting up the collection's metadata, which is crucial for dynamically generating NFTs during gameplay.

## Step-by-Step Guide

### Step 1: Prepare the Metadata
- Locate the `contract.json` file and the `logo.png` in your project's metadata folder.
- Note that the `contract.json` file's image field will be updated with the S3 URL of the `logo.png`.

### Step 2: Set Up AWS Credentials
- Ensure AWS credentials and bucket name are loaded from the environment variables.
- Validate the presence of these variables to prevent execution errors.

### Step 3: Initialize AWS S3 Client
- Define constants for metadata directory, file extensions, and S3 paths.
- Initialize the AWS S3 client using the AWS SDK.

### Step 4: Write the Upload Function
- Create a function `uploadToS3` for uploading files to S3.
- The function should log the status of the upload.

### Step 5: Implement the Init Function
- The `init` function should:
  - Upload the `logo.png` to S3.
  - Read the `contract.json`, update the image URL, and upload it to S3.
  - Output the URIs for the collection metadata and base URI for NFTs.

### Step 6: Run the Script
- Add a script to `package.json` to execute the `init` function.
- Use the command `npm run init-metadata` in the terminal to run the script.

### Step 7: Deploy the Smart Contract
- Go to Immutable Hub, and select your project (Trash Dash).
- Grab coins for testing from the faucet if needed.
- Deploy the contract with a name (e.g., Trash Dash Game), symbol (e.g., TDG), base URI, collection metadata URI, and set royalties.

### Step 8: Verify Contract Deployment
- Check the transaction on Immutable Hub and block explorer.
- Link the collection to your project and verify the details like the logo, Base-URI, and Contract-URI.

## What We Accomplished
Congratulations! We've successfully uploaded our collection's metadata and created a linked smart contract. This setup is vital for our next steps in integrating NFTs into "Trash Dash."

## Next Steps
Stay tuned for the next lesson, where we'll dive into the dynamic metadata upload process. We'll learn how to upload metadata for individual NFTs, enabling unique, randomized assets for our game. See you there!
