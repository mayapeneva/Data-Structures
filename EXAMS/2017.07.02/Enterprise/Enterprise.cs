using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Enterprise : IEnterprise
{
    private Dictionary<Guid, Employee> byIdCollection = new Dictionary<Guid, Employee>();

    public int Count => this.byIdCollection.Count;

    public void Add(Employee employee)
    {
        this.byIdCollection[employee.Id] = employee;
    }

    public bool Contains(Employee employee)
    {
        return this.byIdCollection[employee.Id].Equals(employee);
    }

    public bool Contains(Guid guid)
    {
        return this.byIdCollection.ContainsKey(guid);
    }

    public bool Change(Guid guid, Employee employee)
    {
        if (this.byIdCollection.ContainsKey(guid))
        {
            var employeeToChange = this.byIdCollection[guid];
            employeeToChange.FirstName = employee.FirstName;
            employeeToChange.LastName = employee.LastName;
            employeeToChange.Salary = employee.Salary;
            employeeToChange.Position = employee.Position;
            employeeToChange.HireDate = employee.HireDate;
            return true;
        }

        return false;
    }

    public bool Fire(Guid guid)
    {
        if (!this.Contains(guid))
        {
            return false;
        }

        this.byIdCollection.Remove(guid);
        return true;
    }

    public bool RaiseSalary(int months, int percent)
    {
        var ifRaiseSuccessful = false;
        foreach (var empl in this.byIdCollection.Values)
        {
            if (empl.HireDate.AddMonths(months) <= DateTime.Now)
            {
                empl.Salary += empl.Salary * percent / 100.0;
                ifRaiseSuccessful = true;
            }
        }

        return ifRaiseSuccessful;
    }

    public Employee GetByGuid(Guid guid)
    {
        if (!this.Contains(guid))
        {
            throw new ArgumentException();
        }

        return this.byIdCollection[guid];
    }

    public Position PositionByGuid(Guid guid)
    {
        if (!this.Contains(guid))
        {
            throw new ArgumentException();
        }

        return this.byIdCollection[guid].Position;
    }

    public IEnumerable<Employee> GetByPosition(Position position)
    {
        var result = this.byIdCollection.Values.Where(e => e.Position.Equals(position));

        if (result.Any())
        {
            return result;
        }

        throw new ArgumentException();
    }

    public IEnumerable<Employee> GetBySalary(double minSalary)
    {
        var result = this.byIdCollection.Values.Where(e => e.Salary >= minSalary);

        if (result.Any())
        {
            return result;
        }

        throw new InvalidOperationException();
    }

    public IEnumerable<Employee> GetBySalaryAndPosition(double salary, Position position)
    {
        var result = this.byIdCollection.Values.Where(e => e.Salary.Equals(salary) && e.Position.Equals(position));

        if (result.Any())
        {
            return result;
        }

        throw new InvalidOperationException();
    }

    public IEnumerable<Employee> SearchBySalary(double minSalary, double maxSalary)
    {
        return this.byIdCollection.Values.Where(e => e.Salary >= minSalary && e.Salary <= maxSalary);
    }

    public IEnumerable<Employee> SearchByPosition(IEnumerable<Position> positions)
    {
        return this.byIdCollection.Values.Where(e => positions.Contains(e.Position));
    }

    public IEnumerable<Employee> SearchByFirstName(string firstName)
    {
        return this.byIdCollection.Values.Where(e => e.FirstName.Equals(firstName));
    }

    public IEnumerable<Employee> SearchByNameAndPosition(string firstName, string lastName, Position position)
    {
        return this.byIdCollection.Values.Where(e => e.FirstName.Equals(firstName) && e.LastName.Equals(lastName) && e.Position.Equals(position));
    }

    public IEnumerable<Employee> AllWithPositionAndMinSalary(Position position, double minSalary)
    {
        return this.byIdCollection.Values.Where(e => e.Salary >= minSalary && e.Position.Equals(position));
    }

    public IEnumerator<Employee> GetEnumerator()
    {
        return this.byIdCollection.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return this.GetEnumerator();
    }
}