using UnityEngine;

public class Task
{
    private enum TaskStatus
    {
        NotStarted,
        InProgress,
        Completed,
        Failed // if for example there is a time limit and the player runs out of time
    }

    private TaskStatus status = TaskStatus.NotStarted;
    private int totalProgressUnits; // refers to how many things you need to do to complete the task, for example this would be 5 if you
                                    // need to deliver 5 papers
    private int currentProgressUnits; // number of progress units already completed

    public Task(int totalProgressUnits) 
    {
        this.status = TaskStatus.NotStarted;
        this.totalProgressUnits = totalProgressUnits;
        this.currentProgressUnits = 0;
    }
    void BeginTask()
    {
        status = TaskStatus.InProgress;
    }

    void MakeProgress()
    {
        // ...
    }

    void FailTask()
    {
        // ...
    }

    double PercentComplete()
    {
        if (totalProgressUnits == 0)
        {
            return 0.0;
        }

        return (double)currentProgressUnits / totalProgressUnits * 100.0;
    }
}

public class Printing : Task
{
    public Printing(int totalProgressUnits) : base(totalProgressUnits)
    {

    }
}

public class Delivery : Task
{
    public Delivery(int totalProgressUnits) : base(totalProgressUnits)
    {

    }
}


public class ComputerUse : Task
{
    public ComputerUse(int totalProgressUnits) : base(totalProgressUnits)
    {

    }
}
