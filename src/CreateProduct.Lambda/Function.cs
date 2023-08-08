using Amazon.Lambda.Core;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace CreateProduct.Lambda;

public class Function
{
    public CreateProductResponse FunctionHandler(CreateProductRequest input, ILambdaContext context)
    {
        // Assume Product is Saved to DB
        var response = new CreateProductResponse();
        response.ProductId = Guid.NewGuid().ToString();
        response.Name = input.Name;
        response.Description = input.Description;
        return response;
    }
}

public class CreateProductRequest
{
    public string? Name { get; set; }
    public string? Description { get; set; }
}
public class CreateProductResponse
{
    public string? ProductId { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
}

