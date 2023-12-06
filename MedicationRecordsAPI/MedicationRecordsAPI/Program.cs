using FluentValidation;
using MedicationRecords.Application.Services;
using MedicationRecords.Application.Services.Implementations;
using MedicationRecords.Application.Validators;
using MedicationRecords.Contracts.Requests;
using MedicationRecords.Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

namespace MedicationRecords.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddDbContext<MedicationContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
            });

            builder.Services.AddScoped<IActiveIngredientsService, ActiveIngredientsService>();
            builder.Services.AddScoped<IATCCodeService, ATCCodeService>();
            builder.Services.AddScoped<IClassificationService, ClassificationService>();
            builder.Services.AddScoped<IDataInitializationService, DataInitializationService>();
            builder.Services.AddScoped<IMedicationService, MedicationService>();
            builder.Services.AddScoped<IPharmaceuticalFormsService, PharmaceuticalFormsService>();
            builder.Services.AddScoped<ITherapeuticClassService, TherapeuticClassService>();
            builder.Services.AddScoped<IUnitService, UnitService>();

            builder.Services.AddScoped<IValidator<CreateMedicationRequest>, CreateMedicationRequestValidator>();
            builder.Services.AddScoped<IValidator<UpdateMedicationRequest>, UpdateMedicationRequestValidator>();

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
        }
    }
}
