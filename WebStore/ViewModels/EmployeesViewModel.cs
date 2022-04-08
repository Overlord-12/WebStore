using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.ViewModels;

public class EmployeesViewModel : IValidatableObject
{
    [HiddenInput(DisplayValue = false)]
    public int Id { get; set; }

    [Display(Name = "Фамилия")]
    [Required(ErrorMessage = "Фамилия является обязательной")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "Длина фамилии должна быть от 2 до 255 символов")]
    [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата строки")]
    public string LastName { get; set; }

    [Display(Name = "Имя")]
    [Required(ErrorMessage = "Имя является обязательным")]
    [StringLength(255, MinimumLength = 2, ErrorMessage = "Длина имени должна быть от 2 до 255 символов")]
    [RegularExpression(@"([А-ЯЁ][а-яё]+)|([A-Z][a-z]+)", ErrorMessage = "Ошибка формата строки")]
    public string FirstName { get; set; }

    [Display(Name = "Отчество")]
    [StringLength(255, ErrorMessage = "Длина отчества должна быть до 255 символов")]
    [RegularExpression(@"(([А-ЯЁ][а-яё]+)|([A-Z][a-z]+))?", ErrorMessage = "Ошибка формата строки")]
    public string? Patronymic { get; set; }


    [Display(Name = "ФИО")]
    [StringLength(255, ErrorMessage = "Длина ФИО должна быть до 255 символов")]
    public string? ShortName { get; set; }

    [Display(Name = "Возраст")]
    [Range(18, 80, ErrorMessage = "Возраст должен быть в пределах от 18 до 80 лет")]
    public int Age { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext Context)
    {
        if (LastName.Length > 20)
            yield return new ValidationResult("Длина фамилии превысила 20 символов", new []{ nameof(LastName) });

        yield return ValidationResult.Success!;
    }
}