using SalesLibrary;

CustomersController custCtrl = new CustomersController("localhost", "sqlexpress");

List<Customer> customers = custCtrl.GetBySalesRange(20000, 50000);

foreach(Customer eachCustomer in customers)
{
    Console.WriteLine($"{eachCustomer.Id} | {eachCustomer.Name} | {eachCustomer.City}, {eachCustomer.State} | {eachCustomer.Sales} | {eachCustomer.Active} ");
}

//Customer max = custCtrl.GetById(36);

//if(max is null)
//{
//    Console.WriteLine("Not found");
//    return;
//}

//max.Name = "MAX Technical Training";

//bool success = custCtrl.UpdateRow(max);

//if(!success)
//{
//    Console.WriteLine("Update failed!");
//    return;
//}

//Customer maxCustomer = new Customer
//{
//    Id = 0,
//    Name = "MAX",
//    City = "Mason",
//    State = "OH",
//    Sales = 1000,
//    Active = true
//};

//bool success = custCtrl.AddCustomer(maxCustomer);

//if(!success)
//{
//    Console.WriteLine("Add failed!");
//}

//List<Customer> customers = custCtrl.GetAll();

//foreach (Customer eachCustomer in customers)
//{
//    Console.WriteLine($"{eachCustomer.Id} | {eachCustomer.Name} | {eachCustomer.City} | {eachCustomer.State} | {eachCustomer.Sales} | {eachCustomer.Active}");
//}

//bool success = custCtrl.DeleteRow(36);

//Customer customerById = custCtrl.GetById(36);

//if(customerById is null)
//{
//    Console.WriteLine("Not found!");
//}
//else
//{
//    Console.WriteLine($"{customerById.Id} | {customerById.Name} | {customerById.City}, {customerById.State} | {customerById.Sales} | {customerById.Active}");
//}

custCtrl.CloseConnection();