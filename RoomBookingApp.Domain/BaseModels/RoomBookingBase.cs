using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RoomBookingApp.Domain.BaseModels
{
    /// <summary>
    /// We don't want to create an instance from Room booking base class so we add abstract
    /// </summary>
    public abstract class RoomBookingBase : IValidatableObject
    {
        [Required]
        [StringLength(80)]
        public string FullName { get; set; }

        [Required]
        [StringLength(80)]
        [EmailAddress]
        public string Email { get; set; }

        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (Date <= DateTime.Now.Date)
            {
                yield return new ValidationResult("Date must be in the Future", new[] { nameof(Date) });
            }
        }
    }
}