## NFT Metadata Infrastructure

This project includes a CDK (Cloud Development Kit) setup for deploying a publicly readable Amazon S3 bucket. This bucket is specifically intended for storing NFT (Non-Fungible Token) metadata. It is configured to allow public read access, ensuring that NFT metadata can be easily accessed and read when required.

### Important Note on S3 Bucket
- The deployed S3 bucket will be publicly readable.
- It is designed to store NFT metadata, which requires public accessibility for functionality with NFT platforms and marketplaces.
- Ensure that only the intended NFT metadata is stored in this bucket, as the data will be publicly accessible.

## Useful commands

* `npm run build`   compile typescript to js
* `npm run watch`   watch for changes and compile
* `npm run test`    perform the jest unit tests
* `npx cdk deploy`  deploy this stack to your default AWS account/region, which includes the public S3 bucket for NFT metadata.
* `npx cdk diff`    compare deployed stack with current state
* `npx cdk synth`   emits the synthesized CloudFormation template
