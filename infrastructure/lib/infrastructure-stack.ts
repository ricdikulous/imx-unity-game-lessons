import * as cdk from 'aws-cdk-lib'
import { Construct } from 'constructs'
import { aws_s3 as s3 } from 'aws-cdk-lib'
import { Effect, PolicyStatement, StarPrincipal } from 'aws-cdk-lib/aws-iam'
import * as dotenv from 'dotenv'
dotenv.config()

const bucketName = process.env.S3_BUCKET_NAME
if (!bucketName) {
  throw new Error('Missing environment variable: S3_BUCKET_NAME')
}

export class InfrastructureStack extends cdk.Stack {
  constructor(scope: Construct, id: string, props?: cdk.StackProps) {
    super(scope, id, props)

    const s3MetadataBucket = new s3.Bucket(this, 's3NftMetadataBucket', {
      bucketName: bucketName,
      versioned: false,
      removalPolicy: cdk.RemovalPolicy.DESTROY,
      publicReadAccess: true,
      blockPublicAccess: {
        blockPublicAcls: true,
        ignorePublicAcls: true,
        restrictPublicBuckets: false,
        blockPublicPolicy: false,
      },
    })

    s3MetadataBucket.addToResourcePolicy(
      new PolicyStatement({
        actions: ['s3:GetObject'],
        effect: Effect.ALLOW,
        principals: [new StarPrincipal()],
        resources: [s3MetadataBucket.arnForObjects('*')],
      }),
    )

    new cdk.CfnOutput(this, 'BucketName', {
      value: s3MetadataBucket.bucketName,
    })
  }
}
