using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MainMusicStore.Models.DbModels
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        
        [Required(ErrorMessage ="Bu alan boş geçilemez")]
        [StringLength(250,MinimumLength = 2,ErrorMessage = "Kategori şartlarına uygun değer giriniz (2-250 characters.)")]
        public string CategoryName { get; set; }



    }
}
