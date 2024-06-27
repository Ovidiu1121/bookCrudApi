using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BookCrudApi.Dto;
using Newtonsoft.Json;
using tests.Infrastructure;
using Xunit;

namespace tests.IntegrationTests;

public class BookIntegrationTests: IClassFixture<ApiWebApplicationFactory>
{
    private readonly HttpClient _client;

    public BookIntegrationTests(ApiWebApplicationFactory factory)
    {
        _client = factory.CreateClient();
    }
    
    [Fact]
    public async Task Post_Create_ValidRequest_ReturnsCreatedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Book/create";
        var book = new CreateBookRequest()
            { Title = "new title test", Author = "new author test", Genre = "new genre test" };
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.Created, response.StatusCode);

        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BookDto>(responseString);

        Assert.NotNull(result);
        Assert.Equal(book.Title, result.Title);
        Assert.Equal(book.Author, result.Author);
        Assert.Equal(book.Genre, result.Genre);
        
    }
    
    [Fact]
    public async Task Post_Create_BookAlreadyExists_ReturnsBadRequestStatusCode()
    {
        var request = "/api/v1/Book/create";
        var book = new CreateBookRequest()
            { Title = "new title test", Author = "new author test", Genre = "new genre test" };
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

        await _client.PostAsync(request, content);
        var response = await _client.PostAsync(request, content);

        Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
    }
    
    [Fact]
    public async Task Put_Update_ValidRequest_ReturnsAcceptedStatusCode_ValidProductContentResponse()
    {
        var request = "/api/v1/Book/create";
        var book = new CreateBookRequest()
            { Title = "new title test", Author = "new author test", Genre = "new genre test" };
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BookDto>(responseString)!;

        request = "/api/v1/Book/update/"+result.Id;
        var updateBook = new UpdateBookRequest()
            { Title = "new title updated", Author = "new author updated", Genre = "new genre updated" };
        content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

        response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        responseString = await response.Content.ReadAsStringAsync();
        result = JsonConvert.DeserializeObject<BookDto>(responseString)!;

        Assert.Equal(updateBook.Title, result.Title);
        Assert.Equal(updateBook.Author, result.Author);
        Assert.Equal(updateBook.Genre, result.Genre);
    }
    
    [Fact]
    public async Task Put_Update_BookDoesNotExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Book/update/33";
        var updateBook = new UpdateBookRequest()
            { Title = "new title test", Author = "new author test", Genre = "new genre test" };
        var content = new StringContent(JsonConvert.SerializeObject(updateBook), Encoding.UTF8, "application/json");

        var response = await _client.PutAsync(request, content);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Delete_Delete_BookExists_ReturnsDeletedBook()
    {

        var request = "/api/v1/Book/create";
        var book = new CreateBookRequest()
            { Title = "new title test", Author = "new author test", Genre = "new genre test" };
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BookDto>(responseString)!;

        request = "/api/v1/Book/delete/" + result.Id;
        response = await _client.DeleteAsync(request);
        
        Assert.Equal(HttpStatusCode.Accepted,response.StatusCode);
    }
    
    [Fact]
    public async Task Delete_Delete_BookDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Book/delete/55";

        var response = await _client.DeleteAsync(request);

        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    [Fact]
    public async Task Get_GetByTitle_ValidRequest_ReturnsOKStatusCode()
    {

        var request = "/api/v1/Book/create";
        var book = new CreateBookRequest()
            { Title = "new title test", Author = "new author test", Genre = "new genre test" };
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BookDto>(responseString)!;

        request = "/api/v1/Book/title/" + result.Title;
        response = await _client.GetAsync(request);

        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetByTitle_BookDoesNotExists_ReturnsNotFoundStatusCode()
    {

        var request = "/api/v1/Book/title/test";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);

    }
    
    [Fact]
    public async Task Get_GetById_ValidRequest_ReturnsOKStatusCode()
    {
        var request = "/api/v1/Book/create";
        var book = new CreateBookRequest()
            { Title = "new title test", Author = "new author test", Genre = "new genre test" };
        var content = new StringContent(JsonConvert.SerializeObject(book), Encoding.UTF8, "application/json");

        var response = await _client.PostAsync(request, content);
        var responseString = await response.Content.ReadAsStringAsync();
        var result = JsonConvert.DeserializeObject<BookDto>(responseString);

        request = "/api/v1/Book/id/" + result.Id;

        response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.OK,response.StatusCode);
    }
    
    [Fact]
    public async Task Get_GetById_BookDoesExists_ReturnsNotFoundStatusCode()
    {
        
        var request = "/api/v1/Book/id/1";

        var response = await _client.GetAsync(request);
        
        Assert.Equal(HttpStatusCode.NotFound,response.StatusCode);
        
    }
    
    
}