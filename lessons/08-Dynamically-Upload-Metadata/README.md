# Lesson 08: Upload Metadata Script

## Prerequisites
- Familiarity with Express framework and Swagger.
- Basic understanding of REST API endpoints.
- Knowledge of AWS S3 and its integration in Node.js applications.

## Overview
Welcome to Lesson 08, where we make a significant leap in our "Trash Dash" game's NFT integration. This lesson focuses on the dynamic upload of metadata, a core feature in creating unique NFTs for our players. We'll delve into the following:

- Integration with Express and Swagger for API interaction.
- Transitioning from static to dynamic metadata upload.
- Implementing a function for random metadata selection in the game.

## Bootstrap and Swagger Integration
We've updated our project to include the Express framework, facilitating API endpoint exposure. Additionally, Swagger is now integrated for efficient API documentation and testing.

## Project Endpoints
Our project currently has two primary endpoints:
- A POST endpoint for minting NFTs.
- A GET endpoint for retrieving player's NFTs.
Both are currently set to return mocked values.

## Metadata Folder Structure
We have restructured our metadata folder, adding two subfolders:
- `images/`: Stores visual representations of NFTs.
- `metadata/`: Contains JSON files describing each NFT's attributes.

## Upload Function
We're introducing a new function in our `metadataService`. This function is responsible for uploading random metadata from our collection.

\```typescript
// [Add the new function for uploading random metadata]
\```

## Upload Random Metadata Function
Let's break down the `uploadRandomMetadata` function:
- It starts by determining the number of available NFTs.
- Randomly selects a metadata file.
- Uploads the corresponding image and metadata to S3.
- Updates the metadata with the correct token ID and image URL.
- Returns the parsed metadata.

\```typescript
// [Add uploadRandomMetadata function code here]
\```

## Integration with Mint Endpoint
This function will be integrated into our minting endpoint, ensuring each NFT minted has a unique and randomly assigned piece of metadata.

## Demonstration in Swagger
We'll demonstrate this new functionality using Swagger:
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
