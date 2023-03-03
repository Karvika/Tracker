using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Tracker_App.Models
{
    public class ExpenseCategory
    {
        public int id { get; set; }
        public string categoryName { get; set; }
    }
    public class AddExpense
    {
        public int id { get; set; }
        [ForeignKey("expenseCategory")]

        [DisplayName("Category Name")]
        public int categId { get; set; }
        [DisplayName("Amount")]
        public float expenseAmount { get; set; }
        [DisplayName("Date")]
        public DateTime date { get; set; }
        public ExpenseCategory expenseCategory { get; set; }
        [DisplayName("UserName")]
        public string userEmail { get; set; }
    }
    public class SetBudget
    {
        public int id { get; set; }
        public float budgetAmount { get; set; }
        [ForeignKey("expenseCategory")]
        [DisplayName("CategoryName")]
        public int categId { get; set; }
        [DisplayName("UserName")]
        public string userEmail { get; set; }
        public ExpenseCategory expenseCategory { get; set; }


    }
   /* public class MostExpensiveExpenditure
    {
        public int id { get; set; }
        [ForeignKey("addExpenseCategory")]
        public int categId { get; set; }
        public string name { get; set; }
        public string category { get; set; }
        public AddExpense addExpenseCategory { get; set; }
    }*/
    
   
   
   
        public class TrackerDBContext : DbContext
    {
        public DbSet<ExpenseCategory> ExpenseCategories { get; set; }
        public DbSet<AddExpense> AddExpenses { get; set; }
        public DbSet<SetBudget> SetBudgets { get; set; }
        
        

    }
}