using AutoMapper;
using Microsoft.EntityFrameworkCore;
using VOD.Membership.Database.Contexts;
using VOD.Membership.Database.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy => {
    policy.AddPolicy("CorsAllAccessPolicy", opt =>
    opt.AllowAnyOrigin()
    .AllowAnyHeader()
    .AllowAnyMethod()
    );
});

builder.Services.AddDbContext<VODContext>(
    options =>
        options.UseSqlServer(
            builder.Configuration.GetConnectionString("VODConnection")));

ConfigureAutoMapper();

builder.Services.AddScoped<IDbService, DbService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("CorsAllAccessPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();

 void ConfigureAutoMapper(){

    
    var config = new AutoMapper.MapperConfiguration(cfg =>
    {
        // film 
        cfg.CreateMap<Film, FilmDTO>().ReverseMap();
        cfg.CreateMap<FilmCreateDTO, Film>();
        cfg.CreateMap<FilmEditDTO, Film>();

        cfg.CreateMap<Genre, GenreDTO>().ReverseMap();
        cfg.CreateMap<GenreCreateDTO, Genre>();
        cfg.CreateMap<GenreEditDTO, Genre>();

        cfg.CreateMap<Director, DirectorDTO>().ReverseMap();
        cfg.CreateMap<DirectorEditDTO, Director>();
        cfg.CreateMap<DirectorCreateDTO, Director>();

        cfg.CreateMap<FilmGenre, FilmGenreDTO>().ReverseMap();
        cfg.CreateMap<FilmGenreDeleteDTO, FilmGenre>().ReverseMap();
        cfg.CreateMap<FilmGenreCreateDTO, FilmGenre>().ReverseMap();

        cfg.CreateMap<SimilarFilm, SimilarFilmDTO>().ReverseMap();
        cfg.CreateMap<SimilarFilmCreateDTO, SimilarFilm>();
    });

    var mapper = config.CreateMapper();

    
    builder.Services.AddSingleton(mapper);

}