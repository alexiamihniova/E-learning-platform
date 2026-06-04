using Xunit;
using E_learning_platform.Patterns.Strategy;
using E_learning_platform.Patterns.Observer;
using E_learning_platform.Patterns.Command;
using E_learning_platform.Patterns.Memento;
using E_learning_platform.Patterns.Iterator;

namespace E_learning_platform.Tests
{
    public class BehavioralPatternsTests
    {
        [Fact]
        public void StrategyPattern_StandardGrading_ReturnsCorrectGrade()
        {
            var assignment = new StudentAssignment("Math", 85, new StandardGradingStrategy());
            Assert.Equal("B", assignment.GetGrade());
        }

        [Fact]
        public void StrategyPattern_PassFailGrading_ReturnsCorrectGrade()
        {
            var assignment = new StudentAssignment("History", 40, new PassFailGradingStrategy());
            Assert.Equal("Fail", assignment.GetGrade());
        }

        [Fact]
        public void ObserverPattern_CourseNotifier_NotifiesStudents()
        {
            var course = new CourseNotifier("C# Basics");
            var student1 = new StudentObserver("Alice");
            var student2 = new StudentObserver("Bob");

            course.Attach(student1);
            course.Attach(student2);

            course.AddNewMaterial("Lesson 1: Intro");

            Assert.Contains("Alice", student1.LastNotification);
            Assert.Contains("Lesson 1: Intro", student1.LastNotification);
            Assert.Contains("Bob", student2.LastNotification);
        }

        [Fact]
        public void CommandPattern_EnrollCommand_ExecutesAndUndoes()
        {
            var receiver = new CourseManagerReceiver();
            var command = new EnrollCommand(receiver, "Alice", "C# Basics");
            var invoker = new CommandInvoker();

            invoker.ExecuteCommand(command);
            Assert.True(receiver.IsEnrolled("Alice"));

            invoker.UndoLastCommand();
            Assert.False(receiver.IsEnrolled("Alice"));
        }

        [Fact]
        public void MementoPattern_DraftHistory_SavesAndRestores()
        {
            var originator = new AssignmentDraftOriginator { Content = "Draft 1" };
            var caretaker = new DraftHistoryCaretaker(originator);

            caretaker.Backup();
            
            originator.Content = "Draft 2";
            
            caretaker.Undo();
            Assert.Equal("Draft 1", originator.Content);
        }

        [Fact]
        public void IteratorPattern_CourseModuleCollection_IteratesElements()
        {
            var collection = new CourseModuleCollection();
            collection.AddModule(new CourseModule(1, "Intro"));
            collection.AddModule(new CourseModule(2, "Classes"));

            var iterator = collection.CreateIterator();
            
            Assert.True(iterator.HasNext());
            var first = iterator.Next();
            Assert.Equal("Intro", first.Title);

            Assert.True(iterator.HasNext());
            var second = iterator.Next();
            Assert.Equal("Classes", second.Title);

            Assert.False(iterator.HasNext());
        }
    }
}
