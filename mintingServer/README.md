# IMX Server

## Overview
The `mintingServer` is an Express.js application using TypeScript, designed to interact with Immutable SDK for minting Non-Fungible Tokens (NFTs) and managing NFT data. It features functionalities for minting NFTs, retrieving NFTs, and uploading NFT metadata to an AWS S3 bucket. The server's API is thoroughly documented using Swagger for easy reference and usage.

## Features
- **NFT Minting**: Mint NFTs using Immutable SDK.
- **Upload Metadata**: Automatically upload NFT metadata to a configured S3 bucket upon minting.
- **Retrieve NFTs**: Fetch NFTs associated with a specific account.
- **Swagger Documentation**: Comprehensive API documentation with Swagger.

## Getting Started

1. **Install Dependencies**

```sh
npm install
```


2. **Set Up Environment Variables**
- Copy `env.example` to a new file named `.env`.
- Fill in the details in `.env` for:
  - Ethereum `PRIVATE_KEY` and `CONTRACT_ADDRESS`.
  - AWS credentials (`AWS_ACCESS_KEY_ID`, `AWS_SECRET_ACCESS_KEY`) and S3 details (`S3_BUCKET_NAME`, `S3_REGION`).


3. **Development Mode**
For development, use the following command for hot reloads:

```sh
npm run dev
```

### Swagger Documentation
Access the Swagger API documentation at `http://localhost:3000/docs` once the server is running. Although the base route is also redirected to the docs page too.

## Scripts

- `start`: Launch the server using compiled JavaScript files.
- `build`: Compile TypeScript files to JavaScript.
- `dev`: Run the server in development mode with hot reload.
- `init-metadata`: Upload the metadata for the collection.
- `grant-minter-role`: Run the script to grant minter roles for NFT minting.
