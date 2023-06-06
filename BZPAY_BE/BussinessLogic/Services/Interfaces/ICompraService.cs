﻿using BZPAY_BE.DataAccess;
using BZPAY_BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace BZPAY_BE.BussinessLogic.Services.Interfaces
{
    public interface ICompraService
    {
        Task<IEnumerable<Compra>> GetAllComprasAsync();
        Task<bool> CreateCompraAsync([FromBody] EnterAmountTicket compra);
    }
}