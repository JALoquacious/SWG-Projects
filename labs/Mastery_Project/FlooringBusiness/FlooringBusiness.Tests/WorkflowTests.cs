using System;
using FlooringBusiness.BLL.Factories;
using FlooringBusiness.Models.Enums;
using FlooringBusiness.Models.Interfaces;
using FlooringBusiness.Models.Prototypes;
using NUnit.Framework;

namespace FlooringBusiness.Tests
{
    [TestFixture]
    public class WorkflowTests
    {
        [TestCase("1/1/2020", 1)]
        [TestCase("1/1/2020", 2)]
        [TestCase("1/1/2020", 3)]
        [TestCase("2/2/2020", 1)]
        [TestCase("2/2/2020", 2)]
        public void CanFindExistingOrder(string date, int orderNumber)
        {
            IWorkflow workflow = WorkflowFactory.Create(WorkflowType.Find);

            var request = new Request
            {
                Type = WorkflowType.Find,
                Date = DateTime.Parse(date),
                OrderNumber = orderNumber
            };

            Response response = workflow.Execute(request);

            Assert.IsNotNull(response.Order);
            Assert.IsTrue(response.Success);
            Assert.AreEqual(orderNumber, response.Order.OrderNumber);
        }

        [TestCase("5/5/2020", 1)] // unknown date
        [TestCase("2/2/2020", 9)] // unknown order number
        public void OrderNotFoundReturnsNull(string date, int orderNumber)
        {
            IWorkflow workflow = WorkflowFactory.Create(WorkflowType.Find);

            var request = new Request
            {
                Type = WorkflowType.Find,
                Date = DateTime.Parse(date),
                OrderNumber = orderNumber
            };

            Response response = workflow.Execute(request);

            Assert.IsNull(response.Order);
            Assert.IsFalse(response.Success);
        }

        [TestCase("1/1/2020", "Wise", "Carpet", "OH", 200, true)]
        [TestCase("4/5/1999", "Wise", "Carpet", "OH", 200, false)] // date in the past
        [TestCase("2/2/2020", "Amazon", "Tile", "NJ", 200, false)] // state not found
        [TestCase("2/2/2020", "Facebook", "Dirt", "PA", 200, false)] // product not found
        public void CanAddOrderIfRequestDataCorrect(string date, string customer, string product, string state,
            decimal area, bool expectedResult)
        {
            IWorkflow workflow = WorkflowFactory.Create(WorkflowType.Add);

            var request = new Request
            {
                Type = WorkflowType.Add,
                Date = DateTime.Parse(date),
                Customer = customer,
                Product = product,
                State = state,
                Area = area
            };

            Response response = workflow.Execute(request);

            Assert.AreEqual(expectedResult, response.Success);

        }

        [TestCase("1/1/2020", true, 5)]
        [TestCase("2/2/2020", true, 2)]
        [TestCase("1/1/1999", false, 0)] // date in the past
        [TestCase("9/9/2020", false, 0)] // unknown date
        public void CanDisplayOrderIfRequestDataCorrect(string date, bool expectedResult, int expectedOrderCount)
        {
            IWorkflow workflow = WorkflowFactory.Create(WorkflowType.Display);

            var request = new Request
            {
                Type = WorkflowType.Display,
                Date = DateTime.Parse(date)
            };

            Response response = workflow.Execute(request);

            Assert.AreEqual(expectedResult, response.Success);

            if (response.Success)
            {
                Assert.AreEqual(expectedOrderCount, response.Orders.Count);
            }
        }

        [TestCase("1/1/2020", 1, "The Software Guild", "Carpet", "OH", 200, true)]
        [TestCase("5/5/2020", 1, "Wise", "Carpet", "OH", 200, false)] // unknown date
        [TestCase("2/2/2020", 1, "Amazon", "Tile", "NJ", 200, false)] // state not found
        [TestCase("2/2/2020", 1, "Facebook", "Dirt", "PA", 200, false)] // product not found
        [TestCase("2/2/2020", 9, "Newegg", "Laminate", "IN", 200, false)] // order number not found
        public void CanEditOrderIfRequestDataCorrect(string date, int orderNumber, string customer, string product, string state,
            decimal area, bool expectedResult)
        {
            IWorkflow workflow = WorkflowFactory.Create(WorkflowType.Edit);

            var request = new Request
            {
                Type = WorkflowType.Edit,
                Date = DateTime.Parse(date),
                OrderNumber = orderNumber,
                Customer = customer,
                Product = product,
                State = state,
                Area = area
            };

            Response response = workflow.Execute(request);

            Assert.AreEqual(expectedResult, response.Success);
        }

        [TestCase("1/1/2020", 1, true)]
        [TestCase("2/2/2020", 2, true)]
        [TestCase("8/8/2020", 1, false)] // unknown date
        [TestCase("1/1/2020", 99, false)] // order number not found
        public void CanRemoveOrderIfRequestDataCorrect(string date, int orderNumber, bool expectedResult)
        {
            IWorkflow workflow = WorkflowFactory.Create(WorkflowType.Find);

            var request = new Request
            {
                Type = WorkflowType.Find,
                Date = DateTime.Parse(date),
                OrderNumber = orderNumber
            };

            Response response = workflow.Execute(request);

            if (response.Success)
            {
                request.Type = WorkflowType.Remove;
                response = workflow.Execute(request);

                Assert.AreEqual(expectedResult, response.Success);
            }
        }
    }
}
