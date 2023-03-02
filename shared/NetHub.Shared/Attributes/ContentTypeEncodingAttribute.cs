namespace NetHub.Shared.Attributes;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
public class ContentTypeEncodingAttribute : Attribute
{
    public ContentTypeEncodingAttribute(string contentType)
    {
        ContentType = contentType;
    }

    public string ContentType { get; }
}