using System;
using P01_BillsPaymentSystem.Data.Models;
using P01_BillsPaymentSystem.Data;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;

namespace P01_BillsPaymentSystem.App
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            using (var db = new BillsPaymentSystemContext())
            {
                //db.Database.EnsureDeleted();
                //db.Database.Migrate();
                //Seed(db);

                //Console.WriteLine("Enter UserId: ");
                //int.TryParse(Console.ReadLine(), out int userid);
                //if (db.Users.Find(userid) == null)
                //{
                //    Console.WriteLine($"User with id {userid} not found!");
                //}
                //else
                //{
                //    var user = db.Users.Where(u => u.UserId == userid).Select(u => new
                //    {
                //        Name = $"{u.FirstName} {u.LastName}",
                //        CreditCards = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.CreditCard).Select(pm => pm.CreditCard).ToList(),
                //        BankAccounts = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.BankAccount).Select(pm => pm.BankAccount).ToList()
                //    }).FirstOrDefault();

                //    var bankAccounts = user.BankAccounts;
                //    var creditCards = user.CreditCards;
                //    Console.WriteLine($"{user.Name}");
                //    if (bankAccounts.Any())
                //    {
                //        Console.WriteLine("BankAccounts: ");
                //        foreach (var ba in bankAccounts)
                //        {
                //            Console.WriteLine($"-- ID: {ba.BankAccountId}\n-- - Balance: {ba.Balance:F2}\n-- - Bank: {ba.BankName}\n-- - SWIFT: {ba.SwiftCode}");
                //        }
                //    }

                //    if (creditCards.Any())
                //    {
                //        Console.WriteLine("Credit Cards:");
                //        foreach (var cc in creditCards)
                //        {
                //            Console.WriteLine($"-- ID: {cc.CreditCardId}\n-- - Limit: {cc.Limit:f2}\n-- - Money Owed: {cc.MoneyOwed:f2}\n-- - Limit Left:: {cc.LimitLeft:f2}\n-- - Expiration Date: {cc.ExpirationDate.ToString("yyyy/mm", CultureInfo.InvariantCulture)}");
                //        }
                //    }
                //};
                Console.WriteLine("Enter UserId: ");
                int.TryParse(Console.ReadLine(), out int userid);
                Console.WriteLine("Enter bills amount to be payed: ");
                decimal.TryParse(Console.ReadLine(), out decimal amount);
                string  result = PayBills(userid, amount, db);
                
            }
        }

        //private static void Seed(BillsPaymentSystemContext db)
        //{
        //    using (db)
        //    {
        //        var user1 = new User()
        //        {
        //            FirstName = "Choko",
        //            LastName = "Boko",
        //            Email = "choko.boko@mail.bg",
        //            Password = "ChokoBoko1234"
        //        };

        //        var creditrcards = new CreditCard[]
        //        {
        //                    new CreditCard
        //                    {
        //                        ExpirationDate = DateTime.ParseExact("19.07.2017", "dd.mm.yyyy",CultureInfo.InvariantCulture),
        //                        Limit = 4500m,
        //                        MoneyOwed = 230m
        //                    },
        //                    new CreditCard
        //                    {
        //                        ExpirationDate = DateTime.ParseExact("01.12.1989", "dd.mm.yyyy",CultureInfo.InvariantCulture),
        //                        Limit = 10000m,
        //                        MoneyOwed = 0m
        //                    },
        //                    new CreditCard
        //                    {
        //                        ExpirationDate = DateTime.ParseExact("24.04.2010", "dd.mm.yyyy",CultureInfo.InvariantCulture),
        //                        Limit = 500m,
        //                        MoneyOwed = 50m
        //                    },
        //        };

        //        var bankAccount = new BankAccount
        //        {
        //            BankName = "UniCredit Bulbank",
        //            Balance = 15000m,
        //            SwiftCode = "SWEDJHEWI"
        //        };

        //        var paymentMethods = new PaymentMethod[]
        //        {
        //                    new PaymentMethod()
        //                    {
        //                        User = user1,
        //                        Type = PaymentMethodType.CreditCard,
        //                        CreditCard = creditrcards[0],
        //                        CreditCardId = 10
        //                    },
        //                    new PaymentMethod()
        //                    {
        //                        User = user1,
        //                        Type = PaymentMethodType.CreditCard,
        //                        CreditCard = creditrcards[1],
        //                        CreditCardId = 10
        //                    },
        //                    new PaymentMethod()
        //                    {
        //                        User = user1,
        //                        Type = PaymentMethodType.BankAccount,
        //                        BankAccount = bankAccount
        //                    }
        //        };

        //        //db.Users.Add(user1);
        //        //db.CreditCards.AddRange(creditrcards);
        //        //db.BankAccounts.Add(bankAccount);
        //        db.PaymentMethods.AddRange(paymentMethods);
        //        db.SaveChanges();
        //    }
        //}

        public static string PayBills(int userId, decimal amount, BillsPaymentSystemContext db)
        {
            var result = "";
            using (db)
            {
                var user = db.Users.Where(u => u.UserId == userId).Select(u => new
                {
                    Name = $"{u.FirstName} {u.LastName}",
                    CreditCardsAmounts = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.CreditCard).Select(pm => pm.CreditCard.LimitLeft).Sum(),
                    BankAccountsAmounts = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.BankAccount).Select(pm => pm.BankAccount.Balance).Sum(),
                    bankaccs = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.BankAccount).Select(pm => pm.BankAccount).ToList(),
                    ccs = u.PaymentMethods.Where(pm => pm.Type == PaymentMethodType.CreditCard).Select(pm => pm.CreditCard).ToList()
                }).FirstOrDefault();

                if (amount > (user.BankAccountsAmounts + user.CreditCardsAmounts))
                {
                    result = "Insufficient funds!";
                    Console.WriteLine(result);
                    return result;
                }
                else
                {
                    foreach (var account in user.bankaccs)
                    {
                        if (account.Balance > amount)
                        {
                            account.Withdraw(amount);
                            result = $"Paymen successful! You've withdrawn the {account.Balance} amount from your bank accounts!";
                            amount = 0;
                            System.Console.WriteLine(result);
                            return result;
                            //break;
                        }
                        else if (account.Balance == amount)
                        {
                            account.Withdraw(amount);
                            result = $"Paymen successful! You've withdrawn the {amount} amount from your bank accounts!";
                            amount = 0;
                            System.Console.WriteLine(result);
                            return result;
                        }
                        else
                        {
                            amount -= account.Balance;
                            account.Withdraw(account.Balance);
                            result = $"Withdrawn: {account.Balance:F2} money. You still owe {amount:F2}";
                            if (amount > 0)
                            {
                                foreach (var cc in user.ccs)
                                {
                                    if (cc.LimitLeft >= amount)
                                    {
                                        cc.Withdraw(amount);
                                        result = $"Paymen successful! You've withdrawn the {amount} amount from your credit cards!";
                                        Console.WriteLine(result);
                                        return result;
                                    }

                                    amount -= cc.LimitLeft;
                                    cc.Withdraw(cc.LimitLeft);
                                    result = $"Withdrawn: {cc.LimitLeft:F2} money. You still owe {amount:F2}";
                                    Console.WriteLine(result);
                                    return result;
                                }
                            }
                            return result;
                        }
                        
                    }
                    result = $"Paymen successful!You've made it!";
                    return result;
                }
            }
            
        }
    }
}
