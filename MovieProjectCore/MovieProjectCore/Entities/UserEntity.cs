using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MovieProjectCore.Entities
{
    [Table("User")]
    public sealed class UserEntity
    {
        [Key]
        [Display(Name = "User Id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [StringLength(50)]
        [Display(Name = "Full name")]
        public string FullName { get; set; }

        [StringLength(50)]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
