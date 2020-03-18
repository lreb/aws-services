## DynamoDB

1. Create a table

## Role (Lambda/Dynamo access)

1. Create a role 
2. add policy with access to execute lambda
3. add policy with dynamodb access

## Lambda

1. Create new lambda function
2. Use the existing role
3. Select runtime
4. Load code
	1. Set CORS in your APP (OPTIONAL)
	2. Use SDK to hanlde DynamoDB 
5. Set handler
	1. example: AWSServerlessDemo1::AWSServerlessDemo1.LambdaEntryPoint::FunctionHandlerAsync
	2. Assembly::Namespace.Namespace::Class


## Api Gateway

1. Type: REST API
2. Endpoint type: Regional
3. Create a method 
	1. Type: ANY
	2. Method request: lambda arn
	3. Integration request: LAMBDA_PROXY
	4. Method response: application/json
	5. HTTP Status: PROXY
4. Create resource
	1. Check as a PROXY
	2. Set lambda name or arn
	3. Enable CORS
3. Create a method within last resource
	1. Type: ANY
	2. Method request: lambda arn
	3. Integration request: LAMBDA_PROXY
	4. Method response: application/json
	5. HTTP Status: PROXY
4. Deploy all configurations to required environments
5. OPTIONAL: you can ser WEB ACL
6. Set CORS rules

## S3

1. Create s3
2. Permissions -> Public access - > Block public access to buckets and objects granted through new public bucket or access point policies (ON)
3. Add policy
	1. Effect: Allow
	2. Principal: "AWS": "arn:aws:iam::cloudfront:user/CloudFront Origin Access Identity EXXXXXXXXXX"
	3. Action: s3:GetObject
	4. Resource: "arn:aws:s3:::<BUCKET_NAME>/*"
4. Load files

## Clouf Front

1. Create a new one
2. Type: WEB
3. Origin Domain Name: <S3 NAME OR ARN>
4. Restict bucket access: yes
5. View protocol policy: HTTP and HTTPS
6. AWS WAF Web ACL:	(pending)
7. SSL Certificate	Default CloudFront Certificate (...cloudfront.net)
8. Default Root Object	index.html
9. Edit origins
	1. set origin: s3 name
	2. Restict bucket access: yes
	3. Origin Access Identity: Existing
	4. Identity: CloudFront OAI for aim Frontend S3
	5. Grant Read Permissions on Bucket: No
10. Origin Access Identity is the id that you must use for set principal arn in your s3

## WAF

1. Create new one
2. Type: CloudFront distribution
3. Select last Cloud Front service
4. Add rules

## Sources
[AWS Policy Generator](https://awspolicygen.s3.amazonaws.com/policygen.html)
[Allow access to CloudFront S3 bucket from another account](https://forums.aws.amazon.com/thread.jspa?threadID=229908)
[Policy Permissions](https://docs.aws.amazon.com/AmazonS3/latest/dev/using-with-s3-actions.html)
[Restricting Access to Amazon S3](https://docs.aws.amazon.com/AmazonCloudFront/latest/DeveloperGuide/private-content-restricting-access-to-s3.html#private-content-creating-oai)
[Amazon S3 bucket only from a CloudFront distribution](https://aws.amazon.com/premiumsupport/knowledge-center/cloudfront-access-to-amazon-s3/)
