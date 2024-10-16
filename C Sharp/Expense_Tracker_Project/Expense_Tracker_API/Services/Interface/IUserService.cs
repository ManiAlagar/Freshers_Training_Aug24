﻿using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> Get();

        Task<Users> Get(int id);

        Task<bool> Add(Users user);

        Task Edit(Users entity);
    }


}