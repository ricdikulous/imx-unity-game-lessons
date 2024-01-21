# Lesson 08: Dynamic Metadata Upload for Individual Game Assets
Welcome to Lesson 08, where we focus on the dynamic upload of asset metadata. In our 'Trash Dash' game, players can earn randomized NFT rewards. This tutorial sets up the core dynamic metadata functionality that we will use in later lessons to mint a random NFT.

## Overview
Here's an overview of what we'll cover in this lesson:
1. Integration with Express and Swagger for API interaction.
2. Transitioning from static to dynamic metadata upload.
3. Implementing a function for random metadata selection in the game.

## Prerequisites
- Familiarity with Express framework and Swagger.
- Basic understanding of REST API endpoints.

In the previous lesson we created the contract for the collection and wrote code to upload the collection metadata to our S3 bucket. You can check that out [here](../07-Upload-Initial-Metadata-&-Create-Smart-Contract/README.md).

## Bootstrap and Swagger Integration
We've updated our project to include the Express framework, facilitating API endpoint exposure. Additionally, Swagger is now integrated for efficient API documentation and testing.

- [https://expressjs.com](https://expressjs.com)
- [https://swagger.io](https://swagger.io)

## Project Endpoints
Our project currently has two primary endpoints in the `routes.ts` file:
- A POST endpoint for minting NFTs.
- A GET endpoint for retrieving player's NFTs.
Both are currently set to return mocked values.

## Metadata Folder Structure
We have restructured our `metadata` folder available [here](../08-Dynamically-Upload-Metadata/server-metadata), adding two subfolders:
- `images/`: Stores visual representations of NFTs.
- `metadata/`: Contains JSON files describing each NFT's attributes.

## Upload Random Metadata Function
We're introducing a new function in our `metadataService.ts` file. This function is responsible for uploading random metadata from our collection.

```typescript
export async function uploadRandomMetadata(tokenId: number) {
  const numberOfDifferentNfts = fs.readdirSync(nftMetadataDirectory).length
  const metadataNumber = Math.floor(Math.random() * numberOfDifferentNfts) + 1
  //Upload the image
  const imageLocalPath = path.join(
    nftImagesDirectory,
    `${metadataNumber}.${nftImageExtension}`,
  )
  const imageS3Key = path.join(
    s3NftImagesPath,
    `${tokenId}.${nftImageExtension}`,
  )
  await uploadToS3(
    imageLocalPath,
    imageS3Key,
    fs.readFileSync(imageLocalPath),
    `image/${nftImageExtension}`,
  )

  const localPath = path.join(nftMetadataDirectory, `${metadataNumber}`)
  const s3Key = path.join(s3NftMetadataPath, `${tokenId}`)

  const parsedMetadata = JSON.parse(fs.readFileSync(localPath, 'utf-8'))
  parsedMetadata['image'] =
    `https://${bucketName}.s3.amazonaws.com/${imageS3Key}`
  parsedMetadata['id'] = tokenId
  parsedMetadata['token_id'] = tokenId

  //Upload the metadata
  await uploadToS3(
    localPath,
    s3Key,
    JSON.stringify(parsedMetadata),
    'application/json; charset=utf-8',
  )
  return parsedMetadata
}
```

Let's break down the `uploadRandomMetadata` function:
- It starts by determining the number of available NFTs.
- Randomly selects a metadata file.
- Uploads the corresponding image and metadata to S3.
- Updates the metadata with the correct token ID and image URL.
- Returns the parsed metadata.

## Integration with Mint Endpoint
This function will be integrated into our minting endpoint in the `routes.ts` file, ensuring each NFT minted has a unique and randomly assigned piece of metadata and that the metadata is returned to the caller.

```typescript
const metadata = await uploadRandomMetadata(tokenId)
...
res.status(200).json(metadata)
```

## Test out in Swagger
To test it out you can run `npm run dev` and navigate to `http://localhost:3000` where you will see the swagger docs
- Execute the `/mint` POST endpoint.
- Verify the image and metadata URLs post-upload.

## Conclusion
In this lesson, we've enhanced our NFT functionality by:
- Reviewing and updating our project's backend structure.
- Implementing dynamic metadata selection for unique NFT rewards.
- Demonstrating the functionality through Swagger.

We're now ready to move on to granting the minter role, which will bring us closer to actual NFT creation in "Trash Dash".

## Next Steps
Join us in the next lesson where we'll set up the minter role, paving the way for minting NFTs in-game and elevating the overall player experience.

[Lesson 09: Setting Up Smart Contract Permissions](../09-Setting-Up-Smart-Contract%20Permissions:Granting-the-Minter-Role/README.md)
