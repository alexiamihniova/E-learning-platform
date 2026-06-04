using System.Collections.Generic;

namespace E_learning_platform.Patterns.Iterator
{
    public class CourseModuleCollection : IAggregate<CourseModule>
    {
        private List<CourseModule> _modules = new List<CourseModule>();

        public void AddModule(CourseModule module)
        {
            _modules.Add(module);
        }

        public CourseModule GetModule(int index)
        {
            return _modules[index];
        }

        public int Count => _modules.Count;

        public IIterator<CourseModule> CreateIterator()
        {
            return new CourseModuleIterator(this);
        }
    }
}
