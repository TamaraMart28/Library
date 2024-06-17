using LibraryBusinessLogics.BusinessLogics;
using LibraryContracts.BusinessLogicsContracts;
using LibraryContracts.StoragesContracts;
using LibraryDatebaseImplement.Implements;
using LibraryBusinessLogics.OfficePackage;
using LibraryBusinessLogics.OfficePackage.Implements;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//
//storage
builder.Services.AddTransient<IAdminStorage, AdminStorage>();
builder.Services.AddTransient<IBookStorage, BookStorage>();
builder.Services.AddTransient<IBookBranchStorage, BookBranchStorage>();
builder.Services.AddTransient<IBranchStorage, BranchStorage>();
builder.Services.AddTransient<ILibrarianStorage, LibrarianStorage>();
builder.Services.AddTransient<IReaderStorage, ReaderStorage>();
builder.Services.AddTransient<IReaderBookStorage, ReaderBookStorage>();
builder.Services.AddTransient<IFavoriteBooksStorage, FavoriteBooksStorage>();

//logic
builder.Services.AddTransient<IAdminLogic, AdminLogic>();
builder.Services.AddTransient<IBookLogic, BookLogic>();
builder.Services.AddTransient<IBookBranchLogic, BookBranchLogic>();
builder.Services.AddTransient<IBranchLogic, BranchLogic>();
builder.Services.AddTransient<ILibrarianLogic, LibrarianLogic>();
builder.Services.AddTransient<IReaderLogic, ReaderLogic>();
builder.Services.AddTransient<IReaderBookLogic, ReaderBookLogic>();
builder.Services.AddTransient<IFavoriteBooksLogic, FavoriteBooksLogic>();
builder.Services.AddTransient<IReaderReportLogic, ReaderReportLogic>();
builder.Services.AddTransient<ReaderAbstractSaveToPdf, ReaderSaveToPdf>();
//
builder.Services.AddTransient<ILibrarianReportLogic, LibrarianReportLogic>();
builder.Services.AddTransient<LibrarianAbstractSaveToPdf, LibrarianSaveToPdf>();
//

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

app.Run();
