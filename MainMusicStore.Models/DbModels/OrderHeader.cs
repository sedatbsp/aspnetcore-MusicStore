using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MainMusicStore.Models.DbModels
{
    public class OrderHeader
    {
        [Key]
        public int Id { get; set; }

        public string ApplicationUserId { get; set; }

        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime ShippingDate { get; set; }

        [Required]
        public double OrderTotal { get; set; }

        
        public string TrackingNumber { get; set; }

        public string Carrier { get; set; }

        public string OrderStatus { get; set; }

        public string PaymentStatus { get; set; }

        public DateTime PaymentDate { get; set; }

        public DateTime PaymentDueDate { get; set; }

        public string TransactionId { get; set; }

        [Required(ErrorMessage = "Telefon numarası alanı zorunludur.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Adres bilgilerinizin girilmesi zorunludur.")]
        public string StreetAddress { get; set; }
        [Required(ErrorMessage = "Şehir bilginizi girmeniz zorunludur.")]
        public string City { get; set; }
        [Required(ErrorMessage = "Ülke bilgisi alanı zorunludur.")]
        public string State { get; set; }
        [Required(ErrorMessage = "Posta kodu alanı zorunludur.")]
        public string PostCode { get; set; }
        [Required(ErrorMessage = "Ad alanı zorunludur.")]
        public string Name { get; set; }


    }
}
