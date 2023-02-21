using Demo20230215.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Demo20230215.services
{/// <summary>
/// 取得default Empolee Date
/// </summary>
/// <returns>List的員工物件</returns>
    public class EmpMethods
    {
        public List<EmployeeModel> GetEmpData()
        { List<EmployeeModel> employeeModels= new List<EmployeeModel>();
            employeeModels.Add(new EmployeeModel() { EmpNo = "CT3040", Name ="張建生", Ext = 12365   });
            employeeModels.Add(new EmployeeModel() { EmpNo = "CT3041", Name ="張建生1", Ext = 123 });
            employeeModels.Add(new EmployeeModel() { EmpNo = "CT3042", Name ="張建生2", Ext = 23453 });
            employeeModels.Add(new EmployeeModel() { EmpNo = "CT3043", Name ="張建生3", Ext = 6543 });
            return employeeModels;
        }

        public List<EmployeeModel> InsertEmpData(List<EmployeeModel> employeeModels, string EmpNo , string Name , int Ext)
        {
            employeeModels.Add(new EmployeeModel() { EmpNo = EmpNo, Name = Name, Ext = Ext });
            return employeeModels;
        }
    }
}