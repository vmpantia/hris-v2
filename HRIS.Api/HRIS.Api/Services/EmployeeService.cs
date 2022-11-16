using HRIS.Api.Models;
using HRIS.Api.Models.RequestModels;
using HRIS.Api.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HRIS.Api.Services
{
    public class EmployeeService
    {
        public static Employee GetEmployee(Guid internalID)
        {
            if(internalID == null || internalID == Guid.Empty)
            {
                throw new ArgumentNullException(nameof(internalID));
            }

            using (DBModel db = new DBModel())
            {
                var result = db.Employees.ToList().Where(data => data.InternalID == internalID);
                if (result.Count() < 0)
                {
                    throw new Exception(Constant.ERROR_INTERNALID_NOT_FOUND);
                }
                return result.First();
            }
        }

        public static EmployeeRequest SaveEmployee(EmployeeRequest employeeRequest)
        {
            if (employeeRequest == null || employeeRequest.inputRequest == null || employeeRequest.inputEmployee == null)
            {
                throw new ArgumentNullException(nameof(employeeRequest));
            }

            using (DBModel db = new DBModel())
            {
                var transaction = db.Database.BeginTransaction();
                try
                {
                    /*Insert Employee Request*/
                    RequestService.InsertRequest(employeeRequest.inputRequest, db);

                    /*Insert or Update new Employee Record*/
                    InsertOrUpdateEmployee(employeeRequest.inputEmployee, db);

                    /*Insert Employee Transaction*/
                    InsertEmployee_TRN(employeeRequest, db);

                    db.SaveChanges();
                    transaction.Commit();
                    transaction.Dispose();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    transaction.Dispose();
                    throw ex;
                }
            }
            return employeeRequest;
        }

        private static void InsertOrUpdateEmployee(Employee inputEmployee, DBModel db)
        {
            var isNew = inputEmployee.InternalID == Guid.Empty;
            if (isNew)
            {
                /*Do inserting of new Employee Record*/
                inputEmployee.InternalID = Guid.NewGuid();
                db.Employees.Add(inputEmployee);
                return;
            }

            /*Do updating of updated Employee Record*/
            var result = db.Employees.Where(data => data.InternalID == inputEmployee.InternalID).First();
            //result.InternalID = inputEmployee.InternalID;
            //result.EmployeeID = inputEmployee.EmployeeID;
            result.FirstName = inputEmployee.FirstName;
            result.LastName = inputEmployee.LastName;
            result.MiddleName = inputEmployee.MiddleName;
            result.Suffix = inputEmployee.Suffix;
            result.Gender = inputEmployee.Gender;
            result.Birthdate = inputEmployee.Birthdate;
            result.CivilStatus = inputEmployee.CivilStatus;
            result.Status = inputEmployee.Status;
            //result.CreatedDate = inputEmployee.CreatedDate;
            //result.CreatedBy = inputEmployee.CreatedBy;
            result.ModifiedDate = inputEmployee.ModifiedDate;
            result.ModifiedBy = inputEmployee.ModifiedBy;
            UpdateEmployeeContacts(inputEmployee, db);
            return;
        }

        private static void UpdateEmployeeContacts(Employee inputEmployee, DBModel db)
        {
            db.DeleteContactsByInternalID(inputEmployee.InternalID);
            foreach(var contact in inputEmployee.Contacts)
            {
                db.Contacts.Add(contact);
            }
        }

        private static void InsertEmployee_TRN(EmployeeRequest employeeRequest, DBModel db)
        {
            db.Employee_TRN.Add(new Employee_TRN
            {
                RequestID = employeeRequest.inputRequest.RequestID,
                InternalID = employeeRequest.inputEmployee.InternalID,
                EmployeeID = employeeRequest.inputEmployee.EmployeeID,
                FirstName = employeeRequest.inputEmployee.FirstName,
                LastName = employeeRequest.inputEmployee.LastName,
                MiddleName = employeeRequest.inputEmployee.MiddleName,
                Suffix = employeeRequest.inputEmployee.Suffix,
                Gender = employeeRequest.inputEmployee.Gender,
                Birthdate = employeeRequest.inputEmployee.Birthdate,
                CivilStatus = employeeRequest.inputEmployee.CivilStatus,
                Status = employeeRequest.inputEmployee.Status,
                CreatedDate = employeeRequest.inputEmployee.CreatedDate,
                CreatedBy = employeeRequest.inputEmployee.CreatedBy,
                ModifiedDate = employeeRequest.inputEmployee.ModifiedDate,
                ModifiedBy = employeeRequest.inputEmployee.ModifiedBy
            });
        }

    }
}