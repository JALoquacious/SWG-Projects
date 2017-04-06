using CarDealership.DAL.Interfaces;
using CarDealership.Models.Tables;
using System.Collections.Generic;
using System.Linq;

namespace CarDealership.DAL.Repositories.Mock
{
    public class ContactRepositoryMock : IContactRepository
    {
        private static List<Contact> _contacts;

        static ContactRepositoryMock()
        {
            _contacts = new List<Contact>()
            {
                new Contact()
                {
                    ContactId = 1,
                    Name = "John Doe",
                    Phone = null,
                    Email = "john_doe@test1.com",
                    Message = "Test message 1"
                },
                new Contact()
                {
                    ContactId = 2,
                    Name = "Jane Doe",
                    Phone = "2345678901",
                    Email = "jane_doe@test2.com",
                    Message = "Test message 2"
                },
                new Contact()
                {
                    ContactId = 3,
                    Name = "Bill Smith",
                    Phone = null,
                    Email = "bill_smith@test3.com",
                    Message = "Test message 3"
                },
                new Contact()
                {
                    ContactId = 4,
                    Name = "Bob Jones",
                    Phone = "4567890123",
                    Email = null,
                    Message = "Test message 4"
                }
            };
        }

        public void Insert(Contact targetContact)
        {
            if (_contacts.Any())
            {
                targetContact.ContactId = _contacts.Max(c => c.ContactId) + 1;
            }
            else
            {
                targetContact.ContactId = 1;
            }
            _contacts.Add(targetContact);
        }
    }
}
