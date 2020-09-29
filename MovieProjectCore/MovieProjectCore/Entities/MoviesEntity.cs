using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProjectCore.Entities
{
    [Table("Movie")]
    public sealed class MoviesEntity
    {
        [Key]
        [Display(Name = "Movie Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MovieId { get; set; }

        [StringLength(30)]
        [Display(Name = "Movie Title")]
        public string MovieTitle { get; set; }

        [StringLength(30)]
        [Display(Name = "Movie Description")]
        public string MovieDescription { get; set; }

        [Display(Name = "Is Rented")]
        public bool IsRented { get; set; }

        [Display(Name = "Rental Date")]
        public DateTime RentalDate { get; set; }

        [Display(Name = "Is Deleted")]
        public bool IsDeleted { get; set; }
    }
}
