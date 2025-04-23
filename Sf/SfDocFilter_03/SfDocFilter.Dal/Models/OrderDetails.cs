using System.ComponentModel.DataAnnotations;

// using Microsoft.AspNetCore.Http.HttpResults;
// using Syncfusion.Blazor.DataForm;

namespace SfDocFilter_03.Dal.Models
{
    public class OrderDetails
    {
        public OrderDetails(){}

        // Group 1
        [Required(ErrorMessage = "First Name is required")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string? EmailID { get; set; }

        [Required(ErrorMessage = "Order Date is required")]
        public DateTime? OrderDate { get; set; }

        [Required(ErrorMessage = "You must agree to the Terms and Conditions")]
        public bool AcceptTerms { get; set; }

        //Group-2 Shipping Address
        [Required(ErrorMessage = "Shipped Date is required")]
        public DateTime? ShippedDate { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City { get; set; }

        [Required(ErrorMessage = "Address Line is required")]
        public string? AddressLine { get; set; }

        public string? AddressLine2 { get; set; }
        [Required(ErrorMessage = "Product Name is required")]
        public string? ProductName { get; set; }

        public int Quantity { get; set; }


        public string SelectedPaymentMethod { get; set; } = "credit/debit";    


        //Group-3 Another column
        [Required(ErrorMessage = "Shipped Date is required")]
        public DateTime? ShippedDate1 { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string? Country1 { get; set; }

        [Required(ErrorMessage = "City is required")]
        public string? City1 { get; set; }

        [Required(ErrorMessage = "Address Line is required")]
        public string? AddressLine1 { get; set; }

        public string? AddressLine21 { get; set; }

        [Required(ErrorMessage = "Country 2 is required")]
        public string? Country2 { get; set; }




    }

}
