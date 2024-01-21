# Lesson 07: Upload NFT Collection Metadata & Deploy Smart Contract

Welcome to Lesson 07 of our course! In this tutorial, we'll establish the metadata of our NFT collection for the "Trash Dash" game and deploy our collection's smart contract via the Immutable Hub.

## Overview

Here's an overview of what we'll cover in this lesson:
1. Uploading the **collection metadata** to our AWS S3 bucket.
2. Creating and deploying a smart contract for the collection using Immutable Hub.

*Note*: Our focus for this lesson is setting up the collection's metadata, not the individual NFTs inside the collection.

## Prerequisites
Before starting this lesson, make sure you have:
- Access to the Immutable Hub.
- An AWS account with an S3 bucket set up.

If you haven't already, you can follow our lesson where we [set up an S3 bucket using the AWS CDK here](../06-Creating-an-S3-Bucket-for-NFT-Metadata/README.md).

## Step-by-Step Guide

### Prepare the Metadata
- Clone the `only-metadata` folder from this lesson available [here](../07-Upload-Initial-Metadata-&-Create-Smart-Contract/only-metadata).
- Update the values of the `contract.json` file and the `logo.png` in your project's `metadata` folder.
- Note that the `contract.json` image field will automatically be updated to reference the logo's URL.

### .env Setup and Initialization
- Set up AWS credentials and bucket name from the environment variables.

```sh
AWS_ACCESS_KEY_ID=
AWS_SECRET_ACCESS_KEY=
S3_BUCKET_NAME=
S3_REGION=
```

#### Upload Function in our `metadataService.ts` Initialization Script 
- Load the env variables
```typescript
import * as dotenv from 'dotenv'
dotenv.config()

const awsAccessKeyId = process.env.AWS_ACCESS_KEY_ID
const awsSecretAccessKey = process.env.AWS_SECRET_ACCESS_KEY
const bucketName = process.env.S3_BUCKET_NAME
const region = process.env.S3_REGION

if (!awsAccessKeyId) {
  throw new Error('Missing environment variable: AWS_ACCESS_KEY_ID')
}
if (!awsSecretAccessKey) {
  throw new Error('Missing environment variable: AWS_SECRET_ACCESS_KEY')
}
if (!bucketName) {
  throw new Error('Missing environment variable: S3_BUCKET_NAME')
}
if (!region) {
  throw new Error('Missing environment variable: S3_REGION')
}
```
- Define constants for metadata directory, file extensions, and S3 paths
```typescript
const contractMetadataDirectory = './metadata/contract-metadata'
const nftImageExtension = 'png'
const s3NftMetadataPath = 'nft-metadata/'
```

- Initialize the AWS S3 client using the AWS SDK.
```typescript
const s3 = new S3Client({
  region: region,
  credentials: {
    accessKeyId: awsAccessKeyId,
    secretAccessKey: awsSecretAccessKey,
  },
})
```
- Create a function `uploadToS3` for uploading files to S3, and log the upload status.
```typescript
async function uploadToS3(
  localPath: string,
  key: string,
  body: Buffer | string,
  contentType: string,
): Promise<void> {
  const params: PutObjectCommand = new PutObjectCommand({
    Bucket: bucketName,
    Key: key,
    Body: body,
    ContentType: contentType,
  })

  try {
    await s3.send(params)
    console.log(`Uploaded ${localPath} to ${bucketName}/${key}`)
  } catch (error) {
    console.error(
      `Error uploading ${localPath} to ${bucketName}/${key}:`,
      error,
    )
  }
}
```
- Implement the `init` function to:
  - Upload the `logo.png` to S3.
  - Read, update, and upload the `contract.json` to S3.
  - Output URIs for collection metadata and NFT base URI.
```typescript
export async function init() {
  //Upload the contract logo
  const contractLogoPath = path.join(
    contractMetadataDirectory,
    `logo.${nftImageExtension}`,
  )
  const logoS3Key = 'logo.png'
  await uploadToS3(
    contractLogoPath,
    logoS3Key,
    fs.readFileSync(contractLogoPath),
    `image/${nftImageExtension}`,
  )

  //Upload the contract metadata
  const contractMetadataPath = path.join(
    contractMetadataDirectory,
    'contract.json',
  )
  const s3Key = 'contract.json'

  const parsedContractMetadata = JSON.parse(
    fs.readFileSync(contractMetadataPath, 'utf-8'),
  )
  parsedContractMetadata['image'] =
    `https://${bucketName}.s3.amazonaws.com/${logoS3Key}`

  await uploadToS3(
    contractMetadataPath,
    s3Key,
    JSON.stringify(parsedContractMetadata),
    'application/json; charset=utf-8',
  )

  console.log(`
******************************************************************************

Base URI:                https://${bucketName}.s3.amazonaws.com/${s3NftMetadataPath}
Collection Metadata URI: https://${bucketName}.s3.amazonaws.com/contract.json

******************************************************************************
`)
}
```
- Add a script to `package.json` for executing the `init` function.
```
"init-metadata": "ts-node -e \"import { init } from './src/metadataService'; init();\""
```
- Run the script using `npm run init-metadata`.
- Note down the `Base URI` and the `Collection Metadata URI`.

#### Deploy the Smart Contract
- Go to [https://hub.immutable.com](https://hub.immutable.com/) to deploy the smart contract for your project.
- Define contract details like name, symbol, base URI, and collection metadata URI using the values that were output in the console.
- Check the transaction on Immutable Hub and block explorer.

## What We Accomplished
Congratulations! We've successfully uploaded our collection's metadata and created a linked smart contract. This setup is vital for our next steps in integrating NFTs into "Trash Dash."

## Next Steps
Stay tuned for the next lesson, where we'll dive into the dynamic metadata upload process. We'll learn how to upload metadata for individual NFTs, enabling unique, randomized assets for our game. See you there!

[Lesson 08: Upload Metadata Script](../08-Dynamically-Upload-Metadata/README.md)
