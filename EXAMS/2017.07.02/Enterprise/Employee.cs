using System;

public class Employee
{
    public Employee(string firstName, string lastName, double salary, Position position, DateTime hireDate)
    {
        this.Id = Guid.NewGuid();
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Salary = salary;
        this.Position = position;
        this.HireDate = hireDate;
    }

    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public double Salary { get; set; }
    public Position Position { get; set; }
    public DateTime HireDate { get; set; }
}