using HCM.DataAccess.Repository.IRepository;
using HCM.Models.Models;
using HCM.Services.PayrollClient.Interfaces;
using HCM.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HCM.Services.PayrollClient.Implementations
{
    public class PayrollEngineService : IPayrollEngineService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PayrollEngineService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        //public async Task<List<PayrollResult>> RunBatchPayrollAsync(DateTime periodStart, DateTime periodEnd)
        //{
        //    var employees =  _unitOfWork.Employee.GetAll();
        //    var results = new List<PayrollResult>();

        //    foreach (var employee in employees)
        //    {
        //        var payments = await _unitOfWork.EmployeePayRepository.Get(e=>e.EmployeeId == employee.EmployeeId);

        //        // Build dictionary of component values
        //        var componentValues = payments.ToDictionary(
        //            p => p.PayComponent.ComponentName,
        //            p => p.MonthlyAmount
        //        );

        //        // Evaluate formulas recursively
        //        foreach (var pay in payments)
        //        {
        //            if (!string.IsNullOrEmpty(pay.PayComponent.PayFormula))
        //            {
        //                var evaluator = new FormulaEvaluator(componentValues);
        //                var value = evaluator.Evaluate(pay.PayComponent.PayFormula);
        //                pay.MonthlyAmount = value;
        //                pay.AnnualAmount = value * 12;

        //                // Update dictionary for dependent formulas
        //                componentValues[pay.PayComponent.ComponentName] = value;
        //            }
        //        }

        //        decimal gross = payments.Where(p => p.PayComponent.ComponentType == "Earning").Sum(p => p.MonthlyAmount);
        //        decimal deductions = payments.Where(p => p.PayComponent.ComponentType == "Deduction").Sum(p => p.MonthlyAmount);

        //        var result = new PayrollResult
        //        {
        //            EmployeeId = employee.EmployeeId,
        //            GrossPay = gross,
        //            TotalDeductions = deductions,
        //            NetPay = gross - deductions,
        //            PayPeriodStart = periodStart,
        //            PayPeriodEnd = periodEnd,
        //            ProcessedDate = DateTime.Now
        //        };

        //        _unitOfWork.PayrollResultRepository.Add(result);
        //        results.Add(result);
        //    }

        //    _unitOfWork.Save();
        //    return results;
        //}
    }


}
