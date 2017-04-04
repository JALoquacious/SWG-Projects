﻿namespace CarDealership.Models.Tables
{
    public class Contact
    {
        public int ContactId  { get; set; }
        public int VehicleId  { get; set; }
        public string UserId  { get; set; }
        public string Name    { get; set; }
        public string Phone   { get; set; }
        public string Email   { get; set; }
        public string Message { get; set; }
    }
}
