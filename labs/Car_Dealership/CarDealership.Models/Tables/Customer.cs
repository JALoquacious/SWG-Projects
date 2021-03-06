﻿namespace CarDealership.Models.Tables
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public string UserId  { get; set; }
        public string Name    { get; set; }
        public string Phone   { get; set; }
        public string Email   { get; set; }
        public string Street1 { get; set; }
        public string Street2 { get; set; }
        public string City    { get; set; }
        public string State   { get; set; }
        public string Zip     { get; set; }
    }
}
