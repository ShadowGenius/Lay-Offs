using NUnit.Framework;
using UnityEngine;

public class Task : Action
{

    public static int PRINTING_PROGRESS_UNITS = 5; // placeholder value
    public static int DELIVERY_PROGRESS_UNITS = 5; 
    public static int USE_COMPUTER_PROGRESS_UNITS = 1;

    private int totalProgressUnits; // refers to how many things you need to do to complete the task, for example this would be 5 if you
                                    // need to deliver 5 papers
    private int currentProgressUnits; // number of progress units already completed

    public Task(Character character, int totalProgressUnits) : base(character, "Basic Task", ActionStatus.NotStarted)
    {
        this.totalProgressUnits = totalProgressUnits;
        this.currentProgressUnits = 0;
    }

    void BeginTask()
    {
        BeginAction();
    }

    void MakeProgress()
    {
        // ...
    }

    void FailTask()
    {
        // ...
        status = ActionStatus.Failed;
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
    public Printing(Character character) : base(character, PRINTING_PROGRESS_UNITS)
    {
        title = "Print Papers";
    }
}

public class Delivery : Task
{
    public Delivery(Character character) : base(character, DELIVERY_PROGRESS_UNITS)
    {
        title = "Deliver Papers";
    }
}


public class ComputerUse : Task
{
    public ComputerUse(Character character) : base(character, USE_COMPUTER_PROGRESS_UNITS)
    {
        title = "Use the Computer";
    }
}
