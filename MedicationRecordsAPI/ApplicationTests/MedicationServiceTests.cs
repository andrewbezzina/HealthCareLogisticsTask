using FluentValidation;
using MedicationRecords.Application.Services;
using MedicationRecords.Application.Services.Implementations;
using MedicationRecords.Contracts.Requests;
using MedicationRecords.Domain.Enumerations;
using MedicationRecords.Domain.Models;
using MedicationRecords.Infrastructure.DataAccess;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using FluentValidation.Results;
using Moq;
using System;
using System.Data.Common;


namespace MedicationRecords.Application.Tests
{
    [TestClass]
    public class MedicationServiceTests : IDisposable
    {
        private readonly DbConnection _connection;
        private readonly DbContextOptions<MedicationContext> _contextOptions;

        private readonly Mock<IActiveIngredientsService> _mockActiveIngredientsService;
        private readonly Mock<IPharmaceuticalFormsService> _mockPharmaceuticalFormsService;
        private readonly Mock<ITherapeuticClassService> _mockTherapeuticClassService;
        private readonly Mock<IATCCodeService> _mockAtcCodeService;
        private readonly Mock<IClassificationService> _mockClassificationService;
        private readonly Mock<IUnitService> _mockUnitService;
        private readonly Mock<IValidator<CreateMedicationRequest>> _mockCreateMedicationRequestValidator;
        private readonly Mock<IValidator<UpdateMedicationRequest>> _mockUpdateMedicationRequestValidator;

        const string NAME_1 = "Test Medicine 1";
        const string ACTIVE_INGREDIENTS_1 = "Test Ingredients 1";
        const string PHARMACEUTICAL_FORM_1 = "Gel";
        const string THERAPEUTHIC_CLASS_1 = "STOMATOLOGICAL PREPARATIONS";
        const string ATC_CODE_1 = "A01AA01";
        const string CLASSIFICATION_1 = "POM";
        const string COMPETNET_AUTHORITY_STATUS = "Authorised";
        const string INTERNAL_STATUS = "Active";
        const string UNIT_1 = "ml(s)";


        #region ConstructorAndDispose
        public MedicationServiceTests()
        {
            _mockActiveIngredientsService = new Mock<IActiveIngredientsService>();
            _mockPharmaceuticalFormsService = new Mock<IPharmaceuticalFormsService>();
            _mockTherapeuticClassService = new Mock<ITherapeuticClassService>();
            _mockAtcCodeService = new Mock<IATCCodeService>();
            _mockClassificationService = new Mock<IClassificationService>();
            _mockUnitService = new Mock<IUnitService>();
            _mockCreateMedicationRequestValidator = new Mock<IValidator<CreateMedicationRequest>>();
            _mockUpdateMedicationRequestValidator = new Mock<IValidator<UpdateMedicationRequest>>();

            // Create and open a connection. This creates the SQLite in-memory database, which will persist until the connection is closed
            // at the end of the test (see Dispose below).
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            // These options will be used by the context instances in this test suite, including the connection opened above.
            _contextOptions = new DbContextOptionsBuilder<MedicationContext>()
                .UseSqlite(_connection)
                .Options;

            // Create the schema and seed some data
            using var context = new MedicationContext(_contextOptions);

            context.Database.EnsureCreated();

            context.AddRange(sampleData());
            context.SaveChanges();

        }

        private Medication sampleData()
        {
            return new Medication()
            {
                Name = NAME_1,
                ActiveIngredients = new ActiveIngredients() { ActiveIngredientList = ACTIVE_INGREDIENTS_1 },
                PharmaceuticalForms = new PharmaceuticalForms() { PharmaceuticalFormsList = PHARMACEUTICAL_FORM_1 },
                TherapeuticClass = new TherapeuticClass() { TherapeuticClassName = THERAPEUTHIC_CLASS_1 },
                ATCCode = new ATCCode() { Code = ATC_CODE_1},
                Classification = new Classification() { ClassificationName = CLASSIFICATION_1 },
                CompetentAuthorityStatus = CompetentAuthorityStatus.Authorised,
                InternalStatus = InternalStatus.Active,
                Unit = new Unit() { UnitName = UNIT_1 }
            };
        }

        MedicationContext CreateContext() => new MedicationContext(_contextOptions);

        public void Dispose() => _connection.Dispose();
        #endregion

        [TestMethod]
        public async Task Medication_Create_Valid()
        {
            // Arrange
            using var context = CreateContext();

            const string name = "Test Medicine 2";
            const string activeIngredient = "ActiveIngredient2";
            const string atcCode = "A01AB03";

            CreateMedicationRequest request = new(name, activeIngredient, PHARMACEUTICAL_FORM_1, THERAPEUTHIC_CLASS_1, atcCode, CLASSIFICATION_1, COMPETNET_AUTHORITY_STATUS, INTERNAL_STATUS, UNIT_1);

            _mockActiveIngredientsService.Setup(s => s.GetOrCreateAsync(
                It.Is<string>(s => s == activeIngredient))).Returns(Task.FromResult(new ActiveIngredients() { ActiveIngredientList = activeIngredient }));

            _mockPharmaceuticalFormsService.Setup(s => s.GetOrCreateAsync(
                It.Is<string>(s => s == PHARMACEUTICAL_FORM_1))).Returns(Task.FromResult(new PharmaceuticalForms() { Id = 1, PharmaceuticalFormsList = PHARMACEUTICAL_FORM_1 }));

            _mockTherapeuticClassService.Setup(s => s.GetOrCreateAsync(
                It.Is<string>(s => s == THERAPEUTHIC_CLASS_1))).Returns(Task.FromResult(new TherapeuticClass() { Id = 1, TherapeuticClassName = THERAPEUTHIC_CLASS_1 }));

            _mockAtcCodeService.Setup(s => s.GetOrCreateAsync(
                It.Is<string>(s => s == atcCode))).Returns(Task.FromResult(new ATCCode() { Code = atcCode }));

            _mockClassificationService.Setup(s => s.GetOrCreateAsync(
                It.Is<string>(s => s == CLASSIFICATION_1))).Returns(Task.FromResult(new Classification() { Id = 1, ClassificationName = CLASSIFICATION_1 }));

            _mockUnitService.Setup(s => s.GetOrCreateAsync(
                It.Is<string>(s => s == UNIT_1))).Returns(Task.FromResult(new Unit() { Id = 1, UnitName = UNIT_1 }));

            _mockCreateMedicationRequestValidator.Setup(v => v.Validate(
                It.IsAny<CreateMedicationRequest>())).Returns(new ValidationResult());


            var service = new MedicationService(context,
                                    _mockActiveIngredientsService.Object,
                                    _mockPharmaceuticalFormsService.Object,
                                    _mockTherapeuticClassService.Object,
                                    _mockAtcCodeService.Object,
                                    _mockClassificationService.Object,
                                    _mockUnitService.Object,
                                    _mockCreateMedicationRequestValidator.Object,
                                    _mockUpdateMedicationRequestValidator.Object);

            // Act
            var result = await service.CreateAsync(request);

            // Assert
            Assert.IsTrue(result.IsSuccess);
            Assert.IsNotNull(result.Value);
            Assert.AreEqual(request.Name, result.Value.Name);
            Assert.AreEqual(activeIngredient, result.Value.ActiveIngredients.ActiveIngredientList);
            Assert.AreEqual(atcCode, result.Value.ATCCode.Code);
            Assert.AreEqual(PHARMACEUTICAL_FORM_1, result.Value.PharmaceuticalForms.PharmaceuticalFormsList);
            Assert.AreEqual(UNIT_1, result.Value.Unit.UnitName);
            Assert.AreEqual(CLASSIFICATION_1, result.Value.Classification.ClassificationName);
            Assert.AreEqual(InternalStatus.Active, result.Value.InternalStatus);
            Assert.AreEqual(CompetentAuthorityStatus.Authorised, result.Value.CompetentAuthorityStatus);



            _mockActiveIngredientsService.VerifyAll();
            _mockPharmaceuticalFormsService.VerifyAll();
            _mockTherapeuticClassService.VerifyAll();
            _mockAtcCodeService.VerifyAll();
            _mockClassificationService.VerifyAll();
            _mockUnitService.VerifyAll();
            _mockUpdateMedicationRequestValidator.VerifyAll();
        }


        [TestMethod]
        public async Task Medication_Create_InValid()
        {
            // Arrange
            using var context = CreateContext();

            const string name = "Test Medicine 2";
            const string activeIngredient = "ActiveIngredient2";
            const string atcCode = "A01AB03";

            CreateMedicationRequest request = new(name, activeIngredient, PHARMACEUTICAL_FORM_1, THERAPEUTHIC_CLASS_1, atcCode, CLASSIFICATION_1, COMPETNET_AUTHORITY_STATUS, INTERNAL_STATUS, UNIT_1);

            _mockCreateMedicationRequestValidator.Setup(v => v.Validate(
                It.IsAny<CreateMedicationRequest>())).Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure() }));


            var service = new MedicationService(context,
                                    _mockActiveIngredientsService.Object,
                                    _mockPharmaceuticalFormsService.Object,
                                    _mockTherapeuticClassService.Object,
                                    _mockAtcCodeService.Object,
                                    _mockClassificationService.Object,
                                    _mockUnitService.Object,
                                    _mockCreateMedicationRequestValidator.Object,
                                    _mockUpdateMedicationRequestValidator.Object);

            // Act
            var result = await service.CreateAsync(request);

            // Assert
            Assert.IsTrue(result.IsFailed);

            _mockUpdateMedicationRequestValidator.VerifyAll();
        }


        [TestMethod]
        public async Task Medication_Create_NameAlreadyexists()
        {
            // Arrange
            using var context = CreateContext();

            const string name = NAME_1;
            const string activeIngredient = "ActiveIngredient2";
            const string atcCode = "A01AB03";

            CreateMedicationRequest request = new(name, activeIngredient, PHARMACEUTICAL_FORM_1, THERAPEUTHIC_CLASS_1, atcCode, CLASSIFICATION_1, COMPETNET_AUTHORITY_STATUS, INTERNAL_STATUS, UNIT_1);

            _mockCreateMedicationRequestValidator.Setup(v => v.Validate(
                It.IsAny<CreateMedicationRequest>())).Returns(new ValidationResult(new List<ValidationFailure> { new ValidationFailure() }));


            var service = new MedicationService(context,
                                    _mockActiveIngredientsService.Object,
                                    _mockPharmaceuticalFormsService.Object,
                                    _mockTherapeuticClassService.Object,
                                    _mockAtcCodeService.Object,
                                    _mockClassificationService.Object,
                                    _mockUnitService.Object,
                                    _mockCreateMedicationRequestValidator.Object,
                                    _mockUpdateMedicationRequestValidator.Object);

            // Act
            var result = await service.CreateAsync(request);

            // Assert
            Assert.IsTrue(result.IsFailed);

            _mockUpdateMedicationRequestValidator.VerifyAll();
        }
    }
}