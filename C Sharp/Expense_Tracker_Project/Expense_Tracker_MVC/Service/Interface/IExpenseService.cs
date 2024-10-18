﻿using Expense_Tracker_MVC.Models;

namespace Expense_Tracker_MVC.Service.Interface
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expenses>> Get();

        Task<Expenses> GetByID(int? id);

        Task Delete(int id);

        Task Create(Expenses entity);

        Task Edit(Expenses entity);
    }
}
