using System.ComponentModel.DataAnnotations;

namespace Mearcury.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}