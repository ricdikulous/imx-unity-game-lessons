# Lesson 6: Creating an S3 Bucket for NFT Metadata

## Introduction
Welcome to Lesson 6 of our course, where we will focus on creating an Amazon S3 bucket to store the metadata for our NFTs. S3, known for its fast latency and ease of updates, is a robust choice for our needs in "Trash Dash". This lesson will guide you through the process using the AWS Cloud Development Kit (CDK).

## Prerequisites
- Basic understanding of AWS services.
- Familiarity with TypeScript or a willingness to learn.
- AWS account setup.

## What is AWS CDK?
The AWS Cloud Development Kit (CDK) is a powerful, open-source framework that allows developers to define cloud infrastructure in code. It supports languages like TypeScript and Python, enabling a more intuitive and manageable approach to handling AWS resources.

### Further Resources
- [AWS CDK Documentation](https://aws.amazon.com/cdk/)
- [TypeScript Basics](https://www.typescriptlang.org/docs/)

## Getting Started with CDK
1. **Initialize the CDK Project**: Run `CDK init app --language typescript` in the command line. This will create a basic structure for our project in TypeScript.

2. **Open the Project**: Navigate to the `infra-stack.ts` file in your code editor.

## Configuring the S3 Bucket
1. **Environment Variables**: Use the `.env` library to load the `S3 BUCKET NAME` variable. This makes our script reusable and adaptable.

2. **Bucket Creation**: In the `infra-stack.ts` file:
   - Define the bucket name.
   - Enable versioning.
   - Set the removal policy to DESTROY.
   - Configure public read access and security policies.

3. **Public Readability**: We need our bucket to be publicly readable for NFT metadata access. Add a resource policy using the `addToResourcePolicy` method to allow `s3:GetObject` actions.

4. **CDK Output**: Create an output to display the bucket name post-deployment.

## Pre-Deployment Setup
1. **Create a `.env` File**: Set `S3 BUCKET NAME` to your desired bucket name.

2. **Compile TypeScript Code**: Run `npm run build` in your terminal.

## Deploying the Infrastructure
1. **Initiate Deployment**: Run `npx cdk deploy` in your terminal.

2. **Review and Confirm**: Carefully review the changes and type 'y' to confirm the deployment.

## Conclusion
Congratulations! You've successfully set up an S3 bucket for NFT metadata using AWS CDK. In our next lesson, we will focus on creating a server for uploading and minting NFTs.

