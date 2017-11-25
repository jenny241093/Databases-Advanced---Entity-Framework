using System;
using System.Linq;
using P01_BillsPaymentSystem.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using P01_BillsPaymentSystem.Data.Models;
using System.Globalization;
namespace P01_BillsPaymentSystem.App
{
    class StartUp
    {
        static void Main(string[] args)
        {
            //using (var context = new BillsPaymentSystemContext())
            //{
            //    context.Database.EnsureDeleted();
            //    context.Database.EnsureCreated();

            //    //context.Database.Migrate();
            //    Seed(context);
            //}
            var userId = int.Parse(Console.ReadLine());
            using (var db = new BillsPaymentSystemContext())
            {
                var user = db.Users.Where(u => u.UserId == userId)
                    .Select(u => new
                    {
                        Name = $"{u.FirstName} {u.LastName}",
                        CreditCards = u.PaymentMethods
                            .Where(pm => pm.PaymentType == PaymentMethodType.CreditCard)
                            .Select(pm => pm.CreditCard).ToList(),
                        BankAccounts = u.PaymentMethods
                            .Where(pm => pm.PaymentType == PaymentMethodType.BankAccount)
                            .Select(pm => pm.BankAccount).ToList()
                    }).FirstOrDefault();

                Console.WriteLine($"User: {user.Name}");
                var bankAccounts = user.BankAccounts;
                if (bankAccounts.Any())
                {
                    Console.WriteLine("Bank Accounts");
                    foreach (var acc in bankAccounts)
                    {
                        Console.WriteLine($@"-- ID: {acc.BankAccountId}
--- Balance: {acc.Balance:f2}
--- Bank: {acc.BankName}
--- SWIFT: {acc.SwiftCode}
");
                    }
                }


                var creditCards = user.CreditCards;
                if (creditCards.Any())
                {
                    Console.WriteLine("Credit Cards: ");
                    foreach (var cc in creditCards)
                    {
                        Console.WriteLine($@"-- ID: {cc.CreditCardId}
--- Limit: {cc.Limit:f2}
--- MoneyOwed: {cc.MoneyOwed}
--- Limit Left: {cc.LimitLeft}
--- Expiration Date: {cc.ExpirationDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}
");
                    }
                }

            }
        }

        private static void Seed(BillsPaymentSystemContext db)
        {
            var user = new User()
            {
                FirstName = "Pesho",
                LastName = "Stamatov",
                Email = "pesho@abv.bg",
                Password = "azsymPesho"

            };

            var creditCards = new CreditCard[]
            {
                new CreditCard()
                {
                    ExpirationDate = DateTime.ParseExact("20.05.2020","dd.MM.yyyy",null),
                   Limit = 1000m,
                   MoneyOwed = 5m
                },
                new CreditCard()
                { ExpirationDate = DateTime.ParseExact("20.05.2020","dd.MM.yyyy",null),
                    Limit = 400,
                    MoneyOwed = 10m

                }
            };
            var bankAccount = new BankAccount()
            {
                Balance = 1500m,
                BankName = "Swiss Bank",
                SwiftCode = "SSWSSBANK"

            };
            var paymentMethods = new PaymentMethod[]
            {
                new PaymentMethod()
                {
                    User = user,
                    CreditCard = creditCards[0],
                    PaymentType = PaymentMethodType.CreditCard
                },
                new PaymentMethod()
                {
                    User = user,
                    CreditCard = creditCards[1],
                    PaymentType = PaymentMethodType.CreditCard

                },
                new PaymentMethod()
                {
                    User = user,
                    CreditCard = creditCards[1],
                    PaymentType = PaymentMethodType.BankAccount

                }
            };
            db.Add(user);
            db.CreditCards.AddRange(creditCards);
            db.BankAccounts.Add(bankAccount);
            db.PaymentMethods.AddRange(paymentMethods);
            db.SaveChanges();
        }
    }
}
