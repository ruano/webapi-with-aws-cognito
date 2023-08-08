using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Newtonsoft.Json;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.SystemTextJson.DefaultLambdaJsonSerializer))]

namespace Student.Lambda;

public class Function
{
    public async Task<List<Models.Student>> GetAllStudentsAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        DynamoDBContext dbContext = new DynamoDBContext(client);
        var data = await dbContext.ScanAsync<Models.Student>(default).GetRemainingAsync();
        return data;
    }

    public async Task<APIGatewayHttpApiV2ProxyResponse> CreateStudentAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        var studentRequest = JsonConvert.DeserializeObject<Models.Student>(request.Body);
        AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        DynamoDBContext dbContext = new DynamoDBContext(client);
        await dbContext.SaveAsync(studentRequest);
        var message = $"Student with Id {studentRequest?.Id} Created";
        LambdaLogger.Log(message);
        return new APIGatewayHttpApiV2ProxyResponse
        {
            Body = message,
            StatusCode = 201
        };
    }

    public async Task<Models.Student> GetStudentByIdAsync(APIGatewayHttpApiV2ProxyRequest request, ILambdaContext context)
    {
        AmazonDynamoDBClient client = new AmazonDynamoDBClient();
        DynamoDBContext dbContext = new DynamoDBContext(client);
        string idFromPath = request.PathParameters["id"];
        int id = int.Parse(idFromPath);
        var student = await dbContext.LoadAsync<Models.Student>(id);
        if (student == null) throw new Exception("Not Found!");
        return student;
    }
}
