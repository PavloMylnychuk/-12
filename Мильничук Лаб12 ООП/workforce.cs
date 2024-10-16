using System;
using System.Collections.Generic;

namespace WorkForce
{
    public interface IEmployee
    {
        string Name { get; }
        int WeeklyHours { get; }
    }

    public class StandardEmployee : IEmployee
    {
        public string Name { get; }
        public int WeeklyHours => 40;

        public StandardEmployee(string name)
        {
            Name = name;
        }
    }

    public class PartTimeEmployee : IEmployee
    {
        public string Name { get; }
        public int WeeklyHours => 20;

        public PartTimeEmployee(string name)
        {
            Name = name;
        }
    }

    public class Job
    {
        public string Name { get; }
        public int HoursRemaining { get; private set; }
        public IEmployee Employee { get; }

        public Job(string name, int hours, IEmployee employee)
        {
            Name = name;
            HoursRemaining = hours;
            Employee = employee;
        }

        public void Update()
        {
            HoursRemaining -= Employee.WeeklyHours;
            if (HoursRemaining <= 0)
            {
                Console.WriteLine($"Job {Name} done!");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            List<Job> jobs = new List<Job>();
            List<IEmployee> employees = new List<IEmployee>();

            string command;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] parts = command.Split(' ');
                if (parts[0] == "Job")
                {
                    string jobName = parts[1];
                    int hours = int.Parse(parts[2]);
                    string employeeName = parts[3];

                    var employee = employees.Find(e => e.Name == employeeName);
                    if (employee != null)
                    {
                        jobs.Add(new Job(jobName, hours, employee));
                    }
                }
                else if (parts[0] == "StandardEmployee")
                {
                    string name = parts[1];
                    employees.Add(new StandardEmployee(name));
                }
                else if (parts[0] == "PartTimeEmployee")
                {
                    string name = parts[1];
                    employees.Add(new PartTimeEmployee(name));
                }
                else if (parts[0] == "Pass")
                {
                    foreach (var job in jobs.ToArray())
                    {
                        job.Update();
                        if (job.HoursRemaining <= 0)
                        {
                            jobs.Remove(job);
                        }
                    }
                }
                else if (parts[0] == "Status")
                {
                    foreach (var job in jobs)
                    {
                        Console.WriteLine($"Job: {job.Name} Hours Remaining: {job.HoursRemaining}");
                    }
                }
            }
        }
    }
}
