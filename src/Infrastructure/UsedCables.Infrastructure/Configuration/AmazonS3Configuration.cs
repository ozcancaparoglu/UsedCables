namespace UsedCables.Infrastructure.Configuration;

public class AmazonS3Configuration
{
    public string BucketName { get; set; }
    public string AccessKey { get; set; }
    public string SecretKey { get; set; }
    public string Url { get; set; }
}