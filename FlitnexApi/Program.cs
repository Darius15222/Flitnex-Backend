using FlitnexApi.Repositories;
using FlitnexApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// builder.Services.AddAutoMapper(typeof(Startup));

builder.Services.AddScoped<MovieRepository>();
builder.Services.AddScoped<GenreRepository>();
builder.Services.AddScoped<CastRepository>();

builder.Services.AddScoped<GenreService>();
builder.Services.AddScoped<MovieService>();
builder.Services.AddScoped<CastService>();


// builder.Services.AddScoped<IValidator<DGenre>, CreateGenreValidation>();
// builder.Services.AddScoped<IValidator<DGenre>, UpdateGenreValidation>();
// builder.Services.AddScoped<IValidator<DGenre>, DeleteGenreValidation>();
//
// builder.Services.AddScoped<IValidator<DMovie>, CreateMovieValidation>();
// builder.Services.AddScoped<IValidator<DMovie>, UpdateMovieValidation>();
// builder.Services.AddScoped<IValidator<DMovie>, DeleteMovieValidation>();
//
// builder.Services.AddScoped<IValidator<DCast>, CreateCastValidation>();
// builder.Services.AddScoped<IValidator<DCast>, UpdateCastValidation>();
// builder.Services.AddScoped<IValidator<DCast>, DeleteCastValidation>();



const string myAllowSpecificOrigins = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: myAllowSpecificOrigins,
        policy  =>
        {
            // policy.WithOrigins("");
            policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
        });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseCors(myAllowSpecificOrigins);

app.Run();