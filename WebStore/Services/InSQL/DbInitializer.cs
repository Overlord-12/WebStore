using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WebStore.DAL.Context;
using WebStore.Data;
using WebStore.Domain.Entities.Identity;
using WebStore.Services.Interfaces;

namespace WebStore.Services.InSQL;

public class DbInitializer : IDbInitializer
{
    private readonly WebStoreDB _db;
    private readonly UserManager<User> _UserManager;
    private readonly RoleManager<Role> _RoleManager;
    private readonly ILogger<DbInitializer> _Logger;

    public DbInitializer(
        WebStoreDB db, 
        UserManager<User> UserManager,
        RoleManager<Role> RoleManager,
        ILogger<DbInitializer> Logger)
    {
        _db = db;
        _UserManager = UserManager;
        _RoleManager = RoleManager;
        _Logger = Logger;
    }

    public async Task<bool> RemoveAsync(CancellationToken Cancel = default)
    {
        _Logger.LogInformation("Удаление БД...");

        var removed = await _db.Database.EnsureDeletedAsync(Cancel).ConfigureAwait(false);

        if(removed)
            _Logger.LogInformation("БД удалена успешно.");
        else
            _Logger.LogInformation("Удаление БД не требуется (отсутствует на сервере).");

        return removed;
    }

    public async Task InitializeAsync(bool RemoveBefore = false, CancellationToken Cancel = default)
    {
        _Logger.LogInformation("Инициализация БД...");

        if (RemoveBefore)
            await RemoveAsync(Cancel).ConfigureAwait(false);

        //await _db.Database.EnsureCreatedAsync(Cancel).ConfigureAwait(false);

        var pending_migrations = await _db.Database.GetPendingMigrationsAsync(Cancel).ConfigureAwait(false);
        if (pending_migrations.Any())
        {
            _Logger.LogInformation("Выполнение миграции БД...");
            await _db.Database.MigrateAsync(Cancel).ConfigureAwait(false);
            _Logger.LogInformation("Выполнение миграции БД выполнено успешно.");
        }
        else
            _Logger.LogInformation("Миграция БД не требуется.");

        await InitializeProductsAsync(Cancel).ConfigureAwait(false);

        await InitializeIdentityAsync(Cancel).ConfigureAwait(false);

        _Logger.LogInformation("Инициализация БД выполнена успешно.");
    }

    private async Task InitializeProductsAsync(CancellationToken Cancel)
    {
        if (await _db.Products.AnyAsync(Cancel).ConfigureAwait(false))
        {
            _Logger.LogInformation("Инициализация БД тестовыми данными не требуется");
            return;
        }

        _Logger.LogInformation("Инициализация БД тестовыми данными...");


        var sections_pool = TestData.Sections.ToDictionary(s => s.Id);
        var brands_pool = TestData.Brands.ToDictionary(b => b.Id);

        foreach (var child_section in TestData.Sections.Where(s => s.ParentId is not null))
            child_section.Parent = sections_pool[(int)child_section.ParentId!];

        foreach (var product in TestData.Products)
        {
            product.Section = sections_pool[product.SectionId];
            if (product.BrandId is { } brand_id)
                product.Brand = brands_pool[brand_id];

            product.Id = 0;
            product.SectionId = 0;
            product.BrandId = null;
        }

        foreach (var brand in TestData.Brands)
            brand.Id = 0;

        foreach (var section in TestData.Sections)
        {
            section.Id = 0;
            section.ParentId = null;
        }

        _Logger.LogInformation("Добавление данных в БД.");
        await using (var transaction = await _db.Database.BeginTransactionAsync(Cancel))
        {
            await _db.Sections.AddRangeAsync(TestData.Sections, Cancel);
            await _db.Brands.AddRangeAsync(TestData.Brands, Cancel);
            await _db.Products.AddRangeAsync(TestData.Products, Cancel);

            await _db.SaveChangesAsync(Cancel);

            await transaction.CommitAsync(Cancel).ConfigureAwait(false);
        }

        _Logger.LogInformation("Инициализация БД тестовыми данными выполнена успешно.");
    }

    private async Task InitializeIdentityAsync(CancellationToken Cancel)
    {
        async Task CheckRoleAsync(string RoleName)
        {
            if (await _RoleManager.RoleExistsAsync(RoleName))
                _Logger.LogInformation("Роль {0} существует", RoleName);
            else
            {
                _Logger.LogInformation("Роль {0} не существует. Создаю...", RoleName);

                await _RoleManager.CreateAsync(new Role { Name = RoleName });

                _Logger.LogInformation("Роль {0} успешно создана", RoleName);
            }
        }

        await CheckRoleAsync(Role.Adinistrators);
        await CheckRoleAsync(Role.Users);

        if (await _UserManager.FindByNameAsync(User.Administrator) is null)
        {
            _Logger.LogInformation("Пользователь {0} не найден. Создаю...", User.Administrator);

            var admin = new User
            {
                UserName = User.Administrator,
            };

            var creation_result = await _UserManager.CreateAsync(admin, User.DefaultAdminPassword);
            if (creation_result.Succeeded)
            {
                _Logger.LogInformation("Пользователь {0} создан успешно", User.Administrator);

                await _UserManager.AddToRoleAsync(admin, Role.Adinistrators);

                _Logger.LogInformation("Пользователь {0} наделён ролью {1}", User.Administrator, Role.Adinistrators);
            }
            else
            {
                var errors = creation_result.Errors.Select(e => e.Description);
                _Logger.LogError("Учётная запись {0} не может быть создана. Ошибки: {1}", 
                    User.Administrator, 
                    string.Join(", ", errors));

                throw new InvalidOperationException($"Невозможно создать пользователя {User.Administrator} по причине {string.Join(", ", errors)}");
            }
        }
        else
            _Logger.LogInformation("Пользователь {0} существует.", User.Administrator);

    }
}
