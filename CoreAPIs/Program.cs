using CoreAPIs.Context;
using CoreAPIs.Context.Interface;
using CoreAPIs.Repository.Interface;
using CoreAPIs.Repository.Service;

var builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddScoped<IDbContext, DBContext>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IBranchRepository, BranchRepository>();
builder.Services.AddScoped<IProjectStatusRepository, ProjectStatusRepository>();
builder.Services.AddScoped<IProjectDetailsRepository, ProjectDetailsRepository>();
builder.Services.AddScoped<IBasicLoanDetailsRep, BasicLoanDetailsRep>();
builder.Services.AddScoped<IEmployeeDetails, EmployeeDetailsRepository>();
builder.Services.AddScoped<ITaxPayerFilingStatusRepository, TaxPayerFilingStatusRepository>();
builder.Services.AddScoped<ITaxPayerInfo,TaxPayerInfoRepository>();
builder.Services.AddScoped<ITPSpouseInfo, TPSpouseInfoRepository>();
builder.Services.AddScoped<ICarLoanApplicationRep, CarLoanApplicationRep>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "MyAllowSpecificOrigins",
    builder =>
    {
        builder.WithOrigins("*").AllowAnyHeader().AllowAnyMethod();
    });
});
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                            }
                        },
                        new string[] {}
                }
                 });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("MyAllowSpecificOrigins");
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
